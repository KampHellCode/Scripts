using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    public float velX = 5f;
    float velY = 0f;
    Rigidbody2D rb;
    public float speed = 5;
    public LayerMask doNotHit;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        
    }
	
	// Update is called once per frame
	void Update () {

        rb.velocity = new Vector2(velX, velY) * speed;
        Destroy(gameObject, 3f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("walls"))
        {
            Destroy(gameObject);
        }
    }
}
