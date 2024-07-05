using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


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


        enemyBomPoint,

        pointTypeMax,
    }
    int []score = new int[(int)ePointType.pointTypeMax];
    int []oldScore = new int[(int)ePointType.pointTypeMax];

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
        PlayerPrefs.SetInt("totalPoint",0);

        score[(int)ePointType.flagGetPoint] = PlayerPrefs.GetInt("flagGetPoint");
        PlayerPrefs.SetInt("flagGetPoint", 0);
        score[(int)ePointType.destroyPoint] = PlayerPrefs.GetInt("destroyPoint");
        PlayerPrefs.SetInt("destroyPoint", 0);

        score[(int)ePointType.enemyBomPoint] = PlayerPrefs.GetInt("enemyBomPoint");
        PlayerPrefs.SetInt("enemyBomPoint", 0);
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
        if (oldPoint < score[(int)ePointType.enemyBomPoint]) PlayerPrefs.SetInt("oldEnemyBomPoint", score[(int)ePointType.enemyBomPoint]);
        oldScore[(int)ePointType.enemyBomPoint] = PlayerPrefs.GetInt("oldEnemyBomPoint");
    }

    public int GetScore(ePointType type,bool old = false)
    {
        if(old == false) return score[(int)type];
        return oldScore[(int)type];
    }

#if UNITY_EDITOR
    public void ImagesPosSetting()
    {
        int imagesNum = GetComponentsInChildren<Image>().Length;
        transform.position = GetComponentInParent<Canvas>().transform.position;
        Image[] images = new Image[imagesNum];
        images = GetComponentsInChildren<Image>();
        for (int i = 0; i < imagesNum; i++)
        {
            images[i].transform.position = new Vector3(((imagesNum / 2) * -100) + i * 100,100,0);
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