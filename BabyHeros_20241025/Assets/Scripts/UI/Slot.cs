using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField]
    Skill m_skillWindow;
    [SerializeField]
    bool m_isSelected;

    public bool IsSelected { get { return m_isSelected; } set { m_isSelected = value; } }
    public void InitSlot(Skill skillWindow)
    {
        m_skillWindow = skillWindow;
    }
    public void OnSelect()
    {
        m_skillWindow.OnSelectSlot(this);
    }
    
    public void SetSlot()
    {

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
