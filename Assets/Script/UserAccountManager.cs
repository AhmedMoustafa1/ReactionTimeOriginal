using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class UserAccountManager : MonoBehaviour {
    public static UserAccountManager intance;
    //private userData _registeredUserData;
    private string form;
    public string directory ;
    string user;
    public GameObject _WarningMessage;
    // Use this for initialization
	void Start () {
        intance = this;
    }
	


    public bool AuthenticateUserExist(UserData _thisUser)
    {
        directory = "C:/VR_users/"+_thisUser.lastName+" "+ _thisUser.firstName;
        if (!System.IO.File.Exists(directory))
        {
            return true;
        }
        else
        {
            _WarningMessage.gameObject.GetComponent<Text>().text = "User exists";

            Debug.Log("This user exist");
            return false;
        }

        
    }
    public void SaveUserData(UserData _thisUser)
    {
        form = JsonUtility.ToJson(_thisUser);

        if (Directory.Exists("C:/VR_users/" + _thisUser.lastName + " " + _thisUser.firstName))
        {
            directory = "C:/VR_users/" + _thisUser.lastName + " " + _thisUser.firstName; 
        }
        else
        {
            Directory.CreateDirectory("C:/VR_users/" + _thisUser.lastName + " " + _thisUser.firstName);
            directory = "C:/VR_users/"  +_thisUser.lastName + " " + _thisUser.firstName;

        }

   
       System.IO.File.WriteAllText(directory + "\\"+ _thisUser.lastName + " " + _thisUser.firstName+"-File.txt", form);
    }

    public void ClearUserData(UserData _thisUser)
    {
        _thisUser.firstName = "";
        _thisUser.lastName = "";

        _thisUser.userAge = "";
    }

    public UserData readUserData(string user)
    {
        UserData _thisUser=null;

        string FirstName  = user.Substring(0, user.IndexOf(" "));
        string LastName = user.Substring(user.IndexOf(" ") + 1);
        user = LastName + " " + FirstName;

        directory = "C:/VR_users/" + user;
     
        if (System.IO.File.Exists(directory + "\\" + user + "-File.txt")) { 
            string text = System.IO.File.ReadAllText(directory + "\\" + user + "-File.txt");
           _thisUser=JsonUtility.FromJson<UserData>(text);
            _WarningMessage.gameObject.GetComponent<Text>().text = "User exists";

            return _thisUser;
         
        }
        return _thisUser;
    }



   
}
