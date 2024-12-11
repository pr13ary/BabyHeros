using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : SingletonDontDestroy<PopupManager>
{
    [SerializeField]
    GameObject m_popupOkCancelPrefab;

    List<GameObject> m_popupList = new List<GameObject>();
    int m_popupDepth = 100;
    int m_popupGap = 10;

    public bool IsOpened { get { return m_popupList.Count > 0; } }
    public void ClosePopup()
    {
        if(m_popupList.Count > 0)
        {
            Destroy(m_popupList[m_popupList.Count - 1]);
            m_popupList.RemoveAt(m_popupList.Count - 1);
        }
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
