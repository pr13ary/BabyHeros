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
                print("Double Bullet(Blue)");
                break;
            case SkillType.Green: // Change Weapon
                print("Change Weapon(Green)");
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
        var bullet = m_bulletPool.Get();
        bullet.transform.position = m_bulletPos.position;
        bullet.transform.rotation = m_bulletPos.rotation;
        bullet.gameObject.SetActive(true);
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
            bullet.InitBullet(this); // bullet°ú playerÀÇ ¿¬°á°í¸®
            return bullet;
        });
    }

    // Update is called once per frame
    void Update()
    {
        // Firing State
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ÃÑ ½î±â animation
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
