using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物体控制脚本
/// </summary>
public class ObjectControl : MonoBehaviour {

    /// <summary>
    /// 一半的水果
    /// </summary>
    public GameObject halfFruit;

    public GameObject Splash;

    public GameObject SplashFlat;

    public GameObject Firework;

    public AudioClip audioClip;

    private bool dead = false;
    /// <summary>
    /// 被切割的时候调用
    /// </summary>
	public void OnCut()
    {
        //防止重复调用
        if (dead)
            return;

        if(gameObject.name.Contains("Bomb"))
        {
            //生成特效
            Instantiate(Firework, transform.position, Quaternion.identity);

            //如果是炸弹则扣分
            UIScore.Instance.Remove(20);
        }
        else
        {
            //先生成被切割的水果
            for (int i = 0; i < 2; i++)
            {
                GameObject go = Instantiate<GameObject>(halfFruit, transform.position, Random.rotation);
                go.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * 5f, ForceMode.Impulse);
            }

            //生成特效
            Instantiate(Splash, transform.position, Quaternion.identity);
            Instantiate(SplashFlat, transform.position, Quaternion.identity);

            //如果是水果则加分
            UIScore.Instance.Add(5);
        }

        AudioSource.PlayClipAtPoint(audioClip, transform.position);
        //销毁自身
        Destroy(gameObject);
        
        dead = true;
    }
}
