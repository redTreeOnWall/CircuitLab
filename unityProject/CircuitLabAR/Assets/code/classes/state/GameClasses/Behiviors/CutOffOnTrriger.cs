using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutOffOnTrriger : MonoBehaviour {

	public CircuitBehivior circuitBehivior;
	// Use this for initialization
	void Start () {
		
	}
	
	 void OnTriggerEnter(Collider other)
	{
		Debug.Log(other);
	}
	void OnMouseDown(){
		circuitBehivior.changeSwitch();
	}
}
