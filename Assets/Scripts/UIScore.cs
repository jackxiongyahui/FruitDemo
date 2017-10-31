﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour {

    /// <summary>
    /// 单例对象
    /// </summary>
    public static UIScore Instance = null;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private Text txtScore;
    /// <summary>
    /// 当前分数
    /// </summary>
    private int score = 0;
	
    /// <summary>
    /// 加分
    /// </summary>
    /// <param name="score"></param>
    public void Add(int score)
    {
        this.score += score;
        txtScore.text = this.score.ToString();
    }

    /// <summary>
    /// 减分
    /// </summary>
    public void Remove(int score)
    {
        this.score -= score;

        //如果分数小于0，游戏结束
        if(this.score<=0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("over");
            return;
            
        }
        txtScore.text = this.score.ToString();
    }
}
