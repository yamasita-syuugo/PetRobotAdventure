using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerCreateBlock : MonoBehaviour
{
    public GameObject blockPrefab;
    public AudioSource blockCreateSound;
    [SerializeField] private int blockNum = 3;
     
    private bool creatBlockOn = false;

    bool addBlock = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (addBlock)
        {
            blockNum++;
            addBlock = false;
        }

        if (1 <= Input.GetAxis("CreatBlock"))
        {
            if (!creatBlockOn)
            {
                CreatBlock();
                creatBlockOn = true;
            }
        }
        else
        {
            creatBlockOn = false;
        }
    }

    void CreatBlock()
    {
        if (1 <= blockNum)
        {
            float playerPosX, playerPosY, playerDirectionX,playerDirectionY;
            playerPosX = transform.position.x;
            playerPosY = transform.position.y;
            playerDirectionX = GetComponent<PlayerShot>().moveDirectionX;
            playerDirectionY = GetComponent<PlayerShot>().moveDirectionY;
            float posX, posY;
            posX = playerPosX + playerDirectionX;
            posY = playerPosY + playerDirectionY;
        
            GameObject tmp = Instantiate(blockPrefab);
            tmp.transform.position = new Vector3(posX,posY,0);

            blockCreateSound.Play();

            blockNum--;
        }
    }
    public void AddBlockNum()
    {
        addBlock = true;
    }
}
