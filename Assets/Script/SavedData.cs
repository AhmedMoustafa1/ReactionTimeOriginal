using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using Kandooz;
using System.Diagnostics.Eventing.Reader;

public class SavedData : MonoBehaviour {
    public static SavedData instance;
    public string Experiment;
    public string FullName;
    public string FirstName;
    public string LastName;
    public string Gender;
    public string HandColor;
    public string PanelSize;
    public string Age;
    public string CurrentTime;
    public string CurrentDate;
    public string TotalButtonsPressed;
    public string exerciseTime;
    public List<int> targetPosition;
    public List<float> delay;
    public List<float> reactionTime;
    public List<string> ButtonType;
    public List<string> HandType;


    public float hitRate;
    public float ommissionRate;
    public float minReactionTime;
    public float maxReactionTime;
    public float averageReactionTime;
    public float _savedHitCount;
    public float _savedMissedCount;
    public float _savedHitCounterTotal;
    public int timeTaken;

    public List<float> sortedReactionTime;

    public UnityEvent exEnd;
    public IntField factor;
    public BoolField isOneBlock;
    public int MaxCounts
    {
        get
        {

            switch (TargetGeneration.Instance.type)
            {
                case ExerciseType.Counter16x6:
                    return 16 * 6;
                case ExerciseType.Counter16x6x3:
                    return 16 * 6 * 3;
                case ExerciseType.Timed:
                    return 16 * 6;
                case ExerciseType.Counter16x8redGreen:
                    return 16 * 8;
                case ExerciseType.Counter16x8x3redGreen:
                    return 16 * 8 * 3;
                case ExerciseType.Counter16x6TimeCalculated:
                    return 16 * 6;
                case ExerciseType.Counter16x8Ex4Assessment:
                    return 16 * 8;
                case ExerciseType.Counter16x8Ex4Training:
                    return 16 * 8 * 3;
                case ExerciseType.Counter16x6Ex4Timed:
                    return 16 * 6;
                case ExerciseType.CounterTwoMinuterRedGreen:
                    return (int)_savedHitCounterTotal;
                default:
                    return 16 * 6;
            }
            
        }
    }



