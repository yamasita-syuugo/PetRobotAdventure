using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Display_Point_Break : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        float destroyPoint = Manager_Score.GetDestroyPoint();
        GetComponent<TextMeshProUGUI>().text = destroyPoint.ToString("00");
    }
}
