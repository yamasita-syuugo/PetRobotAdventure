using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    [SerializeField]
    Vector3 move;
    Animator playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (GetComponent<ObjectFall>().GetSituation() == ObjectFall.eSituation.fall) return;

        transform.position += move * moveSpeed * Time.deltaTime;

        move = new Vector3(0.0f, 0.0f, 0.0f);

        {
            move.x = Input.GetAxis("Horizontal");
            move.y = Input.GetAxis("Vertical");
        }
        if (move.x * move.x > move.y * move.y)
        {
            if (move.x < 0) playerAnimation.SetInteger("direction", 3);//todo:アニメーションの変更
            else playerAnimation.SetInteger("direction", 2);
        }
        else
        {
            if (move.y <= 0) playerAnimation.SetInteger("direction", 0);
            else playerAnimation.SetInteger("direction", 1);
        }


    }

    public void AddMove(Vector3 addMove)
    {
        move += addMove;
    }
}
