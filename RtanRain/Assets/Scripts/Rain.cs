using UnityEngine;

public class Rain : MonoBehaviour
{
    float size;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-2.4f, 2.4f);
        float y = Random.Range(3.0f, 5.0f);
        transform.position = new Vector3(x, y);

        int type = Random.Range(1, 4);

        switch(type) {
            case 1 :
                size = 0.8f;
                score = 1;
                GetComponent<SpriteRenderer>().color = new Color(100 / 255f, 100 / 255f, 255 / 255f, 255 / 255f);
                break;
            case 2 :
                size = 1.0f;
                score = 2;
                GetComponent<SpriteRenderer>().color = new Color(130 / 255f, 130 / 255f, 255 / 255f, 255 / 255f);
                break;
            case 3 :
                size = 1.2f;
                score = 3;
                GetComponent<SpriteRenderer>().color = new Color(150 / 255f, 150 / 255f, 255 / 255f, 255 / 255f);
                break;
        }

        transform.localScale = new Vector3(size, size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("Ground")) {
            Destroy(gameObject);
        }
    }
}
