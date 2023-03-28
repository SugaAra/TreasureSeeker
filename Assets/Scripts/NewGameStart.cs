using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TitleからGameSceneに移る
public class NewGameStart : MonoBehaviour
{

    public void SceneChange()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
