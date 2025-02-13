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
    [SerializeField]Sprite shotImage;


    static int[] score = new int[(int)eScoreType.max];
     public int[] GetScore() { return score; }
    public int GetScore(eScoreType scoreType) { return score[(int)scoreType]; }
    static public void AddScore(eScoreType scoreType, int addNum = 1) { score[(int)scoreType] += addNum; }
    static int[] oldScore = new int[(int)eScoreType.max];
     public int[] GetOldScore() { return oldScore; }
     public int GetOldScore(eScoreType scoreType) { return oldScore[(int)scoreType]; }
    // Start is called before the first frame update
    void Start()
    {
        SetScoreImage();

        for (int i = 0; i < score.Length; i++) score[i] = 0;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

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


        PlayerPrefs.SetInt("oldTotalPoint", score[(int)eScoreType.total]);

        PlayerPrefs.SetInt("oldFlagGetPoint", score[(int)eScoreType.Get_Flag]);
        PlayerPrefs.SetInt("oldDestroyPoint", score[(int)eScoreType.Destroy_Bom]);

        PlayerPrefs.SetInt("oldEnemyBomPoint", score[(int)eScoreType.Enemy_Bom]);


    }
    public void DataLoad()
    {
        score[(int)eScoreType.total] = PlayerPrefs.GetInt("totalPoint");

        score[(int)eScoreType.Get_Flag] = PlayerPrefs.GetInt("flagGetPoint");
        score[(int)eScoreType.Destroy_Bom] = PlayerPrefs.GetInt("destroyPoint");

        score[(int)eScoreType.Enemy_Bom] = PlayerPrefs.GetInt("enemyBomPoint");


        score[(int)eScoreType.total] = PlayerPrefs.GetInt("oldTotalPoint");

        score[(int)eScoreType.Get_Flag] = PlayerPrefs.GetInt("oldFlagGetPoint");
        score[(int)eScoreType.Destroy_Bom] = PlayerPrefs.GetInt("oldDestroyPoint");

        score[(int)eScoreType.Enemy_Bom] = PlayerPrefs.GetInt("oldEnemyBomPoint");


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