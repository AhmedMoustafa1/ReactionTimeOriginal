using UnityEngine;
using Valve.VR;

public class headFixer : MonoBehaviour
{
    public Camera head;
    public GameObject cameraRig;

   // private SteamVR_TrackedController trackedController;
    private SteamVR_TrackedObject trackedObject;
    public GameObject Hand;

    void Start()
    {
        cameraRig.transform.position = Vector3.zero;
        Invoke("Fix", 1);
        trackedObject = Hand.GetComponent<SteamVR_TrackedObject>();
        //trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
        //trackedController.controllerIndex = (uint)trackedObject.index;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Fix();
        var device = SteamVR_Controller.Input((int)trackedObject.index);
      
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Fix();
        //}

        if (device.GetPressDown(EVRButtonId.k_EButton_A))
        {
            Fix();

        }
        if (device.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu))
        {
            switchPositionalTracking();
        }


    }
    public void Fix()
    {
        //Debug.Log("Fixing");
        //// head.transform.parent = this.transform.parent;
        //Debug.Log("cameraRig Position : " + cameraRig.transform.position);
        //Debug.Log("Head Ref Position : " + this.transform.position);
        //Debug.Log("CameraHead Position : " + head.transform.position);

        cameraRig.transform.position = cameraRig.transform.position + (this.transform.position - head.transform.position);

        //Debug.Log("New cameraRig Position : " + cameraRig.transform.position);

        //  head.transform.parent = cameraRig.transform;
    }



    public void switchPositionalTracking()
    {
        UnityEngine.XR.InputTracking.disablePositionalTracking = !UnityEngine.XR.InputTracking.disablePositionalTracking;

    }
}
