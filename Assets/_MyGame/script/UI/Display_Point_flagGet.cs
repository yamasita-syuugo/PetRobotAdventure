using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Display_Point_flagGet : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        float time = Manager_Score.GetFlagGetPoint();
        GetComponent<TextMeshProUGUI>().text = time.ToString("00");
    }
}
