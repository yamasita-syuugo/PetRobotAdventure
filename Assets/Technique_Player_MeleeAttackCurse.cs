using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technique_Player_MeleeAttackCurse : Technique_Player__ContainerBase
{
    // Start is called before the first frame update
    void Start()
    {
        endCount = new float[endNum];

    }

    // Update is called once per frame
    void Update()
    {
        EndGame();
    }

    public override void GetPoint() { }

    bool end = false;
    [SerializeField]
    int endNum = 2;
    float[] endCount;
    public void SetEndCount()
    {
        float time = 0;
        int num = 0;
        for (int i = 0; i < endCount.Length; i++)
        {
            if (endCount[i] <= time)
            {
                time = endCount[i];
                num = i;
            }
        }
        endCount[num] = 1;
    }
    int endGameCount = 0;
    void EndGame()
    {
        if (!end)
        {
            endGameCount = 0;
            for (int i = 0; i < endCount.Length; i++)
            {
                if (endCount[i] > 0)
                {
                    endCount[i] -= Time.deltaTime;
                    endGameCount++;
                }
            }
            if (endGameCount >= endNum) end = true;
        }
        else
        {

        }
    }
    public int GetEndCountLength()
    {
        return endCount.Length;
    }
    public int GetEnemyCountNum()
    {
        return endGameCount;
    }
}
