using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class Manager_MousePointerType : MonoBehaviour
{
    [SerializeField]
    AnimatorController[] mousePointerAnimations;
    public AnimatorController[] GetMousePointerAnimations() { return mousePointerAnimations; }
    public AnimatorController GetMousePointerAnimation(int mousePointerIndex_) { return mousePointerAnimations[mousePointerIndex_]; }
    [SerializeField]
    int mousePointerIndex = 0;
    public int GetMousePointerIndex() {  return mousePointerIndex; }
    public void AddMousePointerIndex(int Add = 1)
    {
        mousePointerIndex += Add;
        if (mousePointerIndex >= mousePointerAnimations.Length) mousePointerIndex = 0;
        if (mousePointerIndex < 0) mousePointerIndex = mousePointerAnimations.Length - 1;
    }
    [SerializeField]
    bool[] getSituation = new bool [16];
    public void DataSave()
    {
        PlayerPrefs.SetInt("mousePointerIndex", mousePointerIndex);

        int mousePointerAnimationsSize = mousePointerAnimations.Length;
        for (int i = 0; i < mousePointerAnimationsSize; i += 16)
        {
            int boolNum = 0;
            for (int j = i; j <  i + 16 && j < mousePointerAnimationsSize; j++)
            {
                int boolNumBit = 1;
                for (int bit = 0; bit < j - i; bit++) boolNumBit *= 2;
                boolNum += boolNumBit * (getSituation[j] ? 1 : 0);
            }
            PlayerPrefs.SetInt("MousePointerGetSituation" + (i / 16).ToString(), boolNum);
        }
    }
    public void DataLoad()
    {
        mousePointerIndex = PlayerPrefs.GetInt("mousePointerIndex");

        int mousePointerAnimationsSize = mousePointerAnimations.Length;
        getSituation = new bool[mousePointerAnimationsSize];
        for (int i = 0; i < mousePointerAnimationsSize; i += 16)
        {
            int boolNum = PlayerPrefs.GetInt("MousePointerGetSituation" + (i / 16).ToString()); 
            for (int j = i + 15; i <= j; j--)
            {
                if (j >= mousePointerAnimationsSize) continue;
                int boolNumBit = 1;
                for (int bit = 0; bit < j - i; bit++) boolNumBit *= 2;
                if (boolNum / boolNumBit >= 1) { getSituation[j] = true; boolNum -= boolNumBit; }
                else { getSituation[j] = false; }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DataLoad();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}