using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectFall;

public class Manager_Save : MonoBehaviour
{
    public void DataSave()
    {
        GetComponent<Manager_StageSelect>().DataSave();

        GetComponent<Manager_Player>().DataSave();
        GetComponent<Manager_Player_Technique>().DataSave();

        GetComponent<Manager_BackgroundType>().DataSave();
        GetComponent<Manager_MousePointerType>().DataSave();
        GetComponent<Manager_Music>().DataSave();

        GetComponent<Manager_Collection>().DataSave();

        GetComponent<Manager_Gacha>().DataSave();

        GetComponent<Manager_GameSituation>().DataSave();   //ƒNƒŠƒA‚©Ž¸”s‚©‚Ì•Û‘¶

    }
    public void DataLoad()
    {
        GetComponent<Manager_StageSelect>().DataLoad();

        GetComponent<Manager_Player>().DataLoad();
        GetComponent<Manager_Player_Technique>().DataLoad();

        GetComponent<Manager_BackgroundType>().DataLoad();
        GetComponent<Manager_MousePointerType>().DataLoad();
        GetComponent<Manager_Music>().DataLoad();

        GetComponent<Manager_Collection>().DataLoad();

        GetComponent<Manager_Gacha>().DataLoad();

        GetComponent<Manager_GameSituation>().DataLoad();   //ƒNƒŠƒA‚©Ž¸”s‚©‚Ì•Û‘¶

    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    private void OnApplicationQuit()
    {
        DataLoad();
    }

    static public void BoolSave(string name,int size, bool[] data)
    {
        for (int i = 0; i < size; i += 16)
        {
            int boolNum = 0;
            for (int j = i; j < i + 16 && j < size; j++)
            {
                int boolNumBit = 1;
                for (int bit = 0; bit < j - i; bit++) boolNumBit *= 2;
                boolNum += boolNumBit * (data[j] ? 1 : 0);
            }
            PlayerPrefs.SetInt(name + (i / 16).ToString(), boolNum);
        }
    }
    static public void BoolLoad(string name,int size, out bool[] data)
    {
        data = new bool[size];
        for (int i = 0; i < size; i += 16)
        {
            int boolNum = PlayerPrefs.GetInt(name + (i / 16).ToString());
            for (int j = i + 15; i <= j; j--)
            {
                if (j >= size) continue;
                int boolNumBit = 1;
                for (int bit = 0; bit < j - i; bit++) boolNumBit *= 2;
                if (boolNum / boolNumBit >= 1) { data[j] = true; boolNum -= boolNumBit; }
                else { data[j] = false; }
            }
        }
    }
}
