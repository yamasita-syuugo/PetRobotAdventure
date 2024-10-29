using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Gate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Manager_Gate manager_Gate = GameObject.FindWithTag("Manager").GetComponent<Manager_Gate>();
        Instantiate(manager_Gate.GetGateBase());
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
