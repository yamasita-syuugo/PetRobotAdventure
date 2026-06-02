using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GateConditions : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;
    Manager_Gate manager_Gate;

    GameObject gateConditions;

    private void OnEnable()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
        manager_Gate = manager.GetComponent<Manager_Gate>();
    }
    // Start is called before the first frame update
    void Start()
    { 
        gateConditions = new GameObject();
        gateConditions.transform.parent = transform;
        gateConditions.transform.localPosition = transform.localPosition;
        gateConditions.transform.localScale = transform.localScale * .5f;
        gateConditions.name = "gateConditions";

        SpriteRenderer tmp = gateConditions.AddComponent<SpriteRenderer>();
        tmp.sprite = manager_Gate.GetGateOpenImage(manager_StageSelect.GetStageData(manager_StageSelect.GetStage()).GetGateOpenType());
        tmp.sortingOrder = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager_Gate.GetGateOpen()) Destroy(gateConditions);
    }
}
