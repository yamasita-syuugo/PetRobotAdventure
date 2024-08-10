using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum eObjectType
{
    none,

    player,
    enemy,
    attack,

    scaffold,
}
public class Manager_Hit : MonoBehaviour
{
    ObjectFall objectFall;
    // Start is called before the first frame update
    void Start()
    {
        explosionSource = GameObject.Find("explosionSound").GetComponent<AudioSource>();
        objectFall =GameObject.Find("Player").GetComponent<ObjectFall>();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    GameObject gameObject;
    eObjectType gameObjectType = eObjectType.none;
    GameObject collision;
    eObjectType collisionGameObjectType = eObjectType.none;

    public void Hit(GameObject gameObject_, GameObject collision_)
    {
        if (objectFall.GetSituation() != ObjectFall.eSituation.normal) return;

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
            case eObjectType.scaffold:
                ScaffokdTo();
                break;

            case eObjectType.none: break;
        }
    }

    void TypeCheck()
    {
        gameObjectType = eObjectType.none;
        collisionGameObjectType = eObjectType.none;

        if (gameObject.tag == "Player") gameObjectType = eObjectType.player;
        else if (gameObject.tag == "Enemy") gameObjectType = eObjectType.enemy;
        else if (gameObject.tag == "Attack") gameObjectType = eObjectType.attack;
        else if (gameObject.tag == "Scaffold") gameObjectType = eObjectType.scaffold;

        if (collision.tag == "Player") collisionGameObjectType = eObjectType.player;
        else if (collision.tag == "Enemy") collisionGameObjectType = eObjectType.enemy;
        else if (collision.tag == "Attack") collisionGameObjectType = eObjectType.attack;
        else if (collision.tag == "Scaffold") collisionGameObjectType = eObjectType.scaffold;
    }
    //attack→enemy→enemy→player→scaffold
    void PlayerTo()
    {
        switch (collisionGameObjectType)
        {
            //case eObjectType.player: break;
            //case eObjectType.enemy: break;
            //case eObjectType.attack: break;
            case eObjectType.scaffold: 
                
                break;
        }
    }

    void EnemyTo()
    {
        switch (collisionGameObjectType)
        {
            case eObjectType.player:
                switch (gameObject.GetComponent<EnemyType>().GetEnemyType())
                {
                    case eEnemyType.Bom:
                        ScoreManager.EnemyBomPointAdd();

                        Explosion(gameObject);
                        break;
                    case eEnemyType.Crow:

                        EnemyMove enemyMove = gameObject.GetComponent<EnemyMove>();
                        collision.GetComponent<playerMove>().AddPosition(enemyMove.GetMove() * enemyMove.GetMoveSpeed() * Time.deltaTime);
                        break;
                    case eEnemyType.Golem:
                        Vector3 enelgy = collision.transform.position - transform.position;
                        collision.GetComponent<KnockBack>().SetKnockBackEnergy(enelgy * 3);
                        break;
                    case eEnemyType.LivingArmor:

                        GetComponent<KnockBack>().SetKnockBackEnergy(collision.GetComponent<bulletMove>().GetMoveEnelgy());

                        if (collision.name == "Bullet") Destroy(collision.gameObject);
                        break;
                    case eEnemyType.EnemyMass: break;

                    case eEnemyType.bossEnemy: break;
                }
                break;
            case eObjectType.enemy:
                EnemyToEnemy();
                break;
            //case eObjectType.attack: break;
            //case eObjectType.scaffold: break;
        }
    }
    void EnemyToEnemy()
    {
        switch (gameObject.GetComponent<EnemyType>().GetEnemyType())
        {
            case eEnemyType.Bom:
                switch (collision.GetComponent<EnemyType>().GetEnemyType())
                {
                    case eEnemyType.Bom:
                        ScoreManager.EnemyBomPointAdd(2);

                        Explosion(collision);
                        Explosion(gameObject);
                        break;
                    case eEnemyType.Crow: break;
                    case eEnemyType.Golem: break;
                    case eEnemyType.LivingArmor: break;
                    case eEnemyType.EnemyMass: break;

                    case eEnemyType.bossEnemy: break;
                }
                break;
            case eEnemyType.Crow:
                switch (collision.GetComponent<EnemyType>().GetEnemyType())
                {
                    case eEnemyType.Bom:
                        ScoreManager.EnemyBomPointAdd();

                        Explosion(collision);
                        Explosion(gameObject);
                        break;
                    case eEnemyType.Crow: break;
                    case eEnemyType.Golem: break;
                    case eEnemyType.LivingArmor: break;
                    case eEnemyType.EnemyMass: break;

                    case eEnemyType.bossEnemy: break;
                }
                break;
            case eEnemyType.Golem:
                switch (collision.GetComponent<EnemyType>().GetEnemyType())
                {
                    case eEnemyType.Bom: break;
                    case eEnemyType.Crow: break;
                    case eEnemyType.Golem: break;
                    case eEnemyType.LivingArmor: break;
                    case eEnemyType.EnemyMass: break;

                    case eEnemyType.bossEnemy: break;
                }
                break;
            case eEnemyType.LivingArmor:
                switch (collision.GetComponent<EnemyType>().GetEnemyType())
                {
                    case eEnemyType.Bom: break;
                    case eEnemyType.Crow: break;
                    case eEnemyType.Golem: break;
                    case eEnemyType.LivingArmor: break;
                    case eEnemyType.EnemyMass: break;

                    case eEnemyType.bossEnemy: break;
                }
                break;
            case eEnemyType.EnemyMass:
                switch (collision.GetComponent<EnemyType>().GetEnemyType())
                {
                    case eEnemyType.Bom: break;
                    case eEnemyType.Crow: break;
                    case eEnemyType.Golem: break;
                    case eEnemyType.LivingArmor: break;
                    case eEnemyType.EnemyMass: break;

                    case eEnemyType.bossEnemy: break;
                }
                break;

            case eEnemyType.bossEnemy:
                switch (collision.GetComponent<EnemyType>().GetEnemyType())
                {
                    case eEnemyType.Bom: break;
                    case eEnemyType.Crow: break;
                    case eEnemyType.Golem: break;
                    case eEnemyType.LivingArmor: break;
                    case eEnemyType.EnemyMass: break;

                    case eEnemyType.bossEnemy: break;
                }
                break;
        }
    }
    [SerializeField]
    GameObject explosion;
    AudioSource explosionSource;
    void Explosion(GameObject gameObject)
    {
        GameObject tmp = Instantiate<GameObject>(explosion);
        tmp.transform.position = gameObject.transform.position;
        explosionSource.Play();

        Destroy(gameObject);
    }

