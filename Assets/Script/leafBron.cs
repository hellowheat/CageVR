using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class leafBron : MonoBehaviour
{
    public int maxLeafNum;
    public GameObject leafParticle;
    public float velocityThreshold = 0f;
    
    ObjectPool leafPool;
    // Start is called before the first frame update
    void Start()
    {
        //leafPs = new List<ParticleSystem>();
        leafPool = new ObjectPool(leafParticle, transform, maxLeafNum);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
        if (rb != null && rb.velocity.magnitude >= velocityThreshold)
        {
            GameObject leafGB = leafPool.create(collision.GetContact(0).point, -rb.velocity);
            if (leafGB)
            {
                ParticleSystem leafP = leafGB.GetComponent<ParticleSystem>();
                DOTween.Sequence().AppendCallback(
                    () =>
                    {
                        leafPool.destory(leafP.gameObject);
                    }).SetDelay(leafP.main.startLifetime.constant);
            }
          

        }
    }


}
