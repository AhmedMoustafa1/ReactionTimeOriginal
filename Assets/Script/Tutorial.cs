using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

    public Material AssesmentOutlined;
    public Material TrainingOutlined;
    public Material Exercise1Outlined;
    public Material Exercise2Outlined;
    public Material Exercise3Outlined;
    public Material SmallButtonOutlined;
    public Material ReguralButtonOutlined;
    








    public string welcomeText;
    public string paneltype;
    public string ReguralSizePanel;
    public string SelectType;
    public string SmallSizePanel;
    public string TypesExercises;
    public string FirstType;
    public string FirstTypeTime;

    
    public string SecondType;
    public string SecondTypePage;

    public string SecondTypeAssesmetnt;
    public string SecondTypeTraining;

    public string ThirdType;
    public string Exit;


    public Text DisplayTut;
    public Text buttonDisplayTut;
    public ButtonIden ButtonChecker;
    // Use this for initialization

    public AudioClip clip;
       
    public bool canIdentifyButtons = false;

    private bool smallTypeChecked= false;
    private bool RegrualTypeChecked =false;

    public GameObject smallTypeButton;
    public GameObject regularTypeButton;

    private AudioSource _audioSource;


    public GameObject exercise1button;
    public GameObject exercise2button;
    public GameObject exercise3button;

    public UI3DManager manager;

    bool exerciseOneChecked = false;

    bool exerciseTwoChecked = false;

    bool exerciseThreeChecked = false;

    bool AssesmetChecked = false;
    bool TrainingChecked = false;

    public GameObject BackButton;
    public GameObject BackButtonEbnA7ba;


    void Start () {
        BackButton.SetActive(false);

        #region text

        welcomeText = "Welcome, This system was developed to measure alertness and reaction time where you should push the 'right' green buttons and miss the wrong 'red' buttons with your hands.";
     paneltype = "At this stage you decide the panel size";
     ReguralSizePanel = "Regular Panel size is 120 centimeter width, and 120 centimeter height.";
     SelectType = "Now please touch one of the buttons for two seconds to select the type";
     SmallSizePanel = "Small Panel size is 85 centimeter width, and 85 centimeter height.";
     TypesExercises = "There are three type of exercises";
     FirstType = "First exercise  contains  unlimited amount of targets within a time frame, one, two or three minutes, you need to hit as many targets as possible";
     FirstTypeTime = "You get to this page from pressing on exercise 1, and you get to decided the duration of the exercise";
        SecondTypePage = "You get to this page from either pressing on exercise two or three";
     SecondType = "Second exercise contains assessment type and training, and you will be shown 96 targets of green buttons and you must press them, if you miss any it will be saved as 'a miss'";

    SecondTypeAssesmetnt = "The assesment system for both first and second exercise contains of 96 target, it will determine your response time and your alertness";
     SecondTypeTraining = "The training contains of three sets of 96 targets with 30 second pause in between, this will help you train for the assessment to achieve better score.";

     ThirdType = "The third exercise is similar to the second yet here we introduce red buttons, you shouldn't push the red buttons, if you push any red button it will be saved as 'a wrong'";
     Exit = "Thank you, we hope this introduction was helpful for you, now press on this back button to proceed";

        #endregion


       
        _audioSource = this.gameObject.GetComponent<AudioSource>();
        _audioSource.clip = clip;
        DisplayTut.text = welcomeText;
        buttonDisplayTut.text = "";
        smallTypeButton.SetActive(false);
        regularTypeButton.SetActive(false);
        
        StartCoroutine(StartAfter(10, displayPanelType));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public IEnumerator StartAfter(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
     
        action();

    }
    public GameObject selectedGameObject;
    public Material selectedMaterial;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ButtonIdentifier>()&&canIdentifyButtons)
        {
            selectedGameObject = other.gameObject;
            selectedMaterial = other.gameObject.GetComponent<Renderer>().material;
            _audioSource.Stop();
            _audioSource.Play();
            ButtonChecker = other.gameObject.GetComponent<ButtonIdentifier>().thisbutton;
            Debug.Log(ButtonChecker);
            switch (ButtonChecker)
            {
                case ButtonIden.smallPanel:
                    {
                        buttonDisplayTut.text = SmallSizePanel;
                        smallTypeChecked = true;
                        selectedGameObject.gameObject.GetComponent<Renderer>().material = SmallButtonOutlined;
                        if (RegrualTypeChecked)
                        {
                            StartCoroutine(StartAfter(6, SelectTypeText));

                        }
                        break;
                    }
                        
                 case ButtonIden.regularPanel:
                    {
                        buttonDisplayTut.text = ReguralSizePanel;
                        RegrualTypeChecked = true;
                        selectedGameObject.gameObject.GetComponent<Renderer>().material = ReguralButtonOutlined;

                        //if (smallTypeChecked)
                        //{
                        //    StartCoroutine(StartAfter(6, SelectTypeText));

                        //}
                        break;

                    }
                case ButtonIden.exercise1:
                    DisplayTut.text = FirstType;
                    exerciseOneChecked = true;
                    selectedGameObject.gameObject.GetComponent<Renderer>().material = Exercise1Outlined;

                    if (exerciseOneChecked&& exerciseTwoChecked && exerciseThreeChecked)
                    {
                        StartCoroutine(StartAfter(6, exerciseOneShow));
                    }
                    break;

                case ButtonIden.exercise2:
                    selectedGameObject.gameObject.GetComponent<Renderer>().material = Exercise2Outlined;

                    DisplayTut.text = SecondType;
                    exerciseTwoChecked = true;
                    if (exerciseOneChecked && exerciseTwoChecked && exerciseThreeChecked)
                    {
                        StartCoroutine(StartAfter(6, exerciseOneShow));
                    }
                    break;

                case ButtonIden.exercise3:
                    {
                        selectedGameObject.gameObject.GetComponent<Renderer>().material = Exercise3Outlined;

                        DisplayTut.text = ThirdType;
                        exerciseThreeChecked = true;
                        if (exerciseOneChecked && exerciseTwoChecked && exerciseThreeChecked)
                        {
                            StartCoroutine(StartAfter(6, exerciseOneShow));
                        }
                        break;

                    }
                case ButtonIden.Assesment:
                    {
                        selectedGameObject.gameObject.GetComponent<Renderer>().material = AssesmentOutlined;

                        DisplayTut.text = SecondTypeAssesmetnt;
                        AssesmetChecked = true;
                        if (TrainingChecked)
                        {
                            StartCoroutine(StartAfter(5, backToMain));
                        }
                        break;
                    }
                case ButtonIden.training:
                    {
                        selectedGameObject.gameObject.GetComponent<Renderer>().material = TrainingOutlined;

                        DisplayTut.text = SecondTypeTraining;

                        TrainingChecked = true;
                        if (AssesmetChecked)
                        {
                            StartCoroutine(StartAfter(5, backToMain));
                        }
                    }
                    break;
                case ButtonIden.back:
                    break;
                default:
                    break;
            }


        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == selectedGameObject)
        {
            selectedGameObject.gameObject.GetComponent<Renderer>().material = selectedMaterial;
        }
    }

    private void exerciseOneShow()
    {
        manager.OnExerciseOnePressed();
        DisplayTut.text = FirstTypeTime;
        StartCoroutine(StartAfter(8, exerciseTwoShow));
    }

    private void exerciseTwoShow()
    {
        canIdentifyButtons = false;

        manager.OnBackToExercisePanel();

        exercise1button.gameObject.SetActive(false);
        exercise2button.gameObject.SetActive(false);
        exercise3button.gameObject.SetActive(false);
        manager.OnExerciseTwoPressed();
        DisplayTut.text = SecondTypePage;
        StartCoroutine(StartAfter(3, enableCanIdentify));

    }
    void enableCanIdentify()
    {
        canIdentifyButtons = true;

    }
    private void backToMain()
    {
        manager.OnBackToExercisePanel();

        exercise1button.gameObject.SetActive(false);
        exercise2button.gameObject.SetActive(false);
        exercise3button.gameObject.SetActive(false);
        BackButtonEbnA7ba.SetActive(false);
        DisplayTut.text = Exit;
        BackButton.SetActive(true);

    }
  
    void displayPanelType()
    {
        smallTypeButton.SetActive(true);
        regularTypeButton.SetActive(true);
        smallTypeButton.GetComponent<ButtonBehaviour>().enabled = false;
        regularTypeButton.GetComponent<ButtonBehaviour>().enabled = false;
        canIdentifyButtons = true;
        DisplayTut.text = paneltype;
    }

    void SelectTypeText()
    {
        StopAllCoroutines();

        smallTypeButton.GetComponent<ButtonBehaviour>().enabled = true;
        regularTypeButton.GetComponent<ButtonBehaviour>().enabled = true;
        DisplayTut.text = SelectType;
    }


    public void ExerciseTypes()
    {
        StopAllCoroutines();
        canIdentifyButtons = false;
        DisplayTut.text = TypesExercises;
        buttonDisplayTut.text = " ";
        canIdentifyButtons = true;
        exercise1button.GetComponent<ButtonBehaviour>().enabled = false;
        exercise2button.GetComponent<ButtonBehaviour>().enabled = false;
        exercise3button.GetComponent<ButtonBehaviour>().enabled = false;

        // manager.OnExerciseOnePressed();
        //manager.OnExerciseTwoPressed();
        //manager.OnExerciseThreePressed();

    }


    public void loadVRscene()
    {
        SceneManager.LoadSceneAsync("VRScene");
    }
}
