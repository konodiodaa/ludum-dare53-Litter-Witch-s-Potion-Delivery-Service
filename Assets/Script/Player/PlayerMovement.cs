using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float speed = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private int flip;

    private void Start()
    {
        flip = 1;
        sr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }


    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, y, 0).normalized;
        rb.velocity = dir * speed;

        if (dir.x < 0)
        {
            sr.flipX = true;
            flip = -1;
        }
        else if (dir.x > 0)
        {
            sr.flipX = false;
            flip = 1;
        }
    }
}
