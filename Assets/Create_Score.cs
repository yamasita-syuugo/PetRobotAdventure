using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Score : MonoBehaviour
{
    [SerializeField]
    GameObject scoreObject;

    // Start is called before the first frame update
    void Start()
    {
        ScoreCreate();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    void ScoreCreate()
    {
        int count = 0;
        int[] score = GameObject.FindWithTag("Manager").GetComponent<Manager_Score>().GetScore();
        int[] DisplayScoreNum = new int[score.Length];
        for (int i = 0; i < score.Length; i++) { if (score[i] > 0) DisplayScoreNum[count++] = i; }

        for (int i = 0;i < DisplayScoreNum.Length; i++) {
            GameObject tmp = Instantiate<GameObject>(scoreObject);
            tmp.transform.parent = transform;
            tmp.transform.position = new Vector3( i, 0, 0);
            tmp.transform.localScale = Vector3.one;
            ScoreDisplay []scoreDisplay = tmp.GetComponentsInChildren<ScoreDisplay>();
            for (int j = 0; j < scoreDisplay.Length; j++) { scoreDisplay[j].SetScoreType((eScoreType)i); }
        }
    }
}
