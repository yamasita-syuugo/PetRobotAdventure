using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_BackGround_Image : MonoBehaviour
{
    Manager_BackgroundType manager_BackgroundType = null;
    // Start is called before the first frame update
    void Start()
    {
        manager_BackgroundType = GameObject.FindWithTag("Manager").GetComponent<Manager_BackgroundType>();
    }

    // Update is called once per frame
    int oldBackGround = 0;
    void Update()
    {
        int backGroundType = manager_BackgroundType.GetBackGroundType();
        if(manager_BackgroundType == null) { return; }
        if (oldBackGround == backGroundType) return;oldBackGround = backGroundType;
        GetComponent<SpriteRenderer>().sprite = manager_BackgroundType.GetBackGroundBase(backGroundType);
    }
}
