using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChecker : MonoBehaviour
{
    private MoveObject moveObject;
    [SerializeField]
    private float StartSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        moveObject = GetComponent<MoveObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInitialSpeed()
    {
        moveObject.moveSpeed = StartSpeed;
    }
}
