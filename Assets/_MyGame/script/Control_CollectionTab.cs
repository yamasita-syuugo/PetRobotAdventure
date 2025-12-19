using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_CollectionTab : MonoBehaviour
{
    Manager_Collection manager_Collection;

    private void OnEnable()
    {
        manager_Collection = GameObject.FindWithTag("Manager").GetComponent<Manager_Collection>();
    }
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        int add = 0;
        add += Input.GetKeyDown("joystick button 5") ?1:0;
        add -= Input.GetKeyDown("joystick button 4") ?1:0;
        manager_Collection.AddCollectionsTab(add);
    }
}
