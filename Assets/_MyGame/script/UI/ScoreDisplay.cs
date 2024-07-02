using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    ResultDisplaiPosSet.ePointType pointType = ResultDisplaiPosSet.ePointType.totalPoint;
    int point = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = transform.parent.parent.GetComponent<ResultDisplaiPosSet>().GetScore(pointType).ToString();
        Debug.Log(transform.parent.parent.GetComponent<ResultDisplaiPosSet>().GetScore(pointType).ToString());
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
