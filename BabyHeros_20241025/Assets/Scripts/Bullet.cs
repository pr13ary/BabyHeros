using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float m_speed = 10f;

    Vector3 m_dir;
    Player m_player;

    public void InitBullet(Player player)
    {
        m_player = player;
    }

    void RemoveBullet()
    {
        m_player.RemoveBullet(this);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Wall"))
        {
            RemoveBullet();
        }
        if (collision.CompareTag("Enemy"))
        {
            RemoveBullet();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, m_speed * Time.deltaTime));
    }
}
