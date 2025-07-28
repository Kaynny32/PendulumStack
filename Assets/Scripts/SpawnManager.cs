using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefab Bob")]
    [SerializeField]
    List<GameObject> _prefabs = new List<GameObject>();
    [SerializeField]
    RectTransform chainEndPoint;
    [SerializeField]
    Transform _con;

    GameObject clone;

    public void Start()
    {
        SpawnBob();
    }

    public void SpawnBob()
    {
        clone = Instantiate(_prefabs[Random.Range(0, 3)], _con);
        clone.GetComponent<BobItem>().SetTargeRectransform(chainEndPoint);
        GameManager.instance.SetBobItem(clone.GetComponent<BobItem>());
    }
}
