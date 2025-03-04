using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_ClearCondition_Type : MonoBehaviour
{
    Image sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Image>();
        Manager_Gate manager_Gate = GameObject.FindWithTag("Manager").GetComponent<Manager_Gate>();
        sprite.sprite = manager_Gate.GetGateOpenImage(manager_Gate.GetGateOpenType());
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
