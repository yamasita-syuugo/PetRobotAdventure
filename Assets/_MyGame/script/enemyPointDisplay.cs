using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemyPointDisplay : MonoBehaviour
{
    [SerializeField]
    ScoreManager scoreManager;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        float time = ScoreManager.GetEnemyBomPoint();
        GetComponent<TextMeshProUGUI>().text = time.ToString("00");
    }
}
