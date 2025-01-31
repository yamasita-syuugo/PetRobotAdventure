using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Player : MonoBehaviour
{
    Manager_Player manager_Player;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        manager_Player = GameObject.FindWithTag("Manager").GetComponent<Manager_Player>();
        oldPlayerType = manager_Player.GetPlayerTypeIndex();
        player = Instantiate(manager_Player. GetPlayerTypeBase((int)oldPlayerType));
    }

    // Update is called once per frame
    ePlayerType oldPlayerType;
    void Update()
    {
        if (oldPlayerType == manager_Player.GetPlayerTypeIndex()) return; oldPlayerType = manager_Player.GetPlayerTypeIndex();

        GameObject tmp = Instantiate(manager_Player.GetPlayerTypeBase((int)oldPlayerType));
        tmp .transform.position = player.transform.position;

        GameObject.Destroy(player.gameObject);
        player = tmp;
    }
}
