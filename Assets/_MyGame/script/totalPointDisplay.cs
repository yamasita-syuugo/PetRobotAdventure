using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class totalPointDisplay : MonoBehaviour
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
        float time = scoreManager.GetTotalPoint();
        GetComponent<TextMeshProUGUI>().text = time.ToString("00");
    }
}