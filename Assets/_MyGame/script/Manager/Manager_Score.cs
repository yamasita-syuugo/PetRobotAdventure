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
        Destroy_Bom,

        //negative
        Enemy_Bom,
        Enemy_Golem,

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
        scoreImage[(int)eScoreType.Destroy_Bom] = Destroy_BomImage;
        scoreImage[(int)eScoreType.Enemy_Bom] = Enemy_BomImage;
        scoreImage[(int)eScoreType.Enemy_Golem] = Enemy_GolemImage;
        scoreImage[(int)eScoreType.shot] = shotImage;

        for (int i = 0; i < (int)eScoreType.max; i++) if (scoreImage[i] == null) Debug.Log("scoreImage == null : " + ((eScoreType)i).HumanName());
    }
    [SerializeField] Sprite totalImage;
    [Header("positive")]
    [SerializeField] Sprite Get_FlagImage;
    [SerializeField] Sprite Destroy_BomImage;
    [Header("negative")]
    [SerializeField] Sprite Enemy_BomImage;
    [SerializeField] Sprite Enemy_GolemImage;

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
        score[(int)eScoreType.total] = score[(int)eScoreType.Get_Flag] + score[(int)eScoreType.Destroy_Bom] - score[(int)eScoreType.Enemy_Bom];
    }

    public static void FlagGetPointAdd()
    {
        AddScore(eScoreType.Get_Flag);

        TotalPointReset();
    }
    public static void DestroyPointAdd()
    {
        AddScore(eScoreType.Destroy_Bom);

        TotalPointReset();
    }

    public static void EnemyBomPointAdd(int addPoint = 1)
    {
        AddScore(eScoreType.Enemy_Bom);

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
        return score[(int)eScoreType.Destroy_Bom];
    }
    public static int GetEnemyBomPoint()
    {
        return score[(int)eScoreType.Enemy_Bom];
    }

    public void DataSave()
    {
        PlayerPrefs.SetInt("totalPoint", score[(int)eScoreType.total]);

        PlayerPrefs.SetInt("flagGetPoint", score[(int)eScoreType.Get_Flag]);
        PlayerPrefs.SetInt("destroyPoint", score[(int)eScoreType.Destroy_Bom]);

        PlayerPrefs.SetInt("enemyBomPoint", score[(int)eScoreType.Enemy_Bom]);


        PlayerPrefs.SetInt("oldTotalPoint", oldScore[(int)eScoreType.total]);

        PlayerPrefs.SetInt("oldFlagGetPoint", oldScore[(int)eScoreType.Get_Flag]);
        PlayerPrefs.SetInt("oldDestroyPoint", oldScore[(int)eScoreType.Destroy_Bom]);

        PlayerPrefs.SetInt("oldEnemyBomPoint", oldScore[(int)eScoreType.Enemy_Bom]);


    }
    public void DataLoad()
    {
        score[(int)eScoreType.total] = PlayerPrefs.GetInt("totalPoint");

        score[(int)eScoreType.Get_Flag] = PlayerPrefs.GetInt("flagGetPoint");
        score[(int)eScoreType.Destroy_Bom] = PlayerPrefs.GetInt("destroyPoint");

        score[(int)eScoreType.Enemy_Bom] = PlayerPrefs.GetInt("enemyBomPoint");


        oldScore[(int)eScoreType.total] = PlayerPrefs.GetInt("oldTotalPoint");

        oldScore[(int)eScoreType.Get_Flag] = PlayerPrefs.GetInt("oldFlagGetPoint");
        oldScore[(int)eScoreType.Destroy_Bom] = PlayerPrefs.GetInt("oldDestroyPoint");

        oldScore[(int)eScoreType.Enemy_Bom] = PlayerPrefs.GetInt("oldEnemyBomPoint");


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