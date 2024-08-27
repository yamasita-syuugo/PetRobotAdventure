using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Xml.Serialization;
using UnityEditor;



//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    //public AudioSource setSound;

    public GameObject enemyObjectBom;
    public GameObject enemyObjectCrow;
    public GameObject enemyObjectGolem;
    public GameObject enemyObjectLivingArmor;
    public GameObject enemyObjectEnemyMass;

    public GameObject enemyObjectBoss;
    [SerializeField]
    GameObject[] enemyObjectBase = new GameObject[(int)eEnemyType.enemyTypeMax];
    public GameObject GetEnemyObjectBase(eEnemyType enemyType) { return enemyObjectBase[(int)enemyType]; }

    enum eDirecttion
    {
        north,
        south,
        east,
        west,
    }

    GameObject player = null;
    TimeManager timeManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();

        for (int i = 0; i < enemySpaunTime.Length; i++)
        {
            enemySpaunTime[i] = 0;
        }

        golemCount = 0;
        livingArmorCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpaunSchedule();
    }
    void EnemySpaunSchedule()
    {
        if (player.GetComponent<ObjectFall>().GetSituation() != ObjectFall.eSituation.normal) return;

        if (timeManager.GetTimeStop()) return;
        EnemySpawnTimer(eWaveType.bom);
        switch (timeManager.GetComponent<WaveManager>().GetWaveType())
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
        if (enemySpaunTime[(int)enemyType] <= 0)
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
    int golemCountReset = 1;
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
    int oldEnemyScore = 0;
    bool enemyMassSpawn = false;
    void EnemySpawnEnemyMass()
    {
        if(oldEnemyScore != ScoreManager.GetEnemyBomPoint())
        {
            oldEnemyScore = ScoreManager.GetEnemyBomPoint();
            enemyMassSpawn = true;
        }

        if (!enemyMassSpawn) return;
        GameObject tmp = Instantiate<GameObject>(enemyObjectEnemyMass);
        EnemySpaunPositionSet(tmp);
        enemyMassSpawn = false;
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
    bool endGame = false;
    void EndGame()
    {
        if(endGame)
        {
            GameObject tmp = Instantiate<GameObject>(enemyObjectBom);
            EnemySpaunPositionSet(tmp);
            enemySpaunTime[(int)eWaveType.bom] = enemySpaunTimeReset[(int)eWaveType.bom];
        }
    }
    public void EnemyCreate(GameObject enemy)
    {
        if (GetComponent<EnemyType>() == null) return;
        GameObject tmp = Instantiate<GameObject>(enemy);
        EnemySpaunPositionSet(tmp);

    }
    [SerializeField]
    Vector3 nextPosition = new Vector3(12, 12, 0);
    void EnemySpaunPositionSet(GameObject spawnEnemy)//‰æ–ÊŠO‚ÉˆÚ“®
    {
        spawnEnemy.transform.parent = transform;
        spawnEnemy.transform.position = nextPosition;

        int directtion = Random.Range(0, 4);
        float x = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 1.1f, Camera.main.nearClipPlane)).x;
        float y = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 1.1f, Camera.main.nearClipPlane)).y;
        float width = Random.Range(-x, x);
        float height = Random.Range(-y, y);
        switch (directtion)
        {
            case 0:
                nextPosition = new Vector2(width, y);
                break;
            case 1:
                nextPosition = new Vector2(width, -y);
                break;
            case 2:
                nextPosition = new Vector2(x, height);
                break;
            case 3:
                nextPosition = new Vector2(-x, height);
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
}
