using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoalChecker : MonoBehaviour
{
    public float moveSpeed = 0.03f;
    private float stopPos = 6.5f;
    private bool isGoal;
    private GameDirector gameDirector;

    [SerializeField]
    private GameObject secretfloorObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > stopPos)
        {
            transform.position += new Vector3(-moveSpeed, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
       if (col.gameObject.tag == "Player" && isGoal == false)
        {
            isGoal = true;

            PlayerController playerController = col.gameObject.GetComponent<PlayerController>();
            playerController.uiManager.GenerateResultPopUp(playerController.coinPoint);
            
            gameDirector.GoalClear();
            secretfloorObj.SetActive(true);
            secretfloorObj.transform.DOLocalMoveY(0.45f, 2.5f).SetEase(Ease.Linear).SetRelative();
        } 
    }

    public void SetUpGoalHouse(GameDirector gameDirector)
    {
        this.gameDirector = gameDirector;
        secretfloorObj.SetActive(false);
    }
}
