using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : SingletonDontDestroy<PlayerManager>
{
    [SerializeField]
    public GameObject[] m_playerPrefab;
    [SerializeField]
    CinemachineVirtualCamera m_virtualCamera;

    GameObject[] m_playerInstance;
    int m_selectedPlayerIndex = 0;

    #region UI test
    [SerializeField]
    SkillWindow m_skillWindow;
    #endregion

    public void SetActivePlayer(int index)
    {
        if (index < 0 || index >= m_playerInstance.Length) return;

        // note : 위치, 방향 고정 코드. 필요 없을 시 삭제
        Vector3 lastPosition = m_playerInstance[m_selectedPlayerIndex].transform.position;
        Quaternion lastRotation = m_playerInstance[m_selectedPlayerIndex].transform.rotation;

        m_playerInstance[m_selectedPlayerIndex].SetActive(false);

        m_selectedPlayerIndex = index;

        // note : 위치, 방향 고정 코드. 필요 없을 시 삭제
        m_playerInstance[m_selectedPlayerIndex].transform.position = lastPosition;
        m_playerInstance[m_selectedPlayerIndex].transform.rotation = lastRotation;

        m_playerInstance[m_selectedPlayerIndex].SetActive(true);
        m_virtualCamera.Follow = m_playerInstance[m_selectedPlayerIndex].transform;
        EnemyManager.Instance.m_player = m_playerInstance[m_selectedPlayerIndex].GetComponent<Player>();

    }
    void SpawnAllPlayers()
    {
        m_playerInstance = new GameObject[m_playerPrefab.Length];

        for (int i = 0; i < m_playerPrefab.Length; i++)
        {
            m_playerInstance[i] = Instantiate(m_playerPrefab[i]);
            m_playerInstance[i].transform.SetParent(transform);
            m_playerInstance[i].SetActive(false);
        }
        SetActivePlayer(m_selectedPlayerIndex);
    }
    // Start is called before the first frame update
    protected override void OnAwake()
    {
        SpawnAllPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // note : skill window test
        {
            if (!m_skillWindow.IsOpened)
            {
                m_skillWindow.Open();
            }
            else
            {
                m_skillWindow.Close();
            }
        }
    }
}
