using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实现切水果功能
/// 1.实现刀痕效果
/// </summary>
public class MouseControl : MonoBehaviour {
    /// <summary>
    /// 播放声音的组件
    /// </summary>
    [SerializeField]
    public AudioSource audioSource;

    /// <summary>
    /// 直线渲染器
    /// </summary>
    [SerializeField]
    private LineRenderer lineRenderer;
    //是否第一次鼠标按下
    private bool firstMouseDown = false;
    //是否鼠标一直按下
    private bool mouseDown = false;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            firstMouseDown = true;
            mouseDown = true;
            //播放声音
            audioSource.Play();
        }
        if(Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        onDrawLine();
        firstMouseDown = false;
    }
    /// <summary>
    /// 保存所有的坐标
    /// </summary>
    private Vector3[] positions = new Vector3[10];
    /// <summary>
    /// 当前保存坐标的数量
    /// </summary>
    private int posCount = 0;
    /// <summary>
    /// 代表这一帧的鼠标位置
    /// </summary>
    private Vector3 head;
    /// <summary>
    /// 代表上一帧的鼠标位置
    /// </summary>
    private Vector3 last;
   
    /// <summary>
    /// 控制画线
    /// </summary>
    private void onDrawLine() 
    {
        if(firstMouseDown)
        {
            //先把计数器设为0
            posCount = 0;

            head = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            last = head;
        }
        if(mouseDown)
        {
            head = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(Vector3.Distance(head,last)>0.01f)
            {
                //如果距离比较远了，就保存到数组里面
                savePosition(head);

                posCount++;
                //发射一条射线
                onRayCast(head);
   
            }
            last = head;
        }
        else
        {
            this.positions = new Vector3[10];
        }
        ChangePositions(positions);
    }

    /// <summary>
    /// 发射射线
    /// </summary>
    /// <param name="pos"></param>
    private void onRayCast(Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        //检测到所有物体
        RaycastHit[] hits = Physics.RaycastAll(ray);
        for(int i = 0; i < hits.Length;i++)
        {
            //Debug.Log(hits[i].collider.gameObject.name);
            //Destroy(hits[i].collider.gameObject);
            hits[i].collider.gameObject.SendMessage("OnCut", SendMessageOptions.DontRequireReceiver);
        }
    }

    /// <summary>
    /// 保存坐标点
    /// </summary>
    /// <param name="pos"></param>
    private void savePosition(Vector3 pos)
    {
        pos.z = 0;
        if(posCount<=9)
        {
            for(int i = posCount; i<10; i++)
            {
                positions[i] = pos;
            }    
        }
        else
        {
            for(int i = 0; i<9; i++)
            {
                positions[i] = positions[i + 1];

            }
            positions[9] = pos;
        }
    }

    /// <summary>
    /// 修改直线渲染器的坐标
    /// </summary>
    /// <param name="positions">Positions.</param>
    private void ChangePositions(Vector3[] positions)
    {
        lineRenderer.SetPositions(positions);
    }
}
