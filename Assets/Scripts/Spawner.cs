using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用来产生水果，炸弹
/// </summary>
public class Spawner : MonoBehaviour {
    [Header("水果的预设")]
    public GameObject[] Fruits;
    [Header("炸弹的预设")]
    public GameObject Bomb;
    /// <summary>
    /// 产生水果的音效文件
    /// </summary>
    public AudioClip fruit_launch;

    public AudioSource audioSource;

    //产生时间
    float spawnTime = 3f;
    

    //是否生成水果
    bool isPlaying = true;

    void Update()
    {
        if(!isPlaying)
        {
            return;
        }
        spawnTime -= Time.deltaTime;
        if(0>spawnTime)
        {
            //一次最多产生的水果范围在（1,6）之间
            int fruitCount = Random.Range(1, 6);
            for(int i = 0; i<fruitCount;i++)
            {
                OnSpawn(true);
            }
                
            spawnTime = 3f;

            //随机产生炸弹
            int bombNum = Random.Range(0, 100);
            if(bombNum>70)
            {
                OnSpawn(false);
            }
        }
        

    }
    /// <summary>
    /// 临时存储当前水果的z坐标
    /// </summary>
    private float tmpZ = 0f;
    /// <summary>
    /// 产生水果
    /// </summary>
    private void OnSpawn(bool isFruit)
    {
        //播放音乐
        this.audioSource.Play();
        //x范围：-8.4-8.4
        //y范围：transform.pos.y
        //得到坐标的范围
        float x = Random.Range(-8.4f, 8.4f);
        float y = transform.position.y;
        float z = tmpZ;
        //使水果不在一个平面内
        tmpZ -= 2;
        
        if(tmpZ<=-10)
        {
            tmpZ = 0;
        }

        //实例化水果
        int fruitIndex = Random.Range(0, Fruits.Length);

        GameObject go;
        if(isFruit)
        {
            go = Instantiate<GameObject>(Fruits[fruitIndex], new Vector3(x, y, z), Random.rotation);
            
        }
        else
        {
            go = Instantiate<GameObject>(Bomb, new Vector3(x, y, z), Random.rotation);
        }
        //定义水果的速度
        Vector3 velocity = new Vector3(-x * Random.Range(0.2f, 0.8f), -Physics.gravity.y * Random.Range(1.0f, 1.5f), 0);

        Rigidbody rigidbody = go.GetComponent<Rigidbody>();
        rigidbody.velocity = velocity;
    }

    /// <summary>
    /// 有物体碰撞的时候调用
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
    }

}
