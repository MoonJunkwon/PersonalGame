using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestory : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if(ps && !ps.IsAlive())
        {
            DestorySelf();
        }
    }
    public void DestorySelf()
    {
        Destroy(gameObject);
    }
}
