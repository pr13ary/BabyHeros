using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField]
    GameObject m_enemyPrefab;
    [SerializeField]
    Transform m_plane;
    [SerializeField]
    Player m_player;

    GameObjectPool<Enemy> m_enemyPool;
    int m_enemyCount;
    Vector3 m_planeSize;


    public void RemoveEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        m_enemyPool.Set(enemy);
    }
    void CreateEnemy()
    {
        var enemy = m_enemyPool.Get();
        var agent = enemy.GetComponent<NavMeshAgent>();
        agent.enabled = true;
        enemy.transform.position = SpawnMonsters();
        enemy.gameObject.SetActive(true);
        
    }
    Vector3 SpawnMonsters()
    {
        Vector3 planePosition = m_plane.position;

        float randomX = Random.Range(-m_planeSize.x / 2, m_planeSize.x / 2);
        float randomZ = Random.Range(-m_planeSize.z / 2, m_planeSize.z / 2);

        Vector3 spawnPosition = new Vector3(planePosition.x + randomX, planePosition.y, planePosition.z + randomZ);
        return spawnPosition;
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_planeSize = m_plane.GetComponent<MeshRenderer>().bounds.size;

        // enemy object pool »ý¼º
        m_enemyPool = new GameObjectPool<Enemy>(5, () =>
        {
            var obj = Instantiate(m_enemyPrefab);
            obj.GetComponent<NavMeshAgent>().enabled = false;
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            var enemy = obj.GetComponent<Enemy>();
            enemy.InitEnemy(m_player);
            return enemy;
        });
        //Invoke("CreateEnemy", 1f);
        InvokeRepeating("CreateEnemy", 1f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
