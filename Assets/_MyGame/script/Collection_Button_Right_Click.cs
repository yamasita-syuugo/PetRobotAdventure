using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection_Button_Right_Click : MonoBehaviour
{
    Manager_Player manager_Player;
    Manager_Player_Technique manager_Player_Technique;
    Manager_PlayerController manager_PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        manager_Player = manager.GetComponent<Manager_Player>();
        manager_Player_Technique = manager.GetComponent<Manager_Player_Technique>();
        manager_PlayerController = manager.GetComponent<Manager_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        RightClick();
    }
    ePlayerType playerType = ePlayerType.none;
    public void SetPlayerType(ePlayerType playerType_) { playerType = playerType_; }
    int TechniqueIndex = 0;
    public void SetTechniqueIndex(int techniqueIndex_) { TechniqueIndex = techniqueIndex_; }
    void RightClick()
    {
        if (playerType == ePlayerType.none) return;

        if (onMouse == true && manager_PlayerController.GetMouseButtonDown(1))
        {
            manager_Player.SetPlayerTypeIndex(playerType);
            manager_Player_Technique.SetTwo(TechniqueIndex);
        }
    }

    bool onMouse = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MousePointer") onMouse = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MousePointer") onMouse = false;
    }
}
