using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExerciseButton : MonoBehaviour

{

    //public int reds = 0;
    //public int greens = 0;

    public int buttonID;
    public Material flash;

    private bool active = false;

    private MaterialChanger materialChanger;
    private Material red;
    private Material green;

    [SerializeField]
    private ButtonType type;

    private float[] delayValues = { 0.25f, 0.25f, 0.5f, 0.5f, 1f, 1f };
   
    //[HideInInspector]
    public ButtonType[] types;

    //private int delayCounter = 0;
    public float elapsedTime;
    public int index = 0;
    private int soaIndex = 0;
    public int ex4TimedIndex = 0;
    public Material orange;


    public void Start()

    {

        SetButtonByTypes();
       // Debug.Log("Button Start");
        red = Resources.Load<Material>("red");
        green = Resources.Load<Material>("green");
        this.materialChanger = this.GetComponent<MaterialChanger>();
        ExerciseManager.instance.restart.AddListener(OnRestart);
        this.GetComponent<Collider>().enabled = false;

    }

    public void SetButtonByTypes()
    {
        soaIndex = 0;
        index = 0;

        if (TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment || 
            TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training ||
            TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed)
        {
            //types = new ButtonType[] { ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Red, ButtonType.Red, ButtonType.Red };
            delayValues = new float[] { 0, 0, 0, 0, 0, 0 , 0 , 0};
            ex4TimedIndex = 0;

        }
        else if (TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen ||
            TargetGeneration.Instance.type == ExerciseType.Counter16x8x3redGreen)
        {
            types = new ButtonType[] { ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Red, ButtonType.Red };
            delayValues = new float[] { 0.25f, 0.25f, 0.5f, 0.5f, .1f, .1f };
            types.Shuffle();
        }
        else if(TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter)
        {
            types = new ButtonType[] { ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green };
            delayValues = new float[] { 0.25f, 0.25f, 0.5f, 0.5f, 1f, 1f };
            types.Shuffle();
        }
        else if (TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuterRedGreen)
        {
            types = new ButtonType[] { ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Red };
            delayValues = new float[] { 0.25f, 0.5f, .1f };
            types.Shuffle();
        }
        else
        {
            types = new ButtonType[] { ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Red, ButtonType.Red };
            delayValues = new float[] { 0.25f, 0.25f, 0.5f, 0.5f, 1f, 1f };
            types.Shuffle();
        }

        delayValues.Shuffle();
    }

    public bool Active
    {

        get
        {
            return active;
        }
        set
        {
            if (!materialChanger)
                this.materialChanger = this.GetComponent<MaterialChanger>();

            if (value)
            {
                StartCoroutine(StartAfterdelay());
            }
            else
            {
                materialChanger.SetOffMaterial();
                this.GetComponent<Collider>().enabled = false;
                elapsedTime = 0;
            }
            active = value;

        }

    }

    public ButtonType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
            switch (value)
            {
                case ButtonType.Green:
                    materialChanger.SetMaterial(green);
                    break;
                case ButtonType.Red:
                    materialChanger.SetMaterial(red);
                    break;
                default:
                    break;
            }
        }
    }

    private IEnumerator StartAfterdelay()
    {
        var currentSoa = (Type == ButtonType.Green || delayValues[soaIndex]  == 0 ) ? delayValues[soaIndex] : 0.1f;

        if ((TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen ||
             TargetGeneration.Instance.type == ExerciseType.Counter16x8x3redGreen) && 
             delayValues[soaIndex] == 1)
        {
            currentSoa = .1f;
        }
        
        SavedData.instance.delay.Add(currentSoa);
        if (!TargetGeneration.Instance.gamePaused)
        {
            if (TargetGeneration.Instance.type == ExerciseType.Timed || 
                TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated)
            {
                yield return new WaitForSeconds(0);
            }
            else
            {
                //DEBUG
                //debugDelay = true;
                //debugTimeCounter = 0;
                yield return new WaitForSeconds(currentSoa);
                //DEBUG
                //debugDelay = false;
                //Debug.Log("SOA Taken for Button Type " + Type + " : " + debugTimeCounter + " sec");


            }

            this.GetComponent<Collider>().enabled = true;

            index = (index + 1) % types.Length;
            soaIndex = (soaIndex + 1) % delayValues.Length;

            if (TargetGeneration.Instance.type == ExerciseType.Counter16x8x3redGreen ||
                TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen ||
                TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuterRedGreen)
            {
                Type = types[index];
                if (index == types.Length) types.Shuffle();
            }
            else if (TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training ||
                TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment)
            {
                Type = types[ex4TimedIndex];
                ex4TimedIndex++;
                
                if (ex4TimedIndex >= 8) ex4TimedIndex = 0;

                ////DEBUGGING
                //if (Type == ButtonType.Red) reds++;
                //else greens++;
                ////END DEBUGGING

                //Type = TargetGeneration.Instance.CurrentButtonType;
            }
            else if(TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed)
            {
                Type = types[ex4TimedIndex];
                Debug.Log("Button Type Index" + ex4TimedIndex);
                ex4TimedIndex++;

                if (ex4TimedIndex >= 6) ex4TimedIndex = 0;

                ////DEBUGGING
                //if (Type == ButtonType.Red) reds++;
                //else greens++;
            }
            else
            {
                Type = ButtonType.Green;
            }
            elapsedTime = 0;
            
            if (TargetGeneration.Instance.type != ExerciseType.Timed && 
                TargetGeneration.Instance.type != ExerciseType.Counter16x6TimeCalculated/* &&*/
                /*TargetGeneration.Instance.type != ExerciseType.CounterTwoMinuter*/)
                StartCoroutine(Next());
        }
    }


    IEnumerator Next()
    {
        float delay = 0;
        switch (Type)
        {
            case ButtonType.Green:
                if (TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment ||
                    TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training ||
                    TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed)
                    delay = 2f;
                else delay = 2f;

                break;
            case ButtonType.Red:
                if (TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment ||
                    TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training ||
                    TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed)
                    delay = 1f;
                else delay = 1f;
                break;
            default:
                delay = 1f;
                break;
        }

        
        yield return new WaitForSeconds(delay);




        this.Active = false;
        SavedData.instance.reactionTime.Add(0);

        if (!TargetGeneration.Instance.exerciseEnded)
        {
            TargetGeneration.Instance.ButtonNotPressed(Type);
            SavedData.instance.ButtonType.Add(Type.ToString());
            TargetGeneration.Instance.PlayNextButton();
        }

    }

    public void OnRestart()
    {
        this.materialChanger.SetOffMaterial();
        this.GetComponent<Collider>().enabled = false;
        elapsedTime = 0;


    }

    public void ButtonPressed()
    {
        if (Active)
        {
            SavedData.instance.reactionTime.Add(elapsedTime);
            SavedData.instance.ButtonType.Add(Type.ToString());
            var data = this.GetComponent<ButtonBehaviour>().lastHand;
            SavedData.instance.HandType.Add(data.ToString());

            TargetGeneration.Instance.ButtonPressed(Type,elapsedTime);
            StopAllCoroutines();
        }
    }


    public void Blackout()
    {
        {
            materialChanger.SetMaterial(orange);
        }
    }

    private void BlakIn()
    {
        materialChanger.SetMaterial(orange);
    }


    public void Flash()
    {
        StartCoroutine(FlachTwice());
    }

    IEnumerator FlachTwice()
    {
        materialChanger.SetMaterial(flash);
        yield return new WaitForSeconds(0.5f);
        materialChanger.SetOffMaterial();
        yield return new WaitForSeconds(0.5f);
        materialChanger.SetMaterial(flash);
        yield return new WaitForSeconds(0.5f);
        materialChanger.SetOffMaterial();
    }

    private void Update()
    {
        if (active)
        {
            elapsedTime += Time.deltaTime;
        }

        //if (debugDelay)
        //{
        //    debugTimeCounter += Time.deltaTime;
        //}
    }

    //float debugTimeCounter = 0;
    //bool debugDelay = false;


}
