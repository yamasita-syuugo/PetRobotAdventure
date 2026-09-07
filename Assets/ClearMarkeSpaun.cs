using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearMarkeSpaun : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;
    Manager_Collection manager_Collection;
    private void OnEnable()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
        manager_Collection = manager.GetComponent<Manager_Collection>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        oldStageSelect = (int)manager_StageSelect.GetStage();
    }

    int oldStageSelect;

    // Update is called once per frame
    void Update()
    {
        if (oldStageSelect == (int)manager_StageSelect.GetStage()) return;
        oldStageSelect = (int)manager_StageSelect.GetStage();

        if(manager_Collection.GetGetSituation(eCollectionType.stageClear,oldStageSelect))GetComponent<Renderer>().enabled = true;
        else GetComponent<Renderer>().enabled = false;
    }
}
