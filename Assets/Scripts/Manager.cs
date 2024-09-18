using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { set; get; }

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    } //-- Awake end


    public int currentLevel = 0;    //- For changing from menu to game levels
    public int menuFocus = 0;

}


/*
Project Name : Paper Plane 3D
Created by   : Sir Reyyy
*/