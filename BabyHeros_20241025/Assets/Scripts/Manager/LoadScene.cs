using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneState
{
    None = -1,
    Title,
    Lobby,
    Game,
    Max
}

public class LoadScene : SingletonDontDestroy<LoadScene>
{
    [SerializeField]
    UIPanel m_loadingPanel;
    [SerializeField]
    UIProgressBar m_progressBar;
    [SerializeField]
    UILabel m_progressLabel;
    [SerializeField]
    TweenAlpha m_tweenAlpha;

    AsyncOperation m_loadState;
    SceneState m_state = SceneState.Title;
    SceneState m_loadScene = SceneState.None;

    public SceneState GetScene { get { return m_state; } }

    public void LoadSceneAsync(SceneState scene)
    {
        if (m_loadScene != SceneState.None)
            return;
        m_loadingPanel.alpha = 1f;
        m_loadScene = scene;
        m_loadState = SceneManager.LoadSceneAsync((int)scene);
    }
    public void HideUI()
    {
        m_tweenAlpha.enabled = false;
    }
    protected override void OnStart()
    {
        m_loadingPanel.alpha = 0f;
        m_tweenAlpha.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_loadState != null)
        {
            if(m_loadState.isDone)
            {
                m_state = m_loadScene;
                m_loadScene = SceneState.None;
                m_progressBar.value = 1f;
                m_progressLabel.text = "100%";
                m_tweenAlpha.enabled = true;
                m_tweenAlpha.ResetToBeginning();
                m_tweenAlpha.PlayForward();
                m_loadState = null;
            }
            else
            {
                //print(m_loadState.progress * 100f);
                m_progressBar.value = m_loadState.progress;
                m_progressLabel.text = ((int)(m_loadState.progress * 100f)).ToString() + '%';
            }
        }
    }
}
