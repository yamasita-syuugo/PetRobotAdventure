using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Technique_Magic : Player_Technique_
{
    [SerializeField]
    GameObject magicCircleBase;
    GameObject magicCircle;
    // Start is called before the first frame update
    void Start()
    {
        magicCircle = Instantiate(magicCircleBase);
    }

    // Update is called once per frame
    int chanting = 0;
    void Update()
    {
        if (transform.parent.GetComponent<ObjectFall>().GetSituation() == ObjectFall.eSituation.fall) return;

        Magic__Base();
    }

    bool standby = false;
    private void Magic__Base()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            magicCircle.SetActive(true);
            magicCircle.transform.position = transform.position;

            if (standby)
            {
                transform.parent.GetComponent<ObjectFall>().SetSituation(ObjectFall.eSituation.chanting);

                if (Input.GetKeyDown(KeyCode.W)) chanting = chanting * 10 + 1;
                if (Input.GetKeyDown(KeyCode.A)) chanting = chanting * 10 + 2;
                if (Input.GetKeyDown(KeyCode.S)) chanting = chanting * 10 + 3;
                if (Input.GetKeyDown(KeyCode.D)) chanting = chanting * 10 + 4;

                if (chanting > 500000) standby = false;
            }
            else transform.parent.GetComponent<ObjectFall>().SetSituation(ObjectFall.eSituation.normal);
        }
        else
        {
            magicCircle.SetActive(false);

            standby = true;

            switch (chanting)
            {
                case 0: transform.parent.GetComponent<ObjectFall>().SetSituation(ObjectFall.eSituation.normal); break;
                case 1: Magic_(); break;
                case 2: break;
                case 3: break;
                case 4: break;
                case 14: break;
                case 341: Shot(); break;
                case 242: Teleport(); break;
                case 4123: BladeSlash().GetComponent<SpriteRenderer>().color = Color.red; break;
            }
            if (chanting != 0) Debug.Log("マジックコード : " + chanting);

            chanting = 0;
        }
    }
    private void Magic_()
    {
        Debug.Log("MagicBase");
    }
}
