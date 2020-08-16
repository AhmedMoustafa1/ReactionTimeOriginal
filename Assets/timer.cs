using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour {

    public float time;
    private bool x = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		while (x)
        {
            time = Time.fixedUnscaledDeltaTime;
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            x = !x;
            time = 0;
        }
    }



}
