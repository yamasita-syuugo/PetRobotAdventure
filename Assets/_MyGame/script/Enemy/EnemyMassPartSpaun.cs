using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyMassPartSpaun : MonoBehaviour
{
    [SerializeField]
    GameObject baseObject;
    [SerializeField]
    int objectInNum = 4;
    GameObject[] enemyMassParteIn; 
    [SerializeField]
    int objectOutNum = 7;
    GameObject[] enemyMassParteOut; 
    // Start is called before the first frame update
    void Start()
    {
        enemyMassParteIn = new GameObject[objectInNum];
        for(int i = 0; i < enemyMassParteIn.Length; i++)
        {
            enemyMassParteIn[i] = Instantiate<GameObject>(baseObject);
            enemyMassParteIn[i].transform.parent = transform;
        }
        enemyMassParteOut = new GameObject[objectOutNum];
        for(int i = 0; i < enemyMassParteOut.Length; i++)
        {
            enemyMassParteOut[i] = Instantiate<GameObject>(baseObject);
            enemyMassParteOut[i].transform.parent = transform;
        }
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
    }

    float allTheta = 0;
    void SetPosition()
    {
        bool breakOn = true;
        //ì‡:3Å`6
        //äO:4Å`7
        for(int i = 0;i < enemyMassParteIn.Length;i++)
        {
            if (enemyMassParteIn[i] == null) continue;
            breakOn = false;

            float thisTheta = i / (float)enemyMassParteIn.Length + allTheta;
            enemyMassParteIn[i].transform.position = new Vector2(math.sin(thisTheta * 3.14f * 2) * 0.5f, math.cos(thisTheta * 3.14f * 2) * 0.5f) + (Vector2)transform.position;
        }
        for(int i = 0;i < enemyMassParteOut.Length;i++)
        {
            if (enemyMassParteOut[i] == null) continue;
            breakOn = false;

            float thisTheta = i / (float)enemyMassParteOut.Length + allTheta;
            enemyMassParteOut[i].transform.position = new Vector2(math.sin(thisTheta * 3.14f * -2) * 1, math.cos(thisTheta * 3.14f * -2) * 1) + (Vector2)transform.position;
        }
        allTheta += Time.deltaTime / 2;

        if(breakOn)Destroy(gameObject);
    }
}
