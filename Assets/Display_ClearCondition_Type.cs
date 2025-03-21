using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_ClearCondition_Type : MonoBehaviour
{
    Image sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Image>();
        GameObject manager = GameObject.FindWithTag("Manager");
        Manager_Gate manager_Gate = manager.GetComponent<Manager_Gate>();
        Manager_StageSelect manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
        sprite.sprite = manager_Gate.GetGateOpenImage(manager_StageSelect.GetGateOpenType(manager_StageSelect.GetStage()));
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
