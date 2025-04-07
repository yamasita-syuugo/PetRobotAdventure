using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Stage : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;
    Manager_Collection manager_Collection;
    void AddStageType(int add = 1)
    {
        while (true)
        {
            manager_StageSelect.SetStage(manager_StageSelect.GetStage() + add);

            if (manager_StageSelect.GetStage() == 0 || manager_Collection.GetGetSituation(eCollectionType.stage, (int)manager_StageSelect.GetStage())) break;
        }
    }
    public void LeftButton() { AddStageType(-1); }
    public void RightButton() { AddStageType(1); }
    public void LeftMinButton() { manager_StageSelect.SetStage(0); }
    public void RightMaxButton()
    {
        manager_StageSelect.SetStage(eStage.max - 1);
        while (true) {
            if (manager_StageSelect.GetStage() == 0 || manager_Collection.GetGetSituation(eCollectionType.stage, (int)manager_StageSelect.GetStage())) break;
            else AddStageType(-1); }
    }
    private void OnEnable()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
        manager_Collection = manager.GetComponent<Manager_Collection>();
    }
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
    //}
}
