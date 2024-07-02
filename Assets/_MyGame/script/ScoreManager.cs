using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    static int totalPoint = 0;

    static int flagGetPoint = 0;
    static int destroyPoint = 0;

    static int enemyBomPoint = 0;

    static int shotNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        totalPoint = 0;

        flagGetPoint = 0;
        destroyPoint = 0;

        enemyBomPoint = 0;

        shotNum = 0;
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    private static void TotalPointReset()
    {
        totalPoint = flagGetPoint + destroyPoint - enemyBomPoint;
    }

    public static void FlagGetPointAdd()
    {
        flagGetPoint++;

        TotalPointReset();
    }
    public static void DestroyPointAdd()
    {
        destroyPoint++;

        TotalPointReset();
    }

    public static void EnemyBomPointAdd()
    {
        enemyBomPoint++;

        TotalPointReset();
    }

    public static void ShotNumAdd()
    {
        shotNum++;
    }

    public static int GetTotalPoint()
    {
        return totalPoint;
    }
    public static int GetFlagGetPoint()
    {
        return flagGetPoint;
    }
    public static int GetDestroyPoint()
    {
        return destroyPoint;
    }
    public static int GetEnemyBomPoint()
    {
        return enemyBomPoint;
    }

    public static void ResultSend()
    {
        PlayerPrefs.SetInt("totalPoint", totalPoint);

        PlayerPrefs.SetInt("flagGetPoint", flagGetPoint);
        PlayerPrefs.SetInt("destroyPoint", destroyPoint);

        PlayerPrefs.SetInt("enemyBomPoint", enemyBomPoint);

        PlayerPrefs.Save();
    }
}