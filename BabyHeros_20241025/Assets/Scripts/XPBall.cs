using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBall : MonoBehaviour
{
    IEnumerator CoMoveToTarget()
    {
        Transform target = EnemyManager.Instance.m_player.transform;
        Vector3 dir = Vector3.zero;
        while (true)
        {
            dir = target.position - transform.position;
            transform.position += dir.normalized * 13f * Time.deltaTime;
            yield return null;
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager.Instance.AddXP(1f);
            EnemyManager.Instance.RemoveXPBall(this);
        }
        else if (collision.CompareTag("Magnet"))
        {
            if(gameObject.activeSelf)
            {
                StartCoroutine(CoMoveToTarget());
            }
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
