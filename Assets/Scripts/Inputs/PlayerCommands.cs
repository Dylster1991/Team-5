using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ThisFunction will handle all the keyboard inputs of the player. (not finished)

public class PlayerCommands : MonoBehaviour
{
    public GameObject menu;

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            menu.SetActive(!menu.activeSelf);
        }

        //you cant do anything in the game as long as the menu is open
        if(menu.activeSelf)
            return;

        
    }
}
