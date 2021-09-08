using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private GoalChecker goalHousePrefab;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private FloorGenerator[] floorGenerators;
    [SerializeField]
    private RandomObjectGenerator[] randomObjectGenerators;
    [SerializeField]
    private AudioManager audioManager;

    private bool isSetUp;
    private bool isGameUp;
    private int generateCount;

    public int GenerateCount
    {
        set
        {
            generateCount = value;
            Debug.Log("生成数 / クリア目標数 ：" + generateCount + " / " + clearCount);

            if(generateCount >= clearCount)
            {
                GenerateGoal();
                GameUp();
            }
        }
        get
        {
            return generateCount;
        }
    }

    public int clearCount; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(audioManager.PlayBGM(0));
        isGameUp = false;
        isSetUp = false;

        SetUpFloorGenerators();
        SwitchGenerators(isSetUp);
    }

    private void SetUpFloorGenerators()
    {
        for(int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SetUpGenerator(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.isFirstGenerateBalloon && isSetUp == false)
        {
            isSetUp = true;
            SwitchGenerators(isSetUp);
            StartCoroutine(audioManager.PlayBGM(1));
        }
    }

    private void GenerateGoal()
    {
        GoalChecker goalHouse = Instantiate(goalHousePrefab);
        goalHouse.SetUpGoalHouse(this);
    }

    public void GameUp()
    {
        isGameUp = true;
        SwitchGenerators(!isSetUp);
    }

    private void SwitchGenerators(bool isSwitch)
    {
        for (int i = 0; i<randomObjectGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(isSwitch);
        }

        for (int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(isSwitch);
        }
    }

    public void GoalClear()
    {
        StartCoroutine(audioManager.PlayBGM(2));
    }

    public void GameOver()
    {
        StartCoroutine(audioManager.PlayBGM(3));
    }
}
