using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
    // Start is called before the first frame update
    public enum eScoreType
    {
        totalPoint,
       
        flagGetPoint,
        destroyPoint,

        enemyBomPoint,
    }
    [SerializeField]
    eScoreType scoreType = eScoreType.totalPoint;
    void Start()
    {
        int score = 0;
        switch (scoreType)
        {
            case eScoreType.totalPoint: score = ScoreManager.GetTotalPoint(); break;

            case eScoreType.flagGetPoint: score = ScoreManager.GetFlagGetPoint(); break;
            case eScoreType.destroyPoint: score = ScoreManager.GetDestroyPoint(); break;

            case eScoreType.enemyBomPoint: score = ScoreManager.GetEnemyBomPoint(); break;

        }
        GetComponent<TextMeshPro>()/*.text = score.ToString("00")*/;
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
