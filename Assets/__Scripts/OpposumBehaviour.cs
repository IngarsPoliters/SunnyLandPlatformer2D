using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpposumBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            rb.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0f);
        }

    }

    private bool IsFacingRight()
    {
        return transform.localScale.x < 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //reverse the scale X value
        transform.localScale = new Vector2((Mathf.Sign(rb.velocity.x)),1.0f);
    }
}
