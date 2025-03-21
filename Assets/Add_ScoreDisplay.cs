using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Add_ScoreDisplay : MonoBehaviour
{
    Manager_Score manager_Score;

    GameObject[] score = new GameObject[(int)eScoreType.max];
    // Start is called before the first frame update
    void Start()
    {
        manager_Score = GameObject.FindWithTag("Manager").GetComponent<Manager_Score>();

        CreatScoreCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if(manager_Score.ScoreUpdateCheck())CreatScoreCheck();
    }

    void CreatScoreCheck()
    {
        int count = 0;
        for (int scoreType = 0; scoreType < (int)eScoreType.max; scoreType++)
        {
            if (manager_Score.GetScore((eScoreType)scoreType) <= 0) continue;

            CreatScoreUI((eScoreType)scoreType);
            score[scoreType].transform.localPosition = new Vector3(0, count * -50, 0);

            count++;
        }
    }
    void CreatScoreUI(eScoreType scoreType)
    {
        if (score[(int)scoreType] != null) return;
        score[(int)scoreType] = Instantiate<GameObject>(new GameObject());
        SpriteRenderer tmp_sp = score[(int)scoreType].AddComponent<SpriteRenderer>();
        tmp_sp.sprite = manager_Score.GetScoreImage(scoreType);
            score[(int)scoreType].transform.parent = transform;
            score[(int)scoreType].transform.localScale = new Vector3(50,50,1);

        GameObject tmp = Instantiate<GameObject>(new GameObject());
        tmp.transform.parent = score[(int)scoreType].transform;
        tmp.transform.localPosition = new Vector3(1,0,0);
        tmp.transform.localScale = new Vector3(1,1,1);
        ScoreDisplay scoreDisplay = tmp.AddComponent<ScoreDisplay>();
        scoreDisplay.SetScoreType(scoreType);
        scoreDisplay.SetScoreDisplayType(eScoreDisplayType.score);
        tmp.AddComponent<TextMeshProUGUI>();
    }
}
