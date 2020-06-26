using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoradController : MonoBehaviour
{
    public float speed;
    public bool useFB;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.W) )
        {
            if(useFB)
            transform.localPosition += Vector3.forward * Time.deltaTime * speed;
        }else if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += Vector3.left * Time.deltaTime * speed;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (useFB)
                transform.localPosition += Vector3.back * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += Vector3.right * Time.deltaTime * speed;
        }
    }
}
