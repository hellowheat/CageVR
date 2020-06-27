using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoradController : MonoBehaviour
{
    public float speed;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Quaternion.AngleAxis(-90,Vector3.up)*transform.forward* Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += Quaternion.AngleAxis(180, Vector3.up) * transform.forward * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Quaternion.AngleAxis(90, Vector3.up) * transform.forward * Time.deltaTime * speed;
        }
    }
}
