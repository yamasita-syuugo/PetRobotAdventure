using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum eScoreType
    {
        none,


        total,

        //positive
        Posi_Flag,
        Posi_Destroy,

        //negative
        Nega_EnemyBom,

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

        point[(int)eScoreType.Posi_Flag] = 0;
        point[(int)eScoreType.Posi_Destroy] = 0;

        point[(int)eScoreType.Nega_EnemyBom] = 0;

        point[(int)eScoreType.shot] = 0;
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    private static void TotalPointReset()
    {
        point[(int)eScoreType.total] = point[(int)eScoreType.Posi_Flag] + point[(int)eScoreType.Posi_Destroy] - point[(int)eScoreType.Nega_EnemyBom];
    }

    public static void FlagGetPointAdd()
    {
        point[(int)eScoreType.Posi_Flag]++;

        TotalPointReset();
    }
    public static void DestroyPointAdd()
    {
        point[(int)eScoreType.Posi_Destroy]++;

        TotalPointReset();
    }

    public static void EnemyBomPointAdd(int addPoint = 1)
    {
        point[(int)eScoreType.Nega_EnemyBom] += addPoint;

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
        return point[(int)eScoreType.Posi_Flag];
    }
    public static int GetDestroyPoint()
    {
        return point[(int)eScoreType.Posi_Destroy];
    }
    public static int GetEnemyBomPoint()
    {
        return point[(int)eScoreType.Nega_EnemyBom];
    }

    public static void ResultSend()
    {
        PlayerPrefs.SetInt("totalPoint", point[(int)eScoreType.total]);

        PlayerPrefs.SetInt("flagGetPoint", point[(int)eScoreType.Posi_Flag]);
        PlayerPrefs.SetInt("destroyPoint", point[(int)eScoreType.Posi_Destroy]);

        PlayerPrefs.SetInt("enemyBomPoint", point[(int)eScoreType.Nega_EnemyBom]);

        PlayerPrefs.Save();
    }
}