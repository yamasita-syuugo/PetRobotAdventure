using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEditor.Experimental.GraphView;


//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
    //public AudioSource setSound;
    public float enemySpaunTimeReset = 3.0f;

    enum eWaveType
    {
        None,

        bom,
        crow,
        fallBom,

        waveTimerMax
    }
    [SerializeField]
    eWaveType waveType = eWaveType.bom;

    public GameObject enemyObjectBom;
    public GameObject enemyObjectCrow;
    public GameObject enemyObjectFallBom;

    enum eDirecttion
    {
        north,
        south,
        east,
        west,
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpaunSchedule();
    }

    float waveTimer = 30;
    void EnemySpaunSchedule()
    {
        if (waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
        }
        else
        {
            waveTimer = 30;
            waveType = (eWaveType)Random.Range(((int)eWaveType.None) + 1, ((int)eWaveType.waveTimerMax) - 1);
        }

            switch (waveType)
            {
                case eWaveType.bom: EnemySpaun(enemyObjectBom,eWaveType.bom); break;
                case eWaveType.crow: EnemySpaun(enemyObjectCrow, eWaveType.crow); break;
                case eWaveType.fallBom: EnemySpaun(enemyObjectFallBom, eWaveType.fallBom); break;
            }
    }
    float []enemySpaunTime = new float[(int)eWaveType.waveTimerMax];
    void EnemySpaun(GameObject spawnEnemy,eWaveType enemyType)
    {
        enemySpaunTime[(int)enemyType] -= Time.deltaTime;
        if (enemySpaunTime[(int)enemyType] < 0)
        {
            GameObject tmp = Instantiate<GameObject>(spawnEnemy);
            EnemySpaunPositionSet(tmp);
            enemySpaunTime[(int)enemyType] = enemySpaunTimeReset;
        }
    }
    [SerializeField]
    Vector3 nextPosition = new Vector3(12, 12, 0);
    void EnemySpaunPositionSet(GameObject spawnEnemy)
    {
        spawnEnemy.transform.parent = transform;
        spawnEnemy.transform.position = nextPosition;

        int directtion = Random.Range(0, 4);
        float width = Random.Range(-11, 11);
        switch (directtion)
        {
            case 0:
                nextPosition = new Vector3(width, 12, 0);
                break;
            case 1:
                nextPosition = new Vector3(width, -12, 0);
                break;
            case 2:
                nextPosition = new Vector3(12, width, 0);
                break;
            case 3:
                nextPosition = new Vector3(-12, width, 0);
                break;
        }
    }


    public float GetWaveTime()
    {
        return waveTimer;
    }
    public string GetWaveName()
    {
        switch (waveType)
        {
            case eWaveType.bom: return "BOM";
            case eWaveType.crow: return "CROW";
            case eWaveType.fallBom: return "FALLBOM";
        }
        return "FREE";
    }
}
