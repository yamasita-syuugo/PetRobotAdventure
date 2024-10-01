using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(onTriggerType == eOnTriggerType.none) { Debug.Log(name + " = onTriggerNone"); }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
    enum eOnTriggerType
    {
        none,

        enter,
        stay,
        exit,
    }
    [SerializeField]
    eOnTriggerType onTriggerType = eOnTriggerType.none;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onTriggerType != eOnTriggerType.enter) return;
        GameObject.FindWithTag("Manager").GetComponent<Manager_Hit>().Hit(gameObject,collision.gameObject);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (onTriggerType != eOnTriggerType.stay) return;
        GameObject.FindWithTag("Manager").GetComponent<Manager_Hit>().Hit(gameObject,collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onTriggerType != eOnTriggerType.exit) return;
        GameObject.FindWithTag("Manager").GetComponent<Manager_Hit>().Hit(gameObject,collision.gameObject);
    }
}
