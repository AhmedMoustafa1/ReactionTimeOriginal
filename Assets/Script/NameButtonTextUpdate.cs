using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameButtonTextUpdate : MonoBehaviour {

    private InputField targetText;
    public GameObject CurrentButtonText;


    private void Start()
    {
        targetText = GameObject.FindGameObjectWithTag("TargetText").GetComponent<InputField>();
    }

    public void UpdateText()
    {
        Debug.Log(targetText);
        Debug.Log(CurrentButtonText.GetComponent<Text>().text);
        targetText.text = CurrentButtonText.GetComponent<Text>().text;
    }
}