    // Use this for initialization
    void Start () {
        instance = this;
        SetUserNameAndSurname();

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetTimeAndDate();

          

            Debug.Log(CurrentTime);
            //Debug.Log("Name: "+ LastName + " Gender: " + Gender + " Age: "+ Age);
          //  StoreData();
        }
        {

        }	
	}


    public string SetUpExerciseTypeForName()
    {
        string text = "";
        if (TargetGeneration.Instance.type == ExerciseType.Timed ||
            TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed ||
            TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter ||
            TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuterRedGreen)
        {
            if (exerciseTime == 1.ToString())
            {
                text = "1min";
            }
            if (exerciseTime == 2.ToString())
            {
                text = "2min";

            }
            if (exerciseTime == 3.ToString())
            {
                text = "3min";

            }
        }
        if (TargetGeneration.Instance.type == ExerciseType.Counter16x6x3 || 
            TargetGeneration.Instance.type == ExerciseType.Counter16x8x3redGreen ||
            TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training)
        {
            text = "Training";
        }
        if (TargetGeneration.Instance.type == ExerciseType.Counter16x6 ||
            TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen || 
            TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated ||
            TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment)
        {
            if (isOneBlock.Value)
            {
                text = "1 Block";

            }
            else
            {
                text = "Assesment";
            }

        }


        return text;
    }


    void SetPanelSize()
    {
        if (ExerciseManager.instance._BuilderType == BuildersType.Small) 
        {
            PanelSize = "Small";
        }
        if (ExerciseManager.instance._BuilderType == BuildersType.Regular)
        {
            PanelSize = "Regular";
        }
    }
    void SetUserNameAndSurname()
    {
        FullName = UserManagerInput.instance._thisUser.lastName + UserManagerInput.instance._thisUser.firstName;
        FirstName = UserManagerInput.instance._thisUser.firstName;
        LastName = UserManagerInput.instance._thisUser.lastName;
        //FirstName  = FullName.Substring(0, FullName.IndexOf(" "));
        //LastName = FullName.Substring(FullName.IndexOf(" ") + 1);
    }
    void SetUserAgeAndGender()
    {
        if (UserManagerInput.instance._thisUser.userGender==GenderType.Male)
        {
            Gender = "Male";
        }
        else if (UserManagerInput.instance._thisUser.userGender == GenderType.Female)
        {
            Gender = "Female";
        }

        Age = UserManagerInput.instance._thisUser.userAge.ToString();
    }

    void SetUserHandColor()
    {
        if (UserManagerInput.instance._thisUser.userHandColor == global::HandColor.White)
        {
            HandColor = "White";
        }
        else if (UserManagerInput.instance._thisUser.userHandColor == global::HandColor.Black)
        {
            HandColor = "Black";
        }
        else if (UserManagerInput.instance._thisUser.userHandColor == global::HandColor.Yellow)
        {
            HandColor = "Yellow";
        }
    }

    void SetTimeAndDate()
    {
        CurrentTime = System.DateTime.Now.ToShortTimeString();
        CurrentDate = System.DateTime.Now.ToShortDateString();
    }
    void SetExerciseTypeAndTime()
    {
        if (TargetGeneration.Instance.type == ExerciseType.Timed || TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated)
        {
            Experiment = "Ex1";
        }
        else if (TargetGeneration.Instance.type == ExerciseType.Counter16x6|| TargetGeneration.Instance.type == ExerciseType.Counter16x6x3 || TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter)
        {
            Experiment = "Ex2";
        }
        else if (TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen || TargetGeneration.Instance.type == ExerciseType.Counter16x8x3redGreen || TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuterRedGreen)
        {
            Experiment = "Ex3";
        }
        else if (TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment|| TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed|| TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training)
        {
            Experiment = "Ex4";

        }
        exerciseTime = ExerciseManager.instance.nextExerciseTime.ToString();
    }
    void SetHitCount()
    {

        TotalButtonsPressed = (TargetGeneration.Instance.hitsCounter-1).ToString();
    }

    void UpdateData()
    {
        SetPanelSize();
        SetUserNameAndSurname();
        SetUserAgeAndGender();
        SetUserHandColor();
        SetTimeAndDate();
        SetExerciseTypeAndTime();
        SetHitCount();
    
    }

    string TimeOrModality()
    {
        string value;
        if (TargetGeneration.Instance.type == ExerciseType.Timed || 
            TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed ||
            TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter || 
            TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuterRedGreen )
        {
            value = "Minutes: " + exerciseTime;
        }
        else if (TargetGeneration.Instance.type == ExerciseType.Counter16x6 ||
                 TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen ||
                 TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment ||
                 TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated)
        {
            if (isOneBlock.Value)
            {
                value = "Modality:  1 Block";

            }
            else
            {
                value = "Modality:  Assessment";

            }
        }
        else
        {
            value = "Modality:  Training";
        }
        return value;
    }

    public void ClearLists()
    {
        targetPosition.Clear();
        delay.Clear();
        reactionTime.Clear();
        ButtonType.Clear();
        HandType.Clear();
    }


    public void StoreData() {

        //Debug.Log("### TIME TAKEN: " + timeTaken);
        UpdateData();
        string data="text";
        data = "Experiment: " + Experiment + System.Environment.NewLine +
                TimeOrModality()+ System.Environment.NewLine +
                "PanelSize: " + PanelSize + System.Environment.NewLine +
                "Date: " + CurrentDate + System.Environment.NewLine +
                "Time: " + CurrentTime + System.Environment.NewLine +
                "First: " + FirstName + System.Environment.NewLine +
                "Surname: " + LastName + System.Environment.NewLine +
                "Age: " + Age + System.Environment.NewLine +
                //"HandColor: " + HandColor + System.Environment.NewLine+
                "Sex: " + Gender + System.Environment.NewLine + " " +  System.Environment.NewLine +
                TotalButtonPressed();


        if (TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated)
        {
            data += "Time Taken: " + timeTaken.ToString() + System.Environment.NewLine+ System.Environment.NewLine;
        }
        string finalAdditionalData = "";
        string additionalData = "TargetNumber;TargetPosition;SOADuration;ReactionTime; Button type" +  System.Environment.NewLine;
  


        if( TargetGeneration.Instance.type == ExerciseType.Counter16x6||
            TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen ||
            TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment /*||
            TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated*/) {
            string buttonType = "";
            for (int i = 0; i < MaxCounts; i++)
            {
              //  buttonType = (TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated) ? "" : ButtonType[i].ToString() + ";";

                additionalData = (i + 1).ToString() + ";" +
                    targetPosition[i].ToString() + ";" +
                    (delay[i] * 1000).ToString() + ";" +
                     (((int)(1000 * reactionTime[i]))).ToString().Replace(",", ".") /*.ToString("N2")*/ + ";" +
                       ButtonType[i].ToString() + ";"+
                       buttonType +
                     HandType[i].ToString() +
                    System.Environment.NewLine;
                finalAdditionalData += additionalData;
            }
        }

        if (TargetGeneration.Instance.type == ExerciseType.Counter16x6x3 ||
            TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training ||
            TargetGeneration.Instance.type == ExerciseType.Counter16x8x3redGreen )
        {
            for (int i = 0; i < MaxCounts; i++)
            {

                additionalData = (i + 1).ToString() + ";" +
                    targetPosition[i].ToString() + ";" +
                    (delay[i] * 1000).ToString() + ";" +
                    (((int)(1000 * reactionTime[i]))).ToString().Replace(",", ".") + ";" +
                    ButtonType[i].ToString() + ";" +
                    HandType[i].ToString() +
                    System.Environment.NewLine;
                finalAdditionalData += additionalData;
            }
        }

        if (TargetGeneration.Instance.type == ExerciseType.Timed ||
            TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed ||
            TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter || 
            TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuterRedGreen ||
            TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated)
        {
            string buttonType = "";
            string soaDelay = "";
            for (int i = 0; i < reactionTime.Count-factor.Value; i++)
            {
                buttonType = (TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated) ? "" : ButtonType[i].ToString() + ";";

                buttonType = (TargetGeneration.Instance.type == ExerciseType.Timed || TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter) ? "" : ButtonType[i].ToString() + ";";
                soaDelay = (TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter || TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuterRedGreen) ? (delay[i] * 1000).ToString() + ";": "";

                additionalData =
                    (i + 1).ToString() + ";" +
                    targetPosition[i].ToString() + ";" +
                    ((int)(1000 * reactionTime[i])).ToString().Replace(",", ".") + ";" +
                    soaDelay +
                    buttonType +
                    HandType[i].ToString() +
                    System.Environment.NewLine;
                    finalAdditionalData += additionalData;
            }
        }



        data += finalAdditionalData;
        

        
        
            sortedReactionTime = new List<float>();

            for (int i = 0; i < reactionTime.Count; i++)
            {
                sortedReactionTime.Add(reactionTime[i]);
            }

            sortedReactionTime.Sort();

            for (int i = sortedReactionTime.Count - 1; i >= 0; i--)
            {
                if (sortedReactionTime[i] == 0)
                {
                    sortedReactionTime.RemoveAt(i);
                }
            }

        if (sortedReactionTime.Count > 0)
        {
            // OLD
            //if  (TargetGeneration.Instance.type == ExerciseType.Counter16x6 ||
            //     TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment ||
            //     TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen)
            //{
            //    hitRate = _savedHitCount / MaxCounts;
            //    ommissionRate = _savedMissedCount / MaxCounts;

            //}

            //if (TargetGeneration.Instance.type == ExerciseType.Counter16x6x3 ||
            //    TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training ||
            //    TargetGeneration.Instance.type == ExerciseType.Counter16x8x3redGreen)

            //{
            //    hitRate = _savedHitCount / MaxCounts;
            //    ommissionRate = _savedMissedCount / MaxCounts;
            //}
            // END OLD

            // NEW
            hitRate = _savedHitCount / MaxCounts;
            ommissionRate = _savedMissedCount / MaxCounts;
            // END NEW
            minReactionTime = sortedReactionTime[0];
            maxReactionTime = sortedReactionTime[sortedReactionTime.Count - 1];
            averageReactionTime = sortedReactionTime.Average();

            string StatsData = "";

            string hitRateOmissionRate = "Hit Rate: " + 100 * hitRate + " % " + System.Environment.NewLine +
                    "Ommission Rate: " + 100 * ommissionRate + " % " + System.Environment.NewLine;

            if (TargetGeneration.Instance.type != ExerciseType.Timed &&
               TargetGeneration.Instance.type != ExerciseType.Counter16x6TimeCalculated &&
               TargetGeneration.Instance.type != ExerciseType.CounterTwoMinuter)
            {
                StatsData = hitRateOmissionRate;
            }

            StatsData += "Minimum Reaction Time: " + 1000 * minReactionTime + " ms " + System.Environment.NewLine +
                         "Maxmum Reaction Time: " + 1000 * maxReactionTime + " ms " + System.Environment.NewLine +
                         "Average Reaction Time: " + 1000 * averageReactionTime + " ms " + System.Environment.NewLine;



            data += System.Environment.NewLine + StatsData;

        }
        else
        {
            data += System.Environment.NewLine + "No Reactions Recorded";
        }

        // Hand Color
        data += System.Environment.NewLine +  "HandColor: " + HandColor + System.Environment.NewLine;

        CurrentDate = CurrentDate.Replace("/", "-");
        CurrentTime = CurrentTime.Replace("/", "-");
        CurrentTime = CurrentTime.Replace(":", "-");
        CurrentTime = CurrentTime.Replace(" ", "-");

        string fileName = "VR-RT-" + Experiment +"-"+ SetUpExerciseTypeForName() + "-"+ PanelSize + "-" + LastName + "-" + FirstName + "-" + CurrentDate    +"-" + CurrentTime;
       
        
        Debug.Log(fileName);
        Debug.Log(UserAccountManager.intance.directory);
        System.IO.File.WriteAllText(UserAccountManager.intance.directory + "\\"   + fileName+".txt", data);
        TotalButtonsPressed = 0.ToString();
        Debug.Log("Why the fuck i'm called now");
        exEnd.Invoke();

    }


    string HitsPerMinute(int mins)
    {
        string errorsOne = ", error " + TargetGeneration.Instance.errorOne;
        string errorsTwo = ", error " + TargetGeneration.Instance.errorTwo;
        string errorsThree = ", error " + TargetGeneration.Instance.errorThree;

        if (TargetGeneration.Instance.type == ExerciseType.Timed ||
            TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter) errorsOne = errorsTwo = errorsThree = "";

        switch (mins)
        {
            case 1:
                int hits = TargetGeneration.Instance.hitOne ;

                return "1 minute: hits " + hits + ", anticipation " + TargetGeneration.Instance.anticipationOne + errorsOne;
                
            case 2:

                return "1 minute: hits " + TargetGeneration.Instance.hitOne + ", anticipation " + TargetGeneration.Instance.anticipationOne + errorsOne /*", error " + TargetGeneration.Instance.errorOne*/  + System.Environment.NewLine+
                        "2 minute: hits " + TargetGeneration.Instance.hitTwo + ", anticipation " + TargetGeneration.Instance.anticipationTwo  + errorsTwo /*", error " + TargetGeneration.Instance.errorTwo*/ ;


            case 3:

                return "1 minute: hits " + TargetGeneration.Instance.hitOne + ", anticipation " + TargetGeneration.Instance.anticipationOne +errorsOne /*", error " + TargetGeneration.Instance.errorOne*/ + System.Environment.NewLine+
                        "2 minute: hits " + TargetGeneration.Instance.hitTwo + ", anticipation " + TargetGeneration.Instance.anticipationTwo +errorsTwo /*", error " + TargetGeneration.Instance.errorTwo*/+ System.Environment.NewLine+
                         "3 minute: hits " + TargetGeneration.Instance.hitThree + ", anticipation " + TargetGeneration.Instance.anticipationThree +errorsThree/* ", error " + TargetGeneration.Instance.errorThree*/;

            default:
                break;
        }
        return null;
    }

    string TotalButtonPressed()
    {
        string returned;
        if (TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed ||
            TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuterRedGreen)
        {
            returned = System.Environment.NewLine + "Total buttons pressed: " +
                (TargetGeneration.Instance.buttonPressedCounter + TargetGeneration.Instance.buttonAnticipationCounter + TargetGeneration.Instance.wrongButtonCounter).ToString() +
                System.Environment.NewLine + "Omission: " + TargetGeneration.Instance.buttonMissedCounter.ToString() +
                System.Environment.NewLine + "Anticipation: " + TargetGeneration.Instance.buttonAnticipationCounter.ToString() +
                System.Environment.NewLine + "Errors: " + TargetGeneration.Instance.wrongButtonCounter.ToString() +
                System.Environment.NewLine + HitsPerMinute((int)ExerciseManager.instance.nextExerciseTime) + System.Environment.NewLine + System.Environment.NewLine;


        }
        else if (TargetGeneration.Instance.type == ExerciseType.Timed ||
                 TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated ||
                 TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter) 
        {
            returned = System.Environment.NewLine + "Total buttons pressed: " +
                 (TargetGeneration.Instance.buttonPressedCounter + TargetGeneration.Instance.buttonAnticipationCounter + TargetGeneration.Instance.wrongButtonCounter).ToString() +
                System.Environment.NewLine + "Anticipation: " + TargetGeneration.Instance.buttonAnticipationCounter.ToString();

            if (TargetGeneration.Instance.type != ExerciseType.Counter16x6TimeCalculated)
            {
                returned +=  System.Environment.NewLine + HitsPerMinute((int)ExerciseManager.instance.nextExerciseTime);
            }
            returned += System.Environment.NewLine + System.Environment.NewLine;
        }
        else 
        {
            returned = "Hits: " + TargetGeneration.Instance.buttonPressedCounter.ToString() +
                System.Environment.NewLine + "Omission: " + TargetGeneration.Instance.buttonMissedCounter.ToString() +
                System.Environment.NewLine + "Anticipation: " + TargetGeneration.Instance.buttonAnticipationCounter.ToString();

            if (TargetGeneration.Instance.type == ExerciseType.Counter16x8x3redGreen ||
                TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen ||
                TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment ||
                TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training)
            {
                returned += System.Environment.NewLine + "Errors: " + TargetGeneration.Instance.wrongButtonCounter.ToString();
            }

            returned += System.Environment.NewLine + System.Environment.NewLine;
        }

        return returned;
    }
}

