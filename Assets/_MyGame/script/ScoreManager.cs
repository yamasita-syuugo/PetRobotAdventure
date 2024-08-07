using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum eScoreType
    {
        none,


        total,

        //positive
        flag,
        destroy,

        //negative
        enemyBom,

        shot,


        scoreTypeMax,
    }
public class ScoreManager : MonoBehaviour
{
    static int []point = new int [(int)eScoreType.scoreTypeMax];
    // Start is called before the first frame update
    void Start()
    {
        point[(int)eScoreType.total] = 0;

        point[(int)eScoreType.flag] = 0;
        point[(int)eScoreType.destroy] = 0;

        point[(int)eScoreType.enemyBom] = 0;

        point[(int)eScoreType.shot] = 0;
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    private static void TotalPointReset()
    {
        point[(int)eScoreType.total] = point[(int)eScoreType.flag] + point[(int)eScoreType.destroy] - point[(int)eScoreType.enemyBom];
    }

    public static void FlagGetPointAdd()
    {
        point[(int)eScoreType.flag]++;

        TotalPointReset();
    }
    public static void DestroyPointAdd()
    {
        point[(int)eScoreType.destroy]++;

        TotalPointReset();
    }

    public static void EnemyBomPointAdd(int addPoint = 1)
    {
        point[(int)eScoreType.enemyBom] += addPoint;

        TotalPointReset();
    }

    public static void ShotNumAdd()
    {
        point[(int)eScoreType.shot]++;
    }

    public static int GetTotalPoint()
    {
        return point[(int)eScoreType.total];
    }
    public static int GetFlagGetPoint()
    {
        return point[(int)eScoreType.flag];
    }
    public static int GetDestroyPoint()
    {
        return point[(int)eScoreType.destroy];
    }
    public static int GetEnemyBomPoint()
    {
        return point[(int)eScoreType.enemyBom];
    }

    public static void ResultSend()
    {
        PlayerPrefs.SetInt("totalPoint", point[(int)eScoreType.total]);

        PlayerPrefs.SetInt("flagGetPoint", point[(int)eScoreType.flag]);
        PlayerPrefs.SetInt("destroyPoint", point[(int)eScoreType.destroy]);

        PlayerPrefs.SetInt("enemyBomPoint", point[(int)eScoreType.enemyBom]);

        PlayerPrefs.Save();
    }
}