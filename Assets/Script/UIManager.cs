using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {
    public GameObject LoginPanel;
    public GameObject RegisterationPanel;
    public GameObject WelcomePanel;
    public GameObject _WarningMessage;

    public GameObject tutorial;
    // Use this for initialization
    void Start () {
        BackButton();
    }
	
	// Update is called once per frame
	void Update () {
		
	}



    public void loginButton()
    {
        WelcomePanel.gameObject.SetActive(false);
        LoginPanel.gameObject.SetActive(true);
    }


    public void RegsiterButton()
    {
        WelcomePanel.gameObject.SetActive(false);
        RegisterationPanel.gameObject.SetActive(true);

    }

    public void BackButton()
    {
        WelcomePanel.gameObject.SetActive(true);

        LoginPanel.gameObject.SetActive(false);
        RegisterationPanel.gameObject.SetActive(false);
        _WarningMessage.gameObject.GetComponent<Text>().text = "";//"*";

    }
    public void Quit()
    {
        Application.Quit();

    }

}
