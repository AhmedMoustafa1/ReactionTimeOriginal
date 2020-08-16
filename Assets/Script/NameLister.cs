using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class NameLister : MonoBehaviour {

    public List<string> Allusers;
    public List<string> SearchedUsers;
    public List<string> SeperatedSearchList;
    public List<string> list;


    public GameObject ButtonPrefab;
    public GameObject ContentParet;
    public InputField SearchTextField;
    public string InputCharacters;
    

    // Use this for initialization
    void Start()
    {
       SearchTextField.onValueChanged.AddListener(delegate { ValueChangeUpdate(); });
        ButtonPrefab.GetComponentInChildren<Button>(). onClick.AddListener(delegate { updateText(); });

        NameStarter();

    }

    public void NameStarter()
    {
        Allusers.Clear();
        CreateOriginalList();
        UpdateList();
        SearchTextField.text = "";
    }

    public void ValueChangeUpdate()
    {

        if (SearchTextField.text=="")
        {
            UpdateList();
        }
        else
        {
            UpdateList();
        }
    }

    public void updateText()
    {
        SearchTextField.text = ButtonPrefab.GetComponentInChildren<Text>().text;
    }

    void UpdateList()
    {
        StopAllCoroutines();
        SearchedUsers.Clear();
        SeperatedSearchList.Clear();
        UpdateSearchList(SearchTextField.text);
        ClearUINameButtons();
        StartCoroutine( CreateUINameButtonList());
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            //SearchedUsers.Clear();
            //SeperatedSearchList.Clear();
            //UpdateSearchList(InputCharacters);
            //ClearUINameButtons();
            //CreateUINameButtonList();
            //Debug.Log("Searching");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
           
        }
	}

    void CreateOriginalList()
    {
        foreach (var d in System.IO.Directory.GetDirectories(@"C:/VR_users/"))
        {
            var dir = new DirectoryInfo(d);
            string dirName = dir.Name;
      

            string[] array = dirName.Split(' ');

            string firstName = array[1];
            string lastName = array[0];
            dirName = firstName + " " + lastName;
            Allusers.Add(dirName);
        }
        Allusers.Sort();
    }

    void UpdateSearchList(string searchedCharacters)
    {

        int searchedStringLength = searchedCharacters.Length;

        SeperatedSearchList.Clear();
        foreach (var Name in Allusers)
        {
            string splittedString;
            if (Name.Length> searchedStringLength)
            {
                splittedString = Name.Substring(0, searchedStringLength);
            }
            else
            {
                splittedString = Name;
            }
            SeperatedSearchList.Add(splittedString);
        }


       // SeperatedSearchList.Clear();
        for (int i = 0; i < SeperatedSearchList.Count; i++)
        {
            if (searchedCharacters == (SeperatedSearchList[i]))
            {
                SearchedUsers.Add(Allusers[i]);
              //  Debug.Log(SearchedUsers[i]);

            }

        }




    }
    IEnumerator CreateUINameButtonList()
    {
        yield return new WaitForSeconds(.3f);
        for (int i = 0; i < SearchedUsers.Count; i++)
        {
            var item = SearchedUsers[i];
            ButtonPrefab.GetComponentInChildren<Text>().text = item;
            var user = Instantiate(ButtonPrefab, ContentParet.transform).GetComponentInChildren<Animator>();
            user.Play("ButtonAppear");
            yield return new WaitForSeconds(.1f);
        }
    }

    public void ClearUINameButtons()
    {
      
        for (int i = ContentParet.transform.childCount-1; i >=0; i--)
        {
            Transform child = ContentParet.transform.GetChild(i);

            child.GetComponentInChildren<Animator>(true).SetTrigger("out");

        }
    }

    public void DestroyButtons()
    {

        for (int i = ContentParet.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = ContentParet.transform.GetChild(i);
            Destroy(child.gameObject);

        }
    }

}
