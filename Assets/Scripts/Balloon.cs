using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Balloon : MonoBehaviour
{
    private PlayerController playerController;
    private Tweener tweener;

    public void SetUpBalloon(PlayerController playerController)
    {
        this.playerController = playerController;
        Vector3 scale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(scale, 2.0f).SetEase(Ease.InBounce);
        tweener = transform.DOLocalMoveX(0.02f, 0.2f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            tweener.Kill();

            playerController.DestroyBalloon();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
