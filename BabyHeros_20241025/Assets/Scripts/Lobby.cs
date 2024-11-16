using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
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
