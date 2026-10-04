using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum eJoystickButton
{
    square              = UnityEngine.KeyCode.Joystick1Button0,
    cross               = UnityEngine.KeyCode.Joystick1Button1,
    circle              = UnityEngine.KeyCode.Joystick1Button2,
    triangle            = UnityEngine.KeyCode.Joystick1Button3,
    L1                  = UnityEngine.KeyCode.Joystick1Button4,
    R1                  = UnityEngine.KeyCode.Joystick1Button5,
    L2                  = UnityEngine.KeyCode.Joystick1Button6,
    R2                  = UnityEngine.KeyCode.Joystick1Button7,
    share               = UnityEngine.KeyCode.Joystick1Button8,
    Start               = UnityEngine.KeyCode.Joystick1Button9,
    Lstick              = UnityEngine.KeyCode.Joystick1Button10,
    Rstick              = UnityEngine.KeyCode.Joystick1Button11,
    home                = UnityEngine.KeyCode.Joystick1Button12,
    Center              = UnityEngine.KeyCode.Joystick1Button13,
    Joystick1Button14   = UnityEngine.KeyCode.Joystick1Button14,
    Joystick1Button15   = UnityEngine.KeyCode.Joystick1Button15,
    Joystick1Button16   = UnityEngine.KeyCode.Joystick1Button16,
    Joystick1Button17   = UnityEngine.KeyCode.Joystick1Button17,
    Joystick1Button18   = UnityEngine.KeyCode.Joystick1Button18,
    Joystick1Button19   = UnityEngine.KeyCode.Joystick1Button19,
}

public enum eController
{
    [InspectorName("")] none = -1,

    mouse,
    pad,

    [InspectorName("")] max,
}
public enum eTechnicControl
{
    [InspectorName("")] none = -1,

    one,    //左クリック
    two,    //右クリック

    [InspectorName("")] max,
}
public enum ePushType
{
    down,
    stey,
    up,

    [InspectorName("")] max,
}

public class Manager_PlayerController : MonoBehaviour
{
    public const int padNumMax = 4;

    Vector2 transfer = new Vector2();//移動
    public Vector2 GetTransfer() { return transfer; }
    private void UpdateTransfer()
    {
        float horizontal =
            ((GetKey(KeyCode.D) ? 1 : 0) - (GetKey(KeyCode.A) ? 1 : 0)) +
            (Input.GetAxis("Horizontal")/* + Input.GetAxis("Horizontal-ArrowKey")*/);//todo:コントローラースティックの値を直接取得する方法
        if (horizontal < -1) horizontal = -1; else if (horizontal > 1) horizontal = 1;
        float vertical =
            ((GetKey(KeyCode.W) ? 1 : 0) - (GetKey(KeyCode.S) ? 1 : 0)) +
            (Input.GetAxis("Vertical")/* + Input.GetAxis("Vertical-ArrowKey")*/);
        if (vertical < -1) vertical = -1; else if (vertical > 1) vertical = 1;
        transfer = new Vector2(horizontal, vertical);

        if (Input.GetAxis("Horizontal") < 0) Debug.Log("L : ←"); else if (Input.GetAxis("Horizontal") > 0) Debug.Log("L : →"); else Debug.Log("L : ↔");
        if (Input.GetAxis("Vertical") < 0) Debug.Log("L : ↓"); else if (Input.GetAxis("Horizontal") > 0) Debug.Log("L : ↑"); else Debug.Log("L : ↕");
    }

    Vector2 aim = new Vector2();
    public Vector2 GetAim() { return aim; }
    const float aimTolerance = 0.05f;
    private void UpdateAim()
    {
        float tmpX = Input.GetAxis("AimX"), tmpY = Input.GetAxis("AimY");
        if (tmpX < -aimTolerance || tmpX > aimTolerance) aim.x = tmpX; else aim.x = 0.0f;
        if (tmpY < -aimTolerance || tmpY > aimTolerance) aim.y = tmpY; else aim.y = 0.0f;
    }