    void AttackTo()
    {
        switch (collisionGameObjectType)
        {
            //case eObjectType.player:
            //    break;
            case eObjectType.enemy:
                switch (gameObject.GetComponent<AttackType>().GetAttackType())
                {
                    case eAttackType.Bullet:
                        switch (collision.GetComponent<EnemyType>().GetEnemyType())
                        {
                            case eEnemyType.Bom:
                                ScoreManager.DestroyPointAdd();
                                GameObject.Find("CreateEnemy").GetComponent<CreateEnemy>().LivingArmorCountAdd();   //リビングアーマーカウント
                                GameObject.FindAnyObjectByType<FlagCreate>().FlagSpaun();

                                Explosion(collision);
                                Destroy(gameObject);
                                break;
                            case eEnemyType.Crow: break;
                            case eEnemyType.Golem:
                                collision.GetComponent<KnockBack>().SetKnockBackEnergy(gameObject.GetComponent<bulletMove>().GetMoveEnelgy());
                                collision.GetComponent<KnockBack>().AddMoveSpeed(gameObject.GetComponent<bulletMove>().GetMoveSpeed());
                                Destroy(gameObject);
                                break;
                            case eEnemyType.LivingArmor:
                                collision.GetComponent<KnockBack>().SetKnockBackEnergy(gameObject.GetComponent<bulletMove>().GetMoveEnelgy());
                                Destroy(gameObject.gameObject);
                                break;
                            case eEnemyType.EnemyMass: break;

                            case eEnemyType.bossEnemy:
                                collision.GetComponent<Technique_Boss>().EndPowerDown();

                                break;
                        }


                        break;
                    case eAttackType.MeleeAttack: break;
                    case eAttackType.EarthQuake: break;
                }
                break;
            //case eObjectType.attack: break;
            //case eObjectType.scaffold: break;
        }
    }

    private void ScaffokdTo()
    {
        switch (collisionGameObjectType)
        {
            //case eObjectType.player: break;
            //case eObjectType.enemy: break;
            //case eObjectType.attack: break;
            //case eObjectType.scaffold: break;
        }
    }
}
