using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField]
    GameObject meleeAttackBase;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ObjectFall>().GetSituation() == ObjectFall.eSituation.fall) return;
        ControllerMeleeAttack();
        MouseMeleeAttack();
    }

    [SerializeField]
    int establishment = 25;
    [SerializeField]
    int spawnBomNum = 10;
    private void MouseMeleeAttack()
    {
        if (Input.GetMouseButtonDown(2))
        {
            MeleeAttack();

            //int randam = UnityEngine.Random.Range(0, 100);
            //if (randam < establishment) GameObject.Find("CreateEnemy").GetComponent<EnemyCreate>().SetBomSpawnNum(spawnBomNum);
            GameObject.Find("CreateEnemy").GetComponent<EnemyCreate>().SetEndCount();
        }
    }

    bool MeleeAttackCheck = false;
    private void ControllerMeleeAttack()
    {
        //if (Input.GetAxisRaw("MeleeAttack") == 1 && MeleeAttackCheck == false)
        //{
        //    MeleeAttack();

        //    MeleeAttackCheck = true;
        //}
        //else
        //{
        //    MeleeAttackCheck = false;
        //}
    }

    [SerializeField]
    float attackDistanceSize = 0.5f;
    void MeleeAttack()
    {
        GameObject meleeAttack = Instantiate(meleeAttackBase);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mouseDistansX = mousePos.x - transform.position.x;
        float mouseDistansY = mousePos.y - transform.position.y;
        float ang = -math.atan2(mouseDistansX, mouseDistansY) * Mathf.Rad2Deg;
        meleeAttack.transform.rotation = Quaternion.Euler(0,0,ang);

        float distance = Mathf.Sqrt(mouseDistansX * mouseDistansX + mouseDistansY * mouseDistansY);
        meleeAttack.transform.position = transform.position + new Vector3(mouseDistansX / distance * attackDistanceSize, mouseDistansY / distance *attackDistanceSize, 0);
        meleeAttack.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
