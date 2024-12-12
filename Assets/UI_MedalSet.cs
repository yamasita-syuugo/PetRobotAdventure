using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MedalSet : MonoBehaviour
{
    Manager_Medal manager_Medal;
    [SerializeField,Range(0,2)] int pos;

    SpriteRenderer medal;
    // Start is called before the first frame update
    void Start()
    {
        manager_Medal = GameObject.FindWithTag("Manager").GetComponent<Manager_Medal>();
        medal = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    int oldMedalType = -1;
    void Update()
    {
        if (oldMedalType == (int)manager_Medal.GetMedalType(pos)) return; oldMedalType = (int)manager_Medal.GetMedalType(pos);

        MedalSet_Update();
    }

    public void MedalSet_Update()
    {
        if (manager_Medal.GetMedalType(pos) == eMedalType.none) { medal.color = Color.clear; return; }
        Sprite medalImageBase = manager_Medal.GetMedalImageBase((int)manager_Medal.GetMedalType(pos));
        if (manager_Medal.GetMedalType(pos) != eMedalType.none) { medal.sprite = medalImageBase; medal.color = Color.white; }
    }
}
