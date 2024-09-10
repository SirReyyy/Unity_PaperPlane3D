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

    public RectTransform menuContainer;
    public Transform colorPanel;
    public Transform trailPanel;
    public Transform levelPanel;

    public Text colorBuySetTxt;
    public Text trailBuySetTxt;

    private int[] colorCost = new int[] {0, 5, 5, 5, 10, 10, 15, 15, 20, 20};
    private int[] trailCost = new int[] {0, 20, 20, 40, 40, 40, 60, 60, 80, 80};
    private int selectedColorIndex;
    private int selectedTrailIndex;

    private Vector3 desiredMenuPos;


    //----- Functions

    void Start() {
        fadeGroup = FindAnyObjectByType<CanvasGroup>();
        fadeGroup.alpha = 1.0f;

        InitShop();
        InitLevel();
    } //-- Start end


    void Update() {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

        // Lerping effect
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPos, 0.03f);
    } //-- Update end


    private void InitShop() {
        if (colorPanel == null || trailPanel == null)
            return;
        int childCount = 0;

        // Color Buttons
        foreach(Transform t in colorPanel.GetChild(0)) {
            int currentIndex = childCount;

            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnColorSelect(currentIndex));
            childCount++;
        }
        childCount = 0;

        // Trail Buttons
        foreach (Transform t in trailPanel.GetChild(0))
        {
            int currentIndex = childCount;

            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnTrailSelect(currentIndex));
            childCount++;
        }
        childCount = 0;

    } //-- InitShop end

    private void InitLevel() {
        if (levelPanel == null)
            return;
        int childCount = 0;

        // Level Buttons
        foreach (Transform t in levelPanel.GetChild(0)) {
            int currentIndex = childCount;

            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnLevelSelect(currentIndex));
            childCount++;
        }
        childCount = 0;
    } //-- InitLevel end

    private void SlideTo(int menuIndex) {
        switch (menuIndex) {
            default:
            case 0:     desiredMenuPos = Vector3.zero;      break;  // Home Menu
            case 1:     desiredMenuPos = Vector3.right * Screen.width;      break;  // Level Menu
            case 2:     desiredMenuPos = Vector3.left * Screen.width;      break;  // Shop Menu
        }
    } //-- SlideTo end

    private void SetColor(int index) {

        colorBuySetTxt.text = "Active";
    } //-- SetColor end

    private void SetTrail(int index) {

        trailBuySetTxt.text = "Active";
    } //-- SetTrail end

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
        Debug.Log("Color " + currentIndex);
        selectedColorIndex = currentIndex;

        if(SaveManager.Instance.IsColorOwned(currentIndex)) {
            colorBuySetTxt.text = "Select";
        } else {
            colorBuySetTxt.text = colorCost[currentIndex].ToString();
        }
    } //-- OnColorSelect end

    private void OnTrailSelect(int currentIndex) {
        Debug.Log("Trail " + currentIndex);
        selectedTrailIndex = currentIndex;

        if(SaveManager.Instance.IsTrailOwned(currentIndex)) {
            trailBuySetTxt.text = "Select";
        } else {
            trailBuySetTxt.text = trailCost[currentIndex].ToString();
        }
    } //-- OnTrailSelect end

    private void OnLevelSelect(int currentIndex) {
        Debug.Log("Level " + currentIndex);
    } //-- OnLevelSelect end

    public void OnColorBSClicked() {
        Debug.Log("Color Buy / Set Clicked");
        
        if(SaveManager.Instance.IsColorOwned(selectedColorIndex)) {
            SetColor(selectedColorIndex);
        } else {
            if(SaveManager.Instance.BuyAttemptColor(selectedColorIndex, colorCost[selectedColorIndex])) {
                SetColor(selectedColorIndex);
            } else {
                Debug.Log("Not enough gold."); //--
            }
        }
    } //-- OnColorBSClicked end

    public void OnTrailBSClicked() {
        Debug.Log("Trail Buy / Set Clicked");

        if(SaveManager.Instance.IsTrailOwned(selectedTrailIndex)) {
            SetTrail(selectedTrailIndex);
        } else {
            if(SaveManager.Instance.BuyAttemptTrail(selectedTrailIndex, trailCost[selectedTrailIndex])) {
                SetTrail(selectedTrailIndex);
            } else {
                Debug.Log("Not enough gold."); //--
            }
        }
    } //-- OnTrailBSClicked end

}


/*
Project Name : Paper Plane 3D
Created by   : Sir Reyyy
*/