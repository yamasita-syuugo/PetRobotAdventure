using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEffectType
{
    none,

    cloud,
    rainClouds,

    blackOut,

    [InspectorName("")] max,
}

public class Manager_Background_Effect : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;

    [SerializeField]
    eEffectType effectType;
    public eEffectType GetEffectType() { return manager_StageSelect.GetStageData(manager_StageSelect.GetStage()).GetEffectType();   }

    private void OnEnable()
    {
        manager_StageSelect = GetComponent<Manager_StageSelect>();
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
