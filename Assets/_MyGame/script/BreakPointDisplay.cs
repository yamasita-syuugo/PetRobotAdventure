using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BreakPointDisplay : MonoBehaviour
{
    [SerializeField]
    ScoreManager scoreManager;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        float destroyPoint = scoreManager.GetDestroyPoint();
        GetComponent<TextMeshProUGUI>().text = destroyPoint.ToString("00");
    }
}
