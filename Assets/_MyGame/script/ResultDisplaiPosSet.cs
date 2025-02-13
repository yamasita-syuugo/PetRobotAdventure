using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



#if UNITY_EDITOR
using UnityEditor;
#endif

public class ResultDisplaiPosSet : MonoBehaviour
{
    public enum ePointType
    {
        totalPoint,


        flagGetPoint,
        destroyPoint,


        enemy_Bom,

        max,
    }
    int []score = new int[(int)ePointType.max];
    int []oldScore = new int[(int)ePointType.max];

    private void OnEnable()
    {
        ScoreLoad();
        OldScoreUpdate();
    }

    //Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    void ScoreLoad()
    {
        score[(int)ePointType.totalPoint] = PlayerPrefs.GetInt("totalPoint");

        score[(int)ePointType.flagGetPoint] = PlayerPrefs.GetInt("flagGetPoint");
        score[(int)ePointType.destroyPoint] = PlayerPrefs.GetInt("destroyPoint");

        score[(int)ePointType.enemy_Bom] = PlayerPrefs.GetInt("enemyBomPoint");

        for (int i = 0; i < (int)ePointType.max; i++) if (score[i] == null) score[i] = 0;
    }
    void OldScoreUpdate()
    {
        int oldPoint;

        oldPoint = PlayerPrefs.GetInt("oldTotalPoint");
        if (oldPoint < score[(int)ePointType.totalPoint]) PlayerPrefs.SetInt("oldTotalPoint", score[(int)ePointType.totalPoint]);
        oldScore[(int)ePointType.totalPoint] = PlayerPrefs.GetInt("oldTotalPoint");

        oldPoint = PlayerPrefs.GetInt("oldFlagGetPoint");
        if (oldPoint < score[(int)ePointType.flagGetPoint]) PlayerPrefs.SetInt("oldFlagGetPoint", score[(int)ePointType.flagGetPoint]);
        oldScore[(int)ePointType.flagGetPoint] = PlayerPrefs.GetInt("oldFlagGetPoint");
        oldPoint = PlayerPrefs.GetInt("oldDestroyPoint");
        if (oldPoint < score[(int)ePointType.destroyPoint]) PlayerPrefs.SetInt("oldDestroyPoint", score[(int)ePointType.destroyPoint]);
        oldScore[(int)ePointType.destroyPoint] = PlayerPrefs.GetInt("oldDestroyPoint");

        oldPoint = PlayerPrefs.GetInt("oldEnemyBomPoint");
        if (oldPoint < score[(int)ePointType.enemy_Bom]) PlayerPrefs.SetInt("oldEnemyBomPoint", score[(int)ePointType.enemy_Bom]);
        oldScore[(int)ePointType.enemy_Bom] = PlayerPrefs.GetInt("oldEnemyBomPoint");


        for (int i = 0; i < (int)ePointType.max; i++) if (oldScore[i] == null) oldScore[i] = 0;
    }

    public int GetScore(ePointType type, bool old = false)
    {
        if (old) return oldScore[(int)type];
        return score[(int)type];
    }

    public void OldScoreReset()
    {
        PlayerPrefs.SetInt("oldTotalPoint",0);
        PlayerPrefs.SetInt("oldFlagGetPoint", 0);
        PlayerPrefs.SetInt("oldDestroyPoint", 0);
        PlayerPrefs.SetInt("oldEnemyBomPoint", 0);
        PlayerPrefs.SetInt("totalPoint",0);
        PlayerPrefs.SetInt("flagGetPoint", 0);
        PlayerPrefs.SetInt("destroyPoint", 0);
        PlayerPrefs.SetInt("enemyBomPoint", 0);

        UnityEngine.SceneManagement.SceneManager.LoadScene("BestScoar");
    }

#if UNITY_EDITOR
    public void ImagesPosSetting()
    {
        CanvasScaler.ScaleMode scaleMode = GetComponentInParent<CanvasScaler>().uiScaleMode;

        int imagesNum = GetComponentsInChildren<Image>().Length;
        transform.position = GetComponentInParent<Canvas>().transform.position;
        Image[] images = new Image[imagesNum];
        images = GetComponentsInChildren<Image>();
        for (int i = 0; i < imagesNum; i++)
        {
            if (scaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
            {
                images[i].transform.position = new Vector3(((imagesNum / 2) * -3 + 1.5f) + i * 3, 2, 0);
            }
            else if (scaleMode == CanvasScaler.ScaleMode.ConstantPixelSize)
            {
                images[i].transform.position = new Vector3(((imagesNum / 2) * -100) + i * 100, 100, 0);
            }
        }
    }
# endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(ResultDisplaiPosSet))]
public class b : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ResultDisplaiPosSet trg = target as ResultDisplaiPosSet;

        if (GUILayout.Button("ImagesPosSetting", GUILayout.Width(150f)))
        {
            trg.ImagesPosSetting();
        }
    }
}
#endif