using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

enum eScoreDisplayType
{
    none = -1,

    image,
    name,
    score,
    bestScore,
}

public class ScoreDisplay : MonoBehaviour
{
    Manager_Score manager_Score;

    [SerializeField]
    eScoreType scoreType = eScoreType.none;
    public void SetScoreType(eScoreType scoreType_) {  scoreType = scoreType_; }
    [SerializeField]
    eScoreDisplayType scoreDisplayType = eScoreDisplayType.none;
    // Start is called before the first frame update
    void Start()
    {
        manager_Score = GameObject.FindWithTag("Manager").GetComponent<Manager_Score>();
    }

    // Update is called once per frame
    eScoreType oldScoreType = eScoreType.none;
    void Update()
    {
        if(oldScoreType == scoreType)return; oldScoreType = scoreType;

        if (scoreDisplayType == eScoreDisplayType.none) { Debug.Log("scoreDisplayType == none"); return; }
        else if (scoreDisplayType == eScoreDisplayType.image) GetComponent<Image>().sprite = manager_Score.GetScoreImage(scoreType);
        else if (scoreDisplayType == eScoreDisplayType.name) GetComponent<TextMeshProUGUI>().text = scoreDisplayType.HumanName();
        else if (scoreDisplayType == eScoreDisplayType.score) GetComponent<TextMeshProUGUI>().text = manager_Score.GetScore(scoreType).ToString();
        else if (scoreDisplayType == eScoreDisplayType.bestScore) GetComponent<TextMeshProUGUI>().text = manager_Score.GetOldScore(scoreType).ToString();
    }
}
