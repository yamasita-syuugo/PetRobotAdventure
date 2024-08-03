using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum eObjectType
{
    none,

    player,
    enemy,
    attack,
}
//attackÅ®enemyÅ®player
public class HitManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    GameObject gameObject;
    eObjectType gameObjectType = eObjectType.none;
    GameObject collision;
    eObjectType collisionGameObjectType = eObjectType.none;

    void Hit(GameObject gameObject_, GameObject collision_)
    {
        gameObject = gameObject_;
        collision = collision_;
        TypeCheck();

        switch (gameObjectType)
        {
            case eObjectType.player:
                PlayerTo();
                break;
            case eObjectType.enemy:
                EnemyTo();
                break;
            case eObjectType.attack:
                AttackTo();
                break;
        }
    }

    void TypeCheck()
    {
        if (gameObject.tag == "Player") gameObjectType = eObjectType.player;
        else if (gameObject.tag == "Enemy") gameObjectType = eObjectType.enemy;
        else if (gameObject.tag == "Attack") gameObjectType = eObjectType.attack;

        if (collision.tag == "Player") collisionGameObjectType = eObjectType.player;
        else if (collision.tag == "Enemy") collisionGameObjectType = eObjectType.enemy;
        else if (collision.tag == "Attack") collisionGameObjectType = eObjectType.attack;
    }
    void PlayerTo()
    {
        switch (collisionGameObjectType)
        {
            //case eObjectType.player:
            //    break;
            //case eObjectType.enemy:
            //    break;
            //case eObjectType.attack:
            //    break;
        }
    }

    void EnemyTo()
    {
        switch (collisionGameObjectType)
        {
            case eObjectType.player:
                if(gameObject.name == "Bom")
                {

                }
                else if(gameObject.name == "LivingArmor")
                {
                    GetComponent<KnockBack>().SetKnockBackEnergy(collision.GetComponent<bulletMove>().GetMoveEnelgy());

                    if (collision.name == "Bullet") Destroy(collision.gameObject);
                }
                break;
                //case eObjectType.enemy:
                //    break;
                //case eObjectType.attack:
                //    break;
        }
    }

    void AttackTo()
    {
        switch (collisionGameObjectType)
        {
            //case eObjectType.player:
            //    break;
            case eObjectType.enemy:
                if (gameObject.name == "Bullet")
                {
                    collision.GetComponent<KnockBack>().SetKnockBackEnergy(gameObject.GetComponent<bulletMove>().GetMoveEnelgy());

                    if (gameObject.name == "LivingArmor") Destroy(gameObject.gameObject);
                }
                else if (gameObject.name == "Bom")
                {

                }
                break;
                //case eObjectType.attack:
                //    break;
        }
    }
}
