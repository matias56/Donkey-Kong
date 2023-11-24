using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private new Rigidbody2D rb;
    private float dirX;
    public float speed = 1f;
    public float timer = 4;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dirX = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x == 8f || transform.position.x == -8f)
        {
            dirX *= -1f;
        }

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * speed, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        dirX *= -1f;
    }
}
