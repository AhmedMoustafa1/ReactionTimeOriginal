using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destryer : MonoBehaviour {

    public void Destroy()
    {
        GameObject.Destroy(this.transform.parent.gameObject);
    }
}
