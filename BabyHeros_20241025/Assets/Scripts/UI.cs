using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    int m_currentPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // note : character change test
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            m_currentPlayer++;
            if (m_currentPlayer > 2)
                m_currentPlayer = 0;
            PlayerManager.Instance.SetActivePlayer(m_currentPlayer);
        }
    }
}
