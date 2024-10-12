using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Technique_Player_EarthQuake : MonoBehaviour
{
    [SerializeField]
    GameObject blockPrefab;
    AudioSource blockCreateSound;
    // Start is called before the first frame update
    void Start()
    {
        blockCreateSound = GameObject.FindWithTag("Manager").GetComponent<Manager_Sounds>().GetSound("installation");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool creatBlockOn = false;
    public void ControllerCreatBlock()
    {
                float playerPosX, playerPosY, playerDirectionX, playerDirectionY;
                playerPosX = transform.position.x;
                playerPosY = transform.position.y;
                playerDirectionX = GetComponent<Technique_Player_BulletShot>().moveDirectionX;
                playerDirectionY = GetComponent<Technique_Player_BulletShot>().moveDirectionY;
                posX = playerPosX + playerDirectionX;
                posY = playerPosY + playerDirectionY;

                CreatBlock();
    }
     public void MouseCreatBlock()
    {
       
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posX = pos.x;
        posY = pos.y;

        CreatBlock();
    }
    float posX, posY;
    [SerializeField]
    GameObject impact;
    void CreatBlock()
    {
        if (!GetComponent<Technique_Player_MaterialBag>().MaterialsCheck()) return;
        GetComponent<Technique_Player_MaterialBag>().AddEarthQuakeMaterials(-1);

        GameObject tmp = Instantiate(blockPrefab);
        tmp.transform.position = new Vector3(posX, posY, 0);
        tmp.transform.parent = GameObject.FindWithTag("Create").transform;
        GameObject imp = Instantiate<GameObject>(impact);
        imp.transform.position = tmp.transform.position;

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
            GetComponent<Technique_Player_MaterialBag>().AddEarthQuakeMaterials();
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
