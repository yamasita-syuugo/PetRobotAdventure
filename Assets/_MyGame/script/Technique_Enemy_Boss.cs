using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technique_Enemy_Boss : MonoBehaviour
{
    Manager_Enemy manager_Enemy;
    Create_Enemy create_Enemy;

    float endPower = 0;
    [SerializeField]
    float endPowerMax = 10;

    private void OnEnable()
    {
        manager_Enemy = GameObject.FindWithTag("Manager").GetComponent<Manager_Enemy>();
        create_Enemy = GameObject.FindWithTag("Create").GetComponent<Create_Enemy>();
    }
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        if(endPower < endPowerMax)
        {
            endPower += Time.deltaTime;
        }
        else
        {
            create_Enemy.EnemyCreate(manager_Enemy.GetEnemyObject(eEnemyType.bom));
        }
    }

    [SerializeField]
    float endPowerDown = 3;
    public void EndPowerDown()
    {
        endPower -= endPowerDown;
    }
}
