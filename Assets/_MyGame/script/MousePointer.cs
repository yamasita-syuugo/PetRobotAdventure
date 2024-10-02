using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    Manager_MousePointerType manager_MousePointerType;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        // ÉJÅ[É\ÉãÇâÊñ ì‡Ç≈ìÆÇ©ÇπÇÈ
        Cursor.lockState = CursorLockMode.Confined;

        GameObject tmp = GameObject.FindWithTag("Manager");
        manager_MousePointerType = tmp.GetComponent<Manager_MousePointerType>();
    }

    // Update is called once per frame
    int oldMousePointerIndex = -1;
    void Update()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int mousePointerIndex = manager_MousePointerType.GetMousePointerIndex();
        if (oldMousePointerIndex == mousePointerIndex) return; oldMousePointerIndex = mousePointerIndex;
        GetComponent<SpriteRenderer>().sprite = manager_MousePointerType.GetMousePointerImage(mousePointerIndex);
    }
}
