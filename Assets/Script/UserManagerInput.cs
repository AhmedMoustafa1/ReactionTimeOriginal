using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum GenderType
{
    Male,
    Female
}
public enum HandColor
{
    White,
    Black,
    Yellow
}
[System.Serializable]
public class UserData
{
    public GenderType userGender;
    public HandColor userHandColor;

    public string firstName;
    public string lastName;
    public string userAge;
}

public class UserManagerInput : MonoBehaviour {
    public static UserManagerInput instance;


    public GameObject _FirstName;
    public GameObject _LastName;

    public GameObject _age;
    public GameObject _gender;
    public GameObject _HandColor;


    public GameObject _inputName;

    public GameObject _WarningMessage;

    public Sprite errorTextButton;
    public UserData _thisUser;
    public UserAccountManager _AccountManager;

    private GameObject maleCheck;
    private GameObject femaleCheck;

    private GameObject whiteCheck;
    private GameObject blackCheck;
    private GameObject yellowCheck;

    public GameObject Tutorial;




    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null) instance = this;
        else
        {

            Destroy(instance.gameObject);
            instance = this;
        }
       // instance = this;

    }

    // Use this for initialization
    void Start () {
        //PlayerPrefs.DeleteAll();
        _thisUser = new UserData();
        maleCheck = _gender.gameObject.transform.GetChild(0).gameObject;
        femaleCheck = _gender.gameObject.transform.GetChild(1).gameObject;


        whiteCheck = _HandColor.gameObject.transform.GetChild(0).gameObject;
        blackCheck = _HandColor.gameObject.transform.GetChild(1).gameObject;
        yellowCheck = _HandColor.gameObject.transform.GetChild(2).gameObject;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ThatCoolTabThing();
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Asterisk))
        {
            TimeScaler();
        }
#endif
        if (Input.GetKeyDown(KeyCode.H))
        {          
            Debug.Log(_thisUser.userHandColor);
        }
    }

    public void SubmitValues()
    {
        _thisUser = new UserData();

        if (SetName()&& SetAge() && SetGender() && SetHandColor())
        {
            if (!_AccountManager.AuthenticateUserExist(_thisUser))
            {
                _WarningMessage.gameObject.GetComponent<Text>().text = "This user already exist";
            }
            else
            {
                _AccountManager.SaveUserData(_thisUser);
                _AccountManager.ClearUserData(_thisUser);
               // Debug.Log(Application.persistentDataPath);
                Debug.Log("Registeration completed");
                _WarningMessage.gameObject.GetComponent<Text>().text = "Completed, Save Directory is : \n"+ _AccountManager.directory;

            }
        }
        else {
            Debug.Log("Some values aren't correct");
        }
        Debug.Log(_thisUser.firstName+" "+ _thisUser.lastName +" "+ _thisUser.userAge + " " + _thisUser.userGender);
    }
    
    public void RetrieveValues()
    {
        Debug.Log(_inputName.gameObject.GetComponent<InputField>().text);
        _thisUser = _AccountManager.readUserData(_inputName.gameObject.GetComponent<InputField>().text);
      Debug.Log("this user data is read" + _thisUser.firstName);
       if (_thisUser == null)
        {
            _WarningMessage.gameObject.GetComponent<Text>().text = "User doesn't exist.";
        }
        else
        {
            if (Tutorial.gameObject.GetComponent<Toggle>().isOn)
            {
                SceneManager.LoadSceneAsync("VRTutorialScene");

            }
            else
            {
                SceneManager.LoadSceneAsync("VRScene");

            }

        }
        //Debug.Log(_thisUser.userGender);
        // Debug.Log("this user exist");
    }

    private bool SetGender()
    {
        if (maleCheck.gameObject.GetComponent<Toggle>().isOn)
        {
            _thisUser.userGender = GenderType.Male;
            return true;
        }
        else if (femaleCheck.gameObject.GetComponent<Toggle>().isOn)
        {
            _thisUser.userGender = GenderType.Female;
            return true;
        }

        else
        {
            _WarningMessage.gameObject.GetComponent<Text>().text = "Gender not assigned";

            return false;
        }
    }

    private bool SetHandColor()
    {
        if (whiteCheck.gameObject.GetComponent<Toggle>().isOn)
        {
            _thisUser.userHandColor = HandColor.White;
            return true;
        }
        else if (blackCheck.gameObject.GetComponent<Toggle>().isOn)
        {
            _thisUser.userHandColor = HandColor.Black;
            return true;
        }
        else if (yellowCheck.gameObject.GetComponent<Toggle>().isOn)
        {
            _thisUser.userHandColor = HandColor.Yellow;
            return true;
        }

        else
        {
            _WarningMessage.gameObject.GetComponent<Text>().text = "Hand Color not assigned";

            return false;
        }
    }

    private bool SetName()
    {
        if (_FirstName.gameObject.GetComponent<InputField>().text != ""&& _LastName.gameObject.GetComponent<InputField>().text != "")
        {

            _thisUser.firstName = _FirstName.gameObject.GetComponent<InputField>().text;
            _thisUser.lastName = _LastName.gameObject.GetComponent<InputField>().text;

            Debug.Log(_LastName.gameObject.GetComponent<InputField>().text);
            return true;
        }
        else if (_FirstName.gameObject.GetComponent<InputField>().text == "")
        {
            _FirstName.gameObject.GetComponent<Image>().sprite = errorTextButton;
            _WarningMessage.gameObject.GetComponent<Text>().text = "Please Enter First Name";

            return false;
        }
        else if (_LastName.gameObject.GetComponent<InputField>().text == "")
        {
            _LastName.gameObject.GetComponent<Image>().sprite = errorTextButton;
            _WarningMessage.gameObject.GetComponent<Text>().text = "Please Enter Last Name";

            return false;
        }
        else
            return false;
    }
    private bool SetAge()
    {
        if (_age.gameObject.GetComponent<InputField>().text != "")
        {

            _thisUser.userAge = _age.gameObject.GetComponent<InputField>().text;
            Debug.Log(_age.gameObject.GetComponent<InputField>().text);
            return true;
        }
        else if (_age.gameObject.GetComponent<InputField>().text == "")
        {
            _WarningMessage.gameObject.GetComponent<Text>().text = "Please Enter Age";
            _age.gameObject.GetComponent<Image>().sprite = errorTextButton;
            return false;
        }
        else
            return false;
    }

    private void ThatCoolTabThing()
    {
        if (_FirstName.gameObject.GetComponent<InputField>().isFocused)
        {
            _LastName.gameObject.GetComponent<InputField>().Select();

        }
        if (_LastName.gameObject.GetComponent<InputField>().isFocused)
        {
            _age.gameObject.GetComponent<InputField>().Select();

        }

    }

    bool timeScaled = false;

    void TimeScaler()
    {
        if (!timeScaled)
        {
            Time.timeScale = 100;
            timeScaled = true;
        }
        else
        {
            Time.timeScale = 1;
            timeScaled = false;

        }
    }

    private void FixedUpdate()
    {
       
       
    }

}
