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
        // ResetSave(); //-- remove
        DontDestroyOnLoad(this);
        Instance = this;
        
        Load();
        // Debug.Log(Helper.Serialize<SaveState>(state)); //-- remove
    } //-- Awake end


    public void Save() {
        //- Save data
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    } //-- Save end


    public void Load() {
        //- Load  data
        if(PlayerPrefs.HasKey("save")) {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        } else {
            state = new SaveState();
            Save();
            //- Debug.Log("No save file exists. Creating a new one."); //-- remove
        }
    } //-- Load end


    public bool IsColorOwned(int index) {
        //- Bitwise check, return true if index is = 1 
        return (state.colorOwned & (1 << index)) != 0;
    } //-- IsColorOwned end

    public bool IsTrailOwned(int index) {
        //- Bitwise check, return true if index is = 1
        return (state.trailOwned & (1 << index)) != 0;
    } //-- IsTrailOwned end


    public bool BuyAttemptColor(int index, int cost) {
        //- Gold validation
        if(state.gold >= cost) {
            state.gold -= cost;
            UnlockColor(index);

            //- Save progress
            Save();
            return true;
        } else {
            return false;
        }
    } //-- BuyAttemptColor

    public bool BuyAttemptTrail(int index, int cost) {
        //- Gold validation
        if(state.gold >= cost) {
            state.gold -= cost;
            UnlockTrail(index);

            //- Save progress
            Save();
            return true;
        } else {
            return false;
        }
    } //-- BuyAttemptColor

    public void UnlockColor(int index) {
        //- Toggle color bit at given index
        state.colorOwned |= 1 << index; 
    } //-- UnlockColor end

    public void UnlockTrail(int index) {
        //- Toggle trail bit at given index
        state.trailOwned |= 1 << index; 
    } //-- UnlockTrail end

    public void CompleteLevel(int index) {
        if(state.completedLevel == index) {
            state.completedLevel++;
            Save();    
        }
    } //-- CompleteLevel end

    //- For testing. remove
    public void ResetSave() {
        PlayerPrefs.DeleteKey("save");
    } //-- ResetSave end
}


/*
Project Name : Paper Plane 3D
Created by   : Sir Reyyy
*/