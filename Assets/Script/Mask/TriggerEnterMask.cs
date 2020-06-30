using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterMask : MonoBehaviour
{
   /* public GameObject[] hideObj;
    public GameObject[] showObj;*/
    [HideInInspector]
    public bool isTriggerEnter = false;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.CompareTo("Player") == 0)
        {
            isTriggerEnter = true;
          /*  for (int i = 0; i < hideObj.Length; i++) hideObj[i].SetActive(false);
            for (int i = 0; i < showObj.Length; i++) showObj[i].SetActive(true);*/
        }
    }
}
