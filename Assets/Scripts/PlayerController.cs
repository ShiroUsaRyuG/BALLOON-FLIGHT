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
    public bool isFirstGenerateBalloon;
    private float scale;
    private float maxHeight = 5.0f;
    public GameObject[] balloons;

    public int maxBalloonCount;
    public Transform[] balloonTrans;
    public GameObject balloonPrefab;
    public float generateTime;
    public bool isGenerating;

    public float moveSpeed;
    public float jumpPower;
    public bool isGrounded;
    [SerializeField, Header("Linecast用 地面判定レイヤー")]
    private LayerMask groundLayer;
    [SerializeField]
    private StartChecker startChecker;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        balloons = new GameObject[maxBalloonCount];
        scale = transform.localScale.x;
    }

    void Update()
    {
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 0.4f,
            transform.position - transform.up * 0.9f, groundLayer);
        Debug.DrawLine(transform.position + transform.up * 0.4f,
            transform.position - transform.up * 0.9f, Color.red, 1.0f);
        if (balloons[0] != null)
        {
            if (Input.GetButtonDown(jump))
            {
                Jump();
            }
            if (isGrounded == false && rb.velocity.y < 0.15f)
            {
                anim.SetTrigger("Fall");
            }
        } else
        {
            Debug.Log("バルーンがない。ジャンプ不可");
        }
        if (rb.velocity.y > maxHeight)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxHeight);
        }
        if (isGrounded == true && isGenerating == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(GenerateBalloon());
            }
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

        if (x != 0)
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

    private IEnumerator GenerateBalloon() 
    {
        if (balloons[1] != null)
        {
            yield break;
        }

        isGenerating = true;

        if (isFirstGenerateBalloon == false)
        {
            isFirstGenerateBalloon = true;
            Debug.Log("初回のバルーン生成");
            startChecker.SetInitialSpeed();
        }

        if (balloons[0] == null)
        {
            balloons[0] = Instantiate(balloonPrefab, balloonTrans[0]);
        }
        else
        {
            balloons[1] = Instantiate(balloonPrefab, balloonTrans[1]);
        }

        yield return new WaitForSeconds(generateTime);

        isGenerating = false;
    }
}
