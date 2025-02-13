using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Xml.Serialization;
using UnityEditor;



//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Create_Enemy : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;

    eStage stage;
    //public AudioSource setSound;

    public GameObject enemyObjectBoss;
    [SerializeField]
    GameObject[] enemyObjectBase = new GameObject[(int)eEnemyType.max];
    public GameObject GetEnemyObjectBase(eEnemyType enemyType) { return enemyObjectBase[(int)enemyType]; }

    enum eDirecttion
    {
        north,
        south,
        east,
        west,
    }

    GameObject player = null;
    Manager_Time timeManager;
    // Start is called before the first frame update
    void Start()
    {
        manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();

        stage = manager_StageSelect.GetStage();

        player = GameObject.FindWithTag("Player");
        timeManager = GameObject.FindWithTag("Manager").GetComponent<Manager_Time>();

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
        if(player == null) { player = GameObject.FindWithTag("Player"); return; }
        if (player.GetComponent<ObjectFall>().GetSituation() != ObjectFall.eSituation.normal) return;
        if (timeManager.GetTimeStop()) return;

        stStageData stageData = manager_StageSelect.GetStageData(stage);
        for(int i = 0;i < (int)eEnemyType.max; i++) { if (stageData.GetEnemySerect((eEnemyType)i)) EnemySpawnTimer((eEnemyType)i); }

        //switch (stage)
        //{
        //    case eStage.fastPlay:
        //        EnemySpawnTimer(eWaveType.bom);
        //        break;
        //    case eStage.crowStage:
        //        EnemySpawnTimer(eWaveType.crow);
        //        break;
        //    case eStage.golemLabyrinth:
        //        EnemySpawnTimer(eWaveType.golem);
        //        break;

        //    case eStage.lastGame:
        //        BomSpawn();
        //        switch (timeManager.GetComponent<Manager_Wave>().GetWaveType())
        //        {
        //            case eWaveType.bom: break;
        //            case eWaveType.crow:
        //                EnemySpawnCrow(); break;
        //            case eWaveType.golem:
        //                EnemySpawnCrow();
        //                EnemySpawnGolem(); break;
        //            case eWaveType.livingArmor:
        //                EnemySpawnCrow();
        //                EnemySpawnGolem();
        //                EnemySpawnLivingArmor(); break;
        //            case eWaveType.enemyMass:
        //                EnemySpawnCrow();
        //                EnemySpawnGolem();
        //                EnemySpawnLivingArmor();
        //                EnemySpawnEnemyMass();
        //                break;

        //            case eWaveType.max: EndGame(); break;
        //            default: for (int i = 1; i < (int)eWaveType.max - 1; i++) EnemySpawnTimer((eWaveType)i); break;
        //        }
        //        break;
        //    default: Debug.Log("error : switch(eStage)"); break;
        //}
        EndGame();
    }
    float []enemySpaunTime = new float[(int)eEnemyType.max];
    [SerializeField]
    float[] enemySpaunTimeReset = new float[(int)eEnemyType.max];
    void EnemySpawnTimer(eEnemyType enemyType)
    {
        GameObject spawnEnemy = null;
        spawnEnemy = enemyObjectBase[(int)enemyType];
        //switch (enemyType)
        //{
        //    case eEnemyType.bom:  break;
        //    case eEnemyType.crow: spawnEnemy = enemyObjectBase[(int)eEnemyType.Crow]; break;
        //    case eEnemyType.golem: spawnEnemy = enemyObjectBase[(int)eEnemyType.Golem]; break;
        //    case eEnemyType.livingArmor: spawnEnemy = enemyObjectBase[(int)eEnemyType.LivingArmor]; break;
        //    case eEnemyType.enemyMass: spawnEnemy = enemyObjectBase[(int)eEnemyType.EnemyMass]; break;
        //}
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
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<player_Move>().GetMove() == new Vector2(0, 0)) EnemySpawnTimer(eEnemyType.crow);
        else enemySpaunTime[(int)eEnemyType.crow] = enemySpaunTimeReset[(int)eEnemyType.crow];
    }
    [SerializeField]
    int golemCountReset = 1;
    int golemCount = 0;
    void EnemySpawnGolem()
    {
        if(golemCount <= 0)
        {
            GameObject tmp = Instantiate<GameObject>(enemyObjectBase[(int)eEnemyType.golem]);
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
            GameObject tmp = Instantiate<GameObject>(enemyObjectBase[(int)eEnemyType.livingArmor]);
            EnemySpaunPositionSet(tmp);
            livingArmorCount = livingArmorCountReset;
        }
    }
    int oldEnemyScore = 0;
    bool enemyMassSpawn = false;
    void EnemySpawnEnemyMass()
    {
        if(oldEnemyScore != Manager_Score.GetEnemyBomPoint())
        {
            oldEnemyScore = Manager_Score.GetEnemyBomPoint();
            enemyMassSpawn = true;
        }

        if (!enemyMassSpawn) return;
        GameObject tmp = Instantiate<GameObject>(enemyObjectBase[(int)eEnemyType.enemyMass]);
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

        GameObject tmp = Instantiate<GameObject>(enemyObjectBase[(int)eEnemyType.bom]);
        EnemySpaunPositionSet(tmp);

        bomSpawnNum--;
    }
    bool endGame = false;
    void EndGame()
    {
        if(endGame)
        {
            GameObject tmp = Instantiate<GameObject>(enemyObjectBase[(int)eEnemyType.bom]);
            EnemySpaunPositionSet(tmp);
            enemySpaunTime[(int)eEnemyType.bom] = enemySpaunTimeReset[(int)eEnemyType.bom];
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
