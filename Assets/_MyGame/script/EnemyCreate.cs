using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;



//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
    //public AudioSource setSound;

    enum eWaveType
    {
        None,

        bom,
        crow,
        golem,
        livingArmor,

        waveTypeMax
    }
    [SerializeField]
    eWaveType waveType = eWaveType.bom;

    public GameObject enemyObjectBom;
    public GameObject enemyObjectCrow;
    public GameObject enemyObjectgolem;
    public GameObject enemyObjectLivingArmor;

    enum eDirecttion
    {
        north,
        south,
        east,
        west,
    }

    GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        for (int i = 0; i < enemySpaunTime.Length; i++)
        {
            enemySpaunTime[i] = enemySpaunTimeReset[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpaunSchedule();
    }

    float waveTimer = 30;
    void EnemySpaunSchedule()
    {
        if (player.GetComponent<ObjectFall>().GetSituation() != ObjectFall.eSituation.normal) return;

        if (waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
        }
        else
        {
            if (waveType < eWaveType.waveTypeMax)
            {
                waveTimer = 30;
                waveType += 1;
            }
        }
        switch (waveType)
        {
            case eWaveType.bom: EnemySpawnTimer(eWaveType.bom); break;
            case eWaveType.crow: EnemySpawnTimer(eWaveType.bom); 
                if(GameObject.FindGameObjectWithTag("Player").GetComponent<playerMove>().GetMove() == new Vector2(0,0)) EnemySpawnTimer(eWaveType.crow); break;
            case eWaveType.golem: EnemySpawnTimer(eWaveType.bom);
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerMove>().GetMove() == new Vector2(0, 0)) EnemySpawnTimer(eWaveType.crow); 
                EnemySpawnTimer(eWaveType.golem); break;
            case eWaveType.livingArmor: EnemySpawnTimer(eWaveType.bom);
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerMove>().GetMove() == new Vector2(0, 0)) EnemySpawnTimer(eWaveType.crow); 
                EnemySpawnTimer(eWaveType.golem);
                EnemySpawnTimer(eWaveType.livingArmor); break;

            case eWaveType.waveTypeMax:EndGame();break;
            default:for (int i = 1; i < (int)eWaveType.waveTypeMax - 1; i++) EnemySpawnTimer((eWaveType)i);break;
        }
    }
    [SerializeField]
    float []enemySpaunTime = new float[(int)eWaveType.waveTypeMax];
    [SerializeField]
    float[] enemySpaunTimeReset = new float[(int)eWaveType.waveTypeMax];
    void EnemySpawnTimer(eWaveType enemyType)
    {
        GameObject spawnEnemy = null;
        switch (enemyType)
        {
            case eWaveType.bom: spawnEnemy = enemyObjectBom; break;
            case eWaveType.crow: spawnEnemy = enemyObjectCrow; break;
            case eWaveType.golem: spawnEnemy = enemyObjectgolem; break;
            case eWaveType.livingArmor: spawnEnemy = enemyObjectLivingArmor; break;
        }
        enemySpaunTime[(int)enemyType] -= Time.deltaTime;
        if (enemySpaunTime[(int)enemyType] < 0)
        {
            GameObject tmp = Instantiate<GameObject>(spawnEnemy);
            EnemySpaunPositionSet(tmp);
            enemySpaunTime[(int)enemyType] = enemySpaunTimeReset[(int)enemyType];
        }
    }
    void EndGame()
    {
        GameObject tmp = Instantiate<GameObject>(enemyObjectBom);
        EnemySpaunPositionSet(tmp);
        enemySpaunTime[(int)eWaveType.bom] = enemySpaunTimeReset[(int)eWaveType.bom];

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
                nextPosition = new Vector2(width, 12);
                break;
            case 1:
                nextPosition = new Vector2(width, -12);
                break;
            case 2:
                nextPosition = new Vector2(12, width);
                break;
            case 3:
                nextPosition = new Vector2(-12, width);
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
            case eWaveType.golem: return "GOLEM";
            case eWaveType.livingArmor: return "LIVING_ARMOR";
        }
        return "FREE";
    }
}
