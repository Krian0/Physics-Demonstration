using UnityEngine;

public class KeyManager : MonoBehaviour
{
                                                [Space(10)]
    public KeyCode[] jumpKey;                   [Space(10)]
    public KeyCode[] runKey;                    [Space(10)]
    public KeyCode[] crouchKey;                 [Space(10)]
    public KeyCode[] attackKey;                 [Space(10)]
    public KeyCode[] secondaryAttackKey;        [Space(10)]
    public KeyCode[] useKey;

    public bool getKeyPressing(KeyCode[] keyArray)
    {
        foreach (KeyCode key in keyArray)
            if (Input.GetKey(key))
                return true;

        return false;
    }

    public bool getKeyDown(KeyCode[] keyArray)
    {
        foreach (KeyCode key in keyArray)
            if (Input.GetKeyDown(key))
                return true;

        return false;
    }

    public bool getKeyUp(KeyCode[] keyArray)
    {
        foreach (KeyCode key in keyArray)
            if (Input.GetKeyUp(key))
                return true;

        return false;
    }
}