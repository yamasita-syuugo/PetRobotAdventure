using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Technique_Play_MeleeAttack : Player_Technique_Play__Base
{
    [SerializeField]
    GameObject meleeAttackBase;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    //[SerializeField]
    //int establishment = 25;
    [SerializeField]
    int spawnBomNum = 10;
    override public void MousePlay()
    {
        GetComponent<Player_Technique_Container_MeleeAttackCurse> ().SetEndCount();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mouseDistansX = mousePos.x - transform.position.x;
        float mouseDistansY = mousePos.y - transform.position.y;
        float ang = -math.atan2(mouseDistansX, mouseDistansY) * Mathf.Rad2Deg;
        rotation = Quaternion.Euler(0, 0, ang);
        float distance = Mathf.Sqrt(mouseDistansX * mouseDistansX + mouseDistansY * mouseDistansY);
        position = transform.position + new Vector3(mouseDistansX / distance * attackDistanceSize, mouseDistansY / distance * attackDistanceSize, 0);

        MeleeAttack();
    }

    override public void ControllerPlay()
    {
        GetComponent<Player_Technique_Container_MeleeAttackCurse>().SetEndCount();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mouseDistansX = Input.GetAxis("AimX");
        float mouseDistansY = -Input.GetAxis("AimY");
        float ang = -math.atan2(mouseDistansX, mouseDistansY) * Mathf.Rad2Deg;
        rotation = Quaternion.Euler(0, 0, ang);
        float distance = Mathf.Sqrt(mouseDistansX * mouseDistansX + mouseDistansY * mouseDistansY);
        position = transform.position + new Vector3(mouseDistansX / distance * attackDistanceSize, mouseDistansY / distance * attackDistanceSize, 0);

        MeleeAttack();
    }

    [SerializeField]
    float attackDistanceSize = 0.5f;
    Quaternion rotation;
    Vector3 position;
    void MeleeAttack()
    {
        //int randam = UnityEngine.Random.Range(0, 100);
        //if (randam < establishment) GameObject.Find("Create").GetComponent<EnemyCreate>().SetBomSpawnNum(spawnBomNum);
        GameObject meleeAttack = Instantiate(meleeAttackBase);

        meleeAttack.transform.rotation = rotation;
        meleeAttack.transform.position = position;
        meleeAttack.transform.parent = transform;
    }
}
