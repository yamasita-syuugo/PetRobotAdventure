using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eStage
{
    bomOnly,
    golemOnly,

    lastGame,
}
public class Manager_StageSelect : MonoBehaviour
{
    eStage stage;
    public eStage GetStage() { return stage; }
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
}
