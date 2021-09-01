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

    private bool isSetUp;
    private bool isGameUp;
    private int generateCount;

    public int GenerateCount
    {
        set
        {
            generateCount = value;
            Debug.Log("������ / �N���A�ڕW�� �F" + generateCount + " / " + clearCount);

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
        isGameUp = false;
        isSetUp = false;

        SetUpFloorGenerators();

        Debug.Log("������~");
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
            Debug.Log("�����X�^�[�g");
        }
    }

    private void GenerateGoal()
    {
        GoalChecker goalHouse = Instantiate(goalHousePrefab);
        Debug.Log("�S�[���n�_ ����");
    }

    public void GameUp()
    {
        isGameUp = true;
        Debug.Log("������~");
    }
}
