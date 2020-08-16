using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum ButtonType
{
    Green,
    Red
}
public enum ExerciseType
{
    Counter16x6,
    Counter16x6x3,
    Timed,
    Counter16x8redGreen,
    Counter16x8x3redGreen,
    Counter16x6TimeCalculated,
    Counter16x8Ex4Assessment,
    Counter16x8Ex4Training,
    Counter16x6Ex4Timed,
    CounterTwoMinuter,
    CounterTwoMinuterRedGreen
}


public class TargetGeneration : MonoBehaviour
{

    private static TargetGeneration instance;

    public ExerciseType type;
    private List<GameObject> activeButtons;
    private List<GameObject> usedButtons;
    public ButtonBehaviour currentButton;
    [Space(10)]
    public Timer TimerObject;
    [Space(10)]
    private AudioSource audioSource;
    public AudioClip startedAudio;
    public AudioClip EndeddAudio;
    public AudioClip CancelledAudio;
    public AudioClip ExitedAudio;
    public AudioClip PauseAudio;
    public AudioClip ResumedAudio;
    [Space(10)]
    public int hitsCounter = 0;
    public int buttonPressedCounter = 0;
    public int buttonAnticipationCounter = 0;
    public int buttonMissedCounter = 0;
    public int wrongButtonCounter = 0;
    private int maxCounts = 0;
    public float CurrentExerciseTime;
    // public float timeTillPressedCounter = 0;
    public float exerciseTime = 0;
    public float lifetime = 0;
    [Space(10)]
    public bool gameEnded;
    public bool gamePaused = false;
    public bool exerciseEnded = false;
    private bool buttonPressed = false;
    private bool started = false;
    [Space(10)]
    public Text excerciseName;
    public Text hitCount;
    public Text missCount;
    public Text wrongCount;
    public Text anticiapationCount;
    public Text averageReactionText;

    [Space(10)]
    public Text debuger;
    public Text WaitTime;
    [Space(10)]
    public Text UIExcerciseName;
    public Text UIHitCount;
    public Text UIMissCount;
    public Text UIWrongCount;
    public Text UIAnticiapationCount;
    public Text UIAverageReaction;

    [Space(10)]
    public GameObject UIScreenTimer;
    [Space(10)]
    public GameObject[] ExamButtons;

    private float time = 0;

    [Space(10)]
    public int hitOne;
    public int hitTwo;
    public int hitThree;
    [Space(5)]
    public int anticipationOne;
    public int anticipationTwo;
    public int anticipationThree;
    [Space(5)]
    public int errorOne;
    public int errorTwo;
    public int errorThree;
    [Space(5)]
    public int missOne;
    public int missTwo;
    public int missThree;
    [Space(10)]
    private int timeTaken;

    string currentExcerciseName = "";
    string hitOneText;
    string hitTwoText;
    string hitThreeText;
    string missOneText;
    string missTwoText;
    string missThreeText;
    string anticipationOneText;
    string anticipationTwoText;
    string anticipationThreeText;
    string wrongOneText;
    string wrongTwoText;
    string wrongThreeText;
    public List<float> reactionsTimesOfPressed;

    private ButtonType[] types = new ButtonType[] { ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Red, ButtonType.Red ,ButtonType.Red};
    private List<ExerciseButton> buttons;
    private int lastExtra = 0;
    public int dummyReseter = 0;
    private float AverageReaction
    {
        get
        {
           

            float avg = 0;
            if (reactionsTimesOfPressed != null && reactionsTimesOfPressed.Count > 0)
            {
                for (int i = 0; i < reactionsTimesOfPressed.Count; i++)
                {
                    avg += reactionsTimesOfPressed[i];
                }
                avg /= reactionsTimesOfPressed.Count;
            }
            return avg;
        }
      
    }


    private int HitsCounter
    {
        get
        {
            return hitsCounter;
        }

        set
        {
            hitsCounter = value;
            if (hitsCounter == maxCounts + 1)
            {
                if (type != ExerciseType.Timed &&
                    type != ExerciseType.Counter16x6Ex4Timed &&
                    type != ExerciseType.CounterTwoMinuterRedGreen &&
                    type != ExerciseType.CounterTwoMinuter)
                {
                    gameEnded = true;
                    setControlState(true);
                    if (type == ExerciseType.Counter16x6TimeCalculated)
                    {
                        TimerObject.GetComponent<Timer>().StopTimer();
                        timeTaken = TimerObject.GetComponent<Timer>().gameSeconds;
                    }
                    StopAllCoroutines();
                }

            }
        }
    }
    public HeightSetter heightSetter;

