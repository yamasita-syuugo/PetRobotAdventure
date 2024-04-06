using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    static int totalPoint;

    static int flagGetPoint;
    static int destroyPoint;

    static int shotNum;
    // Start is called before the first frame update
    void Start()
    {
        totalPoint = 0;

        flagGetPoint = 0;
        destroyPoint = 0;

        shotNum = 0;
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    private static void TotalPointReset()
    {
        totalPoint = flagGetPoint + destroyPoint;
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

    public static void ShotNumAdd()
    {
        shotNum++;
    }
}