using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    //----- Variables

    private CanvasGroup fadeGroup;
    private float loadTime;
    private float minimumLogoTime = 5.0f;


    //----- Functions

    void Start() {
        fadeGroup = FindAnyObjectByType<CanvasGroup>();
        fadeGroup.alpha = 1.0f;

        if(Time.time < minimumLogoTime)
            loadTime = minimumLogoTime;
        else
            loadTime = Time.time;
    } //-- Start end


    void Update() {
        // Fade-In
        if (Time.time < minimumLogoTime) {
            fadeGroup.alpha = 1.0f - Time.time;
        }

        // Fade-Out
        if (Time.time > minimumLogoTime && loadTime != 0) {
            fadeGroup.alpha = Time.time - minimumLogoTime;
            
            if (fadeGroup.alpha >= 1) {
                // Change Scene
                SceneManager.LoadScene("Menu");
            }
        }
    } //-- Update end

}


/*
Project Name : Paper Plane 3D
Created by   : Sir Reyyy
*/