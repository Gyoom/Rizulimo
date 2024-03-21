using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AudioManager;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class title : MonoBehaviour
{
    public SceneReference next_scene;

    public Button Button;
    // Start is called before the first frame update
    void Start()
    {
        BGMManager.Instance.Play(
            audioPath       : BGMPath.FIRE2, //再生したいオーディオのパス
            volumeRate      : 1,                //音量の倍率
            delay           : 0,                //再生されるまでの遅延時間
            pitch           : 1,                //ピッチ
            isLoop          : true,             //ループ再生するか
            allowsDuplicate : false             //他のBGMと重複して再生させるか
        );

        this.Button
            .OnClickAsObservable()
            .TakeUntilDestroy(this)
            .ThrottleFirst(TimeSpan.FromMilliseconds(1000))
            .Subscribe(_ => { OnClick(); });
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClick()
    {
        BGMManager.Instance.FadeOut();
            SEManager.Instance.Play(
                audioPath       : SEPath.JUMP, //再生したいオーディオのパス
                volumeRate      : 0.2f,                //音量の倍率
                delay           : 0,                //再生されるまでの遅延時間
                pitch           : 1,                //ピッチ
                isLoop          : false             //ループ再生するか
            );
            SceneManager.LoadSceneAsync(next_scene);
    }
}
