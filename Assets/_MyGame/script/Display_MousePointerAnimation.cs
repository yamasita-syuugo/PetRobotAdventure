using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_MousePointerAnimation : MonoBehaviour
{
    Manager_MousePointerType manager_MousePointerType;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tmp = GameObject.FindWithTag("Manager");
        manager_MousePointerType = tmp.GetComponent<Manager_MousePointerType>();
    }

    // Update is called once per frame
    int oldMousePointerIndex = -1;
    void Update()
    {

        int mousePointerIndex = manager_MousePointerType.GetMousePointerIndex();
        if (oldMousePointerIndex == mousePointerIndex) return; oldMousePointerIndex = mousePointerIndex;
        GetComponent<Animator>().runtimeAnimatorController = manager_MousePointerType.GetMousePointerAnimation(mousePointerIndex);
    }
}
