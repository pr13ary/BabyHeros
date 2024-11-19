using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWindow : MonoBehaviour
{
    [SerializeField]
    GameObject m_slotPrefab;
    [SerializeField]
    UIGrid m_slotGrid;
    [SerializeField]
    TweenScale m_tweenScale;
    [SerializeField]
    UISprite m_cursor;
    [SerializeField]
    GameObject m_skillPrefab;

    List<Slot> m_slotList = new List<Slot>();
    const int MAX_SLOT = 3;

    public bool IsOpened { get { return gameObject.activeSelf; } }

    public void OnSelectSkill()
    {
        var curSlot = m_slotList.Find(slot => slot.IsSelected);
        if (curSlot != null)
        {
            curSlot.SelectSkill();
        }
    }
    public void SelectSlot(Slot slot)
    {
        for(int i=0; i<m_slotList.Count; i++)
        {
            if (m_slotList[i].IsSelected)
            {
                m_slotList[i].IsSelected = false;
                break;
            }
        }
        slot.IsSelected = true;
        m_cursor.enabled = true;
        m_cursor.transform.position = slot.transform.position;
    }
    public void Open()
    {
        RemoveSkill();
        gameObject.SetActive(true);
        FillSkill();
        m_tweenScale.PlayForward();
    }
    public void Close()
    {
        m_tweenScale.PlayReverse();
    }
    public void HideUI()
    {
        if(m_tweenScale.direction == AnimationOrTween.Direction.Reverse) 
        {
            gameObject.SetActive(false);
        }
    }
    void RemoveSkill()
    {
        for(int i=0; i<m_slotList.Count; i++)
        {
            m_slotList[i].EmptySlot();
        }
    }
    void CreateSlot(int count)
    {
        for(int i=0; i<count; i++)
        {
            var obj = Instantiate(m_slotPrefab);
            obj.transform.SetParent(m_slotGrid.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            var slot = obj.GetComponent<Slot>();
            slot.InitSlot(this);
            m_slotList.Add(slot);
        }
    }
    Skill CreateSkill()
    {
        SkillType type = (SkillType)Random.Range((int)SkillType.Blue, (int)SkillType.Max);
        var obj = Instantiate(m_skillPrefab);
        var skill = obj.GetComponent<Skill>();
        skill.SetSkill(type);
        return skill;
    }
    void FillSkill()
    {
        for(int i=0; i<m_slotList.Count; i++)
        {
            var skill = CreateSkill();
            m_slotList[i].SetSlot(skill);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateSlot(MAX_SLOT);
        m_cursor.enabled = false;
        gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