    bool GetKey(KeyCode key) { return Input.GetKey(key); }
    bool GetKey(eJoystickButton key) { return Input.GetKey((KeyCode)key); }
    bool GetKey(string key) { return Input.GetKey(key); }
    bool GetKeyDown(KeyCode key) { return Input.GetKeyDown(key); }
    bool GetKeyDown(eJoystickButton key) { return Input.GetKeyDown((KeyCode)key); }
    bool GetKeyDown(string key) { return Input.GetKeyDown(key); }
    bool GetKeyUp(KeyCode key) { return Input.GetKeyUp(key); }
    bool GetKeyUp(eJoystickButton key) { return Input.GetKeyUp((KeyCode)key); }
    bool GetKeyUp(string key) { return Input.GetKeyUp(key); }

    Vector2 GetMousePoint() { return Input.mousePosition; }
    public bool GetMouseButton(int num) { return Input.GetMouseButton(num); }
    public bool GetMouseButtonDown(int num) { return Input.GetMouseButtonDown(num); }
    public bool GetMouseButtonUp(int num) { return Input.GetMouseButtonUp(num); }

    bool[,] technicMouse;
    public bool GetTechnicMouse(ePushType pushType, eTechnicControl index) { return technicMouse[(int)pushType, (int)index]; }
    private void UpdateTechnicMouse()
    {
        technicMouse = new bool[(int)ePushType.max, (int)eTechnicControl.max];
        technicMouse[(int)ePushType.down, (int)eTechnicControl.one] = Input.GetMouseButtonDown(0);
        technicMouse[(int)ePushType.down, (int)eTechnicControl.two] = Input.GetMouseButtonDown(1);
        technicMouse[(int)ePushType.stey, (int)eTechnicControl.one] = Input.GetMouseButton(0);
        technicMouse[(int)ePushType.stey, (int)eTechnicControl.two] = Input.GetMouseButton(1);
        technicMouse[(int)ePushType.up, (int)eTechnicControl.one] = Input.GetMouseButtonUp(0);
        technicMouse[(int)ePushType.up, (int)eTechnicControl.two] = Input.GetMouseButtonUp(1);
    }
    bool[,,] technicPad;
    public bool GetTechnicPad(ePushType pushType, eTechnicControl index) { for(int i = 0;i < padNumMax; i++)if(technicPad[i, (int)pushType, (int)index]) return true ;return false; }
    public bool GetTechnicPad(ePushType pushType, eTechnicControl index, int padNum/*0～7*/) { return technicPad[padNum, (int)pushType, (int)index]; }
    private void UpdateTechnicPad()
    {
        technicPad = new bool[padNumMax, (int)ePushType.max, (int)eTechnicControl.max];
        string[] padName = Input.GetJoystickNames();
        for (int i = 0; i < padName.Length; i++)
        {
            if (padName[i] == "") continue;
            technicPad[i, (int)ePushType.down, (int)eTechnicControl.one] = GetKeyDown(eJoystickButton.R1 + i * 20);
            technicPad[i, (int)ePushType.down, (int)eTechnicControl.two] = GetKeyDown(eJoystickButton.R2 + i * 20);
            technicPad[i, (int)ePushType.stey, (int)eTechnicControl.one] = GetKey(eJoystickButton.R1 + i * 20);
            technicPad[i, (int)ePushType.stey, (int)eTechnicControl.two] = GetKey(eJoystickButton.R2 + i * 20);
            technicPad[i, (int)ePushType.up, (int)eTechnicControl.one] = GetKeyUp(eJoystickButton.R1 + i * 20);
            technicPad[i, (int)ePushType.up, (int)eTechnicControl.two] = GetKeyUp(eJoystickButton.R2 + i * 20);
        }
        //technicPad[(int)ePushType.up, (int)eTechnicControl.two] = GetKeyUp("joystick button " + (5 + (int)eTechnicControl.two * 2).ToString());
    }
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        UpdateTransfer();
        UpdateTechnicMouse();
        UpdateTechnicPad();
        UpdateAim();

        EventSystemButtonPlay();

