using System;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public static Line instance;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    LineDate lineDate;
    [SerializeField]
    LineTriger lineTriger;
    [SerializeField]
    string lineName;
    [SerializeField]
    bool isDisable = false;
    [SerializeField]
    int index;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddBobLine(string name, GameObject go)
    {
        lineDate.count++;
        lineDate.nameBob.Add(name);
        lineDate.goBob.Add(go);
        CheckLine();
    }

    public void CheckLine()
    {
        if (lineDate.count == 3)
        {
            lineTriger.OnDisableTriger();

            if (lineDate.nameBob[0] == lineName && lineDate.nameBob[1] == lineName && lineDate.nameBob[2] == lineName)
            {
                foreach(GameObject go in lineDate.goBob)
                { 
                    Destroy(go);                    
                }
                lineDate.goBob.Clear();
                lineDate.nameBob.Clear();
                lineDate.count = 0;
                lineTriger.OnEnebleTriger();
                gameManager.SetScore();
            }
            else
            {
                isDisable = true;
                gameManager.CheckLine(index);
            }
        }
    }
}
[Serializable]
public class LineDate
{
    public int count;
    public List<string> nameBob;
    public List<GameObject> goBob;
}