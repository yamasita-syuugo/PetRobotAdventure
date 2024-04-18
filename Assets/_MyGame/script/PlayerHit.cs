using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    const int blockGetNum = 3;
    [SerializeField] private int flagToBlock = blockGetNum;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Flag") {
            ScoreManager.FlagGetPointAdd();
            Destroy(collision.GameObject());

            GetComponent<PlayerShot>().AddMagazine();

            if(flagToBlock <= 0)
            {
                GetComponent<PlayerCreateBlock>().AddBlockNum();
                flagToBlock = blockGetNum;
            }
            else
            {
                flagToBlock--;
            }

            GetComponentInChildren<BlockUI>().UIUpDate();
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
