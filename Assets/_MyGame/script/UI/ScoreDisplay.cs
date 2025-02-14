using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    ResultDisplaiPosSet.ePointType pointType = ResultDisplaiPosSet.ePointType.totalPoint;
    [SerializeField]
    bool old = false;
    // Start is called before the first frame update
    //void Start()
    //{
    //}

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<TextMeshProUGUI>().text != "null") return;
        GetComponent<TextMeshProUGUI>().text = transform.parent.parent.GetComponent<ResultDisplaiPosSet>().GetScore(pointType,old).ToString();
    }
}
