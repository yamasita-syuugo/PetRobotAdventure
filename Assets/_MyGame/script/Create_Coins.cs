using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Create_Coins : MonoBehaviour
{
    Manager_Collection manager_Collection;
    private void Start()
    {
        manager_Collection = GameObject.FindWithTag("Manager").GetComponent<Manager_Collection>();
    }
    public void CreateObject(GameObject []blocks)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (UnityEngine.Random.Range(0, manager_Collection.GetCoinDenominator()) == 0)
            {
                GameObject coin = Instantiate<GameObject>(manager_Collection.GetCollectionCoin());
                coin.transform.position = blocks[i].transform.position;
                coin.transform.parent = transform;
            }
        }
    }
    //çÌèú
    public void DeleteObject()
    {
        Transform[] coins = GetComponentsInChildren<Transform>();
        if (coins == null) return;
        for (int i = 0; i < coins.Length; i++) if (coins[i].tag == "Scaffold") DestroyImmediate(coins[i].gameObject);

    }
}