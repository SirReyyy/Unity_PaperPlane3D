using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //----- Variables
    public static SaveManager Instance { set; get; }
    public SaveState state;


    //----- Functions

    void Awake() {
        DontDestroyOnLoad(this);
        Instance = this;
        Load();

        Debug.Log(Helper.Serialize<SaveState>(state));
    } //-- Awake end


    public void Save() {
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    } //-- Save end


    public void Load() {
        if(PlayerPrefs.HasKey("save")) {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        } else {
            state = new SaveState();
            Save();
            Debug.Log("No save file exists. Creating a new one.");
        }


    } //-- Load end

}


/*
Project Name : Paper Plane 3D
Created by   : Sir Reyyy
*/