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
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

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