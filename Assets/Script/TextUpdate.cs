using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour {
    public static TextUpdate instance;
    public Text HitScore;
    public Text MissScore;
    public Text ExerciseNumber;


    private ExerciseType type;
    // Use this for initialization
    void Start () {
        instance = this;
     
    }
	


    public void UpdateText()
    {
        type = TargetGeneration.Instance.type;
        StartCoroutine(StartAfter(3, SetText));

    }


    public   void ResetText()
    {
        ExerciseNumber.text = "";

    }


    void SetText()
    {
        switch (type)
        {

            case ExerciseType.Timed:
            case ExerciseType.Counter16x6TimeCalculated:
                ExerciseNumber.text = "Exercise 1";
                break;

            case ExerciseType.Counter16x6:
            case ExerciseType.Counter16x6x3:
                ExerciseNumber.text = "Exercise 2";
                break;
      
            case ExerciseType.Counter16x8redGreen:
            case ExerciseType.Counter16x8x3redGreen:
                ExerciseNumber.text = "Exercise 3";
                break;

            case ExerciseType.Counter16x8Ex4Assessment:
            case ExerciseType.Counter16x6Ex4Timed:
            case ExerciseType.Counter16x8Ex4Training:
                ExerciseNumber.text = "Exercise 4";
                break;

            default:
                break;
        }
    }



    IEnumerator StartAfter(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
       
        action();

    }
}
