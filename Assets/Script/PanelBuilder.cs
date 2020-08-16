using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBuilder : MonoBehaviour {

    public GameObject panelButton;
    public GameObject ExeriseManager;
    public Transform parent;
    public float spacing;
    public int rowsCount = 4;
    public int columnsCount = 4;

    private float startX = 0;
    private float currentX = 0;
    private float currentY = 0;

    public int buttonID=0;

    //public List<GameObject> ButtonsofTarget = new List<GameObject>();

    List<GameObject> list;
    public List<GameObject> Build()
    {
        if (list==null)
        {
            list = new List<GameObject>();
            int n = (rowsCount / 2) - 1;
            float w = panelButton.transform.localScale.x;
            float step = (w + spacing);

            if (rowsCount % 2 == 0) startX = ((spacing / 2) + (w / 2)) + ((spacing + w) * n);
            else
            {
                n = (rowsCount - 1) / 2;
                startX = (w + spacing) * n;
            }

            currentX = -startX;
            currentY = -startX;

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    GameObject newButton = Instantiate(panelButton, parent);
                    newButton.transform.localPosition = new Vector3(currentX, currentY, 0);
                    list.Add(newButton);
                    buttonID = buttonID + 1;

                    newButton.gameObject.GetComponent<ExerciseButton>().buttonID = buttonID;

                    currentX += step;
                }
                currentX = -startX;
                currentY += step;
            }

        }

 
        return list;

    }


    

}
