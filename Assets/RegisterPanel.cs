using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

class KeyData
{
    public bool valid = false;
    public long startOfKey = 0;
    public long endOfKey = 0;
    public string rawKey = "";
    public string code = "";
    public bool keyEnded = false;
}

public class RegisterPanel : MonoBehaviour
{
    [Header("KeyDuration in Days")]
    [SerializeField]
    private int keyDuration = 365;
    //public GameObject enterCDKeyPanel;
    [Space(15)]
    public Text text;
    public Text notificationText;
    public Button registerButton;
    public Button skipButton;
    public Button continueButton;
    public Button backButton;
    public Button submitCDKeyButton;
    public InputField cdKeyInput;
    public InputField deviceIDInputFieldDisplay;
    public Text enterCDkeyText;
    private int daysLeft = 0;


    private string registered = "registered";
    private string registeredKey = "registedKey";
    //private long startOfKey = 0; // in ticks
    //private long endOfKey = 0; // in ticks
    //private string code = "XXX";
    // Use this for initialization
    void Start()
    {
        deviceIDInputFieldDisplay.text = SystemInfo.deviceUniqueIdentifier;
        if (PlayerPrefs.HasKey(registered))
        {
            if (PlayerPrefs.GetString(registered) == GetRegisterationKey_Unlimited())
            {
                this.gameObject.SetActive(false) ;
                notificationText.gameObject.SetActive(true);
                notificationText.text = "Registered";
                registerButton.gameObject.SetActive(false);
                notificationText.color = Color.green;
                return;
            }
            else if(PlayerPrefs.GetString(registered) == GetRegisterationKey_OneYear())
            {
                // Check if not in the year
                // if yes return and tell how much left
                // else Say it ended and open registration panel
                if (PlayerPrefs.HasKey(registeredKey))
                {
                    KeyData savedKey = JsonUtility.FromJson<KeyData>(PlayerPrefs.GetString(registeredKey));
                    long time = savedKey.endOfKey - DateTime.Now.Ticks;

                    //Check if start of key is not after the current day (scam alert)
                    long checkingTime = savedKey.startOfKey - DateTime.Now.Ticks;
                    if (checkingTime < 0) // valid
                    {
                        if (time > 0) // Valid
                        {
                            int daysLeft = TimeSpan.FromTicks(time).Days;
                            this.gameObject.SetActive(false);
                            notificationText.gameObject.SetActive(true);
                            notificationText.text = "Registered for " + daysLeft + " days";
                            registerButton.gameObject.SetActive(false);
                            notificationText.color = Color.green;
                        }
                        else // Expired
                        {
                            text.text = "The Registration for this CD key has Expired, please Register with a new CD key";
                            skipButton.interactable = false;

                            notificationText.text = "Registration Expired";
                            notificationText.color = Color.red;

                            daysLeft = -1;
                            Debug.Log("Registration Ended");
                            return;
                        }
                    }
                    else //Invalid (scam Alert) date has been modified
                    {
                        text.text = "The Registration for this CD key has Expired, please Register with a new CD key";
                        skipButton.interactable = false;

                        notificationText.text = "Registration Expired";
                        notificationText.color = Color.red;

                        daysLeft = -1;
                        Debug.Log("Registration Ended");
                        return;
                    }
                    //long time =  DateTime.Now.Ticks - savedKey.endOfKey;
                    //Debug.Log(time);
                    
                }
                else
                {
                    PlayerPrefs.DeleteKey(registered);

                }

            }
            else 
            {
                PlayerPrefs.DeleteKey(registered);
            }
        }

       
        // TRIAL PERIOD
        if (PlayerPrefs.HasKey("endDate"))
        {
            var time = long.Parse(PlayerPrefs.GetString("endDate")) - DateTime.Now.Ticks;
            if (time > 0)
            {
                daysLeft = TimeSpan.FromTicks(time).Days;                
                Debug.Log("Has " + daysLeft + " Days Left");
                text.text = "Welcome\nThis app is valid for a 15-days trial period.\n\nPlease register before the trial period ends. \n\n" + "Trial Version: " + daysLeft + " days left.";


            }
            else
            {
                text.text = "The trial has ended \n please register the app";
                skipButton.interactable = false;
                daysLeft = -1;
                Debug.Log("Trial Ended");
            }

        }
        else
        {
            // FIRST TIME
            var time = (new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day )).Ticks+ (System.TimeSpan.TicksPerDay*15);
            PlayerPrefs.SetString("endDate", time.ToString());
            text.text = "Welcome\n\nThis app is valid for a 15-days trial period.\n\nPlease register before the trial period ends.";
            daysLeft = TimeSpan.FromTicks(long.Parse(time.ToString()) - DateTime.Now.Ticks).Days;
            Debug.Log(daysLeft);
        }

