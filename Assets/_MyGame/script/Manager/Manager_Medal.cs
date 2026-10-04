using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMedalType
{
    none,

    Boots_speed_UP,
    Boots_speed_DOWN,

    Boots_Spike,

    EnemySpawn_UP,
    EnemySpawn_DOWN,

    EnemyFoot_speed_UP,
    EnemyFoot_speed_DOWN,



    [InspectorName("")]max,
}

public class Manager_Medal : MonoBehaviour
{
    Sprite[] medalImageBase = new Sprite[(int)eMedalType.max];
    public Sprite GetMedalImageBase(int index) { return medalImageBase[index]; }
    void SetMedalImageBase()
    {
        medalImageBase[(int)eMedalType.Boots_speed_UP] = boots_Speed_UPMedalImage;
        medalImageBase[(int)eMedalType.Boots_speed_DOWN] = boots_Speed_DOWNMedalImage;

        medalImageBase[(int)eMedalType.Boots_Spike] = boots_SpikeMedalImage;

        medalImageBase[(int)eMedalType.EnemySpawn_UP] = enemySpawn_UPMedalImage;
        medalImageBase[(int)eMedalType.EnemySpawn_DOWN] = enemySpawn_DOWNMedalImage;

        medalImageBase[(int)eMedalType.EnemyFoot_speed_UP] = EnemyFoot_Speed_UPMedalImage;
        medalImageBase[(int)eMedalType.EnemyFoot_speed_DOWN] = EnemyFoot_Speed_DOWNMedalImage;
    }
    [SerializeField] Sprite boots_Speed_UPMedalImage;
    [SerializeField] Sprite boots_Speed_DOWNMedalImage;
    [SerializeField] Sprite boots_SpikeMedalImage;
    [SerializeField] Sprite enemySpawn_UPMedalImage;
    [SerializeField] Sprite enemySpawn_DOWNMedalImage;
    [SerializeField] Sprite EnemyFoot_Speed_UPMedalImage;
    [SerializeField] Sprite EnemyFoot_Speed_DOWNMedalImage;


    public const int medalPocketNum = 3;
    public int GetMedalPocketNum() { return medalPocketNum; }
    [SerializeField]
    eMedalType[] medalType = new eMedalType[medalPocketNum];
    public eMedalType GetMedalType(int medalPocketNum_) { if (medalPocketNum_ < 0 || medalPocketNum_ >= medalPocketNum) return eMedalType.none; return medalType[medalPocketNum_]; }
    public void SetMedalType(int medalPocketNum_, eMedalType medalType_) { medalType[medalPocketNum_] = medalType_; }

    private void OnEnable()
    {
        SetMedalImageBase();
    }
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        BuffUpdate();
    }

    public void DataSave()
    {
        for (int i = 0; i < medalPocketNum; i++) PlayerPrefs.SetInt("MedalSet : " + i.ToString(), (int)medalType[i]);
    }
    public void DataLoad()
    {
        for (int i = 0; i < medalPocketNum; i++) medalType[i] = (eMedalType)PlayerPrefs.GetInt("MedalSet : " + i.ToString());
    }

    //ƒƒ_ƒ‹Œø‰Ê-------------------------------------
    eMedalType[] oldMedalType = new eMedalType[medalPocketNum];
    void BuffUpdate()
    {
        //if (oldMedalType[0] == medalType[0] && oldMedalType[1] == medalType[1] && oldMedalType[2] == medalType[2]) return;
        bool update = false;
        for (int i = 0; i < medalPocketNum; i++) { if (oldMedalType[i] == medalType[i]) continue; oldMedalType[i] = medalType[i]; update = true; }
        if (!update) return;

        moveSpeedBuff = 1;
        bootsSpike = false; 
        enemySpawnBuff = 1;
        enemyMoveSpeedBuff = 1;

        for (int i = 0; i < medalPocketNum; i++)
        {
            switch (medalType[i])
            {
                case eMedalType.none: continue;

                case eMedalType.Boots_speed_UP: moveSpeedBuff += 0.3f; continue;
                case eMedalType.Boots_speed_DOWN: moveSpeedBuff -= 0.3f; continue;

                case eMedalType.Boots_Spike: bootsSpike = true; continue;

                case eMedalType.EnemySpawn_UP: enemySpawnBuff += 1.4f;continue;
                case eMedalType.EnemySpawn_DOWN: enemySpawnBuff -= 1.4f;continue;

                case eMedalType.EnemyFoot_speed_UP: enemyMoveSpeedBuff *= 2f; continue;
                case eMedalType.EnemyFoot_speed_DOWN: enemyMoveSpeedBuff /= 2f; continue;
            }
        }
    }
    float moveSpeedBuff = 1;
    public float GetMoveSpeedBuff() { return moveSpeedBuff; }
    bool bootsSpike = false;
    public bool GetBootsSpike() { return bootsSpike; }
    float enemySpawnBuff = 1;
    public float GetEnemySpawnBuff() {  return enemySpawnBuff; }
    float enemyMoveSpeedBuff = 1;
    public float GetEnemyMoveSpeedBuff() { return enemyMoveSpeedBuff; }

}
