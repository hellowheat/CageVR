using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string mdString;
    
    private void OnTriggerEnter(Collider collision)
    {
        Lock @lock = collision.transform.GetComponent<Lock>();
        if(@lock != null)
        {
            Debug.Log("key trigger enter");
            if (@lock.mdString == mdString)
            {
                //匹配上了
                @lock.open();
            }
            else
            {
                //钥匙不匹配
            }
        }
    }
}
