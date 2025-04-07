using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_Image_StageEnemy : MonoBehaviour
{
    [SerializeField]GameObject imageBase;

    float imageSpase = 50f;
    // Start is called before the first frame update
    void Start()
    {
        Manager_StageSelect manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();
        Manager_Enemy manager_Enemy = GameObject.FindWithTag("Manager").GetComponent<Manager_Enemy>();

        int count = 0;
        for(int enemyType = 0; enemyType < (int)eEnemyType.max; enemyType++)
        {
            if (manager_StageSelect.GetStageData(manager_StageSelect.GetStage()).GetEnemySerect((eEnemyType)enemyType))
            {
                GameObject tmp = Instantiate<GameObject>(imageBase);
                tmp.GetComponent<Image>().sprite = manager_Enemy.GetEnemyImage((eEnemyType)enemyType);
                tmp.transform.parent = transform;
                tmp.transform.localScale = Vector3.one;
                tmp.transform.localPosition = new Vector2(-imageSpase + count % 3 * imageSpase,-count / 3 * imageSpase);
                count++;
            }
        }
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
