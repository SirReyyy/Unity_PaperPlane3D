using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //----- Variables
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.4f;

    //- Main Container
    public RectTransform menuContainer;
    public Transform colorPanel;
    public Transform trailPanel;
    public Transform levelPanel;

    public Text colorBuySetTxt;
    public Text trailBuySetTxt;
    public Text goldText;

    //- Color and Trail cost
    private int[] colorCost = new int[] {0, 5, 5, 5, 10, 10, 15, 15, 20, 20};
    private int[] trailCost = new int[] {0, 20, 20, 40, 40, 40, 60, 60, 80, 80};
    
    private int selectedColorIndex;
    private int selectedTrailIndex;
    private int activeColorIndex;
    private int activeTrailIndex;

    private Vector3 desiredMenuPos;


    //----- Functions

    void Start() {
        SaveManager.Instance.state.gold = 99; //-- for testing
        UpdateGoldText();

        //- Fade effect on start
        fadeGroup = FindAnyObjectByType<CanvasGroup>();
        fadeGroup.alpha = 1.0f;

        //- Initialze Shop and level
        InitShop();
        InitLevel();

        //- Initialize Color button UI states
        OnColorSelect(SaveManager.Instance.state.activeColor);
        SetColor(SaveManager.Instance.state.activeColor);
        colorPanel.GetChild(0).GetChild(SaveManager.Instance.state.activeColor).GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;

        //- Initialize Trail button UI states
        OnTrailSelect(SaveManager.Instance.state.activeTrail);
        SetTrail(SaveManager.Instance.state.activeTrail);
        trailPanel.GetChild(0).GetChild(SaveManager.Instance.state.activeTrail).GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;
    } //-- Start end


    void Update() {
        //- Fade
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

        //- Lerping effect
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPos, 0.03f);
    } //-- Update end


    private void InitShop() {
        //- Validation if panels are set in the Inspector
        if (colorPanel == null || trailPanel == null)
            return;
        int childCount = 0;

        //- Color Buttons
        foreach(Transform t in colorPanel.GetChild(0)) {
            int currentIndex = childCount;

            //- Add button functions via script
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnColorSelect(currentIndex));

            //- Set color to Cyan if owned
            Image img = t.GetChild(0).GetComponent<Image>();
            img.color = SaveManager.Instance.IsColorOwned(currentIndex) ? new Color32(0, 200, 200, 255) : Color.white;
            
            childCount++;
        }
        childCount = 0; //- Reset child count

        // Trail Buttons
        foreach (Transform t in trailPanel.GetChild(0))
        {
            int currentIndex = childCount;

            //- Add button functions via script
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnTrailSelect(currentIndex));
            
            //- Set color to Cyan if owned            
            Image img = t.GetChild(0).GetComponent<Image>();
            img.color = SaveManager.Instance.IsTrailOwned(currentIndex) ? new Color32(0, 200, 200, 255) : Color.white;

            childCount++;
        }
        childCount = 0; //- Reset child count

    } //-- InitShop end

    private void InitLevel() {
        //- Validation if panel is set in the Inspector
        if (levelPanel == null)
            return;
        int childCount = 0;

        // Level Buttons
        foreach (Transform t in levelPanel.GetChild(0)) {
            int currentIndex = childCount;

            //- Add button functions via script
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnLevelSelect(currentIndex));
            childCount++;
        }
        childCount = 0; //- Reset child count
    } //-- InitLevel end

    private void SlideTo(int menuIndex) {
        switch (menuIndex) {
            default:
            case 0:     desiredMenuPos = Vector3.zero;                      break;  //- Home Menu
            case 1:     desiredMenuPos = Vector3.right * Screen.width;      break;  //- Level Menu
            case 2:     desiredMenuPos = Vector3.left * Screen.width;       break;  //- Shop Menu
        }
    } //-- SlideTo end

    private void SetColor(int index) {
        activeColorIndex = index;
        SaveManager.Instance.state.activeColor = index;
        colorBuySetTxt.text = "Active";

        //- Save state
        SaveManager.Instance.Save();
    } //-- SetColor end

    private void SetTrail(int index) {
        activeTrailIndex = index;
        SaveManager.Instance.state.activeTrail = index;
        trailBuySetTxt.text = "Active";

        //- Save state
        SaveManager.Instance.Save();
    } //-- SetTrail end

    public void UpdateGoldText() {
        goldText.text = SaveManager.Instance.state.gold.ToString();
    } //-- UpdateGoldText


    //----- Button Function

    public void OnPlayClicked() {
        SlideTo(1); // Level Menu
    } //-- OnPlayClicked end

    public void OnShopClicked() {
        SlideTo(2); // Shop Menu
    } //-- OnShopClicked end

    public void OnHomeClicked() {
        SlideTo(0); // Home Menu
    } //-- OnHomeClicked end

    private void OnColorSelect(int currentIndex) {
        // Debug.Log("Color " + currentIndex);
        if(selectedColorIndex == currentIndex)
            return;
        
        //- Button UI feedback when selecting buttons
        colorPanel.GetChild(0).GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;
        colorPanel.GetChild(0).GetChild(selectedColorIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.0f;

        //- Text UI feedback when selecting buttons
        selectedColorIndex = currentIndex;
        if(SaveManager.Instance.IsColorOwned(currentIndex)) {
            if(activeColorIndex == currentIndex)
                colorBuySetTxt.text = "Active";
            else
                colorBuySetTxt.text = "Select";
        } else {
            colorBuySetTxt.text = colorCost[currentIndex].ToString();
        }
    } //-- OnColorSelect end

    private void OnTrailSelect(int currentIndex) {
        // Debug.Log("Trail " + currentIndex);
        if(selectedTrailIndex == currentIndex)
            return;
        
        //- UI feedback when selecting buttons
        trailPanel.GetChild(0).GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;
        trailPanel.GetChild(0).GetChild(selectedTrailIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.0f;

        //- Text UI feedback when selecting buttons
        selectedTrailIndex = currentIndex;
        if(SaveManager.Instance.IsTrailOwned(currentIndex)) {
            if(activeTrailIndex == currentIndex)
                trailBuySetTxt.text = "Active";
            else
                trailBuySetTxt.text = "Select";
        } else {
            trailBuySetTxt.text = trailCost[currentIndex].ToString();
        }
    } //-- OnTrailSelect end

    private void OnLevelSelect(int currentIndex) {
        Debug.Log("Level " + currentIndex);

        //-- no functions yet
    } //-- OnLevelSelect end

    public void OnColorBSClicked() {
        // Debug.Log("Color Buy / Set Clicked");
        if(SaveManager.Instance.IsColorOwned(selectedColorIndex)) {
            SetColor(selectedColorIndex);
        } else {
            //- Attempt to Buy selected color
            if(SaveManager.Instance.BuyAttemptColor(selectedColorIndex, colorCost[selectedColorIndex])) {
                SetColor(selectedColorIndex);
                colorPanel.GetChild(0).GetChild(selectedColorIndex).GetChild(0).GetComponent<Image>().color = new Color32(0, 200, 200, 255);
                UpdateGoldText();
            } else {
                Debug.Log("Not enough gold."); //-- remove
            }
        }
    } //-- OnColorBSClicked end

    public void OnTrailBSClicked() {
        // Debug.Log("Trail Buy / Set Clicked");
        if(SaveManager.Instance.IsTrailOwned(selectedTrailIndex)) {
            SetTrail(selectedTrailIndex);
        } else {
            //- Attempt to Buy selected trail
            if(SaveManager.Instance.BuyAttemptTrail(selectedTrailIndex, trailCost[selectedTrailIndex])) {
                SetTrail(selectedTrailIndex);
                trailPanel.GetChild(0).GetChild(selectedTrailIndex).GetChild(0).GetComponent<Image>().color = new Color32(0, 200, 200, 255);
                UpdateGoldText();
            } else {
                Debug.Log("Not enough gold."); //-- remove
            }
        }
    } //-- OnTrailBSClicked end
}


/*
Project Name : Paper Plane 3D
Created by   : Sir Reyyy
*/