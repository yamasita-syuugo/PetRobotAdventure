using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public enum eEnemyType
{
    [InspectorName("")] none = -1,

    bom,            //Bom           :��{�I�ȓG������
    crow,           //Crow          :�v���C���[�������o��
    golem,          //Golem         :�n�ʂ�����v���C���[��e���o��
    livingArmor,    //LivingArmor   :�n�ʂ���������U���
    enemyMass,      //EnemyMass     :�G�̏W���̂��ׂē|���Ə�����

    //              //������󂵂ĉ��  �󒆂��ړ�����     �������̍U���ɑς���
    bossEnemy,    //�J�E���g��endGame�𔭓�   �U���ŃJ�E���g��x�点��    �߂Â��ƘA���ōU���ł���̂ŃJ�����Ɏ��܂�͈͂ŋ�������艓�����U������  �������U����

    [InspectorName("")] max,
}
public class Manager_Enemy : MonoBehaviour
{
    Manager_StageSelect manager_StageSelect;

    Sprite[] enemyImage = new Sprite[(int)eEnemyType.max];
    public Sprite GetEnemyImage(eEnemyType enemyType) { return enemyImage[(int)enemyType]; }
    void SetEnemyImage()
    {
        enemyImage[(int)eEnemyType.bom] = image_Bom;
        enemyImage[(int)eEnemyType.crow] = image_Crow;
        enemyImage[(int)eEnemyType.golem] = image_Golem;
        enemyImage[(int)eEnemyType.livingArmor] = image_LivingArmor;
    }
    [SerializeField] Sprite image_Bom;
    [SerializeField] Sprite image_Crow;
    [SerializeField] Sprite image_Golem;
    [SerializeField] Sprite image_LivingArmor;

    private void OnEnable()
    {
        SetEnemyImage();
    }
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
