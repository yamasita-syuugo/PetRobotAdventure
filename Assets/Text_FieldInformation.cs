using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text_FieldInformation : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;
    Manager_Field manager_Field;
    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_StageSelect = manager.GetComponent<Manager_StageSelect>();
        manager_Field = manager.GetComponent<Manager_Field>();
    }

    // Update is called once per frame
    int oldFieldIndex = -1;
    void Update()
    {
        TextChange();
    }
    void TextChange()
    {
        int fieldIndex = manager_Field.GetFieldCreatTypeIndex(manager_StageSelect.GetStage());
        if (oldFieldIndex == fieldIndex) return; oldFieldIndex = fieldIndex;

        string fieldCreatTypeName = "error";
        switch ((eFieldCreatType)oldFieldIndex)
        {
            case eFieldCreatType.stage: fieldCreatTypeName = "stage";break;
            case eFieldCreatType.labyrinth: fieldCreatTypeName = "labyrinth"; break;
        }
        GetComponent<TextMeshProUGUI>().text = fieldCreatTypeName;
    }
}
