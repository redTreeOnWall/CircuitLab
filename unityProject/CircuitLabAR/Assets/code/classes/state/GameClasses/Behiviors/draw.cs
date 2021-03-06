﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//用于绘制计算贝塞尔曲线
public class draw : MonoBehaviour
{

    public static GameObject propertis;
    //电线的材质
    public static Material line0M;
    public static Material line2M;
    // 电线的renderer组件;
    public Renderer renderer;

    //短路火花
    public GameObject[] fire;

    //电线状态（0没有电流，1正常电流，2短路电流）
    public int state = 0;


    public void setState(int nextState)
    {
        //关闭短路火花
        for (int i = 0; i < fire.Length; i++)
        {
            fire[i].SetActive(false);
            Debug.Log(fire[i].name);
        }
        this.state = nextState;
        switch (nextState)
        {
            case 0:
                this.renderer.material = line0M;
                for (int i = 0; i < this.IPoints.Length; i++)
                {
                    IPoints[i].SetActive(false);
                }
                break;
            case 1:
                this.renderer.material = line0M;
                for (int i = 0; i < this.IPoints.Length; i++)
                {
                    IPoints[i].SetActive(true);
                }
                break;
            case 2:
                this.renderer.material = line2M;
                for (int i = 0; i < this.IPoints.Length; i++)
                {
                    IPoints[i].SetActive(false);
                }
                for (int i = 0; i < fire.Length; i++)
                {
                    fire[i].SetActive(true);
                }
                break;
        }
    }
    public GameObject v0, v1, a0;
    LineRenderer line;
    GameObject IPoint;
    //电流的代表对象以及位置；
    GameObject[] IPoints = new GameObject[10];

    //电流周期，即电流走完一次贝塞尔曲线的时间。
    public float iT = 5.0f;


    //导线的线段上的所有节点的位置
    public Vector3[] linePoints = new Vector3[31];
    //绑定了骨骼的网格
    public GameObject LineMesh;


    LineRenderer lineRenderer;
    //Vector3 v0, v1;//顶点  
    //Vector3 a0;  

    //分段数
    float lineNum = 31f;
    float jianxi = 0.0001f;//绘制的0-1的间隙 越小曲线越接近曲线，  

    //电流点之间的间隔；
    int ppi;
    // Use this for initialization  
    void Start()
    {
        renderer = this.LineMesh.transform.Find("IK").GetComponent<Renderer>();
        //计算所有节点位置。
        for (int i = 0; i < linePoints.Length; i++)
        {
            var t = (float)i / (float)linePoints.Length;
            linePoints[i] = po(t, v0, v1, a0);
        }
        //将lineMesh移动到中心
        LineMesh.transform.position = new Vector3(
            (v0.transform.position.x - v1.transform.position.x) / 2 + v1.transform.position.x,
            (v0.transform.position.y - v1.transform.position.y) / 2 + v1.transform.position.y,
            (v0.transform.position.z - v1.transform.position.z) / 2 + v1.transform.position.z
        );
        //获取所有骨骼
        var bones = new Transform[linePoints.Length];
        for (int i = 0; i < bones.Length; i++)
        {
            var num = i + 1;
            bones[i] = LineMesh.transform.Find("b" + num);
        }

        //控制骨骼的位置
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].transform.position = linePoints[i];
        }
        for (int i = 0; i < bones.Length; i++)
        {
            //控制角度
            if (i == bones.Length - 1)
            {
                float eler = Mathf.Atan2(bones[i].position.z - bones[i - 1].position.z, bones[i].position.x - bones[i - 1].position.x);
                eler = 180 - eler * 180 / Mathf.PI;
                bones[i].eulerAngles = new Vector3(
                     bones[i].eulerAngles.x,
                     eler,
                     bones[i].eulerAngles.z
                );
            }
            else
            {
                float eler3 = Mathf.Atan2(bones[i + 1].position.z - bones[i].position.z, bones[i + 1].position.x - bones[i].position.x);

                eler3 = 180 - eler3 * 180 / Mathf.PI;
                bones[i].eulerAngles = new Vector3(
                     bones[i].eulerAngles.x,
                     eler3,
                     bones[i].eulerAngles.z
                );
            }
        }

        //初始化线条
        line = this.transform.GetComponent<LineRenderer>();
        IPoint = this.transform.Find("iPointSmall").gameObject;

        //绘制line
        jianxi = 1.0f / lineNum;
        List<Vector3> vl = new List<Vector3>();
        for (float i = 0; i < 1; i += jianxi)
        {
            vl.Add(po(i, v0, v1, a0));
        }
        Vector3[] vs = new Vector3[vl.Count];
        for (int ii = 0; ii < vs.Length; ii++)
        {
            vs[ii] = vl[ii];
        }
        line.SetVertexCount(vs.Length);
        line.SetPositions(vs);
        //       Debug.Log(line.positionCount);

        //创建电流点数组
        for (int i = 0; i < IPoints.Length; i++)
        {
            GameObject p = GameObject.Instantiate(IPoint);
            p.transform.parent = line.transform;
            p.transform.localScale = IPoint.transform.localScale;
            IPoints[i] = p;
        }
        IPoint.SetActive(false);
        //计算两个电流点之间的间隔
        ppi = (int)lineNum / IPoints.Length;
        if (ppi < 1)
        {
            ppi = 1;
        }

        setState(0);

    }

    float flowI = 0;
    void Update()
    {
        if (state == 1)
        {
            //移动电流点
            for (int x = 0; x < IPoints.Length; x++)
            {
                float timeX = Time.time % iT;
                var t = (x * (iT / IPoints.Length) + timeX) / iT;
                if (t > 1f)
                {
                    t = t - 1f;
                }
                var v3 = po(t, v0, v1, a0);
                IPoints[x].transform.position = v3;
            }
        }

        /* 
        if (flowI < 1f)
        {
            flowI += jianxi;
        }
        else
        {
            flowI = 0f;
        }



        for (int i = 0; i < IPoints.Length; i++)
        {
            float t = flowI + jianxi * ppi * i;
            if (t > 1f)
            {
                t = t - 1f;
            }
            //            IPoints[i].transform.position = po(t, v0, v1, a0);
        }
        //       IPoint.transform.position = po(flowI, v0, v1, a0); 
        */
    }

    private Vector3 po(float t, GameObject v0, GameObject v1, GameObject a0)//根据当前时间t 返回路径  其中v0为起点 v1为终点 a为中间点   
    {
        Vector3 a;
        a.x = t * t * (v1.transform.position.x - 2 * a0.transform.position.x + v0.transform.position.x) + v0.transform.position.x + 2 * t * (a0.transform.position.x - v0.transform.position.x);//公式为B(t)=(1-t)^2*v0+2*t*(1-t)*a0+t*t*v1 其中v0为起点 v1为终点 a为中间点   
        a.y = t * t * (v1.transform.position.y - 2 * a0.transform.position.y + v0.transform.position.y) + v0.transform.position.y + 2 * t * (a0.transform.position.y - v0.transform.position.y);
        a.z = t * t * (v1.transform.position.z - 2 * a0.transform.position.z + v0.transform.position.z) + v0.transform.position.z + 2 * t * (a0.transform.position.z - v0.transform.position.z);
        return a;
    }
}