using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIconChange : MonoBehaviour
{
    [SerializeField]
    Sprite[] enemyIcon = new Sprite[(int)eEnemyType.enemyTypeMax];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    int oldWave = 0;
    void Update()
    {
        int tmp = (int)GameObject.FindWithTag("Manager").GetComponent<Manager_Wave>().GetWaveType();
        if(oldWave != tmp)
        {
            GetComponent<Image>().sprite = enemyIcon[tmp];
            oldWave = tmp;
        }
    }
}
