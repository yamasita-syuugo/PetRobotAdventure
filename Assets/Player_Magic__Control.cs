using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Magic__Control : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    int chanting = 0;
    void Update()
    {
        Magic__Base();
    }

    private void Magic__Base()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetKeyDown(KeyCode.W)) chanting = chanting * 10 + 1;
            if (Input.GetKeyDown(KeyCode.A)) chanting = chanting * 10 + 2;
            if (Input.GetKeyDown(KeyCode.S)) chanting = chanting * 10 + 3;
            if (Input.GetKeyDown(KeyCode.D)) chanting = chanting * 10 + 4;
        }
        else
        {
            switch (chanting)
            {
                case 0: break;
                case 1: 
                    Magic_();
                    break;
                case 2: break;
                case 3: break;
                case 4: break;
                case 14: break;
            }

            chanting = 0;
        }
    }
    private void Magic_()
    {
        
    }
}
