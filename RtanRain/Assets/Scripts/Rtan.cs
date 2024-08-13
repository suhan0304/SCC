using UnityEngine;

public class Rtan : MonoBehaviour
{
    float direction = 0.05f;
    SpriteRenderer Renderer;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            direction *= -1;
            Renderer.flipX = !Renderer.flipX;
        }

        if (transform.position.x > 2.6f) 
        {
            direction = -0.05f;
            Renderer.flipX = true;
        }
        if (transform.position.x < -2.6f) 
        {
            direction = 0.05f;
            Renderer.flipX = false;
        }
        transform.position += Vector3.right * direction;
    }
}
