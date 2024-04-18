using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIUpDate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIUpDate()
    {
        float color = ((float)GetComponentInParent<PlayerHit>().GetBlockGetNum()) / GetComponentInParent<PlayerHit>().GetFlagToBlock();
        if (color >= 1) color = 0;

        Color UIColor = new Color(color, color, color, color);
        GetComponent<SpriteRenderer>().color = UIColor;
    }
}
