using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject m_bulletPrefab;
    [SerializeField]
    Transform m_bulletPos;
    [SerializeField]
    float m_speed = 3f;

    Camera m_camera;
    Vector3 m_dir;
    GameObjectPool<Bullet> m_bulletPool;

    public void RemoveBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        m_bulletPool.Set(bullet);
    }
    void CreateBullet()
    {
        var bullet = m_bulletPool.Get();
        bullet.transform.position = m_bulletPos.position;
        bullet.transform.rotation = m_bulletPos.rotation;
        bullet.gameObject.SetActive(true);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;
        m_bulletPool = new GameObjectPool<Bullet>(5, () =>
        {
            var obj = Instantiate(m_bulletPrefab);
            obj.SetActive(false);
            var bullet = obj.GetComponent<Bullet>();
            bullet.InitBullet(this); // bullet과 player의 연결고리
            return bullet;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //InvokeRepeating("CreateBullet", 1f, 0.2f);
            CreateBullet();
        }

        m_dir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (m_dir != Vector3.zero)
        {
            transform.forward = m_dir;
        }
        transform.position += m_dir * m_speed * Time.deltaTime;
    }
}
