using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointerSituation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        // ƒJ[ƒ\ƒ‹‚ğ‰æ–Ê“à‚Å“®‚©‚¹‚é
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
