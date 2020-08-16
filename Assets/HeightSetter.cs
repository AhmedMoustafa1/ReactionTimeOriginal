using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using Kandooz;

public class HeightSetter : MonoBehaviour {

    private SteamVR_TrackedController trackedController;
    private SteamVR_TrackedObject trackedObject;
    public bool on = false;

    public float minHeight;
    public float maxHeight;
    public float rate;

    public GameObject coolbox;
    public Transform Test;
    public Text message;

    public VRButtonSelector rightHand;
    public VRButtonSelector leftHand;

    public BoolField panelPosition;
    public BoolField panelHeight;
    public BoolField inGame;

    void Start () {
         trackedObject = this.GetComponent<SteamVR_TrackedObject>();
        trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
        trackedController.controllerIndex = (uint)trackedObject.index;
        message.text = "Set Height";
        panelHeight.Value = false;
        panelPosition.Value = false;
    }

    public void FixedUpdate()
    {
    }
    // Update is called once per frame
    void Update () {
        var device = SteamVR_Controller.Input((int)trackedObject.index);


        
        if (device.GetPressDown(EVRButtonId.k_EButton_A) && !inGame.Value)
        {
            message.text = "Set Height";

            panelHeight.Value = !panelHeight.Value;

            coolbox.SetActive(panelHeight.Value);
            rightHand.enabled = !panelHeight.Value;
            leftHand.enabled = !panelHeight.Value;
            panelPosition.Value = false;

        }

        if (panelHeight.Value)
        {
            if (device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).y > 0.1f &&  Test.position.y <maxHeight)
            {
                Test.Translate(rate*Vector3.up * Time.deltaTime);

            }
            if (device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).y < -0.1f && Test.position.y > minHeight)
            {
                Test.Translate(rate * Vector3.down*Time.deltaTime);



            }
        }
        if (Input.GetKeyDown(KeyCode.W) && Test.position.y < maxHeight)
        {
            Test.Translate(rate * Vector3.up * Time.deltaTime );

        }
        if (Input.GetKeyDown(KeyCode.S) && Test.position.y > minHeight)
        {
            Test.Translate(rate * Vector3.down * Time.deltaTime);

        }

    }
}
