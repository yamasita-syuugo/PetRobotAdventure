using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text_Delete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<TextMeshProUGUI>() != null) GetComponent<TextMeshProUGUI>().text = "";
        else if (GetComponent<TextMeshPro>() != null) GetComponent<TextMeshPro>().text = "";
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
