using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string mdString;

    private void OnCollisionEnter(Collision collision)
    {
        Lock @lock = collision.transform.GetComponent<Lock>();
        if(@lock != null)
        {
            if(@lock.mdString == mdString)
            {
                //匹配上了
            }
            else
            {
                //钥匙不匹配
            }
        }
    }
}
