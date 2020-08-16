using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI3DManager : MonoBehaviour {


    public static UI3DManager instance;
    public GameObject exercisePanel;
    public GameObject exerciseTypePanel1;
    public GameObject exerciseTypePanel2;
    public GameObject exerciseFourTypePanel;
    public GameObject minutesPanel;
    public GameObject buttonSizeTypePanel;
    public GameObject SmallPanel;
    public GameObject RegularPanel;
    public GameObject InGamePanel;

    public Text exerciseText;

    // Use this for initialization
    void Start () {
        instance = this;
	}


    public void SetBuilderSize()
    {
        if (ExerciseManager.instance._BuilderType == BuildersType.Regular)
        {
            SmallPanel.gameObject.SetActive(false);

            RegularPanel.gameObject.SetActive(true);


        }

         if (ExerciseManager.instance._BuilderType == BuildersType.Small)
        {
            RegularPanel.gameObject.SetActive(false);
            SmallPanel.gameObject.SetActive(true);
          //  Debug.Log("Size is sm");

        }
    }

    public void  DesetBuilder()
    {
        RegularPanel.gameObject.SetActive(false);
        SmallPanel.gameObject.SetActive(false);
    }


    public void OnButtonSizeTypePressed()
    {
        buttonSizeTypePanel.gameObject.SetActive(false);
        exercisePanel.gameObject.SetActive(true);
    }

    public void OnExerciseOnePressed()
    {
        exercisePanel.gameObject.SetActive(false);
        minutesPanel.gameObject.SetActive(true);
    }


    public void OnExerciseTwoPressed()
    {
        exercisePanel.gameObject.SetActive(false);
        exerciseTypePanel1.gameObject.SetActive(true);
    }


    public void OnExerciseThreePressed()
    {
        exercisePanel.gameObject.SetActive(false);
        exerciseTypePanel2.gameObject.SetActive(true);
    }
    public void OnExerciseFourPressed()
    {
        exercisePanel.gameObject.SetActive(false);
        exerciseFourTypePanel.gameObject.SetActive(true);
    }

    public void OnTimeButtonsPressed()
    {
        exercisePanel.gameObject.SetActive(false);

        minutesPanel.gameObject.SetActive(false);
        InGamePanel.gameObject.SetActive(true);
    }

    public void OnExerciseTwoTrainingPressed()
    {
        exercisePanel.gameObject.SetActive(false);
        exerciseTypePanel1.gameObject.SetActive(false);
        InGamePanel.gameObject.SetActive(true);


    }

    public void OnExerciseTwoAssesmentPressed()
    {
        exercisePanel.gameObject.SetActive(false);
        exerciseTypePanel1.gameObject.SetActive(false);
        InGamePanel.gameObject.SetActive(true);


    }


    public void OnExerciseThreeAssesmentOrTrainingPressed()
    {
        exercisePanel.gameObject.SetActive(false);
        exerciseTypePanel2.gameObject.SetActive(false);
        InGamePanel.gameObject.SetActive(true);


    }

    //public void OnExerciseThreeAssesmentPressed()
    //{
    //    exercisePanel.gameObject.SetActive(false);
    //    exerciseTypePanel2.gameObject.SetActive(false);
    //    InGamePanel.gameObject.SetActive(true);


    //}


    public void OnExerciseFourAssesmentOrTrainingPressed()
    {
        exercisePanel.gameObject.SetActive(false);
        exerciseFourTypePanel.gameObject.SetActive(false);
        InGamePanel.gameObject.SetActive(true);
    }


    public void OnBackToExercisePanel()
    {
        exercisePanel.gameObject.SetActive(true);
        minutesPanel.gameObject.SetActive(false);
        exerciseTypePanel2.gameObject.SetActive(false);
        exerciseTypePanel1.gameObject.SetActive(false);
        exerciseFourTypePanel.gameObject.SetActive(false);
        InGamePanel.gameObject.SetActive(false);

    }


    public void OnBackToSizePanel()
    {
        exercisePanel.gameObject.SetActive(false);
        buttonSizeTypePanel.gameObject.SetActive(true);
        InGamePanel.gameObject.SetActive(false);
    }


    public void ExitButtonPressed()
    {
        InGamePanel.gameObject.SetActive(false);
        exercisePanel.gameObject.SetActive(true);
    }
}
