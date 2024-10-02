using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_MousePointerType : MonoBehaviour
{
    [SerializeField]
    Sprite[] mousePointerImage;
    public Sprite[] GetMousePointerImage() {  return mousePointerImage; }
    public Sprite GetMousePointerImage(int mousePointerIndex_) {  return mousePointerImage[mousePointerIndex_]; }
    [SerializeField]
    int mousePointerIndex = 0;
    public int GetMousePointerIndex() {  return mousePointerIndex; }
    public void AddMousePointerIndex(int Add = 1)
    {
        mousePointerIndex += Add;
        if (mousePointerIndex >= mousePointerImage.Length) mousePointerIndex = 0;
        if (mousePointerIndex < 0) mousePointerIndex = mousePointerImage.Length - 1;
    }
    public void DataSave() { PlayerPrefs.SetInt("mousePointerIndex", mousePointerIndex); }
    void DataLoad() { mousePointerIndex = PlayerPrefs.GetInt("mousePointerIndex"); }
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
