﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class draw : MonoBehaviour
{
    public GameObject v0, v1, a0;
    LineRenderer line;
    GameObject IPoint;
    GameObject[] IPoints = new GameObject[10];

    LineRenderer lineRenderer;
    //Vector3 v0, v1;//顶点  
    //Vector3 a0;  

    //分段数
    float lineNum = 100f;
    float jianxi = 0.01f;//绘制的0-1的间隙 越小曲线越接近曲线，  

    //电流点之间的间隔；
    int ppi;
    // Use this for initialization  
    void Start()
    {
        //初始化
        line = this.transform.GetComponent<LineRenderer>();
        IPoint = this.transform.Find("i").gameObject;

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
        Debug.Log(line.positionCount);

        //创建电流点数组
        for (int i = 0; i < IPoints.Length; i++)
        {
            GameObject p = GameObject.Instantiate(IPoint);
            p.transform.parent = line.transform;
            p.transform.localScale = IPoint.transform.localScale;
            IPoints[i] = p;
        }
        //计算两个电流点之间的间隔
        ppi = (int)lineNum / IPoints.Length;
        if (ppi < 1)
        {
            ppi = 1;
        }
    }

    // Update is called once per frame  
    float flowI = 0;
    void Update()
    {
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
            IPoints[i].transform.position = po(t, v0, v1, a0);
        }
        IPoint.transform.position = po(flowI, v0, v1, a0);
        //  Debug.DrawLine(po(i, v0, v1, a0), po(i + jianxi, v0, v1, a0), Color.red); 


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