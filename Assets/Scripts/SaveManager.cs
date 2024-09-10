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
        ResetSave();
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


    public bool IsColorOwned(int index) {
        return (state.colorOwned & (1 << index)) != 0;
    } //-- IsColorOwned end

    public bool IsTrailOwned(int index) {
        return (state.trailOwned & (1 << index)) != 0;
    } //-- IsTrailOwned end


    public bool BuyAttemptColor(int index, int cost) {
        if(state.gold >= cost) {
            state.gold -= cost;
            UnlockColor(index);

            // Save progress
            Save();
            return true;
        } else {
            return false;
        }
    } //-- BuyAttemptColor

    public bool BuyAttemptTrail(int index, int cost) {
        if(state.gold >= cost) {
            state.gold -= cost;
            UnlockTrail(index);

            // Save progress
            Save();
            return true;
        } else {
            return false;
        }
    } //-- BuyAttemptColor

    public void UnlockColor(int index) {
        // toggle color bit at given index
        state.colorOwned |= 1 << index; 
    } //-- UnlockColor end

    public void UnlockTrail(int index) {
        // toggle trail bit at given index
        state.trailOwned |= 1 << index; 
    } //-- UnlockTrail end


    // for testing only
    public void ResetSave() {
        PlayerPrefs.DeleteKey("save");
    } //-- ResetSave end
}


/*
Project Name : Paper Plane 3D
Created by   : Sir Reyyy
*/