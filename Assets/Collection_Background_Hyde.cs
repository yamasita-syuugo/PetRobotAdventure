using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Collection_Background_Hyde : MonoBehaviour
{
    [SerializeField]
    GameObject []hydeObject;

    bool hyde = false;
    public void ChangeHyde() {  hyde = !hyde; }
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    bool oldHyde = false;
    void Update()
    {
        if (hyde && Input.GetMouseButtonDown(0))hyde = false;
        if(oldHyde == hyde) return;oldHyde = hyde;

        for (int i = 0; i < hydeObject.Length; i++) hydeObject[i].SetActive(!oldHyde);
        GetComponent<Image>().enabled = !oldHyde;
        GetComponent<Button>().enabled = !oldHyde;
        GetComponentInChildren<TextMeshProUGUI>().enabled = !oldHyde;
    }
}
