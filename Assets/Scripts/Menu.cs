using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    //----- Variables
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;


    //----- Functions

    void Start() {
        fadeGroup = FindAnyObjectByType<CanvasGroup>();
        fadeGroup.alpha = 1.0f;


    } //-- Start end


    void Update() {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
    } //-- Update end

}


/*
Project Name : Paper Plane 3D
Created by   : Sir Reyyy
*/