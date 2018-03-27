using UnityEngine;

public class EscToQuit : MonoBehaviour
{

	void Update ()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
	}
}
