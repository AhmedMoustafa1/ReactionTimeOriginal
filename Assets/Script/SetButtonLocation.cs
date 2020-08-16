using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetButtonLocation : MonoBehaviour {
	public Vector3 FirstLocation;
	public Vector3 FirstRotation;

	public Vector3 SecondLocation;
	public Vector3 SecondRotation;


	public void SetFirstLocation()
	{
		//Debug.Log("set Frirt Locaion");
		this.transform.localPosition = FirstLocation;
		this.transform.localRotation = Quaternion.Euler(FirstRotation);

	}
	public void SetSecondLocation()
	{
		//Debug.Log("set second location");
		this.transform.localPosition = SecondLocation;
		this.transform.localRotation = Quaternion.Euler(SecondRotation); ;

	}

}
