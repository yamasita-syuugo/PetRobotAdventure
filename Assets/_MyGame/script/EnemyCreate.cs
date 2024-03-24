using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
    public AudioSource setSound;
    float enemySpaunTime;
    public float enemySpaunTimeReset = 3.0f;

    public GameObject enemyObject;
    [SerializeField]
    Vector3 nextPosition = new Vector3(12,12,0);

    enum eDirecttion
    {
        north, 
        south,
        east, 
        west,
    }

    // Start is called before the first frame update
    void Start()
    {
        enemySpaunTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpaun();
    }

    void EnemySpaun()
    {
        enemySpaunTime -= Time.deltaTime;

        if(enemySpaunTime < 0)
        {
            GameObject tmp = Instantiate<GameObject>(enemyObject);
            tmp.transform.parent = transform;
            tmp.transform.position = nextPosition;
            tmp.GetComponent<BombHit>().ExplosionSource = setSound;

            int directtion = Random.Range(0, 4);
            float width = Random.Range(-11, 11);
            switch (directtion)
            {
                case 0:
                    nextPosition = new Vector3(width, 12, 0);
                    break;
                case 1:
                    nextPosition = new Vector3(width, -12, 0);
                    break;
                case 2:
                    nextPosition = new Vector3(12, width, 0);
                    break;
                case 3:
                    nextPosition = new Vector3(-12, width, 0);
                    break;
            }

            enemySpaunTime = enemySpaunTimeReset;
        }
    }
}
