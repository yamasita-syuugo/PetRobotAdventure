using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum eScoreDisplayType
{
    none = -1,

    image,
    name,
    score,
    bestScore,
    bestScoreUpDate,
}

public class ScoreDisplay : MonoBehaviour
{
    Manager_Score manager_Score;

    [SerializeField]
    eScoreType scoreType = eScoreType.none;
    public void SetScoreType(eScoreType scoreType_) { scoreType = scoreType_; }
    [SerializeField]
    eScoreDisplayType scoreDisplayType = eScoreDisplayType.none;
    public void SetScoreDisplayType(eScoreDisplayType scoreDisplayType_) {  scoreDisplayType = scoreDisplayType_; }
    // Start is called before the first frame update
    void Start()
    {
        manager_Score = GameObject.FindWithTag("Manager").GetComponent<Manager_Score>();
    }

    // Update is called once per frame
    eScoreType oldScoreType = eScoreType.none;
    int oldScore = -1;
    void Update()
    {
        if (scoreDisplayType == eScoreDisplayType.score) { if (oldScore == manager_Score.GetScore(scoreType)) return; oldScore = manager_Score.GetScore(scoreType); }
        else if (oldScoreType == scoreType) return; oldScoreType = scoreType;

        switch (scoreDisplayType)
        {
            case eScoreDisplayType.none: Debug.Log("scoreDisplayType == none"); return;
            case eScoreDisplayType.image: GetComponent<Image>().sprite = manager_Score.GetScoreImage(scoreType); break;
            case eScoreDisplayType.name: GetComponent<TextMeshProUGUI>().text = oldScoreType.ToString(); break;
            case eScoreDisplayType.score: GetComponent<TextMeshProUGUI>().text = manager_Score.GetScore(scoreType).ToString(); break;
            case eScoreDisplayType.bestScore: GetComponent<TextMeshProUGUI>().text = manager_Score.GetOldScore(scoreType).ToString(); break;
            case eScoreDisplayType.bestScoreUpDate:
                if (manager_Score.GetOldScoreUpDate((int)scoreType)) GetComponent<Image>().color = Color.white;
                else GetComponent<Image>().color = Color.clear; break;
        }
    }
}
