using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_CollectionsType : MonoBehaviour
{
    eCollectionType collectionsType = eCollectionType.stage;
    public eCollectionType GetCollectionsTabType() { return collectionsType; }
    public void SetCollectionsType(int collectionsType_) { collectionsType = (eCollectionType)collectionsType_ ; }
    [SerializeField]
    GameObject[] collection;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    eCollectionType oldCollectionsType = eCollectionType.none;
    void Update()
    {
        if (oldCollectionsType == collectionsType) return; oldCollectionsType = collectionsType;

        for(int i = 0; i < collection.Length; i++) { if (collection[i] == null) continue; if (i == (int)collectionsType) collection[i].SetActive(true);else collection[i].SetActive(false); }
    }
}
