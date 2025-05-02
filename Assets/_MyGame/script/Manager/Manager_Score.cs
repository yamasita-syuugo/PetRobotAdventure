using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum eScoreType
    {
        none = -1,


        total,

        //positive
        Get_Flag,
        Destroy_,
        Destroy_Max = eScoreType.Destroy_ + eEnemyType.max,

        //negative
        Enemy_,
        Enemy_Max = eScoreType.Enemy_ + eEnemyType.max,

        shot,


        max,
    }
public class Manager_Score : MonoBehaviour
{
    static Sprite[] scoreImage = new Sprite[(int)eScoreType.max];
    public Sprite GetScoreImage(eScoreType scoreType) { return scoreImage[(int)scoreType]; }
    void SetScoreImage()
    {
        scoreImage[(int)eScoreType.total] = totalImage;
        scoreImage[(int)eScoreType.Get_Flag] = Get_FlagImage;
        scoreImage[(int)eScoreType.Destroy_ + (int)eEnemyType.bom] = Destroy_BomImage;
        scoreImage[(int)eScoreType.Destroy_ + (int)eEnemyType.crow] = Destroy_CrowImage;
        scoreImage[(int)eScoreType.Destroy_ + (int)eEnemyType.golem] = Destroy_GolemImage;
        scoreImage[(int)eScoreType.Destroy_ + (int)eEnemyType.livingArmor] = Destroy_LivingArmorImage;
        scoreImage[(int)eScoreType.Destroy_ + (int)eEnemyType.enemyMass] = Destroy_EnemyMassImage;
        scoreImage[(int)eScoreType.Destroy_ + (int)eEnemyType.bossEnemy] = Destroy_BossEnemyImage;
        scoreImage[(int)eScoreType.Enemy_ + (int)eEnemyType.bom] = Enemy_BomImage;
        scoreImage[(int)eScoreType.Enemy_ + (int)eEnemyType.crow] = Enemy_CrowImage;
        scoreImage[(int)eScoreType.Enemy_ + (int)eEnemyType.golem] = Enemy_GolemImage;
        scoreImage[(int)eScoreType.Enemy_ + (int)eEnemyType.livingArmor] = Enemy_GolemImage;
        scoreImage[(int)eScoreType.Enemy_ + (int)eEnemyType.enemyMass] = Enemy_GolemImage;
        scoreImage[(int)eScoreType.Enemy_ + (int)eEnemyType.bossEnemy] = Enemy_GolemImage;
        scoreImage[(int)eScoreType.shot] = shotImage;

        for (int i = 0; i < (int)eScoreType.max; i++) if (scoreImage[i] == null) Debug.Log("scoreImage == null : " + ((eScoreType)i).ToString());
    }
    [SerializeField] Sprite totalImage;
    [Header("positive")]
    [SerializeField] Sprite Get_FlagImage;
    [SerializeField] Sprite Destroy_BomImage;
    [SerializeField] Sprite Destroy_CrowImage;
    [SerializeField] Sprite Destroy_GolemImage;
    [SerializeField] Sprite Destroy_LivingArmorImage;
    [SerializeField] Sprite Destroy_EnemyMassImage;
    [SerializeField] Sprite Destroy_BossEnemyImage;
    [Header("negative")]
    [SerializeField] Sprite Enemy_BomImage;
    [SerializeField] Sprite Enemy_CrowImage;
    [SerializeField] Sprite Enemy_GolemImage;
    [SerializeField] Sprite Enemy_LivingArmorImage;
    [SerializeField] Sprite Enemy_EnemyMassImage;
    [SerializeField] Sprite Enemy_BossEnemyImage;

    [Header("")]
    [SerializeField] Sprite shotImage;


