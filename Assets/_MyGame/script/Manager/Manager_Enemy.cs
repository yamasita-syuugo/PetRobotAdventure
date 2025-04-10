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
    [Header("enemyImage")]
    [SerializeField] Sprite image_Bom;
    [SerializeField] Sprite image_Crow;
    [SerializeField] Sprite image_Golem;
    [SerializeField] Sprite image_LivingArmor;

    GameObject[]enemyObject = new GameObject[(int)eEnemyType.max];
    public GameObject GetEnemyObject(eEnemyType enemyType) { return enemyObject[(int)enemyType]; }
    void SetEnemyObject()
    {
        enemyObject[(int)eEnemyType.bom] = enemyObject_Bom;
        enemyObject[(int)eEnemyType.crow] = enemyObject_Crou;
        enemyObject[(int)eEnemyType.golem] = enemyObject_Golem;
        enemyObject[(int)eEnemyType.livingArmor] = enemyObject_LivingArmor;
        enemyObject[(int)eEnemyType.enemyMass] = enemyObject_EnemyMass;
    }
    [Header("enemyObject")]
    [SerializeField] GameObject enemyObject_Bom;
    [SerializeField] GameObject enemyObject_Crou;
    [SerializeField] GameObject enemyObject_Golem;
    [SerializeField] GameObject enemyObject_LivingArmor;
    [SerializeField] GameObject enemyObject_EnemyMass;

    float[] enemySpaunTimeReset = new float[(int)eEnemyType.max];
    public float GetEnemySpaunTimeReset(eEnemyType enemyType) { return enemySpaunTimeReset[(int)enemyType]; } 
    void SetEnemySpaunTimeReset()
    {
        enemySpaunTimeReset[(int)eEnemyType.bom] = enemySpaunTimeReset_Bom;
        enemySpaunTimeReset[(int)eEnemyType.crow] = enemySpaunTimeReset_Crow;
        enemySpaunTimeReset[(int)eEnemyType.golem] = enemySpaunTimeReset_Golem;
        enemySpaunTimeReset[(int)eEnemyType.livingArmor] = enemySpaunTimeReset_LivingArmor;
        enemySpaunTimeReset[(int)eEnemyType.enemyMass] = enemySpaunTimeReset_EnemyMass;
    }
    [Header("enemySpaunTimeReset")]
    [SerializeField] float enemySpaunTimeReset_Bom;
    [SerializeField] float enemySpaunTimeReset_Crow;
    [SerializeField] float enemySpaunTimeReset_Golem;
    [SerializeField] float enemySpaunTimeReset_LivingArmor;
    [SerializeField] float enemySpaunTimeReset_EnemyMass;

    void OnEnable(){
        SetEnemyImage(); 
        SetEnemyObject();
        SetEnemySpaunTimeReset();
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
