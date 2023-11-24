using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    private new Rigidbody2D rb;

    public Vector2 td;
    public float tdTimer = 3;

    public float speed = 3;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        td = Vector2.right; 
    }

    // Update is called once per frame
    void Update()
    {
        tdTimer -= Time.deltaTime;

        if (td == Vector2.right && tdTimer <= 0)
        {
            td = Vector2.left;

            tdTimer = 5;
        }
        else if (td == Vector2.left && tdTimer <= 0)
        {
            td = Vector2.right;

            tdTimer = 5;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Treadmill"))
        {

            transform.Translate(td * speed * Time.deltaTime);

        }
    }
}
