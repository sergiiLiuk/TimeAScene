//############################################
// Created: 15/09/2015
// Updated: 30/01/2016
// Author: Sergii Liuk
// Author's e-mail: sergiiliuk@yahoo.dk
// Company: Copyright (c) Kuadd Corporation All Rights Reserved
// Company's e-mail: contact@kuadd.com
// version: 1.0.0
//############################################
using UnityEngine;
using System.Collections;
using Vuforia;
using System.IO;
using System;

public class MainMenuManager : MonoBehaviour
{
    AppManager appManager;
    AppsUILayout buttonsLayout;

    // *** Variables.
    private int _clickCounter = 0;
    public static bool _triggerMenu = false;
    private bool _waterMarks = false;
    //--------------------------------------------------

    // *** Starts initialization.
    // *** Start is called before the first frame update only if the script instance is enabled.
    void Start()
    {
        appManager = GameObject.FindObjectOfType<AppManager>();
        buttonsLayout = GameObject.FindObjectOfType<AppsUILayout>();
    }
    // *** Ends initialization.
    //---------------------------------------------------

    // *** Starts method hideCameraBtn.
    // *** Make unvisible all buttons, start _currentAnimTime delay and make after delay buttons visible again,
    // *** Needs to make a picture without buttons on display.
    private IEnumerator hideForShotButtons()
    {
        _triggerMenu = false;
        yield return new WaitForSeconds(0.05f);
        _triggerMenu = true;
        _waterMarks = false;
    }
    // *** Ends method hideCameraBtn.
    //---------------------------------------------------

    // *** Starts method btnHideShow.
    // *** Makes a delay between touching screen .
    private void btnHideShow()
    {
        if (Input.anyKey && SceneViewManager._stage == 4)
        {
            _clickCounter++;
        }
        if (_clickCounter == 1)
        {
            _triggerMenu = true;
            _clickCounter = 0;
        }
    }
    // *** Endss method btnHideShow.
    //----------------------------------------------------

    // *** Starts method Update.
    // *** Update is called once per frame. It is the main workhorse function for frame updates.
    void Update()
    {
        btnHideShow();
    }
    // *** Ends method Update.
    //----------------------------------------------------

