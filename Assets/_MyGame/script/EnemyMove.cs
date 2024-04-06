using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using Unity.VisualScripting;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
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
    }
    [SerializeField]
    eMoveType moveType = eMoveType.tracking;



    // Start is called before the first frame update
    void Start()
    {
        //todo:playerのオートサーチ(複数の場合の対応)
        player = GameObject.FindWithTag("Player");
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
        }
    }

    void MoveGoStraight()
    {
        if (playerFall.GetSituation() != ObjectFall.eSituation.normal) return;
        if (move == new Vector3(0.0f, 0.0f, 0.0f))
        {
            move.x = player.transform.position.x - transform.position.x;
            move.y = player.transform.position.y - transform.position.y;
        }

        float distance = Mathf.Sqrt(move.x * move.x + move.y * move.y);

        move /= distance;

        transform.position += move * moveSpeed * Time.deltaTime;
    }
    void MoveTracking()
    {
        if (playerFall.GetSituation() != ObjectFall.eSituation.normal) return;
        Vector3 move = new Vector3(0.0f,0.0f,0.0f);
        move.x = player.transform.position.x - transform.position.x;
        move.y = player.transform.position.y - transform.position.y;

        float distance = Mathf.Sqrt(move.x * move.x + move.y * move.y);

        move /= distance;

        transform.position += move * moveSpeed * Time.deltaTime;
    }

    public Vector3 GetMove()
    {
        return move;
    }
}
