using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{

    private new Rigidbody2D rb;
    private new Collider2D collider;
    private Collider2D[] results;
    private Vector2 dir;
    public float speed = 1f;
    public float jumpS = 4f;
    private bool grounded;
    private bool climbing;
    public Sprite[] doHammer;
    public SpriteRenderer sr;
    public Sprite[] runSprites;
    public Sprite climb;
    private int spriteIndex;
    public bool isHammer;
    public float timer;
    public Vector2 td;
    public float tdTimer = 3;
    public float destroyed;
    // Start is called before the first frame update
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        results = new Collider2D[4];
        isHammer = false;
        td = Vector2.right;
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(AnimateSprite), 1f / 12f, 1f / 12f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void CheckCollision()
    {
        grounded = false;
        climbing = false;
        Vector2 size = collider.bounds.size;
        size.y += 0.1f;
        size.x /= 2f;
        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results);

        for(int i = 0; i < amount; i++)
        {
            GameObject hit = results[i].gameObject;

            if(hit.layer == LayerMask.NameToLayer("Ground"))
            {
                grounded = hit.transform.position.y < (transform.position.y - 0.5f + 0.1f);

                Physics2D.IgnoreCollision(collider, results[i], !grounded);
            }
            else if (hit.layer == LayerMask.NameToLayer("Ladder") && !isHammer)
            {
                climbing = true;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        CheckCollision();

        if(climbing)
        {
            dir.y = Input.GetAxis("Vertical") * speed;
        } 
        else if(grounded && Input.GetButtonDown("Jump"))
        {
            dir = Vector2.up * jumpS;
        }
        else
        {
            dir += Physics2D.gravity * Time.deltaTime;
        }

        dir.x = Input.GetAxis("Horizontal") * speed;

        if (grounded)
        {
            dir.y = Mathf.Max(dir.y, -1f);
        }
        

        if(dir.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (dir.x <0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if(isHammer)
        {
            

            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                isHammer = false;

                timer = 6;
            }
        }

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

        if(destroyed == 8)
        {
            enabled = false;
            FindObjectOfType<GameManager>().LevelComplete();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + dir * Time.fixedDeltaTime);
    }

    private void AnimateSprite()
    {
        if(climbing)
        {
            sr.sprite = climb;
        }
        else if(dir.x != 0 && !isHammer)
        {
            spriteIndex++;

            if (spriteIndex >= runSprites.Length)
            {
                spriteIndex = 0;
            }

            sr.sprite = runSprites[spriteIndex];
            
        }
        if (isHammer)
        {
            spriteIndex++;

            if (spriteIndex >= doHammer.Length)
            {
                spriteIndex = 0;
            }

            sr.sprite = doHammer[spriteIndex];
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Pauline"))
        {
            enabled = false;
            FindObjectOfType<GameManager>().LevelComplete();
        }
        else if(other.gameObject.CompareTag("Barrel") && !isHammer)
        {
            
            enabled = false;
            FindObjectOfType<GameManager>().LevelFailed();
            Debug.Log(FindObjectOfType<GameManager>().lives);
        }
        else if (other.gameObject.CompareTag("Barrel") && isHammer)
        {
            FindObjectOfType<GameManager>().score += 100;
            Destroy(other.gameObject);
            
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            FindObjectOfType<GameManager>().score += 200;
            Destroy(other.gameObject);

        }
        else if (other.gameObject.CompareTag("Support"))
        {
            FindObjectOfType<GameManager>().score += 50;
            destroyed++;
            Destroy(other.gameObject);

        }

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Treadmill"))
        {

            transform.Translate(td * speed * Time.deltaTime);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Hammer"))
        {
            Destroy(other.gameObject);
            isHammer = true;
        }
    }
}
