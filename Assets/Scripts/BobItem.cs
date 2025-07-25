using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobItem : MonoBehaviour
{
    [SerializeField]
    string nameBob;

    public string GetBobName()
    { 
        return nameBob;
    }
}
