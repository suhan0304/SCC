using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform cards;
    public GameObject card;

    public Text timeText;
    float time = 0.00f;

    public Card firstCard;
    public Card secondCard;

    public int cardCount = 0;

    public GameObject endText;

    void Awake() {
        if(Instance == null)  {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        Time.timeScale = 1.0f;

        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7};
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < 16; i++) {
            GameObject go = Instantiate(card, this.transform);
            go.transform.parent = cards;

            float x = (i%4) * 1.4f - 2.1f;
            float y = (i/4) * 1.4f - 3.0f;

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);

            cardCount = arr.Length;
        }
    }

    void Update() {
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");
        if (time >= 30.0f) { 
            EndGame();
        }
    }

    public void isMatched() {
        if(firstCard.idx == secondCard.idx) {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;

            if (cardCount == 0) {
                EndGame();
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

    void EndGame() {
        endText.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
