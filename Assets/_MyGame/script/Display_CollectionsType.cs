using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCollectionsTab
{
    [InspectorName("")]none = -1,

    stage = eCollectionType.stage,
    player = eCollectionType.player,
    medal = eCollectionType.medal,
    other,

    [InspectorName("")]max,
}

public class Display_CollectionsType : MonoBehaviour
{
    eCollectionsTab collectionsType = eCollectionsTab.stage;
    public eCollectionsTab GetCollectionsTabType() { return collectionsType; }
    public void SetCollectionsType(int collectionsType_) { collectionsType = (eCollectionsTab)collectionsType_ ; }
    [SerializeField]
    GameObject[] collection;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    eCollectionsTab oldCollectionsType = eCollectionsTab.none;
    void Update()
    {
        if (oldCollectionsType == collectionsType) return; oldCollectionsType = collectionsType;

        for(int i = 0; i < collection.Length; i++) { if (collection[i] == null) continue;
            if (i == (int)collectionsType || (collectionsType == eCollectionsTab.other && i >= (int)eCollectionsTab.other)) collection[i].SetActive(true);else collection[i].SetActive(false); }
    }
}
