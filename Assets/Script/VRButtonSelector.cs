using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hands
{
    Left,
    Right,
    None
}

public class VRButtonSelector : MonoBehaviour {

    public float gazeTime = 1f;

    public Hands thishand;
    public Transform sphereCastPos;
    public float castRadius = 0.1f;

    private float timeCounter = 0;
    private ButtonBehaviour currentButtonBehaviour = null;
    private ButtonBehaviour intObj;
    private List<ButtonBehaviour> nearObjects;


    private int currentObjectIndex;
    private float distance;
    private float newDis;

    public ButtonBehaviour CurrentButtonBehaviour
    {
        get
        {
            return currentButtonBehaviour;
        }

        set
        {
            if (value != currentButtonBehaviour)
            {
                if (currentButtonBehaviour != null)
                {
                    StopAllCoroutines();
                    timeCounter = 0;
                    currentButtonBehaviour.lastHand = Hands.None;
                    currentButtonBehaviour.buttonHovered = false;
                    if (currentButtonBehaviour.onButtonExit.GetPersistentEventCount() > 0)
                    {
                        currentButtonBehaviour.onButtonExit.Invoke();
                    }
                }

                currentButtonBehaviour = value;
                if (currentButtonBehaviour != null)
                {
                    Debug.Log(currentButtonBehaviour);

                    currentButtonBehaviour.lastHand = thishand;
                    currentButtonBehaviour.buttonHovered = true;
                    if (currentButtonBehaviour.onButtonEnter.GetPersistentEventCount() > 0)
                    {
                        currentButtonBehaviour.onButtonEnter.Invoke();
                    }
                    if (currentButtonBehaviour.onButtonPressed.GetPersistentEventCount() > 0) StartGazing();
                }
            }

    }
    }

    private void StartGazing()
    {
        timeCounter = 0;
        StopAllCoroutines();
        StartCoroutine(Gazing());
    }
    #region OLD
    //private void OnTriggerEnter(Collider other)
    //{
    //    //currentButtonBehaviour = other.gameObject.GetComponent<ButtonBehaviour>();
    //    buttonBehaviour = other.gameObject.GetComponent<ButtonBehaviour>();
    //    if (buttonBehaviour != null/* && !insideButton*/)
    //    {
    //        if (buttonBehaviour.needHover && buttonBehaviour.buttonHovered) return;

    //            insideButton = true;
    //            CurrentButtonBehaviour = buttonBehaviour;              



    //        //if (currentButtonBehaviour.onButtonEnter.GetPersistentEventCount() > 0)
    //        //{
    //        //    currentButtonBehaviour.lastHand = thishand;

    //        //    currentButtonBehaviour.onButtonEnter.Invoke();

    //        //}


    //        //if (currentButtonBehaviour.onButtonPressed.GetPersistentEventCount() > 0) StartGazing();
    //    }


    //}



    //private void OnTriggerExit(Collider other)
    //{
    //    buttonBehaviour = other.gameObject.GetComponent<ButtonBehaviour>();
    //    if(buttonBehaviour != null)
    //    {
    //        if (buttonBehaviour.buttonHovered && buttonBehaviour.lastHand == thishand)
    //        {
    //            if (CurrentButtonBehaviour == buttonBehaviour)
    //            {
    //                insideButton = false;
    //            }
    //            StopAllCoroutines();
    //            timeCounter = 0;
    //            CurrentButtonBehaviour = null;
    //        }

    //    }


    //    //if (currentButtonBehaviour != null)
    //    //{
    //        //if (currentButtonBehaviour.onButtonExit.GetPersistentEventCount() > 0)
    //        //{
    //        //    currentButtonBehaviour.lastHand = thishand;

    //        //    currentButtonBehaviour.onButtonExit.Invoke();

    //        //}
    //    //}
    //    //currentButtonBehaviour = null;
    //}
    #endregion

    private IEnumerator Gazing()
    {
        yield return new WaitForSeconds(1f);
        timeCounter+=09f;
        if(timeCounter < gazeTime)
        {
            StartCoroutine(Gazing());
        }
        else
        {
            timeCounter = 0;
            //currentButtonBehaviour.lastHand = thishand;

            //currentButtonBehaviour.onButtonExit.Invoke();
            currentButtonBehaviour.onButtonPressed.Invoke();
           // currentButtonBehaviour.buttonHovered = false;
           
            //currentButtonBehaviour = null;
        }

    }

    // NEW

    void Update()
    {
        #region Object Setting and Checking


        Collider[] colliders = Physics.OverlapSphere(sphereCastPos.position, castRadius);
        nearObjects = new List<ButtonBehaviour>();



        foreach (var collider in colliders)
        {
            
            intObj = collider.gameObject.GetComponent<ButtonBehaviour>();
            if (intObj != null)
            {
                if (/*intObj.buttonHovered == false && */(intObj.lastHand == Hands.None || intObj.lastHand == thishand))
                {
                    nearObjects.Add(intObj);
                }
            }
        }

        if (nearObjects.Count > 0)
        {

            if (nearObjects.Count == 1)
            {
                CurrentButtonBehaviour = nearObjects[0];
            }
            else
            {
                currentObjectIndex = 0;
                distance = Vector3.Distance(this.transform.position, nearObjects[0].transform.position);
                for (int i = 1; i < nearObjects.Count; i++)
                {
                    newDis = Vector3.Distance(this.transform.position, nearObjects[i].transform.position);
                    if (newDis < distance)
                    {
                        distance = newDis;
                        currentObjectIndex = i;
                    }
                }

                CurrentButtonBehaviour = nearObjects[currentObjectIndex];
            }
        }
        else
        {
            CurrentButtonBehaviour = null;
        }



        #endregion





    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (sphereCastPos == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sphereCastPos.position, castRadius);
        
    }
#endif
}
