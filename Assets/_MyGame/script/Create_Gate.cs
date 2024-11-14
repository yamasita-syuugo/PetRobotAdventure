using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Gate : MonoBehaviour
{
    Manager_Gate manager_Gate;

    GameObject gate;
    // Start is called before the first frame update
    void Start()
    {
        manager_Gate = GameObject.FindWithTag("Manager").GetComponent<Manager_Gate>();
        gate = Instantiate(manager_Gate.GetGateBase());
        oldGatePos = new Vector3( manager_Gate.GetGatePos().x,manager_Gate.GetGatePos().y,1);
        gate.transform.localPosition = oldGatePos;
    }

    // Update is called once per frame
    Vector2 oldGatePos;
    void Update()
    {
        if (oldGatePos == manager_Gate.GetGatePos()) return;oldGatePos = manager_Gate.GetGatePos();

        gate.transform.localPosition = oldGatePos;
    }
}
