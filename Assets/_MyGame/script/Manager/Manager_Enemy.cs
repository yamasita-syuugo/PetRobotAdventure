using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyType
{
    [InspectorName("")] none,

    Bom,            //Bom           :��{�I�ȓG������
    Crow,           //Crow          :�v���C���[�������o��
    Golem,          //Golem         :�n�ʂ�����v���C���[��e���o��
    LivingArmor,    //LivingArmor   :�n�ʂ���������U���
    EnemyMass,      //EnemyMass     :�G�̏W���̂��ׂē|���Ə�����

    //              //������󂵂ĉ��  �󒆂��ړ�����     �������̍U���ɑς���
    bossEnemy,    //�J�E���g��endGame�𔭓�   �U���ŃJ�E���g��x�点��    �߂Â��ƘA���ōU���ł���̂ŃJ�����Ɏ��܂�͈͂ŋ�������艓�����U������  �������U����

    [InspectorName("")] enemyTypeMax,
}
public class Manager_Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
