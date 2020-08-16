using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class Handmation : MonoBehaviour {
    public enum HandModes{
        single,
        multiple
    }
    private Animator animator;
    public SteamVR_TrackedController trackedController;
    public HandModes handMode=HandModes.single;
    // Use this for initialization
    void Start () {
        var trackedObject = this.GetComponent<SteamVR_TrackedObject>();
        trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
        trackedController.controllerIndex = (uint)trackedObject.index;
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (trackedController) 
        {
            if (!animator)
            {
                animator =this.GetComponentInChildren<Animator>();
            }

            switch (handMode)
            {
                case HandModes.single:
                    if (trackedController.triggerPressed )
                    {
                        animator.SetBool("index", true);
                        animator.SetBool("grip", true);

                    }
                    if (!trackedController.triggerPressed )
                    {
                        animator.SetBool("index", false);
                        animator.SetBool("grip", false);

                    }
                    break;
                case HandModes.multiple:
                    if (trackedController.gripped )
                    {
                        animator.SetBool("grip", true);
                        
                        //float senstivity = hand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x;

                    }
                    if (!trackedController.gripped)
                    {
                        animator.SetBool("grip", false);
                    }
                    if (trackedController.triggerPressed )
                    {
                        animator.SetBool("index", true);

                    }
                    if (!trackedController.triggerPressed )
                    {
                        animator.SetBool("index", false);

                    }
                    break;
                default:
                    break;
            }

            
        }
 

    }
}
