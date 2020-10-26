using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class is attached to the MainMenu object in the scene and store every button-related functions that are required for the game menu buttons to work properly
//TO BE CONTINUED...

public class GameMenu : MonoBehaviour
{
    private void Awake() {
        this.gameObject.SetActive(false);
    }
    

    public void Resume(){
        //turn the menu off
        this.gameObject.SetActive(false);
    }

    public void Save(){
        //open the save menu
        Debug.Log("SAVE PRESSED");
    }

    public void Load(){
        //open the load menu
        Debug.Log("LOAD PRESSED");
    }

    public void Settings(){
        //open the settings menu
        Debug.Log("SETTINGS PRESSED");
    }

    public void RunAway(){
        //popup if player didnt save before exiting to ask if he still wanna
        Debug.Log("RUNAWAY PRESSED");
        //exit the game
    }

}
