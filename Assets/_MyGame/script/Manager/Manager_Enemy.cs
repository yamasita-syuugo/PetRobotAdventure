using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public enum eEnemyType
{
    [InspectorName("")] none = -1,

    Bom,            //Bom           :��{�I�ȓG������
    Crow,           //Crow          :�v���C���[�������o��
    Golem,          //Golem         :�n�ʂ�����v���C���[��e���o��
    LivingArmor,    //LivingArmor   :�n�ʂ���������U���
    EnemyMass,      //EnemyMass     :�G�̏W���̂��ׂē|���Ə�����

    //              //������󂵂ĉ��  �󒆂��ړ�����     �������̍U���ɑς���
    bossEnemy,    //�J�E���g��endGame�𔭓�   �U���ŃJ�E���g��x�点��    �߂Â��ƘA���ōU���ł���̂ŃJ�����Ɏ��܂�͈͂ŋ�������艓�����U������  �������U����

    [InspectorName("")] max,
}
public class Manager_Enemy : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;

    //�G�̏o���p�^�[��
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
