using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum eEnemyType
{
    none,

    Bom,            //Bom           :基本的な敵床を壊す
    Crow,           //Crow          :プレイヤーを押し出す
    Golem,          //Golem         :地面を歩きプレイヤーを弾き出す
    LivingArmor,    //LivingArmor   :地面を歩き武器を振り回す
    EnemyMass,      //EnemyMass     :敵の集合体すべて倒すと消える

    enemyTypeMax,
}
public class Manager_Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
