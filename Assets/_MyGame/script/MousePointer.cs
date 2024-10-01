using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    [SerializeField]
    Sprite[] mousePointerImage; 
    int mousePointerIndex = 0;
    public void AddMousePointerIndex(int Add = 1) { 
        mousePointerIndex += Add;
        if (mousePointerIndex >= mousePointerImage.Length) mousePointerIndex = 0;
        if (mousePointerIndex < 0) mousePointerIndex = mousePointerImage.Length;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        // ƒJ[ƒ\ƒ‹‚ð‰æ–Ê“à‚Å“®‚©‚¹‚é
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    int oldMousePointerIndex = -1;
    void Update()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(oldMousePointerIndex == mousePointerIndex) return; oldMousePointerIndex = mousePointerIndex;
        GetComponent<SpriteRenderer>().sprite = mousePointerImage[mousePointerIndex];
    }
}
