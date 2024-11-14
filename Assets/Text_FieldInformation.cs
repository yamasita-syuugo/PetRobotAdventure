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
        manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();
        manager_Field = GameObject.FindWithTag("Manager").GetComponent<Manager_Field>();
    }

    // Update is called once per frame
    int oldFieldIndex = -1;
    void Update()
    {
        TextChange();
    }
    void TextChange()
    {
        if (oldFieldIndex == manager_Field.GetFieldCreatTypeIndex(manager_StageSelect.GetStage())) return; oldFieldIndex = manager_Field.GetFieldCreatTypeIndex(manager_StageSelect.GetStage());
        Debug.Log("Text_FieldInformation.TextChange");
        string fieldCreatTypeName = "error";
        switch ((eFieldCreatType)oldFieldIndex)
        {
            case eFieldCreatType.stage: fieldCreatTypeName = "stage";break;
            case eFieldCreatType.labyrinth: fieldCreatTypeName = "labyrinth"; break;
        }
        GetComponent<TextMeshProUGUI>().text = fieldCreatTypeName;
    }
}
