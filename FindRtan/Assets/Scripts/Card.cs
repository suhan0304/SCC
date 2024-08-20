using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public SpriteRenderer frontImage;
    public Animator Anim;

    public GameObject front;
    public GameObject back;

    public AudioClip clip;
    public AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Setting(int number) {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }

    public void OpenCard() {
        audioSource.PlayOneShot(clip);

        if (GameManager.Instance.secondCard == null) {
            Anim.SetBool("isOpen", true);
            front.SetActive(true);
            back.SetActive(false);

            if(GameManager.Instance.firstCard == null) {
                GameManager.Instance.firstCard = this;
            }
            else {
                GameManager.Instance.secondCard = this;
                GameManager.Instance.isMatched();
            }
        }
    }

    public void DestroyCard() {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    void DestroyCardInvoke() {
        Destroy(gameObject);
    }

    public void CloseCard() {
        Invoke("CloseCardInvoke", 0.5f);
    }

    void CloseCardInvoke() {
        Anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
