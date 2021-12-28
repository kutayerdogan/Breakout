using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform explosion;
    public GameManager gm;
    public Transform extraLifePowerup;
    public Transform halfPaddlePowerup;
    public Transform doublePaddlePowerup;
    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver)
        {
            return;
        }
        if(inPlay == false)
        {
            transform.position = paddle.position;
        }
        if(Input.GetButtonDown("Jump") && inPlay == false)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("bottom"))
        {
            Debug.Log("Ball hit the bottom of the screen!");
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("brick"))
        {
            int randomChance = Random.Range(1, 101);
            if(randomChance < 10)
            {
                Instantiate(extraLifePowerup, collision.transform.position, collision.transform.rotation);
            }
            if(randomChance > 90)
            {
                Instantiate(halfPaddlePowerup, collision.transform.position, collision.transform.rotation);
            }
            if(randomChance > 50 && randomChance < 55)
            {
                Instantiate(doublePaddlePowerup, collision.transform.position, collision.transform.rotation);
            }
            Transform newExplosion = Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(newExplosion.gameObject, 2.5f);
            gm.UpdateScore(collision.gameObject.GetComponent<BrickScript>().points);
            gm.UpdateNumberOfBricks();
            Destroy(collision.gameObject);
            audioSource.Play();
        }
    }
}
