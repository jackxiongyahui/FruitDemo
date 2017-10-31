using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIStart : MonoBehaviour {
    
    /// <summary>
    /// 开始按钮
    /// </summary>
    private Button btnPlay;
    /// <summary>
    /// 声音按钮,如果是private,则不在UIStart中显示，如果是public,则会在UISart中显示
    /// </summary>
    private Button btnSound;
    /// <summary>
    /// 背景音乐播放器
    /// </summary>
    private AudioSource audioSourceBG;

    private Image imgSound;
    /// <summary>
    /// 声音的图片
    /// </summary>
    public Sprite[] soundSprites;

    // Use this for initialization
    void Start () {
        GetComponents();
        btnPlay.onClick.AddListener(OnPlayClick);
        btnSound.onClick.AddListener(OnSoundClick);
	}

    //销毁控件
    void OnDestroy()
    {
        btnPlay.onClick.RemoveListener(OnPlayClick);
        btnSound.onClick.RemoveListener(OnSoundClick);
    }

    //寻找，初始化组件
    private void GetComponents()
    {
        btnPlay = transform.Find("btnPlay").GetComponent<Button>();
        btnSound = transform.Find("btnSound").GetComponent<Button>();
        audioSourceBG = transform.Find("btnSound").GetComponent<AudioSource>();
        imgSound = transform.Find("btnSound").GetComponent<Image>();
    }

    //当开始按钮按下时的点击事件,当“play”按钮点击之后，跳转到play界面
    void OnPlayClick()
    {
        SceneManager.LoadScene("play", LoadSceneMode.Single);
    }

    //当声音按钮点击时调用
    void OnSoundClick()
    {
        //如果正在播放，停止播放
        if(audioSourceBG.isPlaying)
        { 
            audioSourceBG.Pause();
            imgSound.sprite = soundSprites[1];  //切换图片
        }else   //如果没有播放，就开始播放
        {
            audioSourceBG.Play();
            imgSound.sprite = soundSprites[0];  //切换图片
        }
    }
}
