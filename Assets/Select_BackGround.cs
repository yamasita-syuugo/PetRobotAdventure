using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBackGroundType
{
    [InspectorName("")]none,

    sea,
    forest,

    [InspectorName("")] backGroundTypeMax,
}

public class Select_BackGround : MonoBehaviour
{
    [SerializeField]
    eBackGroundType backGroundType = eBackGroundType.sea;
    [SerializeField]
    Sprite[] backGroundBase = new Sprite[(int)eBackGroundType.backGroundTypeMax];
    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.Find("TitleManager");
        if (manager == null) manager = GameObject.Find("GameManager");
        if (manager == null) return;

        Manager_StageSelect stageSelect = manager.GetComponent<Manager_StageSelect>();
        backGroundType = stageSelect.GetBackGroundTypes()[(int)stageSelect.GetStage()] - 1;
        GetComponent<SpriteRenderer>().sprite = backGroundBase[(int)backGroundType];
    }

    // Update is called once per frame
    eStage oldStage = eStage.none;
    void Update()
    {
        {
            GameObject manager = GameObject.Find("TitleManager");
            if (manager == null) return;

            Manager_StageSelect manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
            if (oldStage == manager_StageSelect.GetStage()) return; oldStage = manager_StageSelect.GetStage();

            backGroundType = manager_StageSelect.GetBackGroundTypes()[(int)manager_StageSelect.GetStage()] - 1;
            GetComponent<SpriteRenderer>().sprite = backGroundBase[(int)backGroundType];
        }
    }

}
