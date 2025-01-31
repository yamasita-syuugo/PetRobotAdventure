using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_MousePointerType : MonoBehaviour
{
    [SerializeField]
    RuntimeAnimatorController[] mousePointerAnimations;
    public RuntimeAnimatorController[] GetMousePointerAnimations() { return mousePointerAnimations; }
    public RuntimeAnimatorController GetMousePointerAnimation(int mousePointerIndex_) { return mousePointerAnimations[mousePointerIndex_]; }
    [SerializeField]
    int mousePointerIndex = 0;
    public int GetMousePointerIndex() {  return mousePointerIndex; }
    public void SetMousePointerIndex(int mousePointerIndex_) { mousePointerIndex = mousePointerIndex_; }
    public void AddMousePointerIndex(int Add = 1)
    {
        mousePointerIndex += Add;
        if (mousePointerIndex >= mousePointerAnimations.Length) mousePointerIndex = 0;
        if (mousePointerIndex < 0) mousePointerIndex = mousePointerAnimations.Length - 1;
    }
    //GetSituation
    public bool GetGetSituation(int index) {  return GetComponent<Manager_Collection>().GetGetSituation(eCollectionType.MousePointer,index) ; }
    public void SetGetSituation(int index, bool getSituation_) { GetComponent<Manager_Collection>().SetGetSituation(eCollectionType.MousePointer, index, getSituation_); }
    public void DataSave()
    {
        PlayerPrefs.SetInt("mousePointerIndex", mousePointerIndex);
    }
    public void DataLoad()
    {
        mousePointerIndex = PlayerPrefs.GetInt("mousePointerIndex");
    }
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
}