using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_Situation_Gate : MonoBehaviour
{
    Image sprite;

    [SerializeField]SpriteRenderer gate;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gate == null)gate = GameObject.FindWithTag("Gate").GetComponent<SpriteRenderer>();

        sprite.sprite = gate.sprite;
    }
}
