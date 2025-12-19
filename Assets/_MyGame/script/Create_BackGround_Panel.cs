using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Create_BackGround_Panel : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;
    Manager_BackgroundType manager_BackgroundType;

    // Start is called before the first frame update
    const int wide = 25;const int height = 20;
    void Start()
    {
        manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();
        manager_BackgroundType = GameObject.FindWithTag("Manager").GetComponent<Manager_BackgroundType>();

        for (int horizontal = 0; horizontal < wide; horizontal++)
            for (int vertical = 0; vertical < height; vertical++)
            {
                GameObject tmp = new GameObject();
                tmp.name = "BackGround_Panel";
                tmp.transform.parent = transform;
                tmp.transform.localPosition = new Vector3(-wide / 2 - vertical % 2 * 0.5f + horizontal, -height / 2 * 0.75f + vertical * 0.75f);
                SpriteRenderer tmp_sp = tmp.AddComponent<SpriteRenderer>();
                tmp_sp.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
            }
    }

    // Update is called once per frame
    eBackGroundType oldBackGroundType = eBackGroundType.none;
    bool oldRandom = false;
    void Update()
    {
        bool backGroundSerect = false;
        eBackGroundType backGroundType;
        if (manager_StageSelect.GetBackGroundSerect() || SceneManager.GetActiveScene().name == "Collection") { backGroundType = manager_BackgroundType.GetBackGroundIndex(); backGroundSerect = true; }
        else { backGroundType = manager_StageSelect.GetStageData(manager_StageSelect.GetStage()).GetBackGroundIndex(); backGroundSerect = false; }
        bool random = manager_StageSelect.GetRandomStage();

        if (oldBackGroundType == (eBackGroundType)backGroundType && oldRandom == random) return; oldBackGroundType = (eBackGroundType)backGroundType;oldRandom = random;

        if (!backGroundSerect && random) { backGroundType = eBackGroundType.question; }
        SpriteRenderer []tmp_sp = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < tmp_sp.Length; i++) tmp_sp[i].sprite = manager_BackgroundType.GetBackGround_Panel_Base(backGroundType);
    }
}
