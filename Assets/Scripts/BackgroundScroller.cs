using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Header("背景画像のスクロール速度 = 強制スクロールの速度")]
    public float scrollSpeed = 0.01f;

    [Header("画像のスクロール終了地点")]
    public float stopPosition = -18f;

    [Header("画像の再スタート地点")]
    public float restartPosition = 7.6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-scrollSpeed, 0, 0);

        if (transform.position.x < stopPosition)
        {
            transform.position = new Vector2(restartPosition, 0);
        }
    }
}
