using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum eCollectionsTabType
{
    [InspectorName("")]none = -1,

    player,
    medal,
    mousePointer,
    background,
    music,

    [InspectorName("")]max,
}

public class Display_CollectionsType : MonoBehaviour
{
    eCollectionsTabType collectionsType = eCollectionsTabType.player;
    public void SetCollectionsType(int collectionsType_) { collectionsType = (eCollectionsTabType)collectionsType_ ; }
    [SerializeField]
    GameObject[] collection; 
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    eCollectionsTabType oldCollectionsType = eCollectionsTabType.none;
    void Update()
    {
        if (oldCollectionsType == collectionsType) return; oldCollectionsType = collectionsType;

        for(int i = 0; i < collection.Length; i++) { if (collection[i] == null) continue; if (i == (int)collectionsType) collection[i].SetActive(true);else collection[i].SetActive(false); }
    }
}
