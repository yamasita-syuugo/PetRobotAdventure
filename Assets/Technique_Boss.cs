using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technique_Boss : MonoBehaviour
{
    float endPower = 0;
    [SerializeField]
    float endPowerMax = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(endPower < endPowerMax)
        {
            endPower += Time.deltaTime;
        }
        else
        {
            CreateEnemy tmp = GameObject.Find("CreateEnemy").GetComponent<CreateEnemy>();
            tmp.EnemyCreate(tmp.GetEnemyObjectBase(eEnemyType.Bom));
        }
    }

    [SerializeField]
    float endPowerDown = 3;
    public void EndPowerDown()
    {
        endPower -= endPowerDown;
    }
}
