using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eGateOpenType
{
    [InspectorName("")] none,

    scoreCheck_Posi_Destroy_Bom,
    time_Countdown,

    [InspectorName("")] max,
}
public class Manager_Gate : MonoBehaviour
{
    [SerializeField]
    GameObject []gateBase;
    public GameObject GetGateBase(int gateIndex) {  return gateBase[gateIndex]; }
    public GameObject GetGateBase() {  return gateBase[gateIndex]; }
    [SerializeField]
    int gateIndex = 0;
    [SerializeField]
    bool gateOpen = false;
    public bool GetGateOpen() {  return gateOpen; }
    public void SetGateOpen(bool gateOn_ = true) { gateOpen = gateOn_; }
    [SerializeField]
    Vector2 gatePos = new Vector2(0,0);
    public Vector2 GetGatePos() { return gatePos; }
    public void SerGatePos(Vector2 gatePos_) {  gatePos = gatePos_; }

    private void OnEnable()
    {
        SetGateOpenImage();
    }
    // Start is called before the first frame update
    void Start()
    {
        Manager_StageSelect tmp = GetComponent<Manager_StageSelect>();
        gateOpenType = tmp.GetGateOpenType(tmp.GetStage());
        gateOpenNum = tmp.GetGateOpenNum(tmp.GetStage());
    }

    eGateOpenType gateOpenType;
    public eGateOpenType GetGateOpenType() { return gateOpenType; }
    Sprite []gateOpenImage = new Sprite[(int)eGateOpenType.max];
    public Sprite GetGateOpenImage(eGateOpenType gateOpenType_) { return gateOpenImage[(int)gateOpenType_]; } 
    void SetGateOpenImage()
    {
        gateOpenImage[(int)eGateOpenType.scoreCheck_Posi_Destroy_Bom] = destroy_Bom_Image;
        gateOpenImage[(int)eGateOpenType.time_Countdown] = timer_Image;
    }
    [SerializeField] Sprite destroy_Bom_Image;
    [SerializeField] Sprite timer_Image;

    // Update is called once per frame
    int gateOpenNum;
    public int GetGateOpenNum() {  return gateOpenNum; }
    void Update()
    {
        switch (gateOpenType)
        {
            case eGateOpenType.none:
                if (!GetGateOpen()) SetGateOpen();
                break;
            case eGateOpenType.time_Countdown:
                if (GetComponent<Manager_Time>().GetPlayTime() >= gateOpenNum) SetGateOpen();
                break;
            case eGateOpenType.scoreCheck_Posi_Destroy_Bom:
                if (Manager_Score.GetDestroyPoint() >= gateOpenNum) SetGateOpen();
                break;
        }
        if (false) SetGateOpen(true);
    }
}
