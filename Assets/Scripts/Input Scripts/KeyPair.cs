using UnityEngine;


//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  KEY PAIR
//
//  Contains:   A string that displays in place of "Element #" in KeyPair Lists/Arrays;
//              A main KeyCode;
//              An alternate KeyCode;
//
//  Use:        N/A;
//
//  Purpose:    To contain a main and an alternate KeyCode, primarily to detect whether either the main or alternate keys have any input
//              in a form that allows containing classes to track what kind of action these two KeyCodes belong to;
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  Notes:      N/A;
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------


[System.Serializable]
public class KeyPair
{
    [HideInInspector]
    public string name;

    public KeyCode mainKey;
    public KeyCode altKey;


    public KeyPair(string pairName)
    {
        name = pairName;
    }



    public bool getKey()
    {
        return (Input.GetKey(mainKey) || Input.GetKey(altKey));
    }

    public bool getKeyDown()
    {
        return (Input.GetKeyDown(mainKey) || Input.GetKeyDown(altKey));
    }

    public bool getKeyUp()
    {
        return (Input.GetKeyUp(mainKey) || Input.GetKeyUp(altKey));
    }
}