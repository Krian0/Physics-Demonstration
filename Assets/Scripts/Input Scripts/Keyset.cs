using System;
using System.Collections.Generic;
using UnityEngine;


//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  KEY SET
//
//  Contains:   A List of KeyPairs set in order based on the size of eActions, in order;
//              An int to store the eActions end index (ENUM_END);
//
//  Use:        Right click > Create > Keyset in Unity to create the scriptable object;
//              Set the values of the scriptable object in the inspector;
//              Attach the object to the Keyset Array field in the Input Manager;
//
//  Purpose:    To contain the KeyCodes for each action specified by the eActions enum; 
//              To provide the Input Manager access to KeyPair GetKey functions;
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  Notes:      eActions can be customized as needed, the Keyset will always be based on the eKeyName size, in correct order, so long as the 
//              last element is "ENUM_END";
//              
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------


[CreateAssetMenu(menuName = "Keyset")]
public class Keyset : ScriptableObject
{
    public List<KeyPair> keys = createList();                                  
    private const int sizeLimit = (int)eActions.ENUM_END;


    //Remove or add KeyPairs appropriately if the user changes the array size to a number other than ENUM_END
    private void OnValidate()
    {
        if (keys.Count == sizeLimit)
            return;

        else if (keys.Count > sizeLimit)
            keys.RemoveRange(sizeLimit, keys.Count - sizeLimit);

        else
            for (int i = keys.Count; i < sizeLimit; i++)
                keys.Add(new KeyPair(((eActions)i).ToString()));
    }


    //Creates and return a list of KeyPairs based on the eKeyActions enum (list is the same size minus ENUM_END, each element is named after the appropriate eKeyActions)
    private static List<KeyPair> createList()
    {
        List<KeyPair> k = new List<KeyPair>();

        foreach (eActions action in Enum.GetValues(typeof(eActions)))
            k.Add(new KeyPair(action.ToString()));     
                                                              
        return k;           
    }
}