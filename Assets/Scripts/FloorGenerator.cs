using System.Collections;
using System.Collections.Generic;
using UnityEngine;    

public class FloorGenerator : MonoBehaviour    
{
    public float heightRange = 4.0f;
    [SerializeField]
    private GameObject aerialFloorPrefab;
    [SerializeField]
    private Transform genetateTran;

    [Header("¶¬‚Ü‚Å‚Ì‘Ò‹@ŽžŠÔ")]
    public float waitTime;
    private float timer;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
    // Start is called before the first frame update
    void Start()
    {
                                                                                                                                                                                                                                                                                                                                                                                                   
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
