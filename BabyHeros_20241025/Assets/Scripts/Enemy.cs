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
    Player m_player;
    [SerializeField]
    AiState m_curState;
    [SerializeField]
    float m_attackRange = 1.5f;
    [SerializeField]
    float m_sightRange = 5f;

    NavMeshAgent m_navAgent;

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
