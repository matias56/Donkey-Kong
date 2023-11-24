using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

    private new Rigidbody2D rb;

    private new Collider2D collider;
    private Collider2D[] results;

    public float speed = 2;

    public float jumpS = 2f;

    public bool grounded;

    
    private Vector2 dir;

    private float dirX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        results = new Collider2D[4];
        grounded = false;
        
    }

    private void CheckCollision()
    {
        grounded = false;
        Vector2 size = collider.bounds.size;
        size.y += 0.1f;
        size.x /= 2f;
        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results);

        for (int i = 0; i < amount; i++)
        {
            GameObject hit = results[i].gameObject;

            if (hit.layer == LayerMask.NameToLayer("Ground"))
            {
                grounded = hit.transform.position.y < (transform.position.y - 0.5f + 0.1f);

                Physics2D.IgnoreCollision(collider, results[i], !grounded);
            }
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();

        dir.x = speed * Time.deltaTime;

        if (grounded)
        {
            dir.y = Mathf.Max(dir.y, -1f);
        }


        rb.AddForce(transform.right * speed * Time.deltaTime, ForceMode2D.Impulse);

    }

    private void FixedUpdate()
    {
        
    }

   

    private void OnCollisionEnter2D(Collision2D other)
    {
        

        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
