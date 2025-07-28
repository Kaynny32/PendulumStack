using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTriger : MonoBehaviour
{
    [SerializeField]
    Line lineManager;
    [SerializeField]
    BoxCollider2D _triger;


    private void Start()
    {
        _triger = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BobItem>())
        {
            lineManager.AddBobLine(collision.gameObject.GetComponent<BobItem>().GetBobName(), collision.gameObject);
        }
    }

    public void OnDisableTriger()
    {
        _triger.enabled = false;
    }
    public void OnEnebleTriger()
    {
        _triger.enabled = true;
    }
}
