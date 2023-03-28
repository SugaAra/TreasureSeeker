using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleObject : MonoBehaviour
{
    // TitleのBGMを流す
    void Start()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
    }
}
