using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void GoNextScene()
    {
        LoadScene.Instance.LoadSceneAsync(SceneState.Game);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
