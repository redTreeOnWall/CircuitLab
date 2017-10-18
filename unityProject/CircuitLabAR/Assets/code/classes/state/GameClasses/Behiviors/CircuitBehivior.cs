using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitBehivior : MonoBehaviour
{

    //所有线路无电流；
    public draw[] lines;
    public FlareBehv[] lights;
    //开关关闭，有正常电流的线路
    public draw[] lineMormalI;
    public FlareBehv[] norMorLights;
    //开关关闭，短路电流的线路
    public draw[] linesWhenCutOpen;

    //开关是否关闭
    bool switchDown = false;
    //开关动画的状态 0 已经关闭 1 正在打开 ，2已将打开，3正在关闭
    int switchState = 0;
    public float rotSpeed = 90f;
    //开关的轴
    public Transform cutRoot;
    // Use this for initialization
    void Start()
    {
        this.changeSwitch();
    }
    // Update is called once per frame
    void Update()
    {
        switch (switchState)
        {
            case 0:

                break;
            case 1:
                this.cutRoot.eulerAngles = new Vector3(
                	this.cutRoot.eulerAngles.x - Time.deltaTime * rotSpeed,
                	this.cutRoot.eulerAngles.y,
                	this.cutRoot.eulerAngles.z
                );
				Debug.Log(this.cutRoot.eulerAngles.x);
                if (this.cutRoot.eulerAngles.x < 360f-45f)
                {
                    this.cutRoot.eulerAngles = new Vector3(
                		360f-45f,
                		this.cutRoot.eulerAngles.y,
                		this.cutRoot.eulerAngles.z
                	);
					switchState = 2;
                }
                break;
            case 2:
                break;
            case 3:
				this.cutRoot.eulerAngles = new Vector3(
                	this.cutRoot.eulerAngles.x + Time.deltaTime * rotSpeed,
                	this.cutRoot.eulerAngles.y,
                	this.cutRoot.eulerAngles.z
                );
                if (this.cutRoot.eulerAngles.x > 0f && this.cutRoot.eulerAngles.x < 360f-45f)
                {
                    this.cutRoot.eulerAngles = new Vector3(
                		0,
                		this.cutRoot.eulerAngles.y,
                		this.cutRoot.eulerAngles.z
                	);
					switchState = 0;
                }
                break;
        }
    }
    //开关状态改变
    public void changeSwitch()
    {
        switchDown = !switchDown;
        if (switchDown)
        {
			//所有线路状态改变
            for (int i = 0; i < lineMormalI.Length; i++)
            {
                lineMormalI[i].setState(1);
            }
            for (int i = 0; i < linesWhenCutOpen.Length; i++)
            {
                linesWhenCutOpen[i].setState(2);
            }
			//所有灯泡状态改变
            for (int i = 0; i < norMorLights.Length; i++)
            {
                norMorLights[i].haveI = true;
            }
        }
        else
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].setState(0);
            }
            //所有灯泡状态改变
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].haveI = false;
            }
        }
        AnimSwitch();
    }
    public void AnimSwitch()
    {
        if (switchDown)
        {
            switchState = 3;
        }
        else
        {
            switchState = 1;
        }
    }

}
