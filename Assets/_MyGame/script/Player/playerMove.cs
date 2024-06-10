using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    enum eScaffold
    {
        none,

        block,
        ice,

        scaffoldMax,
    }
    [SerializeField]
    eScaffold scaffold = eScaffold.block;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Direction();
    }

    Vector3 move;
    float moveSpeed;
    float moveMax = 1.5f;
    [SerializeField]
    public float[] moveSpeedSetting = new float[(int)eScaffold.scaffoldMax];
    [SerializeField]
    float[] moveMaxSetting = new float[(int)eScaffold.scaffoldMax];
    void Move()
    {
        if (GetComponent<ObjectFall>().GetSituation() == ObjectFall.eSituation.fall) return;

        transform.position += move * Time.deltaTime;

        switch (scaffold)
        {
            case eScaffold.block:
                move = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case eScaffold.ice:
                //move /= 1.5f;
                break;
        }
        moveSpeed = moveSpeedSetting[(int)scaffold];
        moveMax = moveMaxSetting[(int)scaffold];

        move.x = Input.GetAxis("Horizontal") * moveSpeed;
        move.y = Input.GetAxis("Vertical") * moveSpeed;
        if (move.x > moveMax * moveSpeed) move.x = moveMax * moveSpeed;
        if (move.y > moveMax * moveSpeed) move.y = moveMax * moveSpeed;

    }
    Animator playerAnimation;
    void Direction()//todo:アニメーションの変更
    {
        if (move.x * move.x > move.y * move.y)
        {
            if (move.x < 0) playerAnimation.SetInteger("direction", 3);
            else playerAnimation.SetInteger("direction", 2);
        }
        else
        {
            if (move.y <= 0) playerAnimation.SetInteger("direction", 0);
            else playerAnimation.SetInteger("direction", 1);
        }
    }

    public void AddPosition(Vector3 addMove)
    {
        transform.position += addMove;
    }
    public Vector2 GetMove()
    {
        return move;
    }
}
