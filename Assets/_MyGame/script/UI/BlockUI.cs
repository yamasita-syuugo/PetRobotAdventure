using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIUpDate();
    }

    public void UIUpDate()
    {
        float color = ((float)GetComponentInParent<PlayerCreateScaffold>().GetBlockGetNum()) / GetComponentInParent<PlayerCreateScaffold>().GetFlagToBlock();
        if (color >= 1) color = 0;

        Color UIColor = new Color(color, color, color, color);
        GetComponent<SpriteRenderer>().color = UIColor;
    }
}
