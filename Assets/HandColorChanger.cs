using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandColorChanger : MonoBehaviour {
    private Material material;
    public Color BlackTint;
    public Color YellowTint;

    // Use this for initialization
    void Start () {
        material = this.gameObject.GetComponent<Renderer>().material;
        if(UserManagerInput.instance != null)
        {
            if (UserManagerInput.instance._thisUser.userHandColor == HandColor.Black)
            {
                material.color = BlackTint;
            }
            else if (UserManagerInput.instance._thisUser.userHandColor == HandColor.Yellow)
            {
                material.color = YellowTint;

            }
            else
            {
                material.color = Color.white;
            }
        }
        


    }


}
