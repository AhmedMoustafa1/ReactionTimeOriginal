using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
[System.Serializable]

public class ButtonBehaviour : MonoBehaviour {

    public UnityEvent onButtonEnter;
    public UnityEvent onButtonExit;
    public UnityEvent onButtonPressed;
    [Space(5)]
    public Hands lastHand = Hands.None;
    public bool buttonHovered = false;
    public bool needHover = true;
    private void Start()
    {
        lastHand = Hands.None;
    }
#if UNITY_EDITOR
    private void OnMouseDown()
    {

        onButtonPressed.Invoke();

    }
    private void OnMouseEnter()
    {
#if (UNITY_EDITOR)
        onButtonEnter.Invoke();
#endif 
    }

    private void OnMouseExit()
    {
#if (UNITY_EDITOR)
        onButtonExit.Invoke();
#endif 
    }
#endif
    private void OnDisable()
    {
        buttonHovered = false;
    }
}
