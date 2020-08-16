using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegateTracking : MonoBehaviour {
    public Transform HMD;
    public Transform Hight;
    public Transform Eyes;

	// Use this for initialization
	void Start () {

        HMD.position = Hight.position;

    }

    // Update is called once per frame
    void Update () {
      

    }
}
