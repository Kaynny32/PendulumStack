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
        for (int i = 0; i < lineDate.goBob.Count; i++)
        {
            if (lineDate.goBob[i] == null)
            {
                lineDate.goBob.RemoveAt(i);
                lineDate.nameBob.RemoveAt(i);
                lineDate.count = 1; 
                break;
            }
        }

        if (lineDate.count == 3)
        {
            lineTriger.OnDisableTriger();

            if (lineDate.nameBob[0] == lineName && lineDate.nameBob[1] == lineName && lineDate.nameBob[2] == lineName)
            {
                foreach(GameObject go in lineDate.goBob)
                {
                    go.GetComponent<BobItem>().AnimBobDestroy();
                    Loom.QueueOnMainThread(() => {
                        Destroy(go);
                    },0.5f);                                        
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

    public void ResetLine()
    {
        lineDate.count = 0;
        lineDate.nameBob.Clear();
        lineDate.goBob.Clear();
        isDisable = false;
        lineTriger.OnEnebleTriger();
    }
}
[Serializable]
public class LineDate
{
    public int count;
    public List<string> nameBob;
    public List<GameObject> goBob;
}