    // *** Method void GUI starts.
    // *** Called multiple times per frame in response to GUI events.
    void OnGUI()
    {
        //GUI.DrawTexture(AppsUILayout._lblCenter, buttonsLayout._txtrCenter); // Draw a line on the centre of the screen.
        // If "ApplicationController._photoFrameMode == true" - than starts photo frame mode, else program run to main menu area.
        if (AppManager._photoFrameMode == true)
        {
            appManager.lastClipOpeningAnimationObj.SetActive(false); // Disable last clip of opening animation.
        }
        else
        {
            // *** Target detecting label. Appears on the screen if target object has been lost.
            if (SceneViewManager._stage == 4 && DefaultTrackableEventHandler._objectDetected == false && AppManager._hideTargetGuide == false)
            {
                GUI.DrawTexture(AppsUILayout._lblTargetGuide, buttonsLayout._txtrTargetGuide); // draw label. 
            }
            if (SceneViewManager._stage == 3 && AppManager._tartgetDetectionMode == true)
            {
                GUI.DrawTexture(AppsUILayout._btnTargetDetection, buttonsLayout._txtrButtonDetection); // Draw pause button.

                // By pressing button below, user define a target picture.
                if (GUI.Button(AppsUILayout._btnTargetDetection, "", new GUIStyle()))
                {
                    UserDefinedTargetEventHandler._readyToTake = true;
                    Debug.Log("Target detection button has been pressed");
                }
            }
            // *** Ends target detecting label.
            //------------------------------------------------------------------------            

            //***************************************************************
            // Buttons Area Starts
            //***************************************************************
            // *** Starts main screen buttons area.
            if (_triggerMenu == true && SceneViewManager._stage == 4 && AppManager._lastStage == false)
            {
                if (AppManager._infoPage == false)
                {
                    GUI.DrawTexture(AppsUILayout._lblMenuBackground, buttonsLayout._txtrBtnMenuBackground);
                    GUI.DrawTexture(AppsUILayout._btnCamera, buttonsLayout._txtrCamera); // Draw camera button.
                    if (GUI.Button(AppsUILayout._btnCamera, "", new GUIStyle()) && DefaultTrackableEventHandler._objectDetected == true) // Checks, if button is pressed.
                    {
                        Debug.Log("Camera button has been pressed");
                        StartCoroutine(hideForShotButtons());
                        _waterMarks = true;
                        appManager.takePicture();
                        appManager._camShutterSound.Play();
                    }
                    // *** Ends take a picture button.
                    //--------------------------------------------------------------

                    // *** Starts play button.
                    if (AppManager._switchPlayPauseBtn == true) // Switcher between play and pause button (If one is visible - another is hidden).
                    {
                        GUI.DrawTexture(AppsUILayout._btnPlay, buttonsLayout._txtrPlay); // Draw play button.
                        if (GUI.Button(AppsUILayout._btnPlay, "", new GUIStyle()) && DefaultTrackableEventHandler._objectDetected == true)
                        {
                            Debug.Log("Play button has been pressed");
                            appManager.audioOnPause();
                            appManager.animationPlay();
                            appManager.audioPlay();
                            AppManager._switchPlayPauseBtn = false; //  If play button is pressed, it become to be unvisible 
                                                                    // and after pause button become to be visible.
                            appManager._buttonClickSound.Play();
                        }
                    }
                    // *** Ends play button.
                    //--------------------------------------------------------------

                    // *** Starts pause button. 
                    if (AppManager._switchPlayPauseBtn == false)
                    {
                        GUI.DrawTexture(AppsUILayout._btnPause, buttonsLayout._txtrPause); // Draw pause button.
                        if (GUI.Button(AppsUILayout._btnPause, "", new GUIStyle()) && DefaultTrackableEventHandler._objectDetected == true)
                        {
                            // If buttons is pressed, than pause animation and audio.
                            Debug.Log("Pause button has been pressed");
                            appManager.animationOnPause();
                            appManager.audioOnPause();
                            AppManager._pauseMode = true; // If pause button is pressed, than play mode - deactivating.
                            AppManager._switchPlayPauseBtn = true; //  If pause button is pressed, it become to be unvisible
                                                                   // and after play button become to be visible.                       
                            appManager._buttonClickSound.Play();
                        }
                    }
                    // *** Ends pause batton.  
                    //--------------------------------------------------------------

                    // *** Starts _rewind backwards button.   
                    GUI.DrawTexture(AppsUILayout._btnBackward, buttonsLayout._txtrBackward); // Draw backward button.
                    if (GUI.Button(AppsUILayout._btnBackward, "", new GUIStyle()) && DefaultTrackableEventHandler._objectDetected == true)
                    {
                        // If buttons is pressed, than play faster animation and audio backward.
                        Debug.Log("Rewind backwards button has been pressed");
                        appManager.animationBackward();
                        appManager.audioBackward();
                        appManager._buttonClickSound.Play();
                    }
                    // *** End frewind  backwards button.
                    //--------------------------------------------------------------

                    // *** Starts rewind forwards button.  
                    GUI.DrawTexture(AppsUILayout._btnForward, buttonsLayout._txtrForward); // Draw forward button.
                    if (GUI.Button(AppsUILayout._btnForward, "", new GUIStyle()) && DefaultTrackableEventHandler._objectDetected == true)
                    {
                        // If buttons has been pressed, than animation and audio rewind forwards.
                        Debug.Log("Rewind forward button has been pressed");
                        appManager.animationForward();
                        appManager.audioForward();
                        appManager._buttonClickSound.Play();
                    }
                    // *** End rewind forwards button.
                    //--------------------------------------------------------------

                    // *** Starts replay button.  
                    GUI.DrawTexture(AppsUILayout._btnReplay, buttonsLayout._txtrReplay); // Draw replay button.
                    if (GUI.Button(AppsUILayout._btnReplay, "", new GUIStyle()))
                    {
                        // If buttons is pressed, than the scene starts again from begining.    
                        Debug.Log("Replay button has been pressed");
                        appManager.sceneReplay();
                        appManager._buttonClickSound.Play();
                    }
                    // *** Ends replay button.  
                    //-------------------------------------------------------   

                    // *** Starts info button.  
                    GUI.DrawTexture(AppsUILayout._btnInfo, buttonsLayout._txtrInfo);
                    if (GUI.Button(AppsUILayout._btnInfo, "", new GUIStyle()))
                    {
                        Debug.Log("Info button has been pressed");
                        AppManager._infoPage = true;
                        _triggerMenu = false;
                        appManager.animationOnPause();
                        appManager.audioOnPause();
                        appManager.subtitlesSet.SetActive(false);
                        AppManager._hideTargetGuide = true;
                        appManager._buttonClickSound.Play();
                    }
                    // *** Ends info button.  
                    //-------------------------------------------------------        

                    // *** Starts close application button.  
                    GUI.DrawTexture(AppsUILayout._btnCloseApp, buttonsLayout._txtrCloseApp);
                    if (GUI.Button(AppsUILayout._btnCloseApp, "", new GUIStyle()))
                    {
                        // If buttons is pressed, than the scene starts again from begining.    
                        Debug.Log("Close application button has been pressed");
                        Application.Quit();
                        appManager._buttonClickSound.Play();
                    }
                    // *** Ends  close application button.  
                    //-------------------------------------------------------        
                }
                // *** Starts button background space.
                // This button simulate a touchable background, it take place behind the rest buttons.
                // This button is unvisible and it used for hide / show main menu buttons.
                GUI.DrawTexture(AppsUILayout._btnSpace, buttonsLayout._txtrBtnSpace); // Draw background space button.
                if (_clickCounter == 0 && AppManager._infoPage == false)
                {
                    if (GUI.Button(AppsUILayout._btnSpace, " ", new GUIStyle()))
                    {
                        Debug.Log("Hide buttons area has been pressed");
                        _triggerMenu = false;
                    }
                }
                else
                {
                    if (GUI.Button(AppsUILayout._btnSpace, " ", new GUIStyle()))
                    {
                        AppManager._infoPage = false;
                        _triggerMenu = true;
                        AppManager._hideTargetGuide = false;
                        appManager.subtitlesSet.SetActive(true);
                    }
                }
                // *** Ends button background space.
                // ------------------------------------------------------
            }
            // *** Ends main screen buttons area.
            // ----------------------------------------------------------

            // *** Starts watermark lables.
            if (_waterMarks == true)
            {
                GUI.DrawTexture(AppsUILayout._lblWaterMarkLogo, buttonsLayout._txtrWaterMarkLogo);
                GUI.DrawTexture(AppsUILayout._lblWaterMarkAddress, buttonsLayout._txtrWaterMarkAddress);
            }
            // *** Ends watermark lables.
            // --------------------------------------------------------------

            // *** Starts app's info lable.
            if (AppManager._infoPage == true)
            {
                GUI.DrawTexture(AppsUILayout._lblInfoContent, buttonsLayout._txtrInfoContent);
            }
            // *** Ends app's info lable.
            // --------------------------------------------------------------
        }
    }
}






