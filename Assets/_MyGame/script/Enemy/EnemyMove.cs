using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//using System.Collections;
//using Unity.VisualScripting;
//using Unity.Mathematics;

public class CPUMove : MonoBehaviour
{
    GameObject player;
    ObjectFall playerFall;

    public float moveSpeed = 1.0f; 
    [SerializeField]
    Vector3 move;

    enum eMoveType
    {
        none,
        goStraight,
        tracking,
        topToFall,
        walk,
        runAway,

        moveTyptMax,
    }
    [SerializeField]
    eMoveType moveType = eMoveType.tracking;



    // Start is called before the first frame update
    void Start()
    {
        //todo:playerのオートサーチ(複数の場合の対応)
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Destroy(this.gameObject);
            return;
        }
        playerFall = player.GetComponent<ObjectFall>();
        move = new Vector3(0.0f,0.0f,0.0f);

        SetScaffold(GameObject.Find("CreateScaffold"));
    }

    // Update is called once per frame
    void Update()
    {
        switch (moveType)
        {
            case eMoveType.none:
                break;
            case eMoveType.goStraight:
                MoveGoStraight();
                break;
            case eMoveType.tracking:
                MoveTracking();
                break;
            case eMoveType.topToFall:
                MoveTopToFall();
                break;
            case eMoveType.walk:
                MoveWalk();
                break;
            case eMoveType.runAway:
                MoveRunAway();
                break;
        }
    }

    void MoveGoStraight()
    {
        if (move == new Vector3(0.0f, 0.0f, 0.0f))
        {
            move.x = player.transform.position.x - transform.position.x;
            move.y = player.transform.position.y - transform.position.y;
            float z = -Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, z - 90);
        }

        float distance = Mathf.Sqrt(move.x * move.x + move.y * move.y);

        move /= distance;

        transform.position += move * moveSpeed * Time.deltaTime;
    }
    void MoveTracking()
    {
        if (playerFall == null) return;
        if (playerFall.GetSituation() == ObjectFall.eSituation.fall) return;
        Vector3 move = new Vector3(0.0f,0.0f,0.0f);
        move.x = player.transform.position.x - transform.position.x;
        move.y = player.transform.position.y - transform.position.y;

        float distance = Mathf.Sqrt(move.x * move.x + move.y * move.y);

        move /= distance;

        transform.position += move * moveSpeed * Time.deltaTime;
    }
    Vector2 tmpPosishon;
    void MoveTopToFall()
    {
        if (playerFall == null) return;
        if (move == new Vector3(0.0f, 0.0f, 0.0f))
        {
            move.x = player.transform.position.x - transform.position.x;
            move.y = player.transform.position.y - transform.position.y;
            tmpPosishon = player.transform.position;
            GetComponent<Landing>().SetShadowPosision(tmpPosishon);
        }

        float distance = Mathf.Sqrt(move.x * move.x + move.y * move.y);

        move /= distance;
        if ((move.x < 0 && tmpPosishon.x <= transform.position.x) || (move.x > 0 && tmpPosishon.x >= transform.position.x))
        {
            transform.position += move * moveSpeed * Time.deltaTime;
        }
    }
    static Type_Scaffold[] position = new Type_Scaffold[200];
    GameObject []previousPos = new GameObject[20];
    GameObject nextPos;
    void MoveWalk()
    {
        if (nextPos == null || (nextPos.transform.position.x <= transform.position.x + 0.1f && nextPos.transform.position.x >= transform.position.x - 0.1f &&
            nextPos.transform.position.y <= transform.position.y + 0.1f && nextPos.transform.position.y >= transform.position.y - 0.1f))
        {
            GameObject[] rmd = new GameObject[20];
            int rmdNum = 0;
            for (int i = 0; i < position.Length; i++)
            {
                if (position[i] == null) continue;
                if ((position[i].transform.position.x > transform.position.x - 1.3f && position[i].transform.position.x < transform.position.x + 1.3f &&
                    position[i].transform.position.y > transform.position.y - 1.3f && position[i].transform.position.y < transform.position.y + 1.3f) == false) continue;

                bool continueOn;
                continueOn = false;
                for (int j = previousPos.Length - 1; j >= 0; j--)
                {
                    if (previousPos[j] == null) continue;
                    else if (previousPos[j].transform.position.x == position[i].transform.position.x && previousPos[j].transform.position.y == position[i].transform.position.y)
                    {
                        continueOn = true;
                        continue;
                    }
                }
                if (continueOn == true) continue;
                rmd[rmdNum++] = position[i].gameObject;
            }
            nextPos = rmd[Random.Range(0, rmdNum)];
            for (int i = previousPos.Length - 1; i >= 0; i--)
            {
                if (i <= 0) previousPos[i] = nextPos;
                else previousPos[i] = previousPos[i - 1];
            }
        }
        Vector3 move = new Vector3(0.0f, 0.0f, 0.0f);
        move.x = nextPos.transform.position.x - transform.position.x;
        move.y = nextPos.transform.position.y - transform.position.y;

        float distance = Mathf.Sqrt(move.x * move.x + move.y * move.y);

        move /= distance;

        transform.position += move * moveSpeed * Time.deltaTime;
    }
    void MoveRunAway()
    {
        if (nextPos == null || (nextPos.transform.position.x <= transform.position.x + 0.1f && nextPos.transform.position.x >= transform.position.x - 0.1f &&
    nextPos.transform.position.y <= transform.position.y + 0.1f && nextPos.transform.position.y >= transform.position.y - 0.1f))
        {
            GameObject[] rmd = new GameObject[110];
            int rmdNum = 0;
            for (int i = 0; i < position.Length; i++)
            {
                if (position[i] == null) continue;
                if ((position[i].transform.position.x > transform.position.x - 5.3f && position[i].transform.position.x < transform.position.x + 5.3f &&
                    position[i].transform.position.y > transform.position.y - 5.3f && position[i].transform.position.y < transform.position.y + 5.3f) == false) continue;

                bool continueOn;
                continueOn = false;
                for (int j = previousPos.Length - 1; j >= 0; j--)
                {
                    if (previousPos[j] == null) continue;
                    else if (previousPos[j].transform.position.x == position[i].transform.position.x && previousPos[j].transform.position.y == position[i].transform.position.y)
                    {
                        continueOn = true;
                        continue;
                    }
                }
                if (continueOn == true) continue;
                rmd[rmdNum++] = position[i].gameObject;
            }
            if (rmd[0] == null)
            {
                float scaDistance = 100;
                rmd[0] = position[0].gameObject;
                for(int i = 0; i < position.Length; i++)
                {
                    float x = position[i].transform.position.x - transform.position.x;
                    float y = position[i].transform.position.y - transform.position.y;
                    if (scaDistance >= Mathf.Sqrt(x * x + y * y))
                    {
                        scaDistance = Mathf.Sqrt(x * x + y * y);
                        rmd[0] = position[i].gameObject;
                    }
                }
            }
            float plaDistance = 0;
            for(int i = 0; i < rmd.Length; i++)
            {
                if (rmd[i] == null) continue;
                float x = rmd[i].transform.position.x - player.transform.position.x;
                float y = rmd[i].transform.position.y - player.transform.position.y;
                if(plaDistance <= Mathf.Sqrt(x * x + y * y))
                {
                    plaDistance = Mathf.Sqrt(x * x + y * y);
                    nextPos = rmd[i];
                }
            }
            for (int i = previousPos.Length - 1; i >= 0; i--)
            {
                if (i <= 0) previousPos[i] = nextPos;
                else previousPos[i] = previousPos[i - 1];
            }
        }




        Vector3 move = new Vector3(0.0f, 0.0f, 0.0f);
        move.x = nextPos.transform.position.x - transform.position.x;
        move.y = nextPos.transform.position.y - transform.position.y;

        float distance = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        move /= distance;

        transform.position += move * moveSpeed * Time.deltaTime;
    }

    public void SetScaffold(GameObject createScaffold)
    {
        position = createScaffold.GetComponentsInChildren<Type_Scaffold>();
    }

    public Vector3 GetMove()
    {
        return move;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
