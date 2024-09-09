using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
   private static volatile CardManager instance;

    public Card firstCard;
    public Card secondCard;
    public Transform cards;
    public AudioClip audioClip;
    public AudioSource audioSource;
    public GameObject card;

   private CardManager() {}

   public static CardManager Instance
   {
      get 
      {
        if (instance == null) 
            instance = new CardManager();
        return instance;
      }
   }

   void SpwanCard() {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();

        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7};
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < 16; i++) {
            GameObject go = Instantiate(card, this.transform);
            go.transform.SetParent(cards);

            float x = (i%4) * 1.4f - 2.1f;
            float y = (i/4) * 1.4f - 3.0f;

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);

            GameManager.Instance.cardCount = arr.Length;
        }
   }

    public void isMatched() {
        if(firstCard.idx == secondCard.idx) {
            audioSource.PlayOneShot(audioClip);

            firstCard.DestroyCard();
            secondCard.DestroyCard();
            GameManager.Instance.cardCount -= 2;

            if (GameManager.Instance.cardCount == 0) {
                GameManager.Instance.EndGame();
            }
        }
        else {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        CardReset();
    }

    void CardReset() {
        Invoke("CardResetInvoke", 0.5f);

    }
    void CardResetInvoke() {
        firstCard = null;
        secondCard = null;
    }

}
