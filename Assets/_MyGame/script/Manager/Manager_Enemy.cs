using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public enum eEnemyType
{
    [InspectorName("")] none = -1,

    Bom,            //Bom           :基本的な敵床を壊す
    Crow,           //Crow          :プレイヤーを押し出す
    Golem,          //Golem         :地面を歩きプレイヤーを弾き出す
    LivingArmor,    //LivingArmor   :地面を歩き武器を振り回す
    EnemyMass,      //EnemyMass     :敵の集合体すべて倒すと消える

    //              //足場を壊して回る  空中を移動する     いくつかの攻撃に耐える
    bossEnemy,    //カウントでendGameを発動   攻撃でカウントを遅らせる    近づくと連続で攻撃できるのでカメラに収まる範囲で距離を取り遠距離攻撃する  遠距離攻撃は

    [InspectorName("")] max,
}
public class Manager_Enemy : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;

    //敵の出現パターン
    public bool[] GetStageEnemy(eStage stage) { return manager_StageSelect.GetStageData(stage).GetEnemySerect(); }
    // Start is called before the first frame update
    void Start()
    {
        manager_StageSelect = GetComponent<Manager_StageSelect>();
    }

    // Update is called once per frame
    eStage oldStage = eStage.none;
    void Update()
    {
        if (oldStage == manager_StageSelect.GetStage()) return;oldStage = manager_StageSelect.GetStage();
    }
}
