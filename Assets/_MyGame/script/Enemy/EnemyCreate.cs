using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Xml.Serialization;
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
        enemyMass,

        waveTypeMax
    }
    [SerializeField]
    eWaveType waveType = eWaveType.bom;

    public GameObject enemyObjectBom;
    public GameObject enemyObjectCrow;
    public GameObject enemyObjectGolem;
    public GameObject enemyObjectLivingArmor;
    public GameObject enemyObjectEnemyMass;

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
            enemySpaunTime[i] = 0;//enemySpaunTimeReset[i];
        }

        golemCount = golemCountReset;
        livingArmorCount = livingArmorCountReset;

        endCount = new float[endNum];
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
            if (waveType < eWaveType.waveTypeMax - 1)
            {
                waveTimer = 30;
                waveType += 1;
            }
        }
        EnemySpawnTimer(eWaveType.bom);
        switch (waveType)
        {
            case eWaveType.bom:  break;
            case eWaveType.crow: 
                EnemySpawnCrow(); break;
            case eWaveType.golem:
                EnemySpawnCrow();
                EnemySpawnGolem(); break;
            case eWaveType.livingArmor:
                EnemySpawnCrow();
                EnemySpawnGolem();
                EnemySpawnLivingArmor(); break;
            case eWaveType.enemyMass:
                EnemySpawnCrow();
                EnemySpawnGolem();
                EnemySpawnLivingArmor();
                EnemySpawnEnemyMass();
                break;

            case eWaveType.waveTypeMax:EndGame();break;
            default:for (int i = 1; i < (int)eWaveType.waveTypeMax - 1; i++) EnemySpawnTimer((eWaveType)i);break;
        }
        BomSpawn();
        EndGame();
    }
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
            case eWaveType.golem: spawnEnemy = enemyObjectGolem; break;
            case eWaveType.livingArmor: spawnEnemy = enemyObjectLivingArmor; break;
            case eWaveType.enemyMass: spawnEnemy = enemyObjectEnemyMass; break;
        }
        enemySpaunTime[(int)enemyType] -= Time.deltaTime;
        if (enemySpaunTime[(int)enemyType] < 0)
        {
            GameObject tmp = Instantiate<GameObject>(spawnEnemy);
            EnemySpaunPositionSet(tmp);

            enemySpaunTime[(int)enemyType] = enemySpaunTimeReset[(int)enemyType];
        }
    }
    void EnemySpawnCrow()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerMove>().GetMove() == new Vector2(0, 0)) EnemySpawnTimer(eWaveType.crow);
        else enemySpaunTime[(int)eWaveType.crow] = enemySpaunTimeReset[(int)eWaveType.crow];
    }
    [SerializeField]
    int golemCountReset = 3;
    int golemCount = 0;
    void EnemySpawnGolem()
    {
        if(golemCount <= 0)
        {
            GameObject tmp = Instantiate<GameObject>(enemyObjectGolem);
            EnemySpaunPositionSet(tmp);
            golemCount = golemCountReset;
        }
    }
    [SerializeField]
    int livingArmorCountReset = 15;
    int livingArmorCount = 0;
    void EnemySpawnLivingArmor()
    {
        if (livingArmorCount <= 0)
        {
            GameObject tmp = Instantiate<GameObject>(enemyObjectLivingArmor);
            EnemySpaunPositionSet(tmp);
            livingArmorCount = livingArmorCountReset;
        }
    }
    void EnemySpawnEnemyMass()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShot>().GetMagazine() >= 7) EnemySpawnTimer(eWaveType.enemyMass);
        else enemySpaunTime[(int)eWaveType.enemyMass] = enemySpaunTimeReset[(int)eWaveType.enemyMass];
    }
    bool end = false;
    [SerializeField]
    int endNum = 4;
    float[] endCount ;
    public void SetEndCount()
    {
        float time = 0;
        int num = 0; 
        for(int i = 0;i < endCount.Length; i++)
        {
            if (endCount[i] <= time)
            {
                time = endCount[i];
                num = i;
            }
        }
        endCount[num] = 1;
    }
    int bomSpawnNum = 0;
    public void SetBomSpawnNum(int num)
    {
        bomSpawnNum += num;
    }
    void BomSpawn()
    {
        if (bomSpawnNum <= 0) return;

        GameObject tmp = Instantiate<GameObject>(enemyObjectBom);
        EnemySpaunPositionSet(tmp);

        bomSpawnNum--;
    }
    void EndGame()
    {
        if (!end)
        {
            int count = 0;
            for(int i = 0;i < endCount.Length; i++)
            {
                if (endCount[i] > 0)
                {
                    endCount[i] -= Time.deltaTime;
                    count++;
                }
            }
            if (count == endNum) end = true;
        }
        else
        {
            GameObject tmp = Instantiate<GameObject>(enemyObjectBom);
            EnemySpaunPositionSet(tmp);
            enemySpaunTime[(int)eWaveType.bom] = enemySpaunTimeReset[(int)eWaveType.bom];
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

    public void GolemCountAdd()
    {
        golemCount--;
    } 
    public void LivingArmorCountAdd()
    {
        livingArmorCount--;
    } 
    public void LivingArmorCountAdd(int count)
    {
        livingArmorCount -= count;
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
