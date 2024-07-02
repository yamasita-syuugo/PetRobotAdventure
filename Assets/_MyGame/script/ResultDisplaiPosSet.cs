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

    //Start is called before the first frame update
    void Start()
    {
        score[(int)ePointType.totalPoint] = PlayerPrefs.GetInt("totalPoint");

        score[(int)ePointType.flagGetPoint] = PlayerPrefs.GetInt("flagGetPoint");
        score[(int)ePointType.destroyPoint] = PlayerPrefs.GetInt("destroyPoint");

        score[(int)ePointType.enemyBomPoint] = PlayerPrefs.GetInt("enemyBomPoint");
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public int GetScore(ePointType type)
    {
        return score[(int)type];
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