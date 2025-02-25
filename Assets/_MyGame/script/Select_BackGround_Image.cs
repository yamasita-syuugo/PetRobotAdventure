using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Select_BackGround_Image : MonoBehaviour
{
    Manager_BackgroundType manager_BackgroundType = null;
    Manager_StageSelect manager_StageSelect = null;
    // Start is called before the first frame update
    void Start()
    {
        manager_BackgroundType = GameObject.FindWithTag("Manager").GetComponent<Manager_BackgroundType>();
        manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent <Manager_StageSelect>();
    }

    // Update is called once per frame
    int oldBackGround = 0;
    void Update()
    {
        int backGroundType;
        if (manager_StageSelect.GetBackGroundSerect() || SceneManager.GetActiveScene().name == "Collection") { backGroundType = manager_BackgroundType.GetBackGroundIndex();}
        else { backGroundType = manager_StageSelect.GetStageData(manager_StageSelect.GetStage()).GetBackGroundIndex(); }
        
        if(manager_BackgroundType == null) { return; }
        if (oldBackGround == backGroundType) return;oldBackGround = backGroundType;
        GetComponent<SpriteRenderer>().sprite = manager_BackgroundType.GetBackGroundBase(backGroundType);
    }
}
