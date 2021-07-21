using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private string horizontal = "Horizontal";
    private Rigidbody2D rb;
    private Animator anim;
    private float scale;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    ///<summary>
    ///ˆÚ“®
    ///</summary>
    private void Move()
    {
        float x = Input.GetAxis(horizontal);

        if(x != 0)
        {
            rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
            Vector3 temp = transform.localScale;
            temp.x = x;
            if (temp.x > 0)
            {
                temp.x = scale;
            }
            else
            {
                temp.x = -scale;
            }
            transform.localScale = temp;
            anim.SetBool("Idle", false);
            anim.SetFloat("Run", 0.5f);
        } else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetFloat("Run", 0.0f);
            anim.SetBool("Idle", true);
        }
    }
}
