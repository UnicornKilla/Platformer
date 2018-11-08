using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    private void OnMouseUpAsButton()
    {
        switch (gameObject.name)
        {
            case "Play":
                Application.LoadLevel(1);
                break;
        }

        switch (gameObject.name)
        {
            case "Quit":
                Application.Quit();
                break;
        }
    }
}
