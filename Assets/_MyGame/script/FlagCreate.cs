using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCreate : MonoBehaviour
{
    [SerializeField]
    float flagSpaunTime;
    public float flagSpaunTimeReset = 10.0f;

    public GameObject flag;
    // Start is called before the first frame update
    void Start()
    {
        flagSpaunTime = flagSpaunTimeReset;
    }

    // Update is called once per frame
    void Update()
    {
        FlagSpaun();
    }

    void FlagSpaun()
    {
        flagSpaunTime -= Time.deltaTime;
        if(flagSpaunTime < 0)
        {
            GameObject tmp;
            tmp = Instantiate<GameObject>(flag);
            GameObject []tmp1 = GameObject.FindGameObjectsWithTag("Block");
            int tmp2 = Random.Range(0, tmp1.Length);
            tmp.transform.position = tmp1[tmp2].transform.position;
            tmp.transform.parent = this.transform;

            flagSpaunTime = flagSpaunTimeReset;
        }
    }
}