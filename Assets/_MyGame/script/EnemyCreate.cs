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
    float enemySpaunTime;
    public float enemySpaunTimeReset = 3.0f;

    float waveTimer;
    enum eWaveType
    {
        None,

        bom,
        crow,
        fallBom,

        waveTimerMax
    }
    [SerializeField]
    eWaveType waveType;

    public GameObject enemyObjectBom;
    public GameObject enemyObjectCrow;
    public GameObject enemyObjectFallBom;
    [SerializeField]
    Vector3 nextPosition = new Vector3(12, 12, 0);

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
        enemySpaunTime = 0;

        waveTimer = 30;
        waveType = eWaveType.fallBom;
    }

    // Update is called once per frame
    void Update()
    {
        enemySpaunTime -= Time.deltaTime;
        if (waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
        }
        else
        {
            waveTimer = 30;
            waveType = (eWaveType)Random.Range(((int)eWaveType.None) + 1, ((int)eWaveType.waveTimerMax) - 1);
        }

        if (enemySpaunTime < 0)
        {
            EnemySpaun();
        }
    }

    void EnemySpaun()
    {
        GameObject spawnEnemy = null;
        switch (waveType)
        {
            case eWaveType.bom: spawnEnemy = enemyObjectBom; break;
            case eWaveType.crow: spawnEnemy = enemyObjectCrow; break;
            case eWaveType.fallBom: spawnEnemy = enemyObjectFallBom; break;
        }
        GameObject tmp = Instantiate<GameObject>(spawnEnemy);
        tmp.transform.parent = transform;
        tmp.transform.position = nextPosition;
        //if (waveType == eWaveType.bom)
        //{
        //    tmp.GetComponent<BombHit>().ExplosionSource = setSound;
        //}

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

        enemySpaunTime = enemySpaunTimeReset;
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