        JoystickButtonTest();
    }

    int[] num = new int[19];
    void JoystickButtonTest()
    {
        string[] name = Input.GetJoystickNames();
        for (int i = 0; i < name.Length; i++) Debug.Log("JoystickType - " + (i + 1) + " / " + name.Length + " - " + name[i]);
        for (int i = 0; i < name.Length; i++)
        {
            float axisCheckSize = 0.95f;
            if (i < 4)
            {
                if (Input.GetAxis("Horizontal_Pad" + (i + 1)) < -axisCheckSize || Input.GetAxis("Horizontal_Pad" + (i + 1)) > axisCheckSize) Debug.Log("Joystick" + (i + 1) + " - Horizontal_Pad" + (i + 1) + " - " + Input.GetAxis("Horizontal_Pad" + (i + 1)));
                if (Input.GetAxis("Vertical_Pad" + (i + 1)) < -axisCheckSize || Input.GetAxis("Vertical_Pad" + (i + 1)) > axisCheckSize) Debug.Log("Joystick" + (i + 1) + " - Vertical_Pad" + (i + 1) + " - " + Input.GetAxis("Vertical_Pad" + (i + 1)));
                if (Input.GetAxis("AimX") < -axisCheckSize || Input.GetAxis("AimX") > axisCheckSize) Debug.Log("Joystick" + (i + 1) + " - AimX - " + Input.GetAxis("AimX"));
                if (Input.GetAxis("AimY") < -axisCheckSize || Input.GetAxis("AimY") > axisCheckSize) Debug.Log("Joystick" + (i + 1) + " - AimY - " + Input.GetAxis("AimY"));
            }
            if (Input.GetAxis("Horizontal") < -axisCheckSize || Input.GetAxis("Horizontal") > axisCheckSize) Debug.Log("Joystick" + (i + 1) + " - Horizontal - " + Input.GetAxis("Horizontal"));
            if (Input.GetAxis("Vertical") < -axisCheckSize || Input.GetAxis("Vertical") > axisCheckSize) Debug.Log("Joystick" + (i + 1) + " - Vertical - " + Input.GetAxis("Vertical"));
            if (Input.GetAxis("AimX") < -axisCheckSize || Input.GetAxis("AimX") > axisCheckSize) Debug.Log("Joystick" + (i + 1) + " - AimX - " + Input.GetAxis("AimX"));
            if (Input.GetAxis("AimY") < -axisCheckSize || Input.GetAxis("AimY") > axisCheckSize) Debug.Log("Joystick" + (i + 1) + " - AimY - " + Input.GetAxis("AimY"));
            switch (name[i])
            {
                case "Wireless Controller":
                    if (GetKeyDown(KeyCode.Joystick1Button0 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - ☐ - " + (num[0]++));
                    if (GetKeyDown(KeyCode.Joystick1Button1 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - × - " + (num[1]++));
                    if (GetKeyDown(KeyCode.Joystick1Button2 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - 〇 - " + (num[2]++));
                    if (GetKeyDown(KeyCode.Joystick1Button3 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - △ - " + (num[3]++));
                    if (GetKeyDown(KeyCode.Joystick1Button4 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - L1 - " + (num[4]++));
                    if (GetKeyDown(KeyCode.Joystick1Button5 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - R1 - " + (num[5]++));
                    if (GetKeyDown(KeyCode.Joystick1Button6 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - L2 - " + (num[6]++));
                    if (GetKeyDown(KeyCode.Joystick1Button7 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - R2 - " + (num[7]++));
                    if (GetKeyDown(KeyCode.Joystick1Button8 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - share - " + (num[8]++));
                    if (GetKeyDown(KeyCode.Joystick1Button9 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Start - " + (num[9]++));
                    if (GetKeyDown(KeyCode.Joystick1Button10 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Lstick - " + (num[10]++));
                    if (GetKeyDown(KeyCode.Joystick1Button11 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Rstick - " + (num[11]++));
                    if (GetKeyDown(KeyCode.Joystick1Button12 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - home - " + (num[12]++));
                    if (GetKeyDown(KeyCode.Joystick1Button13 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Center - " + (num[13]++));
                    if (GetKeyDown(KeyCode.Joystick1Button14 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button14 - " + (num[14]++));
                    if (GetKeyDown(KeyCode.Joystick1Button15 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button15 - " + (num[15]++));
                    if (GetKeyDown(KeyCode.Joystick1Button16 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button16 - " + (num[16]++));
                    if (GetKeyDown(KeyCode.Joystick1Button17 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button17 - " + (num[17]++));
                    if (GetKeyDown(KeyCode.Joystick1Button18 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button18 - " + (num[18]++));
                    if (GetKeyDown(KeyCode.Joystick1Button19 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button19 - " + (num[19]++));
                    break;


                default:
                    if (GetKeyDown(KeyCode.Joystick1Button0 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button0 - " + (num[0]++));
                    if (GetKeyDown(KeyCode.Joystick1Button1 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button1 - " + (num[1]++));
                    if (GetKeyDown(KeyCode.Joystick1Button2 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button2 - " + (num[2]++));
                    if (GetKeyDown(KeyCode.Joystick1Button3 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button3 - " + (num[3]++));
                    if (GetKeyDown(KeyCode.Joystick1Button4 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button4 - " + (num[4]++));
                    if (GetKeyDown(KeyCode.Joystick1Button5 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button5 - " + (num[5]++));
                    if (GetKeyDown(KeyCode.Joystick1Button6 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button6 - " + (num[6]++));
                    if (GetKeyDown(KeyCode.Joystick1Button7 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button7 - " + (num[7]++));
                    if (GetKeyDown(KeyCode.Joystick1Button8 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button8 - " + (num[8]++));
                    if (GetKeyDown(KeyCode.Joystick1Button9 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button9 - " + (num[9]++));
                    if (GetKeyDown(KeyCode.Joystick1Button10 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button10 - " + (num[10]++));
                    if (GetKeyDown(KeyCode.Joystick1Button11 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button11 - " + (num[11]++));
                    if (GetKeyDown(KeyCode.Joystick1Button12 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button12 - " + (num[12]++));
                    if (GetKeyDown(KeyCode.Joystick1Button13 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button13 - " + (num[13]++));
                    if (GetKeyDown(KeyCode.Joystick1Button14 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button14 - " + (num[14]++));
                    if (GetKeyDown(KeyCode.Joystick1Button15 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button15 - " + (num[15]++));
                    if (GetKeyDown(KeyCode.Joystick1Button16 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button16 - " + (num[16]++));
                    if (GetKeyDown(KeyCode.Joystick1Button17 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button17 - " + (num[17]++));
                    if (GetKeyDown(KeyCode.Joystick1Button18 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button18 - " + (num[18]++));
                    if (GetKeyDown(KeyCode.Joystick1Button19 + i * 20)) Debug.Log("Joystick" + (i + 1) + " - Button19 - " + (num[19]++));
                    break;
            }
        }
        //Debug.Log("十字キー : " + Input.GetAxis("7th axis").ToString());
    }

    public bool JoystickButton(eJoystickButton button , int joyConNum = 0) { return Input.GetKey((KeyCode)button + joyConNum * 20); }
    public bool JoystickButtonDown(eJoystickButton button) {  for(int i = 0;i < padNumMax;i++)if(Input.GetKeyDown((KeyCode)button + i * 20))return true;return false; }
    public bool JoystickButtonDown(eJoystickButton button, int joyConNum) {return Input.GetKeyDown((KeyCode)button + joyConNum * 20); }
    public bool JoystickButtonDownAll() { for (int i = 0; i < 20 * 4; i++) if (Input.GetKeyDown((KeyCode)(KeyCode.Joystick1Button0 + i))) return true; return false; }
    public bool JoystickButtonDownAll(int joyConNum) { for (int i = 0; i < 20 * 4; i++) if (Input.GetKeyDown((KeyCode)(joyConNum * 20 + KeyCode.Joystick1Button0 + i))) return true; return false; }
    public bool JoystickButtonUp(eJoystickButton button, int joyConNum = 0) { return Input.GetKeyUp((KeyCode)button + joyConNum * 20); }

    void EventSystemButtonPlay()
    {
        bool buttonDown = false;for(int i = 0;i < padNumMax;i++ ) if(JoystickButtonDown(eJoystickButton.circle,i) || JoystickButtonDown(eJoystickButton.cross,i))buttonDown = true;
        if (buttonDown) EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
    }
}
