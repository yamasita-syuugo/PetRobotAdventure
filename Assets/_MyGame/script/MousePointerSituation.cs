using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointerSituation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        // カーソルを画面内で動かせる
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
