using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerMove : MonoBehaviour
{//todo;�m�b�N�o�b�N��
    enum eScaffold
    {
        none,

        block,
        ice,
        grass,

        scaffoldMax,
    }
    [SerializeField]
    eScaffold scaffold = eScaffold.block;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = GetComponent<Animator>();

        moveSpeedSetting[(int)eScaffold.block] = moveSpeedSetting_block;
        moveSpeedSetting[(int)eScaffold.ice] = moveSpeedSetting_ice;
        moveSpeedSetting[(int)eScaffold.grass] = moveSpeedSetting_grass;

        moveMaxSetting[(int)eScaffold.block] = moveMaxSetting_bloce;
        moveMaxSetting[(int)eScaffold.ice] = moveMaxSetting_ice;
        moveMaxSetting[(int)eScaffold.grass] = moveMaxSetting_grass;
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
    float[] moveSpeedSetting = new float[(int)eScaffold.scaffoldMax];
    float[] moveMaxSetting = new float[(int)eScaffold.scaffoldMax];
    [SerializeField,Header("�u���b�N")]
    float moveSpeedSetting_block = 2.0f;
    [SerializeField]
    float moveMaxSetting_bloce = 2.0f;
    [SerializeField, Header("�A�C�X")]
    float moveSpeedSetting_ice = 0.004f;
    [SerializeField]
    float moveMaxSetting_ice = 1.5f;
    [SerializeField]
    float stopEnelgy = 2.0f;
    [SerializeField, Header("�O���X(��)")]
    float moveSpeedSetting_grass = 0.7f;
    [SerializeField]
    float moveMaxSetting_grass = 0.7f;
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
                move /= 1 + 1f / 1000;
                break;
            case eScaffold.grass:
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
    void Direction()//todo:�A�j���[�V�����̕ύX
    {
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
        if (collision.tag == "Scaffold")//todo:���ꂪ�Y���Ă����ꍇ�ʂ̑����ΏۂɂƂ��Ă��܂��B
        {
            float tmpX = (transform.position.x - collision.transform.position.x) * (transform.position.x - collision.transform.position.x);
            float tmpY = (transform.position.y - collision.transform.position.y) * (transform.position.y - collision.transform.position.y);
            if (onScoffild == null)
            {
                onScoffild = collision.gameObject;
                onScoffildDistancX = tmpX;
                onScoffildDistancY = tmpY;
                scaffold = (eScaffold)collision.GetComponent<_Scaffold_Base>().GetScaffold();
                return;
            }
            else if(onScoffildDistancX > tmpX)
            {
                if(onScoffildDistancY > tmpY)
                {
                    onScoffild = collision.gameObject;
                    onScoffildDistancX = tmpX;
                    onScoffildDistancY = tmpY;
                    scaffold = (eScaffold)collision.GetComponent<_Scaffold_Base>().GetScaffold();
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
