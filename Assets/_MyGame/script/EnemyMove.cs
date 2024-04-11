using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using Unity.VisualScripting;
using Unity.Mathematics;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    ObjectFall playerFall;
    Vector2 tmpPosishon;

    public float moveSpeed = 1.0f; 
    [SerializeField]
    Vector3 move;

    enum eMoveType
    {
        none,
        goStraight,
        tracking,
        topToFall,

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
        if (playerFall.GetSituation() != ObjectFall.eSituation.normal) return;
        Vector3 move = new Vector3(0.0f,0.0f,0.0f);
        move.x = player.transform.position.x - transform.position.x;
        move.y = player.transform.position.y - transform.position.y;

        float distance = Mathf.Sqrt(move.x * move.x + move.y * move.y);

        move /= distance;

        transform.position += move * moveSpeed * Time.deltaTime;
    }
    void MoveTopToFall()
    {
        if (playerFall == null) return;
        if (move == new Vector3(0.0f, 0.0f, 0.0f))
        {
            move.x = player.transform.position.x - transform.position.x;
            move.y = player.transform.position.y - transform.position.y;
            tmpPosishon = player.transform.position;
        }

        float distance = Mathf.Sqrt(move.x * move.x + move.y * move.y);

        move /= distance;
        if ((move.x < 0 && tmpPosishon.x <= transform.position.x) || (move.x > 0 && tmpPosishon.x >= transform.position.x))
        {
            transform.position += move * moveSpeed * Time.deltaTime;
        }
    }

    public Vector3 GetMove()
    {
        return move;
    }
}
