using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Flag : MonoBehaviour
{
    [SerializeField]
    float flagSpaunTime;
    public float flagSpaunTimeReset = 10.0f;

    [SerializeField]GameObject flag;
    // Start is called before the first frame update
    void Start()
    {
        flagSpaunTime = flagSpaunTimeReset;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Manager").GetComponent<Manager_Time>().GetTimeStop()) return;
        FlagSpaunCount();
    }

    void FlagSpaunCount()
    {
        flagSpaunTime -= Time.deltaTime;
        if (flagSpaunTime < 0)
        {
            FlagSpaun();

            flagSpaunTime = flagSpaunTimeReset;
        }
    }
    public void FlagSpaun()
    {
        GameObject tmp;
        tmp = Instantiate<GameObject>(flag);
        GameObject[] tmp1 = GameObject.FindGameObjectsWithTag("Scaffold");
        int tmp2 = Random.Range(0, tmp1.Length);
        tmp.transform.position = tmp1[tmp2].transform.position;
        tmp.transform.parent = this.transform;
    }
}
