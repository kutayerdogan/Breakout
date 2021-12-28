using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public float speed;
    public float rightScreenEdge;
    public float leftScreenEdge;
    public GameManager gm;
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gameOver)
        {
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
        if(transform.position.x < leftScreenEdge)
        {
            transform.position = new Vector2(leftScreenEdge, transform.position.y);
        }
        if (transform.position.x > rightScreenEdge)
        {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("extraLife"))
        {
            gm.UpdateLives(1);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("halfPaddle"))
        {
            Vector2 scale_original = transform.localScale;
            transform.localScale = new Vector2(1.5f, 0.4f);
            Destroy(collision.gameObject);
            //StartCoroutine(WaitingToNormal());
            //transform.localScale = scale_original;
        }
        if (collision.CompareTag("doublePaddle"))
        {
            Vector2 scale_original = transform.localScale;
            transform.localScale = new Vector2(6f, 0.4f);
            Destroy(collision.gameObject);
            //StartCoroutine(WaitingToNormal());
            //transform.localScale = scale_original;
        }
        audio.Play();

    }
    /*IEnumerator WaitingToNormal()
    {
        print(Time.time);
        yield return new WaitForSecondsRealtime(10);
        print(Time.time);
    }*/
}
