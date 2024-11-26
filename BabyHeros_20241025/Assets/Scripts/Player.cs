using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region move
    [SerializeField]
    AnimationCurve m_moveStartCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField]
    AnimationCurve m_moveStopCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField]
    float m_speed = 3f;

    Vector3 m_dir;
    Animator m_animator;
    float m_scale;
    float m_moveStartTime;
    float m_moveStopTime;
    float m_moveStartDuration = 1.5f;
    float m_moveStopDuration = 0.8f;
    bool m_isMoveStart;
    bool m_isMoveStop;
    #endregion

    #region Bullet
    [SerializeField]
    GameObject m_bulletPrefab;
    [SerializeField]
    Transform m_bulletPos;
    
    GameObjectPool<Bullet> m_bulletPool;
    float m_bulletCount = 1f;
    float m_damage = 1f;
    #endregion

    public void RemoveBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        m_bulletPool.Set(bullet);
    }
    public void ApplySkill(SkillType type)
    {
        switch (type)
        {
            case SkillType.Blue: // Double Bullet
                m_bulletCount *= 2f;
                break;
            case SkillType.Green: // Double Power
                print("Double Power(Green)");
                m_damage *= 2f;
                break;
            case SkillType.Red: // Add Weapon
                print("Add Weapon(Red)");
                break;
            case SkillType.Magnet:
                print("Magnet");
                break;
        }
    }
    void CreateBullet()
    {
        float totalAngle = Mathf.Lerp(10f, 30f, (float)(m_bulletCount - 1) / 10f);
        totalAngle = Mathf.Clamp(totalAngle, 10f, 30f);
        float angleStep = totalAngle / (m_bulletCount - 1);
        float currentAngle = -totalAngle / 2f;

        for (int i = 0; i < m_bulletCount; i++)
        {
            var bullet = m_bulletPool.Get();
            bullet.transform.position = m_bulletPos.position;
            Quaternion rotation = Quaternion.Euler(0, currentAngle, 0); // y축 기준 회전
            Vector3 direction = rotation * m_bulletPos.forward;
            bullet.transform.rotation = Quaternion.LookRotation(direction);
            bullet.ChangeDamage(m_damage);
            bullet.gameObject.SetActive(true);
            currentAngle += angleStep;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
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
        // Firing State
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 총 쏘기 animation
            CreateBullet();
        }
        
        // Move State
        m_dir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (m_dir != Vector3.zero)
        {
            if (!m_isMoveStart)
            {
                m_moveStartTime = 1f - m_moveStopTime;
                m_isMoveStart = true;
                m_isMoveStop = false;
            }
            m_moveStopTime = 0f;
            transform.forward = m_dir;
            m_moveStartTime += Time.deltaTime / m_moveStartDuration;
            if (m_moveStartTime > 1f)
                m_moveStartTime = 1f;
            m_scale = m_moveStartCurve.Evaluate(m_moveStartTime);
            m_animator.SetFloat("Speed", m_scale);
        }
        else
        {
            if (!m_isMoveStop)
            {
                m_moveStopTime = 1f - m_moveStartTime;
                m_isMoveStop = true;
                m_isMoveStart = false;
            }
            m_moveStartTime = 0f;
            m_moveStopTime += Time.deltaTime / m_moveStopDuration;
            if(m_moveStopTime > 1f)
                m_moveStopTime = 1f;
            m_scale = m_moveStopCurve.Evaluate(m_moveStopTime);
            m_animator.SetFloat("Speed", m_scale);
        }
        transform.position += m_dir * m_speed * m_scale * Time.deltaTime;
    }
}
