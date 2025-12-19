using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_CollectionsType : MonoBehaviour
{
    Manager_Collection manager_Collection;
    [SerializeField]
    GameObject[] collection;

    private void OnEnable()
    {
        manager_Collection = GameObject.FindWithTag("Manager").GetComponent<Manager_Collection>();
    }
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    eCollectionsTab oldCollectionsTab = eCollectionsTab.none;
    void Update()
    {
        eCollectionsTab collectionsTab = manager_Collection.GetCollectionsTab();
        if (oldCollectionsTab == collectionsTab) return; oldCollectionsTab = collectionsTab;

        for (int i = 0; i < collection.Length; i++)
        {
            if (collection[i] == null) continue;
            if (i == (int)collectionsTab || (collectionsTab == eCollectionsTab.other && i >= (int)eCollectionsTab.other)) collection[i].SetActive(true); else collection[i].SetActive(false);
        }
    }
}
