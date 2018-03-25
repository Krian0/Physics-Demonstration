using UnityEngine;

public enum eActions     { JUMP = 0, RUN, CROUCH, USE, ENUM_END }
public enum eInputConfig   { KEYBOARD = 0, CONTROLLER, ENUM_END }


//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  INPUT MANAGER
//
//  Contains:   An array of Keyset scriptable objects, size adjustable in the Inspector;
//              An eInputConfig variable;
//
//  Use:        Require this as a component of the controllable character;
//              SetCurrentInput with an eInputConfig to switch between Keysets;
//              Use GetKey functions just as Input.GetKey functions would be, except with an eActions in place of a KeyCode;
//
//  Purpose:    To contain the sets of KeyCodes for different Input types such as Keyboard and Controller configurations;
//              To provide another script access to KeyPair GetKey functions, which detect the called function for either of the KeyCodes 
//              the KeyPair contains;
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  Notes:      eActions and eInputConfig can be customized as needed, the Keysets will always be based on the eActions size, in correct order, 
//              so long as the last element is "ENUM_END".
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------


public class InputManager : MonoBehaviour
{
    public Keyset[] keysets = new Keyset[(int)eInputConfig.ENUM_END - 1];
    private eInputConfig currentConfig = eInputConfig.KEYBOARD;


    public bool getKey(eActions eName)
    {
        return keysets[(int)currentConfig].keys[(int)eName].getKey();
    }

    public bool getKeyDown(eActions eName)
    {
        return keysets[(int)currentConfig].keys[(int)eName].getKeyDown();
    }

    public bool getKeyUp(eActions eName)
    {
        return keysets[(int)currentConfig].keys[(int)eName].getKeyUp();
    }



    public void setCurrentInput(eInputConfig inputType)
    {
        currentConfig = inputType;
    }
}