    private void Awake()
    {
        instance = this;
        types = new ButtonType[] { ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Red, ButtonType.Red, ButtonType.Red };
        types.Shuffle();
    }
    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        WaitTime.gameObject.SetActive(false);
    }
    public static TargetGeneration Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newGO = new GameObject();
                newGO.name = "TargetGenerator";
                instance = newGO.AddComponent<TargetGeneration>();
            }
            return instance;
        }
    }

   


    public void PlayTargets(List<GameObject> buttonsList, float exerciseTime, ExerciseType type)
    {

        reactionsTimesOfPressed = new List<float>();
        hitOne = 0;
        hitTwo = 0;
        hitThree = 0;

        activeButtons = new List<GameObject>(buttonsList);
        activeButtons.Shuffle();

        usedButtons = new List<GameObject>();
        CurrentExerciseTime = exerciseTime;
        this.exerciseTime = exerciseTime;
        this.type = type;

        ExerciseButton button;
        buttons = new List<ExerciseButton>();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            button = buttonsList[i].GetComponent<ExerciseButton>();
            if (button != null)
            {
                button.SetButtonByTypes();
                buttons.Add(button);
            }
        }
        buttons.Shuffle();

        lastExtra = 0;
        if (type == ExerciseType.Counter16x6Ex4Timed ||
            type == ExerciseType.Counter16x8Ex4Training ||
            type == ExerciseType.Counter16x8Ex4Assessment)
        {
            SetButtonTypesForEx4(buttons);
            SetEX4RedGreensOrder();           
        }

        //uncomment maybe?


       StartCoroutine(StartGameDelay());
    }

    private void SetButtonTypesForEx4(List<ExerciseButton> buttons)
    {


            int counter = 0;
        if (type == ExerciseType.Counter16x8Ex4Assessment ||
            type == ExerciseType.Counter16x8Ex4Training)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (i > 0 && i % 4 == 0)
                {
                    counter+=2;
                }
                buttons[i].types = new ButtonType[] { ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green , ButtonType.Green ,ButtonType.Green };
                buttons[i].types[counter]  = ButtonType.Red;
                buttons[i].types[counter + 1] = ButtonType.Red;
                

            }
        }
        else if(type == ExerciseType.Counter16x6Ex4Timed)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                //Debug.Log("i = " + i + " ... i%3 = " + (i % 3));
                if (i > 0 && i % 3 == 0)
                {
                    counter++;
                    //Debug.Log("Counter ++ = "+counter);
                }
                buttons[i].types = new ButtonType[] { ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green };
                buttons[i].types[counter] = ButtonType.Red;
            }
        }


        if (type == ExerciseType.Counter16x6Ex4Timed)
        {
            if (buttons[lastExtra].types[counter] != ButtonType.Red) buttons[lastExtra].types[counter] = ButtonType.Red;
            else
            {
                buttons[lastExtra].types[0] = ButtonType.Red;
                int nextExtra = lastExtra + 1;
                if (lastExtra + 1 >= buttons.Count) nextExtra = 0;
                buttons[nextExtra].types = new ButtonType[] { ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green };
                buttons[nextExtra].types[counter] = ButtonType.Red;
            }
            lastExtra++;
            if (lastExtra >= buttons.Count) lastExtra = 0;          
        }


    }
    private void SetEX4RedGreensOrder()
    {
        List<ExerciseButton> redButtons = new List<ExerciseButton>();
        List<ExerciseButton> greenButtons = new List<ExerciseButton>();
        ExerciseButton butt;

        if (type == ExerciseType.Counter16x6Ex4Timed ||
            type == ExerciseType.Counter16x8Ex4Assessment ||
            type == ExerciseType.Counter16x8Ex4Training)
        {

            for (int i = 0; i < activeButtons.Count; i++)
            {
                butt = activeButtons[i].gameObject.GetComponent<ExerciseButton>();
                if (butt.types[butt.ex4TimedIndex] == ButtonType.Green) greenButtons.Add(butt);
                else redButtons.Add(butt);
            }
            redButtons.Shuffle();
            greenButtons.Shuffle();
            int counter = 0;
            int indx = 0;
            int randomModifier = 5;

            if (type == ExerciseType.Counter16x8Ex4Assessment ||
            type == ExerciseType.Counter16x8Ex4Training)
            {
                randomModifier = 3;
            }
            for (int i = 0; i < redButtons.Count; i++)
            {
                indx = UnityEngine.Random.Range(0, randomModifier) + counter;
                greenButtons.Insert(Mathf.Min(indx, greenButtons.Count - 1), redButtons[i]);
                counter += 5;
            }
            activeButtons.Clear();

            //string debugString = "";
            for (int i = 0; i < greenButtons.Count; i++)
            {
                activeButtons.Add(greenButtons[i].gameObject);
              //  debugString += greenButtons[i].types[greenButtons[i].ex4TimedIndex] + "\n";
            }
            // Debug.Log(debugString + "\n");
        }
        
        
    }


    private void StartExercise()
    {
        WaitTime.gameObject.SetActive(false);
        ExamButtons = GameObject.FindGameObjectsWithTag("ExamButton");
        buttonMissedCounter = 0;
        buttonPressedCounter = 0;
        buttonAnticipationCounter = 0;
        wrongButtonCounter = 0;
        HitsCounter = 0;
        gamePaused = false;
        gameEnded = false;
        TimerObject.timeScoreCounter = 0;
        SavedData.instance.ClearLists();

        if (activeButtons.Count < 0)
            throw new UnityException("Error Setting First Button active buttons empty");

        exerciseEnded = false;
        started = true;

        #region UI
        if (!audioSource) audioSource = this.GetComponent<AudioSource>();

        TextUpdate.instance.UpdateText();

        hitCount.text = "Hits: 0";
        UIHitCount.text = "Hits: 0";

        UIAnticiapationCount.text = "Anticipation: 0";
        anticiapationCount.text = "Anticipation: 0";

        ResetAverageReactionTime();

        missCount.text = "Miss: 0";
        UIMissCount.text = "Miss: 0";

        wrongCount.text = "Wrong: 0";
        UIWrongCount.text = "Wrong: 0";

        if (this.type != ExerciseType.Timed &&
            this.type != ExerciseType.CounterTwoMinuter &&
            this.type != ExerciseType.CounterTwoMinuterRedGreen)
        {

            wrongCount.gameObject.SetActive(false);
            UIWrongCount.gameObject.SetActive(false);
            missCount.gameObject.SetActive(true);
            UIMissCount.gameObject.SetActive(true);

            if (this.type == ExerciseType.Counter16x6TimeCalculated)
                UIScreenTimer.SetActive(true);
            else UIScreenTimer.SetActive(false);
        }

        // Miss Wrong Red types
        if (this.type == ExerciseType.Counter16x8redGreen ||
            this.type == ExerciseType.Counter16x8x3redGreen ||
            this.type == ExerciseType.Counter16x8Ex4Assessment ||
            this.type == ExerciseType.Counter16x6Ex4Timed ||
            this.type == ExerciseType.Counter16x8Ex4Training ||
            this.type == ExerciseType.CounterTwoMinuterRedGreen)
        {
            wrongCount.gameObject.SetActive(true);
            UIWrongCount.gameObject.SetActive(true);
        }
        else
        {
            wrongCount.gameObject.SetActive(false);
            UIWrongCount.gameObject.SetActive(false);
        }



        #endregion

        for (int i = 0; i < activeButtons.Count; i++)
        {
            activeButtons[i].GetComponent<ExerciseButton>().Flash();
        }


        if (type == ExerciseType.Timed)
        {
            UIScreenTimer.SetActive(true);
            missCount.gameObject.SetActive(false);
            UIMissCount.gameObject.SetActive(false);
            wrongCount.gameObject.SetActive(false);
            UIWrongCount.gameObject.SetActive(false);
            TimerObject.GetComponent<Timer>().StartTimer(3 + (int)CurrentExerciseTime * 60);
        }

        if (type == ExerciseType.Counter16x6TimeCalculated)
        {
            UIScreenTimer.SetActive(true);
            missCount.gameObject.SetActive(false);
            UIMissCount.gameObject.SetActive(false);
            wrongCount.gameObject.SetActive(false);
            UIWrongCount.gameObject.SetActive(false);
            TimerObject.GetComponent<Timer>().StartTimer(-3, TimerType.Increment);
        }

        if (type == ExerciseType.Counter16x6Ex4Timed)
        {
            UIScreenTimer.SetActive(true);
            TimerObject.GetComponent<Timer>().StartTimer(3 + (int)CurrentExerciseTime * 60, TimerType.Decrement);
        }


        if (type == ExerciseType.CounterTwoMinuter || type == ExerciseType.CounterTwoMinuterRedGreen)
        {
            UIScreenTimer.SetActive(true);
            TimerObject.GetComponent<Timer>().StartTimer(3 + (int)CurrentExerciseTime * 60, TimerType.Decrement);
        }

        StartCoroutine(StartAfter(2, PlayNextButton));
        SetEnding();
        excerciseName.text = UIExcerciseName.text = currentExcerciseName;


    }

    public void ResetAverageReactionTime()
    {
        UIAverageReaction.text = "";
        averageReactionText.text = "";
        dummyReseter = 0;
    }

    IEnumerator DelayedPlayedNextButton(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (currentButton != null)
        {
            currentButton.GetComponent<ExerciseButton>().Active = false;

        }
        if (!gamePaused)
        {
            //if (type != ExerciseType.Counter16x6Ex4Timed)
            //{
                GameObject button;
            if (activeButtons.Count > 0)
            {
                button = activeButtons[0];

               // activeButtons.Shuffle(); // TESTING
                //button = activeButtons.GetRandomValue();
            }
            else
            {
                activeButtons.Clear();
                activeButtons = new List<GameObject>(usedButtons);
                activeButtons.Shuffle();
                usedButtons = new List<GameObject>();
                SetEX4RedGreensOrder();

                button = activeButtons[0];

                



                //activeButtons.Shuffle(); // TESTING

                //button = activeButtons.GetRandomValue();
            }
            currentButton = button.GetComponent<ButtonBehaviour>();

                int id = currentButton.gameObject.GetComponent<ExerciseButton>().buttonID;

                SavedData.instance.targetPosition.Add(17 - id);

                currentButton.GetComponent<ExerciseButton>().Active = true;


                usedButtons.Add(button);
                activeButtons.Remove(button);
                buttonPressed = false;

            //}
            //else
            //{
            //    //List<ExerciseButton> allButtons = new List<ExerciseButton>();
            //    //for (int i = 0; i < activeButtons.Count; i++)
            //    //{
            //    //    allButtons.Add(activeButtons[i].GetComponent<ExerciseButton>());
            //    //}
            //    //List<ExerciseButton> redButtons = new List<ExerciseButton>();
            //    //List<ExerciseButton> greenButtons = new List<ExerciseButton>();
            //}
        }
            
            
    }

    public void PlayNextButton()
    {
        HitsCounter++;
        //DEBUG
        //if((HitsCounter-1)%96 == 0) EditorApplication.isPaused = true;


        if (gameEnded)
        {

            // making sure last digit appear on UI
            hitThree = buttonPressedCounter - hitTwo - hitOne;
            anticipationThree = buttonAnticipationCounter - anticipationOne - anticipationTwo;
            errorThree = wrongButtonCounter - errorTwo - errorOne;
            missThree = buttonMissedCounter - missTwo - missOne;
            UpdateUIHitAnticipationAndErrorsCounterText();
            //
            GameEnded();
        }

        if ((hitsCounter == 97 || hitsCounter == 193 ) &&
            (type == ExerciseType.Counter16x6x3 ))
        {
        
            if (hitsCounter == 97 || hitsCounter == 193)
            {
                PauseFor30Sec();
            }
        }
        else 
        if((hitsCounter == 129 || hitsCounter == 257) && 
            (type == ExerciseType.Counter16x8Ex4Training ||
             type == ExerciseType.Counter16x8x3redGreen))
        {
            if (hitsCounter == 129 || hitsCounter == 257)
            {
                PauseFor30Sec();
            }
        }
        else
        {
            if (type == ExerciseType.Counter16x6x3)           
            {
                if (hitsCounter == 98) TimerObject.timeScoreCounter = 65;
                else if (hitsCounter == 194) TimerObject.timeScoreCounter = 125;          
            }
            else
            if(type == ExerciseType.Counter16x8Ex4Training ||
             type == ExerciseType.Counter16x8x3redGreen)
            {
                if (hitsCounter == 130) TimerObject.timeScoreCounter = 65;
                else if (hitsCounter == 258) TimerObject.timeScoreCounter = 125;

                if (hitsCounter > 1 && (hitsCounter - 1) % 128 == 0)
                {
                    SetButtonTypesForEx4(buttons);
                    Debug.Log("RESET Buttons");
                }
            }

            if (type == ExerciseType.Counter16x6Ex4Timed)
            {
                if(hitsCounter > 1 && (hitsCounter-1) % 96 == 0)
                {
                    SetButtonTypesForEx4(buttons);
                    Debug.Log("RESET Buttons");
                }
            }
            
            if(!gameEnded) StartCoroutine(DelayedPlayedNextButton(0));


            //if (type != ExerciseType.Timed)
            //{
            //    StartCoroutine(DelayedPlayedNextButton(0));
            //}
            //else
            //{
            //    StartCoroutine(DelayedPlayedNextButton(0));
            //}
        }


    }

    private void PauseFor30Sec()
    {
        time = 30;
        StartCoroutine(StartAfter(29, Resuming));
        WaitTime.gameObject.SetActive(true);

        // ExerciseButton.instance.StopAllCoroutines();
        currentButton.GetComponent<ExerciseButton>().Active = false;

        gamePaused = true;
        audioSource.clip = PauseAudio;
        audioSource.Stop();
        audioSource.Play();
    }

    public void ButtonPressed(ButtonType type,float elapsedTime)
    {
        if (exerciseEnded) return;
    //    Debug.Log("ElapsedTime :" + elapsedTime);

        buttonPressed = true;

        if (elapsedTime < .1)
        {
            buttonAnticipationCounter += 1;

        }
        else
        if (type == ButtonType.Green)
        {
            buttonPressedCounter += 1;
            reactionsTimesOfPressed.Add(elapsedTime);

        }
        else 
        if (type == ButtonType.Red)
        {
            wrongButtonCounter += 1;
        }

        UpdateUIHitAnticipationAndErrorsCounterText();
    
        //UIAnticiapationCount.text = "Anticipation: " + buttonAnticipationCounter;
        //anticiapationCount.text = "Anticipation: " + buttonAnticipationCounter;
        //hitCount.text = "Hits: " + buttonPressedCounter;
        //UIHitCount.text = "Hits: " + buttonPressedCounter;

        //if (   this.type == ExerciseType.Counter16x6redGreen 
        //    || this.type == ExerciseType.Counter16x6x3redGreen
        //    || this.type == ExerciseType.Counter16x6Ex4Assessment
        //    || this.type == ExerciseType.Counter16x6Ex4Training
        //    || this.type == ExerciseType.Counter16x6Ex4Timed)
        //{
        //    wrongCount.text = "Wrong: " + wrongButtonCounter;
        //    UIWrongCount.text = "Wrong: " + wrongButtonCounter;
        //}



        PlayNextButton();

    }

    public void ButtonNotPressed(ButtonType type)
    {
        //Debug.Log(" Button not pressed " + lifetime);

        SavedData.instance.HandType.Add("none");



        if (type == ButtonType.Green)
        {
            buttonMissedCounter += 1;
        }

        if (type == ButtonType.Red)
        {
            buttonPressedCounter += 1;
        }

        UpdateUIHitAnticipationAndErrorsCounterText();

    }

    private void UpdateUIHitAnticipationAndErrorsCounterText()
    {
        hitOneText = hitOne + "";
        hitTwoText = hitTwo + "";
        hitThreeText = hitThree + "";
        missOneText = missOne + "";
        missTwoText = missTwo + "";
        missThreeText = missThree + "";
        anticipationOneText = anticipationOne + "";
        anticipationTwoText = anticipationTwo + "";
        anticipationThreeText = anticipationThree + "";
        wrongOneText = errorOne + "";
        wrongTwoText = errorTwo + "";
        wrongThreeText = errorThree + "";

        switch (this.type)
        {
            case ExerciseType.Counter16x8Ex4Training:
            case ExerciseType.Counter16x6x3:
            case ExerciseType.Counter16x8x3redGreen:

                break;
            case ExerciseType.Counter16x6TimeCalculated:
            case ExerciseType.Counter16x8Ex4Assessment:
            case ExerciseType.Counter16x8redGreen:
            case ExerciseType.Counter16x6:
                // Only Complete values
                hitTwoText = hitThreeText = missTwoText = missThreeText = anticipationTwoText = anticipationThreeText = wrongTwoText = wrongThreeText = "";

                hitOneText = buttonPressedCounter +"";
                missOneText = buttonMissedCounter + "";
                anticipationOneText = buttonAnticipationCounter + "";
                wrongOneText = wrongButtonCounter + "";

                break;      
                
            case ExerciseType.Counter16x6Ex4Timed:
            case ExerciseType.Timed:
            case ExerciseType.CounterTwoMinuter:
            case ExerciseType.CounterTwoMinuterRedGreen:
                // Depends on game time
                if (exerciseTime == 1) { hitTwoText = hitThreeText = missTwoText = missThreeText = anticipationTwoText = anticipationThreeText = wrongTwoText = wrongThreeText = ""; }
                else if (exerciseTime == 2) {  hitThreeText  = missThreeText  = anticipationThreeText  = wrongThreeText = ""; }
                    break;
            default:
                break;
        }

        hitCount.text = "Hits: " + hitOneText + " " + hitTwoText + " " + hitThreeText;
        UIHitCount.text = "Hits: " + hitOneText + " " + hitTwoText + " " + hitThreeText;

        missCount.text = "Miss: " + missOneText + " " + missTwoText + " " + missThreeText;
        UIMissCount.text = "Miss: " + missOneText + " " + missTwoText + " " + missThreeText;

        anticiapationCount.text = "Anticipation: " + anticipationOneText + " " + anticipationTwoText + " " + anticipationThreeText;
        UIAnticiapationCount.text = "Anticipation: " + anticipationOneText + " " + anticipationTwoText + " " + anticipationThreeText;

        wrongCount.text = "Wrong: " + wrongOneText + " " + wrongTwoText + " " + wrongThreeText;
        UIWrongCount.text = "Wrong: " + wrongOneText + " " + wrongTwoText + " " + wrongThreeText;


        averageReactionText.text = "Average Reaction Time: "+(int)(AverageReaction * 1000);
        UIAverageReaction.text = averageReactionText.text;

        #region saved
        //hitCount.text = "Hits: " + buttonPressedCounter;
        //UIHitCount.text = "Hits: " + buttonPressedCounter;

        //missCount.text = "Miss: " + buttonMissedCounter;
        //UIMissCount.text = "Miss: " + buttonMissedCounter;

        //anticiapationCount.text = "Anticipation: " + buttonAnticipationCounter;
        //UIAnticiapationCount.text = "Anticipation: " + buttonAnticipationCounter;

        //wrongCount.text = "Wrong: " + wrongButtonCounter;
        //UIWrongCount.text = "Wrong: " + wrongButtonCounter;

        //if (this.type == ExerciseType.Counter16x6redGreen
        //    || this.type == ExerciseType.Counter16x6x3redGreen
        //    || this.type == ExerciseType.Counter16x6Ex4Assessment
        //    || this.type == ExerciseType.Counter16x6Ex4Training
        //    || this.type == ExerciseType.Counter16x6Ex4Timed)
        //{
        //    wrongCount.text = "Wrong: " + wrongButtonCounter;
        //    UIWrongCount.text = "Wrong: " + wrongButtonCounter;
        //}
        #endregion
    }

    private void SetEnding()
    {
        switch (type)
        {
            case ExerciseType.Counter16x6:
                currentExcerciseName = "Exercise 2: Assessment";
                maxCounts = 16 * 6;
                break;

            case ExerciseType.Counter16x6x3:
                currentExcerciseName = "Exercise 2: Training";
                maxCounts = 16 * 6 * 3;
                break;

            case ExerciseType.Counter16x8redGreen:
                currentExcerciseName = "Exercise 3: Assessment";
                maxCounts = 16 * 8/*16 * 6*/;
                break;

            case ExerciseType.Counter16x8x3redGreen:
                currentExcerciseName = "Exercise 3: Training";
                maxCounts = 16 * 8 * 3;
                break;

            case ExerciseType.Timed:
                StartCoroutine(ExerciseTimer(exerciseTime));
                currentExcerciseName = "Exercise 1: " + exerciseTime + " Minutes";
                maxCounts = 1;
                break;

            case ExerciseType.Counter16x6TimeCalculated:
                currentExcerciseName = "Exercise 1: Assessment";
                maxCounts = 16 * 6;
                break;

            case ExerciseType.Counter16x6Ex4Timed:
                types.Shuffle();
                currentExcerciseName = "Exercise 4: "+ exerciseTime+" Minutes";
                maxCounts = 16 * 6;
                StartCoroutine(ExerciseTimer(exerciseTime));
                break;

            case ExerciseType.Counter16x8Ex4Assessment:
                currentExcerciseName = "Exercise 4: Assessment";
                maxCounts = 16 * 8 /*16 * 6*/;
                break;

            case ExerciseType.Counter16x8Ex4Training:
                types.Shuffle();
                currentExcerciseName = "Exercise 4: Training";
                maxCounts = 16 * 8 * 3 /*16 * 6* * 3*/;
                break;

            case ExerciseType.CounterTwoMinuter:
                types.Shuffle();
                currentExcerciseName = "Exercise 2: 2 Minutes";
                maxCounts = 1;
                StartCoroutine(ExerciseTimer(exerciseTime));
                break;

            case ExerciseType.CounterTwoMinuterRedGreen:
                types.Shuffle();
                currentExcerciseName = "Exercise 3: 2 Minutes";
                maxCounts = 1;
                StartCoroutine(ExerciseTimer(exerciseTime));
                break;
        }
    }

    public void GameEnded()
    {
        StopAllCoroutines();
        SavedData.instance._savedHitCount = buttonPressedCounter;
        SavedData.instance._savedMissedCount = buttonMissedCounter;
        SavedData.instance._savedHitCounterTotal = buttonPressedCounter + buttonMissedCounter;

        for (int i = 0; i < ExamButtons.Length; i++)
        {
            ExamButtons[i].GetComponent<ExerciseButton>().StopAllCoroutines();
            ExamButtons[i].GetComponent<ExerciseButton>().Active = false;
            ExamButtons[i].GetComponent<ExerciseButton>().Blackout();          
        }


        if (type == ExerciseType.Timed || 
            type == ExerciseType.Counter16x6Ex4Timed || 
            type == ExerciseType.CounterTwoMinuter || 
            type == ExerciseType.CounterTwoMinuterRedGreen)
        {
            SavedData.instance.reactionTime.Add(0);
        }
        else if (type == ExerciseType.Counter16x6TimeCalculated)
        {
            SavedData.instance.timeTaken = timeTaken;
        }

        SavedData.instance.StoreData();
        
        HitsCounter = 0;
        audioSource.clip = EndeddAudio;

        audioSource.Stop();
        audioSource.Play();

        //Debug.Log("ended audio");
        exerciseEnded = true;
        started = false;
        TimerObject.timeScoreCounter = 0;
        //currentButton.GetComponent<MaterialChanger>().SetOffMaterial();
        currentButton.GetComponent<ExerciseButton>().Active = false;
        currentButton.GetComponent<ExerciseButton>().Blackout();


        #region Hi It's my shit again
        StopAllCoroutines();

        //buttonMissedCounter = 0;
        //buttonPressedCounter = 0;
        #endregion
    }

    public void GameExited()
    {
        HitsCounter = 0;

        if (!exerciseEnded)
        {
            audioSource.clip = CancelledAudio;
            audioSource.Stop();
            audioSource.Play();
            setControlState(true);

        }
        DisableUIScrore();

        exerciseEnded = true;
        started = false;
        //currentButton.GetComponent<MaterialChanger>().SetOffMaterial();
        currentButton.GetComponent<ExerciseButton>().Active = false;
        currentButton.GetComponent<ExerciseButton>().Blackout();

        StopAllCoroutines();
        TimerObject.gameObject.GetComponent<Timer>().TimeEnded();


        //buttonMissedCounter = 0;
        //buttonPressedCounter = 0;
    }

    public void DisableUIScrore()
    {
        excerciseName.text = "";
        hitCount.text = "";
        missCount.text = "";
        wrongCount.text = "";
        anticiapationCount.text = "";

        UIExcerciseName.text = "";
        UIHitCount.text = "";
        UIMissCount.text = "";
        UIWrongCount.text = "";
        UIAnticiapationCount.text = "";
        UIAverageReaction.text = "";
        averageReactionText.text = "";
    }


    private IEnumerator StartGameDelay()
    {
        yield return new WaitForSeconds(2);
        StartExercise();
        setControlState(false);

    }

    private IEnumerator ExerciseTimer(float time) // In Minutes
    {
        yield return new WaitForSeconds(time * 60f + 3);
        if ( type == ExerciseType.Timed ||
             type == ExerciseType.Counter16x6Ex4Timed ||
             type == ExerciseType.CounterTwoMinuter ||
             type == ExerciseType.CounterTwoMinuterRedGreen)
        {
            Debug.Log("Actual Timer Ended");
            GameEnded();
        }
    }

    public IEnumerator StartAfter(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        audioSource.clip = startedAudio;
        audioSource.Stop();
        audioSource.Play();
        action();
    }

    public void Resuming()
    {
        gamePaused = false;
        audioSource.clip = ResumedAudio;
        audioSource.Stop();
        audioSource.Play();
        StartCoroutine(DelayedPlayedNextButton(1));
        WaitTime.gameObject.SetActive(false);


    }

    public void QuittingApplication()
    {
        audioSource.clip = ExitedAudio;
        audioSource.Stop();
        audioSource.Play();
        StartCoroutine(TargetGeneration.Instance.StartAfter(5, Exiting));
        WaitTime.gameObject.SetActive(false);
    }

    public void Exiting()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void Update()
    {
        time -= Time.deltaTime;
        WaitTime.text = "00:"+time.ToString("00");
        lifetime += Time.deltaTime;
        debuger.text = lifetime.ToString();
        if (!started) return;
        // TimerBtngaga

        //NEW
        if(TimerObject.timeScoreCounter < 60)
        {
            hitOne = buttonPressedCounter;
            anticipationOne = buttonAnticipationCounter;
            errorOne = wrongButtonCounter;
            missOne = buttonMissedCounter;

            hitTwo = 0;
            anticipationTwo = 0;
            errorTwo = 0;
            missTwo = 0;

            hitThree = 0;
            anticipationThree = 0;
            errorThree = 0;
            missThree = 0;
        }
        else if (TimerObject.timeScoreCounter < 120)
        {

            hitTwo = buttonPressedCounter - hitOne;
            anticipationTwo = buttonAnticipationCounter - anticipationOne;
            errorTwo = wrongButtonCounter - errorOne;
            missTwo = buttonMissedCounter - missOne;

            hitThree = 0;
            anticipationThree = 0;
            errorThree = 0;
            missThree = 0;
        }
        else if (TimerObject.timeScoreCounter < 180)
        {
            hitThree = buttonPressedCounter - hitTwo - hitOne;
            anticipationThree = buttonAnticipationCounter - anticipationOne - anticipationTwo;
            errorThree = wrongButtonCounter - errorTwo - errorOne;
            missThree = buttonMissedCounter - missTwo - missOne;
        }

        // END NEW
        if (TimerObject.timeScoreCounter == 60)
        {
            hitOne = buttonPressedCounter;
            anticipationOne = buttonAnticipationCounter;
            errorOne = wrongButtonCounter;
            missOne = buttonMissedCounter;

        }

        if (TimerObject.timeScoreCounter == 120)
        {
            hitTwo = buttonPressedCounter - hitOne;
            anticipationTwo = buttonAnticipationCounter - anticipationOne;
            errorTwo = wrongButtonCounter - errorOne;
            missTwo = buttonMissedCounter - missOne;


        }

        if (TimerObject.timeScoreCounter == 180)
        {
            hitThree = buttonPressedCounter - hitTwo - hitOne;
            anticipationThree = buttonAnticipationCounter - anticipationOne - anticipationTwo;
            errorThree = wrongButtonCounter - errorTwo - errorOne ;
            missThree = buttonMissedCounter - missTwo - missOne;
        }
        // NEW
        if( TimerObject.timeScoreCounter != 60 &&
            TimerObject.timeScoreCounter != 120 )
            //&&
            //TimerObject.timeScoreCounter != 180)
        UpdateUIHitAnticipationAndErrorsCounterText();
    }

    public void setControlState(bool active)
    {
    //  locationSetter.enabled = active;
        heightSetter.enabled = active;
    }
}
