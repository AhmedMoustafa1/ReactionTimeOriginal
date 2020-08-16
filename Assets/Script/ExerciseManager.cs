using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum BuildersType
{
    Regular, 
    Small
}

public class ExerciseManager : MonoBehaviour {
    public static ExerciseManager instance;

    public PanelBuilder panelBuilderRegular;
    public PanelBuilder panelBuilderSmall;
    public UnityEvent restart;
     public BuildersType _BuilderType;

    private PanelBuilder currentBuilder;


    private List<GameObject> currentButtonsList;

    private int currentIndex = 0;


    private ExerciseType nextExerciseType = ExerciseType.Timed;
    public float nextExerciseTime = 2;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
       
    }
    public void OnRegularPanelsPressed()
    {
        currentBuilder = panelBuilderRegular ;
        _BuilderType = BuildersType.Regular;
    }



    public void OnSmallPanelsPressed()
    {
        currentBuilder = panelBuilderSmall;
        _BuilderType = BuildersType.Small;

    }

    

    public void StartExcersice()
    {

       
        TargetGeneration.Instance.PlayTargets(currentButtonsList, nextExerciseTime, nextExerciseType);

       
    }

    public void CreatePanel() {
        UI3DManager.instance.SetBuilderSize();
        currentButtonsList = new List<GameObject>(currentBuilder.Build());
        restart.Invoke();
    }

    public void SetExersiceTime(float time)
    {

        Debug.Log("1- SetExersiceTime");
        nextExerciseType = ExerciseType.Timed;

        nextExerciseTime = time;
        CreatePanel();
    }
    public void StartCounterTimedExercise()
    {

        nextExerciseType = ExerciseType.Counter16x6TimeCalculated;
        CreatePanel();
    }

    public void startExerciseTwoAssesment()
    {
        nextExerciseType = ExerciseType.Counter16x6;
        CreatePanel();

    }

    public void StartExerciseTwoTraining()
    {
        nextExerciseType = ExerciseType.Counter16x6x3;
        CreatePanel();

    }

    public void StartExerciseTwoTwoMinuter()
    {
        nextExerciseType = ExerciseType.CounterTwoMinuter;
        nextExerciseTime = 2;
        CreatePanel();

    }

    public void StartExerciseThreeTwoMinuter()
    {
        nextExerciseType = ExerciseType.CounterTwoMinuterRedGreen;
        nextExerciseTime = 2;
        CreatePanel();

    }

    public void StartExerciseThreeAssesment()
    {
        nextExerciseType = ExerciseType.Counter16x8redGreen;
        CreatePanel();


    }
    public void StartExerciseThreeTraining()
    {
        nextExerciseType = ExerciseType.Counter16x8x3redGreen;
        CreatePanel();


    }
    public void StartExerciseFourAssesment()
    {
        nextExerciseType = ExerciseType.Counter16x8Ex4Assessment;
        CreatePanel();


    }
    public void StartExerciseFourTraining()
    {
        nextExerciseType = ExerciseType.Counter16x8Ex4Training;
        CreatePanel();

    }
    public void StartExerciseFourTimed(float time)
    {
        nextExerciseType = ExerciseType.Counter16x6Ex4Timed;
        nextExerciseTime = time;
        CreatePanel();

    }

}
