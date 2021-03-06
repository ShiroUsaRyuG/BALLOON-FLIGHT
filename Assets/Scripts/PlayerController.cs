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
    private bool isGameOver = false;
    public bool isFirstGenerateBalloon;
    private float scale;
    private float maxHeight = 5.0f;
    public List<Balloon> balloonList = new List<Balloon>();

    public int maxBalloonCount;
    public Transform[] balloonTrans;

    public Balloon balloonPrefab;
    public float generateTime;
    public bool isGenerating;
    public float knockbackPower;
    public int coinPoint;
    public UIManager uiManager;

    public float moveSpeed;
    public float jumpPower;
    public bool isGrounded;

    [SerializeField, Header("Linecast用 地面判定レイヤー")]
    private LayerMask groundLayer;
    [SerializeField]
    private StartChecker startChecker;
    [SerializeField]
    private AudioClip knockbackSE;
    [SerializeField]
    private AudioClip coinSE;
    [SerializeField]
    private GameObject knockbackEffectPrefab;


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
        if (balloonList.Count > 0)
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
        if (isGrounded == true && isGenerating == false && balloonList.Count < maxBalloonCount)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(GenerateBalloon(1, generateTime));
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
        if(isGameOver == true)
        {
            return;
        }

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

    private IEnumerator GenerateBalloon(int balloonCount, float waitTime) 
    {
        if (balloonList.Count >= maxBalloonCount)
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

      /*if (balloons[0] == null)
        {
            balloons[0] = Instantiate(balloonPrefab, balloonTrans[0]);
            balloons[0].GetComponent<Balloon>().SetUpBalloon(this);
        }
        else
        {
            balloons[1] = Instantiate(balloonPrefab, balloonTrans[1]);
            balloons[1].GetComponent<Balloon>().SetUpBalloon(this);
        }

        yield return new WaitForSeconds(generateTime);*/

        for (int i = 0;i < balloonCount; i++)
        {
            Balloon balloon;
            if (balloonTrans[0].childCount == 0)
            {
                balloon = Instantiate(balloonPrefab, balloonTrans[0]);
            }
            else
            {
                balloon = Instantiate(balloonPrefab, balloonTrans[1]);
            }
            balloon.SetUpBalloon(this);
            balloonList.Add(balloon);
            yield return new WaitForSeconds(generateTime);
        }

        isGenerating = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Vector3 direction = (transform.position - col.transform.position).normalized;
            transform.position += direction * knockbackPower;
            AudioSource.PlayClipAtPoint(knockbackSE, transform.position);
            GameObject knockbackEffect = 
                Instantiate(knockbackEffectPrefab, col.transform.position, Quaternion.identity);
            Destroy(knockbackEffect, 0.5f);
        }
    }

    public void DestroyBalloon(Balloon balloon)
    {
        // TODO 後ほど、バルーン破壊の際に「割れた」ように見えるアニメ演出を追加

        /*if (balloons[1] != null)
          {
              Destroy(balloons[1]);
          }else if (balloons[0] != null)
          {
              Destroy(balloons[0]);
          }*/

        balloonList.Remove(balloon);
        Destroy(balloon.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Coin")
        {
            coinPoint += col.gameObject.GetComponent<Coin>().Point;
            uiManager.UpdateDisplayScore(coinPoint);
            AudioSource.PlayClipAtPoint(coinSE, transform.position);
            Destroy(col.gameObject);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        uiManager.DisplayGameOverInfo();
    }
}
