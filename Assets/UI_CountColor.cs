using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CountColor : MonoBehaviour
{
    Manager_Player_Technique manager_Player_Technique;

    [SerializeField, Header("L = true;R = false")]
    bool left;
    // Start is called before the first frame update
    void Start()
    {
        manager_Player_Technique = GameObject.FindWithTag("Manager").GetComponent<Manager_Player_Technique>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ColorChange()
    {

    }
}
