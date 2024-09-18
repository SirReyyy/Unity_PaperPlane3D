using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    //----- Variables



    //----- Functions

    void Awake() {
        // code
    } //-- Awake end


    public void CompleteLevel() {
        //- Complete level and Save progress
        SaveManager.Instance.CompleteLevel(Manager.Instance.currentLevel);

        //- Refocus menu
        Manager.Instance.menuFocus = 1;
        ExitScene();
    } //-- CompleteLevel end

    public void ExitScene() {
        SceneManager.LoadScene("Menu");
    } //-- ExitScene end
}


/*
Project Name : Paper Plane 3D
Created by   : Sir Reyyy
*/