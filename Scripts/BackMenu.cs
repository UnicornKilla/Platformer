using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMenu : MonoBehaviour
{

    private void OnMouseUpAsButton()
    {
        switch (gameObject.name)
        {
            case "Menu":
                Application.LoadLevel(0);
                break;
        }
    }
}
