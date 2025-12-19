using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collection_TabColor : MonoBehaviour
{
    Manager_Collection manager_Collection;
    [SerializeField]
    eCollectionsTab collectionsTab = eCollectionsTab.none;
    [SerializeField]
    Color normalColor = Color.white;
    [SerializeField]
    Color selectColor = Color.red;

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
        if (oldCollectionsTab == manager_Collection.GetCollectionsTab()) return;oldCollectionsTab = manager_Collection.GetCollectionsTab();

        if(collectionsTab == oldCollectionsTab) GetComponent<Image>().color = selectColor;
        else GetComponent<Image>().color = normalColor;
    }
}
