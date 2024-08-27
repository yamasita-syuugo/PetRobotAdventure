using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerMove : MonoBehaviour
{//todo;ノックバックを
    [SerializeField]
    eScaffoldType scaffold = eScaffoldType.block;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = GetComponent<Animator>();

        moveSpeedSetting[(int)eScaffoldType.block] = moveSpeedSetting_block;
        moveSpeedSetting[(int)eScaffoldType.ice] = moveSpeedSetting_ice;
        moveSpeedSetting[(int)eScaffoldType.grass] = moveSpeedSetting_grass;

        moveMaxSetting[(int)eScaffoldType.block] = moveMaxSetting_bloce;
        moveMaxSetting[(int)eScaffoldType.ice] = moveMaxSetting_ice;
        moveMaxSetting[(int)eScaffoldType.grass] = moveMaxSetting_grass;
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
    float[] moveSpeedSetting = new float[(int)eScaffoldType.scaffoldMax];
    float[] moveMaxSetting = new float[(int)eScaffoldType.scaffoldMax];
    [SerializeField,Header("ブロック")]
    float moveSpeedSetting_block = 2.0f;
    [SerializeField]
    float moveMaxSetting_bloce = 2.0f;
    [SerializeField, Header("アイス")]
    float moveSpeedSetting_ice = 0.004f;
    [SerializeField]
    float moveMaxSetting_ice = 1.5f;
    [SerializeField]
    float stopEnelgy = 2.0f;
    [SerializeField, Header("グラス(草)")]
    float moveSpeedSetting_grass = 0.7f;
    [SerializeField]
    float moveMaxSetting_grass = 0.7f;
    void Move()
    {
        if (GetComponent<ObjectFall>().GetSituation() == ObjectFall.eSituation.fall) return;

        transform.position += move * Time.deltaTime;

        switch (scaffold)
        {
            case eScaffoldType.block:
                move = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case eScaffoldType.ice:
                move /= 1 + 1f / 1000;
                break;
            case eScaffoldType.grass:
                move = new Vector3(0.0f, 0.0f, 0.0f);
                break;
        }
        moveSpeed = moveSpeedSetting[(int)scaffold];
        moveMax = moveMaxSetting[(int)scaffold];

        move.x += Input.GetAxis("Horizontal") * moveSpeed;
        move.y += Input.GetAxis("Vertical") * moveSpeed;
        if (move.x > moveMax) move.x = moveMax;
        if (move.y > moveMax) move.y = moveMax;
        if (move.x < -moveMax) move.x = -moveMax;
        if (move.y < -moveMax) move.y = -moveMax;

    }
    Animator playerAnimation;
    void Direction()//todo:アニメーションの変更
    {
        if (playerAnimation == null) return;

        float directionX = Input.GetAxis("Horizontal");
        float directionY = Input.GetAxis("Vertical");
        if (directionX * directionX > directionY * directionY)
        {
            if (directionX < 0) playerAnimation.SetInteger("direction", 3);
            else playerAnimation.SetInteger("direction", 2);
        }
        else
        {
            if (directionY <= 0) playerAnimation.SetInteger("direction", 0);
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

    GameObject onScoffild = null;
    float onScoffildDistancX = 0;
    float onScoffildDistancY = 0;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Scaffold")//todo:足場がズレていた場合別の足場を対象にとってしまう。
        {
            float tmpX = (transform.position.x - collision.transform.position.x) * (transform.position.x - collision.transform.position.x);
            float tmpY = (transform.position.y - collision.transform.position.y) * (transform.position.y - collision.transform.position.y);
            if (onScoffild == null)
            {
                onScoffild = collision.gameObject;
                onScoffildDistancX = tmpX;
                onScoffildDistancY = tmpY;
                scaffold = (eScaffoldType)collision.GetComponent<Type_Scaffold>().GetScaffoldType();
                return;
            }
            else if(onScoffildDistancX > tmpX)
            {
                if(onScoffildDistancY > tmpY)
                {
                    onScoffild = collision.gameObject;
                    onScoffildDistancX = tmpX;
                    onScoffildDistancY = tmpY;
                    scaffold = (eScaffoldType)collision.GetComponent<Type_Scaffold>().GetScaffoldType();
                }
            }
        }
    }
    private void FixedUpdate()
    {
        onScoffild = null;
        onScoffildDistancX = 0;
        onScoffildDistancY = 0;
    }
}
