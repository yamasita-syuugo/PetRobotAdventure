using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGateOpenType
{
    [InspectorName("")] none,

    scoreCheck_Posi_Destroy_Bom,

    [InspectorName("")] gateOpenTypeMax,
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
    public void SetGateOpen(bool gateOn_) { gateOpen = gateOn_; }
    [SerializeField]
    Vector2 gatePos = new Vector2(0,0);
    public Vector2 GetGatePos() { return gatePos; }
    public void SerGatePos(Vector2 gatePos_) {  gatePos = gatePos_; }
    // Start is called before the first frame update
    void Start()
    {
        Manager_StageSelect tmp = GetComponent<Manager_StageSelect>();
        gateOpenType = tmp.GetGateOpenType()[(int)tmp.GetStage()];
        gateOpenNum = tmp.GetGateOpenNum()[(int)tmp.GetStage()];
    }


    // Update is called once per frame
    eGateOpenType gateOpenType;
    int gateOpenNum;
    void Update()
    {
        switch (gateOpenType)
        {
            case eGateOpenType.none:
                if (!GetGateOpen()) SetGateOpen(true);
                break;
            case eGateOpenType.scoreCheck_Posi_Destroy_Bom:
                if (gateOpenNum <= Manager_Score.GetDestroyPoint()) SetGateOpen(true);
                break;
        }
        if (false) SetGateOpen(true);
    }
}
