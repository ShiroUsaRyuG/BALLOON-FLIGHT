using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private string horizontal = "Horizontal";
    private string jump = "Jump";
    private Rigidbody2D rb;
    private Animator anim;
    private float limitPosX = 8.9f;
    private float limitPosY = 4.4f;
    private float scale;
    private float maxHeight = 5.0f;
    public float moveSpeed;
    public float jumpPower;
    public bool isGrounded;
    [SerializeField, Header("Linecast用 地面判定レイヤー")]
    private LayerMask groundLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scale = transform.localScale.x;
    }

    void Update()
    {
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 0.4f,
            transform.position - transform.up * 0.9f, groundLayer);
        Debug.DrawLine(transform.position + transform.up * 0.4f,
            transform.position - transform.up * 0.9f, Color.red, 1.0f);
        if (Input.GetButtonDown(jump))
        {
            Jump();
        }
        if (isGrounded == false && rb.velocity.y < 0.15f)
        {
            anim.SetTrigger("Fall");
        }
        if (rb.velocity.y > maxHeight)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxHeight);
        }
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpPower);
        anim.SetTrigger("Jump");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    ///<summary>
    ///移動
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
        float posX = Mathf.Clamp(transform.position.x, -limitPosX, limitPosX);
        float posY = Mathf.Clamp(transform.position.y, -limitPosY, limitPosY);
        transform.position = new Vector2(posX, posY);
    }
}