    static int scoreUpdate = 2;
    public bool ScoreUpdateCheck() { return scoreUpdate > 0; }
    static int[] score = new int[(int)eScoreType.max];
    public int[] GetScore() { return score; }
    public int GetScore(eScoreType scoreType) { return score[(int)scoreType]; }
    static public void AddScore(eScoreType scoreType, int addNum = 1) { score[(int)scoreType] += addNum; if (score[(int)scoreType] == addNum) scoreUpdate = 2; }
    static int[] oldScore = new int[(int)eScoreType.max];
    public int[] GetOldScore() { return oldScore; }
    public int GetOldScore(eScoreType scoreType) { return oldScore[(int)scoreType]; }
    static bool[] oldScoreUpDate = new bool[(int)eScoreType.max];
    public bool GetOldScoreUpDate(int index) { return oldScoreUpDate[index]; }
    private void OnEnable()
    {
        SetScoreImage();
        for (int i = 0; i < (int)eScoreType.max; i++)
            if (score[i] > oldScore[i]) { oldScore[i] = score[i]; oldScoreUpDate[i] = true; } else oldScoreUpDate[i] = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainGame") for (int i = 0; i < (int)eScoreType.max; i++) score[i] = 0;

        BestCheck();
    }
    void BestCheck()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreUpdate > 0) scoreUpdate--;
    }

    private static void TotalPointReset()
    {
        score[(int)eScoreType.total] = score[(int)eScoreType.Get_Flag] + score[(int)eScoreType.Destroy_ + (int)eEnemyType.bom] - score[(int)eScoreType.Enemy_ + (int)eEnemyType.bom];
    }

    public static void FlagGetPointAdd()
    {
        AddScore(eScoreType.Get_Flag);

        TotalPointReset();
    }
    public static void DestroyPointAdd()
    {
        AddScore(eScoreType.Destroy_ + (int)eEnemyType.bom);

        TotalPointReset();
    }

    public static void EnemyBomPointAdd(int addPoint = 1)
    {
        AddScore(eScoreType.Enemy_ + (int)eEnemyType. bom);

        TotalPointReset();
    }

    public static void ShotNumAdd()
    {
        AddScore(eScoreType.shot);
    }

    public static int GetTotalPoint()
    {
        return score[(int)eScoreType.total];
    }
    public static int GetFlagGetPoint()
    {
        return score[(int)eScoreType.Get_Flag];
    }
    public static int GetDestroyPoint()
    {
        return score[(int)eScoreType.Destroy_ + (int)eEnemyType.bom];
    }
    public static int GetEnemyBomPoint()
    {
        return score[(int)eScoreType.Enemy_ + (int)eEnemyType.bom];
    }

    public void DataSave()
    {
        PlayerPrefs.SetInt("totalPoint", score[(int)eScoreType.total]);

        PlayerPrefs.SetInt("flagGetPoint", score[(int)eScoreType.Get_Flag]);
        PlayerPrefs.SetInt("destroyPoint", score[(int)eScoreType.Destroy_ + (int)eEnemyType.bom]);

        PlayerPrefs.SetInt("enemyBomPoint", score[(int)eScoreType.Enemy_ + (int)eEnemyType.bom]);


        PlayerPrefs.SetInt("oldTotalPoint", oldScore[(int)eScoreType.total]);

        PlayerPrefs.SetInt("oldFlagGetPoint", oldScore[(int)eScoreType.Get_Flag]);
        PlayerPrefs.SetInt("oldDestroyPoint", oldScore[(int)eScoreType.Destroy_ + (int)eEnemyType.bom]);

        PlayerPrefs.SetInt("oldEnemyBomPoint", oldScore[(int)eScoreType.Enemy_ + (int)eEnemyType.bom]);


    }
    public void DataLoad()
    {
        score[(int)eScoreType.total] = PlayerPrefs.GetInt("totalPoint");

        score[(int)eScoreType.Get_Flag] = PlayerPrefs.GetInt("flagGetPoint");
        score[(int)eScoreType.Destroy_ + (int)eEnemyType.bom] = PlayerPrefs.GetInt("destroyPoint");

        score[(int)eScoreType.Enemy_ + (int)eEnemyType.bom] = PlayerPrefs.GetInt("enemyBomPoint");


        oldScore[(int)eScoreType.total] = PlayerPrefs.GetInt("oldTotalPoint");

        oldScore[(int)eScoreType.Get_Flag] = PlayerPrefs.GetInt("oldFlagGetPoint");
        oldScore[(int)eScoreType.Destroy_ + (int)eEnemyType.bom] = PlayerPrefs.GetInt("oldDestroyPoint");

        oldScore[(int)eScoreType.Enemy_ + (int)eEnemyType.bom] = PlayerPrefs.GetInt("oldEnemyBomPoint");


    }

    public void OldScoreReset()
    {
        PlayerPrefs.SetInt("oldTotalPoint", 0);
        PlayerPrefs.SetInt("oldFlagGetPoint", 0);
        PlayerPrefs.SetInt("oldDestroyPoint", 0);
        PlayerPrefs.SetInt("oldEnemyBomPoint", 0);
        PlayerPrefs.SetInt("totalPoint", 0);
        PlayerPrefs.SetInt("flagGetPoint", 0);
        PlayerPrefs.SetInt("destroyPoint", 0);
        PlayerPrefs.SetInt("enemyBomPoint", 0);

        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}