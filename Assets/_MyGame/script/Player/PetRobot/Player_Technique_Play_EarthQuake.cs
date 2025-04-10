using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Player_Technique_Play_EarthQuake : Player_Technique_Play__Base
{
    Manager_StageSelect manager_StageSelect;
    Manager_Field manager_Field;

    AudioSource blockCreateSound;

    private void OnEnable()
    {
        manager_StageSelect = GameObject.FindWithTag("Manager").GetComponent<Manager_StageSelect>();
        manager_Field = GameObject.FindWithTag("Manager").GetComponent<Manager_Field>();
    }
    // Start is called before the first frame update
    void Start()
    {
        a = 1 - manager_StageSelect.GetStageData(manager_StageSelect.GetStage()).GetFieldSize() % 2;

        blockCreateSound = GameObject.FindWithTag("Manager").GetComponent<Manager_Sounds>().GetSound(eSoundType.impact);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private bool creatBlockOn = false;
    override public void ControllerPlay()
    {
        float playerPosX, playerPosY, playerDirectionX, playerDirectionY;
        playerPosX = transform.position.x;
        playerPosY = transform.position.y;
        playerDirectionX = Input.GetAxis("AimX"); ;
        playerDirectionY = Input.GetAxis("AimY"); ;
        posX = playerPosX + playerDirectionX;
        posY = playerPosY + playerDirectionY;

        CreatBlock();
    }
    int a;
    override public void MousePlay()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posX = pos.x; if (posX > 0 && posX % 1 > 0.5f + a * 0.5f) posX = (int)posX + 1 + a * 0.5f; else if (posX < 0 && posX % 1 < -0.5f + a * 0.5f) posX = (int)posX - 1 + a * 0.5f; else posX = (int)posX + a * 0.5f;
        posY = pos.y; if (posY > 0 && posY % 1 > 0.5f + a * 0.5f) posY = (int)posY + 1 + a * 0.5f; else if (posY < 0 && posY % 1 < -0.5f + a * 0.5f) posY = (int)posY - 1 + a * 0.5f; else posY = (int)posY + a * 0.5f;

        CreatBlock();
    }
    float posX, posY;
    [SerializeField]
    GameObject impact;
    void CreatBlock()
    {
        if (!GetComponent<Player_Technique_Container_MaterialBag>().MaterialsCheck()) return;
        GetComponent<Player_Technique_Container_MaterialBag>().AddEarthQuakeMaterials(-1);

        GameObject tmp = Instantiate(manager_Field.ScaffoldSelect());
        tmp.transform.position = new Vector3(posX, posY, 0);
        tmp.transform.parent = GameObject.FindWithTag("Create").transform;
        GameObject imp = Instantiate<GameObject>(impact);
        imp.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//todo:コントローラーで生成時マウスの位置に出る

        blockCreateSound.Play();

        GameObject.FindWithTag("Create").GetComponent<Create_Enemy>().GolemCountAdd();


    }
    [SerializeField]
    const int blockGetNum = 3;
    [SerializeField] private int flagToBlock = blockGetNum;
    public void FlagToBlock()
    {
        if (flagToBlock <= 0)
        {
            GetComponent<Player_Technique_Container_MaterialBag>().AddEarthQuakeMaterials();
            flagToBlock = blockGetNum;
        }
        else
        {
            flagToBlock--;
        }
    }

    public int GetBlockGetNum()
    {
        return blockGetNum;
    }
    public int GetFlagToBlock()
    {
        return flagToBlock;
    }
}
