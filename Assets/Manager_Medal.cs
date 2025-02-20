using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMedalType
{
    none,

    Boots_speed_UP,
    Boots_speed_DOWN,

    Boots_Spike,

    [InspectorName("")]max,
}

public class Manager_Medal : MonoBehaviour
{
    Sprite[] medalImageBase = new Sprite[(int)eMedalType.max];
    public Sprite GetMedalImageBase(int index) { return medalImageBase[index]; }
    void SetMedalImageBase()
    {
        medalImageBase[(int)eMedalType.Boots_speed_UP] = boots_Speed_UPMedalImage;
        medalImageBase[(int)eMedalType.Boots_speed_DOWN] = boots_Speed_DOWNMedalImage;

        medalImageBase[(int)eMedalType.Boots_Spike] = boots_SpikeImage;
    }
    [SerializeField] Sprite boots_Speed_UPMedalImage;
    [SerializeField] Sprite boots_Speed_DOWNMedalImage;
    [SerializeField] Sprite boots_SpikeImage;


    public const int medalPocketNum = 3;
    public int GetMedalPocketNum() { return medalPocketNum; }
    [SerializeField]
    eMedalType[] medalType = new eMedalType[medalPocketNum];
    public eMedalType GetMedalType(int medalPocketNum_) { if (medalPocketNum_ < 0 || medalPocketNum_ >= medalPocketNum) return eMedalType.none; return medalType[medalPocketNum_]; }
    public void SetMedalType(int medalPocketNum_, eMedalType medalType_) { medalType[medalPocketNum_] = medalType_; }

    private void OnEnable()
    {
        SetMedalImageBase();
    }
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        BuffUpdate();
    }

    public void DataSave()
    {
        for (int i = 0; i < medalPocketNum; i++) PlayerPrefs.SetInt("MedalSet : " + i.ToString(), (int)medalType[i]);
    }
    public void DataLoad()
    {
        for (int i = 0; i < medalPocketNum; i++) medalType[i] = (eMedalType)PlayerPrefs.GetInt("MedalSet : " + i.ToString());
    }

    //ƒƒ_ƒ‹Œø‰Ê-------------------------------------
    eMedalType[] oldMedalType = new eMedalType[medalPocketNum];
    void BuffUpdate()
    {
        //if (oldMedalType[0] == medalType[0] && oldMedalType[1] == medalType[1] && oldMedalType[2] == medalType[2]) return;
        bool update = false;
        for (int i = 0; i < oldMedalType.Length; i++) { if (oldMedalType[i] == medalType[i]) continue; update = true; }
        if (!update) return;

        moveSpeedBuff = 1;
        bootsSpike = false;

        for (int i = 0; i < oldMedalType.Length; i++)
        {
            switch (oldMedalType[i])
            {
                case eMedalType.none: continue;

                case eMedalType.Boots_speed_UP: moveSpeedBuff += 0.3f; continue;
                case eMedalType.Boots_speed_DOWN: moveSpeedBuff -= 0.3f; continue;

                case eMedalType.Boots_Spike:bootsSpike = true; continue;
            }
        }
    }
    float moveSpeedBuff = 1;
    public float GetMoveSpeedBuff() { return moveSpeedBuff; }
    bool bootsSpike = false;
    public bool GetBootsSpike() { return bootsSpike; }

}
