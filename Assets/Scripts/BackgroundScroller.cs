using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Header("�w�i�摜�̃X�N���[�����x = �����X�N���[���̑��x")]
    public float scrollSpeed = 0.01f;

    [Header("�摜�̃X�N���[���I���n�_")]
    public float stopPosition = -18f;

    [Header("�摜�̍ăX�^�[�g�n�_")]
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
