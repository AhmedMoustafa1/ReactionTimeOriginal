using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomList : MonoBehaviour

{
    private int maxNumbers = 16;
    private List<int> uniqueNumbers = new List<int>();
    public List<int> finishedList = new List<int>();

    private void Awake()
    {
        CreateRandomList();
    }



    public List<int>  CreateRandomList()
    {

        finishedList = new List<int>();

        for (int i = 0; i < maxNumbers; i++)
        {
            /*uniqueNumbers*/
            finishedList.Add(i);

        }
        finishedList.Shuffle();

        //for (int i = 0; i < maxNumbers; i++)
        //{
        //    int ranNum = uniqueNumbers[Random.Range(1, uniqueNumbers.Count)];
        //    finishedList.Add(ranNum);
        //    uniqueNumbers.Remove(ranNum);
   

        //}
      
        return finishedList;
    }





}
