using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Chara_Image : MonoBehaviour
{
    Manager_Player manager_Player;
    //Start is called before the first frame update
    void Start()
    {
        manager_Player = GameObject.FindWithTag("Manager").GetComponent<Manager_Player>();
    }

    // Update is called once per frame
    int oldPlayerType = -1;
    void Update()
    {
        int playerTypeIndex = manager_Player.GetPlayerTypeIndex();
        if (oldPlayerType == playerTypeIndex) return; oldPlayerType = playerTypeIndex;

        RuntimeAnimatorController animator = manager_Player.GetPlayerIconAnimaterBase(playerTypeIndex);

        if (animator != null) GetComponent<Animator>().runtimeAnimatorController = animator;
        else
        {
            GetComponent<Animator>().runtimeAnimatorController = null;
            GetComponent<Image>().sprite = manager_Player.GetPlayerTypeBase(playerTypeIndex).GetComponent<SpriteRenderer>().sprite;
        }
    }
}
