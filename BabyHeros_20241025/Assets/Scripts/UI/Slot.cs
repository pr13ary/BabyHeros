using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField]
    Skill m_skill;
    [SerializeField]
    SkillWindow m_skillWindow;
    [SerializeField]
    bool m_isSelected;

    public bool IsSelected { get { return m_isSelected; } set { m_isSelected = value; } }
    public void InitSlot(SkillWindow skillWindow)
    {
        m_skillWindow = skillWindow;
    }
    public void OnSelect()
    {
        m_skillWindow.SelectSlot(this);
    }
    public void SelectSkill()
    {
        if (m_skill == null) return;
        m_skill.UseSkill();
    }
    public void SetSlot(Skill skill)
    {
        m_skill = skill;
        skill.transform.SetParent(transform);
        skill.transform.localPosition = Vector3.zero;
        skill.transform.localScale = Vector3.one;
    }
    public void EmptySlot()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
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