        //Debug.Log(GetRegisterationKey_Unlimited());

    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {
    //        PlayerPrefs.DeleteKey("startDate");
    //        PlayerPrefs.DeleteKey("registered");
    //    }
    //}

    string GetRegisterationKey_Unlimited()
    {
        var str = SystemInfo.deviceUniqueIdentifier;
        var ret = "";
        for (int i = 0; i < str.Length; i += 8)
        {
            var segment = str.Substring(i, 8);
            int sum = 0;
            for (int j = 0; j < segment.Length; j++)
            {
                sum += segment[j];

            }
            //Debug.Log(i+":"+segment.Length);
            //Debug.Log(sum);
            sum %= 10;
            //Debug.Log(sum);
            ret += ""+sum + ""+segment[0];
            //ret += segment[0];

        }
        
        return ret;
    }

    string GetRegisterationKey_OneYear()
    {
        var str = SystemInfo.deviceUniqueIdentifier;
        var ret = "";
        string edit = "365DaysOFLIFEKandooz";
        int x = 0;
        for (int i = 0; i < str.Length; i += 8)
        {
            var segment = str.Substring(i, 8);
            int sum = 0;
            for (int j = 0; j < segment.Length; j++)
            {
                sum += segment[j];
                sum += edit[x];
            }
            x++;
            //Debug.Log(i+":"+segment.Length);
            //Debug.Log(sum);
            sum %= 10;
            //Debug.Log(sum);
            ret += "" + sum + "" + segment[0];
            //ret += segment[0];

        }

        return ret;
    }

    private KeyData ExtractDataFromKey(string key)
    {
        KeyData keyData = new KeyData { valid = false };
        if (key.Length < 20)
        {
            Debug.Log("Something not right");
            return keyData;
        }

        string day = "" + key[2] + key[4];
        string month = "" + key[6] + key[9];
        string year = "" + key[14] + key[17] + key[19];

        string code = "" + key[3] + key[10] + key[15];

        int daynum = int.Parse(day);
        int monthnum = int.Parse(month);
        int yearnum = int.Parse("2" + year);


        keyData.startOfKey = new System.DateTime(yearnum, monthnum, daynum).Ticks;
        //Debug.Log(new System.DateTime(yearnum, monthnum, daynum));
        keyData.endOfKey = new System.DateTime(yearnum, monthnum, daynum).Ticks + TimeSpan.TicksPerDay * keyDuration;
        //Debug.Log(new DateTime(keyData.endOfKey));
        //var timeLeft = keyData.endOfKey - DateTime.Now.Ticks;
        //Debug.Log("TIMWSPAN THING: "+TimeSpan.FromTicks(timeLeft).Days);

        var keyarr = key.ToCharArray();
        keyarr[2] = '*';
        keyarr[4] = '*';
        keyarr[6] = '*';
        keyarr[9] = '*';
        keyarr[14] = '*';
        keyarr[17] = '*';
        keyarr[19] = '*';
        keyarr[3] = '*';
        keyarr[10] = '*';
        keyarr[15] = '*';
        key = "";
        for (int i = 0; i < keyarr.Length; i++)
        {
            key += keyarr[i];
        }
        key = key.Replace("*", "");

        var time = keyData.endOfKey - DateTime.Now.Ticks;
        if(time > 0)
        {
            keyData.valid = true;
            keyData.keyEnded = false;
        }
        else
        {
            keyData.valid = false;
            keyData.keyEnded = true;

        }

        keyData.code = code;
        keyData.rawKey = key;
        return keyData;
    }

    public void OnSubmitCDKeyButtonPressed()
    {
        KeyData extractedKey = ExtractDataFromKey(cdKeyInput.text);
        string key = "";
        bool unlimited = true;
        if (extractedKey.valid)
        {
            
            string registeredMessage = "Registered for Life";
            switch (extractedKey.code)
            {
                case "ULK":
                    key = GetRegisterationKey_Unlimited();
                    break;
                case "OYL":
                    key = GetRegisterationKey_OneYear();
                    unlimited = false;         
                    break;
            }

            if(key == extractedKey.rawKey)
            {
                PlayerPrefs.SetString(registered, key);
                string keyJSoned = JsonUtility.ToJson(extractedKey);
                PlayerPrefs.SetString(registeredKey, keyJSoned);

                if (!unlimited)
                {
                    var time = extractedKey.endOfKey - DateTime.Now.Ticks;
                    if (time > 0)
                    {
                        int daysLeft = TimeSpan.FromTicks(time).Days;
                        registeredMessage = "Registered for " + daysLeft +" days";
                    }
                }
                
                // General
                cdKeyInput.interactable = false;

                continueButton.gameObject.SetActive(true);
                submitCDKeyButton.gameObject.SetActive(false);
                backButton.gameObject.SetActive(false);
                registerButton.gameObject.SetActive(false);

                notificationText.text = registeredMessage;
                notificationText.color = Color.green;

                enterCDkeyText.gameObject.SetActive(true);
                enterCDkeyText.text = "Success, Press Continue Button";
            }
            else
            {
                enterCDkeyText.gameObject.SetActive(true);
                enterCDkeyText.text = "Wrong CD key entered, \nplease re-enter the correct CD key for this device";
            }
            
        }
        else if (!extractedKey.keyEnded)
        {
            enterCDkeyText.gameObject.SetActive(true);
            enterCDkeyText.text = "Wrong CD key entered, \nplease re-enter the correct CD key for this device";
        }
        else if (extractedKey.keyEnded)
        {
            enterCDkeyText.gameObject.SetActive(true);
            enterCDkeyText.text = "The CD key entered has Expired, \nplease enter a valid CD key";
        }
       

        //string registerationKey = GetRegisterationKey_Unlimited();


        //if (cdKeyInput.text == registerationKey)
        //{
        //    PlayerPrefs.SetString("registered", registerationKey);
        //    cdKeyInput.interactable = false;

        //    continueButton.gameObject.SetActive(true);
        //    submitCDKeyButton.gameObject.SetActive(false);
        //    backButton.gameObject.SetActive(false);
        //    registerButton.gameObject.SetActive(false);

        //    //notificationText.gameObject.SetActive(true);
        //    notificationText.text = "Registered";
        //    notificationText.color = Color.green;

        //    enterCDkeyText.gameObject.SetActive(true);
        //    enterCDkeyText.text = "Success, Press Continue Button";

        //}
        //else
        //{
        //    enterCDkeyText.gameObject.SetActive(true);
        //    enterCDkeyText.text = "Wrong CD key entered, \nplease re-enter the correct CD key for this device";
        //}
    }

    public void OnSkipButtonPressed()
    {
        if(daysLeft >= 0)
        {
            notificationText.text = "Trial version , Remaining days: " + daysLeft;
        }
        else
        {
            notificationText.text = "Trial version ended";
        }
        notificationText.gameObject.SetActive(true);
        notificationText.color = Color.red;
    }
    
}
