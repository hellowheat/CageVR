using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterMask : MonoBehaviour
{
    public GameObject[] hideObj;
    public GameObject[] showObj;
    [HideInInspector]
    public bool isTriggerEnter = false;
    GameObject eventCallGameObject = null;

    public void setEventCallObject(GameObject gameObject)
    {
        eventCallGameObject = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (eventCallGameObject)
        {
            isTriggerEnter = true;
            for (int i = 0; i < hideObj.Length; i++) hideObj[i].SetActive(false);
            for (int i = 0; i < showObj.Length; i++) hideObj[i].SetActive(true);
        }
    }
}
