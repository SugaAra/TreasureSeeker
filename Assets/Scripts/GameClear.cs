using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    public void Start()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.GameClear);
    }
    public void SceneChange()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.BackToTitle);
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }
}