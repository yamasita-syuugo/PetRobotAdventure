using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateBlockNumDisplay : MonoBehaviour
{
    [SerializeField]
    PlayerCreateScaffold player;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        int blockNum = player.GetBlockNum();
        GetComponent<TextMeshProUGUI>().text = blockNum.ToString("00");
    }
}
