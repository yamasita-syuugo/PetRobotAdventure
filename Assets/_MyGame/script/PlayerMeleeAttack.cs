using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField]
    GameObject meleeAttackBase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ObjectFall>().GetSituation() == ObjectFall.eSituation.fall) return;
        ControllerMeleeAttack();
        MouseMeleeAttack();
    }

    private void MouseMeleeAttack()
    {
        if (Input.GetMouseButtonDown(2))
        {
            MeleeAttack();
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

    void MeleeAttack()
    {
        GameObject meleeAttack = Instantiate(meleeAttackBase);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mouseDistansX = mousePos.x - transform.position.x;
        float mouseDistansY = mousePos.y - transform.position.y;
        float ang = -math.atan2(mouseDistansX, mouseDistansY) * Mathf.Rad2Deg;
        meleeAttack.transform.rotation = Quaternion.Euler(0,0,ang);

        meleeAttack.transform.position = transform.position /*+*/ ;
    }
}
