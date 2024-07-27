using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockUI : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        UIUpDate();
    }
    bool oldDisplay = false;
    public void UIUpDate()
    {
        bool display = GetComponentInParent<PlayerCreateScaffold>().GetBlockGetNum() > 0;
        if (oldDisplay == display) return;
        oldDisplay = display;
        if(display) GetComponent<SpriteRenderer>().color = Color.white;
        else GetComponent<SpriteRenderer>().color = Color.clear;
    }
}
