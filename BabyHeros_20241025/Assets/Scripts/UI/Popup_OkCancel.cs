using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_OkCancel : MonoBehaviour
{
    [SerializeField]
    UILabel m_title;
    [SerializeField]
    UILabel m_body;
    [SerializeField]
    UILabel m_okBtnText;
    [SerializeField]
    UILabel m_cancelBtnText;

    Action m_okDel;
    Action m_cancelDel;

    public void SetUI(string title, string body, Action okDel = null, Action cancelDel = null, string okBtnText = "Ok", string cancelBtnText = "Cancel")
    {
        m_title.text = title;
        m_body.text = body;
        m_okDel = okDel;
        m_cancelDel = cancelDel;
        m_okBtnText.text = okBtnText;
        m_cancelBtnText.text = cancelBtnText;
    }
    public void OnPressOK()
    {
        if (m_okDel != null)
            m_okDel();
        else
            PopupManager.Instance.ClosePopup();
    }
    public void OnPressCancel()
    {
        if (m_cancelDel != null)
            m_cancelDel();
        else
            PopupManager.Instance.ClosePopup();
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
