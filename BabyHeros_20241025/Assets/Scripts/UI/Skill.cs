using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    GameObject m_slotPrefab;
    [SerializeField]
    UIGrid m_slotGrid;
    [SerializeField]
    TweenScale m_tweenScale;
    [SerializeField]
    UISprite m_cursor;

    List<Slot> m_slotList = new List<Slot>();
    const int MAX_SLOT = 3;

    public bool IsOpened { get { return gameObject.activeSelf; } }

    public void OnSelectSlot(Slot slot)
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
        gameObject.SetActive(true);
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
