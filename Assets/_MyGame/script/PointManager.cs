using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PointManager : MonoBehaviour
{
    static int totalPoint;

    static int flagPoint;
    static int shotPoint;
    // Start is called before the first frame update
    void Start()
    {
        totalPoint = 0;

        flagPoint = 0;
        shotPoint = 0;
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    private static void ToralPointReset()
    {
        totalPoint = flagPoint + shotPoint;
    }

    public static void FlagPointAdd()
    {
        flagPoint++;

        ToralPointReset();
    }
    public static void ShotPointAdd()
    {
        shotPoint++;

        ToralPointReset();
    }
}