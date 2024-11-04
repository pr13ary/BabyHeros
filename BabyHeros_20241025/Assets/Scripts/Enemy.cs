using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum AiState
    {
        Idle,
        Attack,
        Chase,
        Max
    }

    [SerializeField]
    AiState m_curState;
    [SerializeField]
    float m_attackRange = 1.5f;
    [SerializeField]
    float m_sightRange = 5f;

    Player m_player;
    NavMeshAgent m_navAgent;
    int m_hp;

    public void AiBehaviorProcess()
    {
        switch (m_curState)
        {
            case AiState.Idle:
                // 인식 영역 check
                if(CheckRange(m_player.transform.position, m_sightRange))
                {
                    // 공격 영역 check
                    if(CheckRange(m_player.transform.position, m_attackRange))
                    {
                        SetState(AiState.Attack);
                    }
                    SetState(AiState.Chase);
                    // Animation : Run/Walk
                    m_navAgent.stoppingDistance = m_attackRange;
                    SetDestination();
                    return;
                }
                break;
            case AiState.Attack:
                break;
            case AiState.Chase:
                if(CheckRange(m_player.transform.position, m_attackRange))
                {
                    SetState(AiState.Idle);
                    // Animation : Idle
                }
                break;
        }
    }

    public void SetDamage(int damage)
    {
        // hit animation
        if (damage < 0) 
            m_hp = 0;
        else 
            m_hp -= damage;
        if (m_hp <= 0)
        {
            EnemyManager.Instance.RemoveEnemy(this);
            m_navAgent.enabled = false;
        }
    }
    public void InitEnemy(Player player)
    {
        // note : test hp
        m_hp = 3;
        m_player = player;
    }
    IEnumerator CoChaseToTarget(int frame)
    {
        while(m_curState == AiState.Chase)
        {
            m_navAgent.SetDestination(m_player.transform.position);
            for (int i = 0; i < frame; i++)
                yield return null; // 1 frame 대기
        }
    }
    bool CheckRange(Vector3 target, float range)
    {
        var dir = target - transform.position;
        if (dir.sqrMagnitude <= Mathf.Pow(range, 2f)) 
            return true;
        return false;
    }
    void SetDestination()
    {
        m_navAgent.Warp(transform.position);
        StartCoroutine(CoChaseToTarget(20));
    }
    void SetState(AiState state)
    {
        m_curState = state;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        AiBehaviorProcess();
    }
}
