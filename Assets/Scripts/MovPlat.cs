using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlat : MonoBehaviour
{
    private new Rigidbody2D rb;

    public float speed = 2;

    public Vector2 dir;

    public bool up;

    public Transform point;

    private Vector3 locA;

    private Vector3 locB;

    private Vector3 nextLoc;

    [SerializeField] private Transform pos;

    [SerializeField] private Transform movLoc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        locA = pos.localScale;

        locB = movLoc.localPosition;

        nextLoc = locB;
    }

    // Update is called once per frame
    void Update()
    {
        if(up == true)
        {
            pos.localPosition = Vector3.MoveTowards(pos.localPosition, nextLoc, speed * Time.deltaTime);
        }
        else
        {
            pos.localPosition = Vector3.MoveTowards(pos.localPosition, nextLoc, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Barrel"))
        {
            transform.position = point.position;
        }
    }
}
