using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-moveSpeed, 0, 0);

        if (transform.position.x <= -12.5f)
        {
            Destroy(gameObject);
        }
    }
}
