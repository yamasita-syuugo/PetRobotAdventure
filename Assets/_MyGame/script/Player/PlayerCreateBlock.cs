using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerCreateBlock : MonoBehaviour
{
    public GameObject blockPrefab;
    public AudioSource blockCreateSound;
    [SerializeField] 
    private int blockNum = 3;

    [SerializeField]
    bool addBlock = false;
    // Start is called before the first frame update
    void Start()
    {
        blockCreateSound = GameObject.Find("installation").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (addBlock)
        {
            blockNum++;
            addBlock = false;
        }

        ControllerCreatBlock();
        MouseCreatBlock();
    }

    private bool creatBlockOn = false;
    void ControllerCreatBlock()
    {
        if (1 <= Input.GetAxis("CreatBlock"))
        {
            if (!creatBlockOn)
            {
                float playerPosX, playerPosY, playerDirectionX, playerDirectionY;
                playerPosX = transform.position.x;
                playerPosY = transform.position.y;
                playerDirectionX = GetComponent<PlayerShot>().moveDirectionX;
                playerDirectionY = GetComponent<PlayerShot>().moveDirectionY;
                posX = playerPosX + playerDirectionX;
                posY = playerPosY + playerDirectionY;

                CreatBlock();
                creatBlockOn = true;
            }
        }
        else
        {
            creatBlockOn = false;
        }
    }
     void MouseCreatBlock()
    {
        if (Input.GetMouseButtonDown(1) == false) return;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posX = pos.x;
        posY = pos.y;
        CreatBlock();
    }
    float posX, posY;
    void CreatBlock()
    {
        if (GetComponent<ObjectFall>().GetSituation() == ObjectFall.eSituation.fall) return;

        if (1 <= blockNum)
        {
        
            GameObject tmp = Instantiate(blockPrefab);
            tmp.transform.position = new Vector3(posX,posY,0);

            blockCreateSound.Play();

            GameObject.Find("CreateEnemy").GetComponent<EnemyCreate>().GolemCountAdd();

            blockNum--;
        }
    }
    [SerializeField]
    const int blockGetNum = 3;
    [SerializeField] private int flagToBlock = blockGetNum;
    public void FlagToBlock()
    {
        if (flagToBlock <= 0)
        {
            AddBlockNum();
            flagToBlock = blockGetNum;
        }
        else
        {
            flagToBlock--;
        }
        GetComponentInChildren<BlockUI>().UIUpDate();
    }
    public void AddBlockNum()
    {
        addBlock = true;
    }

    public int GetBlockNum() {
        return blockNum;
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
