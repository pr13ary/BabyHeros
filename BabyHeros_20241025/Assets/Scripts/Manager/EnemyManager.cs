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
    GameObject m_XPBallPrefab;
    [SerializeField]
    public Player m_player;

    GameObjectPool<Enemy> m_enemyPool;
    GameObjectPool<XPBall> m_XPBallPool;
    int m_enemyCount;
    Vector3 m_planeSize;

    public void RemoveEnemy(Enemy enemy)
    {
        CreateXPBall(enemy);
        enemy.gameObject.SetActive(false);
        m_enemyPool.Set(enemy);
    }
    public void StopSpawning()
    {
        CancelInvoke("CreateEnemy");
    }
    public void ResumeSpawning()
    {
        InvokeRepeating("CreateEnemy", 1f, 2f);
    }
    public void RemoveXPBall(XPBall XPBall)
    {
        XPBall.gameObject.SetActive(false);
        m_XPBallPool.Set(XPBall);
    }
    void CreateXPBall(Enemy enemy)
    {
        var XPBall = m_XPBallPool.Get();
        XPBall.transform.position = enemy.transform.position;
        XPBall.gameObject.SetActive(true);
    }
    void CreateEnemy()
    {
        var enemy = m_enemyPool.Get();
        enemy.GetComponent<NavMeshAgent>().enabled = true;
        enemy.transform.position = SpawnMonster();
        enemy.InitEnemy();
        enemy.gameObject.SetActive(true);
    }
    Vector3 SpawnMonster()
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

        // XPBall object pool 持失
        m_XPBallPool = new GameObjectPool<XPBall>(5, () =>
        {
            var obj = Instantiate(m_XPBallPrefab);
            obj.SetActive(false);
            var XPBall = obj.GetComponent<XPBall>();
            return XPBall;
        });

        // Enemy object pool 持失
        m_enemyPool = new GameObjectPool<Enemy>(5, () =>
        {
            var obj = Instantiate(m_enemyPrefab);
            obj.GetComponent<NavMeshAgent>().enabled = false;
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            var enemy = obj.GetComponent<Enemy>();
            enemy.SetEnemy(m_player);
            return enemy;
        });
        //Invoke("CreateEnemy", 1f);
        InvokeRepeating("CreateEnemy", 3f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
