using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float m_speed = 10f;

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
            var enemy = collision.gameObject.GetComponent<Enemy>(); // note : getcomponent 수정 필요
            enemy.SetDamage(1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //m_rigidbody.velocity = transform.forward * m_speed;
        transform.Translate(new Vector3(0, 0, m_speed * Time.deltaTime));
    }
}
