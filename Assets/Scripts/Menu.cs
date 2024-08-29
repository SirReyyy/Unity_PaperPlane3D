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
    private float fadeInSpeed = 0.33f;

    public Transform colorPanel;
    public Transform trailPanel;


    //----- Functions

    void Start() {
        fadeGroup = FindAnyObjectByType<CanvasGroup>();
        fadeGroup.alpha = 1.0f;

        InitShop();
    } //-- Start end


    void Update() {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
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


    //----- Button Function

    public void OnPlayClicked() {
        Debug.Log("Play");
    } //-- OnPlayClicked end

    public void OnShopClicked() {
        Debug.Log("Shop");
    } //-- OnShopClicked end

    public void OnHomeClicked() {
        Debug.Log("Home");
    } //-- OnHomeClicked end

    private void OnColorSelect(int currentIndex) {
        Debug.Log("Color " + currentIndex);
    } //-- OnColorSelect end

    private void OnTrailSelect(int currentIndex) {
        Debug.Log("Trail " + currentIndex);
    } //-- OnTrailSelect end

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