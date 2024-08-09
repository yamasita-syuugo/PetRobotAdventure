using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type_Scaffold : MonoBehaviour
{
    [SerializeField]
    eScaffoldType scaffoldType = eScaffoldType.none;
    // Start is called before the first frame update
    void Start()
    {
        if (scaffoldType == eScaffoldType.none) Debug.Log(name + " scaffoldType = none");
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public eScaffoldType GetScaffoldType() { return scaffoldType; }
}
