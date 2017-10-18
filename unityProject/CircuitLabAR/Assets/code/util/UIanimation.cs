using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIanimation
{
    GameObject obj;
	public Vector3 aimPosition;

	//0 变换完成， 1 正在变换 
	public int animationStatw =0;
    UIanimation(GameObject o)
    {
		this.obj = o;
    }
	
}
