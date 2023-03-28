using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Start()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.GameOver);
    }
    public void SceneChange()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.BackToTitle);
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }
}
