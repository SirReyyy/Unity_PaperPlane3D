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

    private Vector3 desiredMenuPos;


    //----- Functions

    void Start() {
        fadeGroup = FindAnyObjectByType<CanvasGroup>();
        fadeGroup.alpha = 1.0f;

        InitShop();
        InitLevel();

        Debug.Log(Screen.width);
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
    } //-- OnColorSelect end

    private void OnTrailSelect(int currentIndex) {
        Debug.Log("Trail " + currentIndex);
    } //-- OnTrailSelect end

    private void OnLevelSelect(int currentIndex) {
        Debug.Log("Level " + currentIndex);
    } //-- OnLevelSelect end

    public void OnColorBSClicked() {
        Debug.Log("Color Buy / Set Clicked");
    } //-- OnColorBSClicked end

    public void OnTrailBSClicked() {
        Debug.Log("Trail Buy / Set Clicked");
    } //-- OnTrailBSClicked end

}


/*
Project Name : Paper Plane 3D
Created by   : Sir Reyyy
*/