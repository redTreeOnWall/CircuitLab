using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using state.GameClasses.Behiviors;

public class FlareBehv : MonoBehaviour {
	//是否有电流通过
	public bool haveI = false;
	public GameObject light;
	public GameBehivior game;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//如果图像未识别就灯光关闭
		if(GameBehivior.TrackFound && haveI){
			light.SetActive(true);
		}else{
			light.SetActive(false);
		}
	}
}
