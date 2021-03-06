using System.Collections;
using System.Collections.Generic;
using UnityEngine;    

public class FloorGenerator : MonoBehaviour    
{
    [SerializeField]
    private float heightRange = 4.0f;
    [SerializeField]
    private GameObject aerialFloorPrefab;
    [SerializeField]
    private Transform genetateTran;

    private GameDirector gameDirector;

    [Header("生成までの待機時間")]
    public float waitTime;
    private float timer;

    private bool isActivate;

    // Start is called before the first frame update
    void Start()
    {
                                                                                                                                                                                                                                                                                                                                                                                                   
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivate == false)
        {
            return;
        }
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            timer = 0;
            GenerateFloor();
        }
    }

    private void GenerateFloor()
    {
        GameObject obj = Instantiate(aerialFloorPrefab, genetateTran);
        float randomPosY = Random.Range(-heightRange, heightRange);
        obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + randomPosY);
        gameDirector.GenerateCount++;
    }

    public void SetUpGenerator(GameDirector gameDirector)
    {
        this.gameDirector = gameDirector;

        // TODO 他にも初期設定したい情報がある場合にここに処理を追加

    }

    public void SwitchActivation(bool isSwitch)
    {
        isActivate = isSwitch;
    }
}
