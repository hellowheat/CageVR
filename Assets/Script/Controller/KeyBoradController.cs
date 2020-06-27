using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoradController : MonoBehaviour
{
    public float speed;
    public float angleSpeed;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Quaternion.AngleAxis(-90,Vector3.up)*transform.forward* Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Quaternion.AngleAxis(180, Vector3.up) * transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Quaternion.AngleAxis(90, Vector3.up) * transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -angleSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, angleSpeed * Time.deltaTime);
        }

    }
}
