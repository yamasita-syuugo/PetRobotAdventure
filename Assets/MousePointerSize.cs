using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMousePointerSize
{
    [InspectorName("")]None = -1,

    baseSize,
    bigSize,
}

public class MousePointerSize : MonoBehaviour
{
    eMousePointerSize mousePointerSize = eMousePointerSize.baseSize;
    public void SetMousePointerSize(eMousePointerSize mousePointerSize_) {  mousePointerSize = mousePointerSize_;}

    float baseSize = 0.3f;
    float bigSize = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector2 (baseSize, baseSize);
    }

    // Update is called once per frame
    eMousePointerSize oldMousePointerSize = eMousePointerSize.None;
    void Update()
    {
        if (oldMousePointerSize == mousePointerSize) return; oldMousePointerSize = mousePointerSize;
        switch (mousePointerSize)
        {
            case eMousePointerSize.baseSize:
                transform.localScale = new Vector2(baseSize, baseSize);
                break;
            case eMousePointerSize.bigSize:
                transform.localScale = new Vector2(bigSize, bigSize);
                break;
        }
    }
}
