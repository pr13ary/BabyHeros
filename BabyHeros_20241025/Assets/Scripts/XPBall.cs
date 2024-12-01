using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBall : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager.Instance.AddXP(1f);
            EnemyManager.Instance.RemoveXPBall(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
