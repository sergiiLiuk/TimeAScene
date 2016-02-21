//############################################
// Created: 15/09/2015
// Updated: 15/01/2016
// Author: Sergii Liuk
// Author's e-mail: sergiiliuk@yahoo.dk
// Company: Copyright (c) Kuadd Corporation All Rights Reserved
// Company's e-mail: contact@kuadd.com
// version: 1.0.0
//############################################
using UnityEngine;
using System.Collections;


public class PhotoMode : MonoBehaviour
{
    AppManager appManager;
    AppsUILayout appsUILayout;

    // *** Varibles.
    private bool _photoMode = false;
    private bool _waterMarks = false;
    private bool frame1 = false;
    private bool frame2 = false;
    private bool frame3 = false;

    // Use this for initialization
    void Start()
    {
        appManager = GameObject.FindObjectOfType<AppManager>();
        appsUILayout = GameObject.FindObjectOfType<AppsUILayout>();
    }

    // *** Starts method hideCameraBtn.
    // *** Make unvisible all buttons for a fixed period of time, needs to make a picture without buttons on display.
    private IEnumerator hideButtons()
    {
        _photoMode = true;
        yield return new WaitForSeconds(0.1f);
        _photoMode = false;
        _waterMarks = false;
    }
    // *** Ends method hideCameraBtn.
    //---------------------------------------------------

    // *** Method void GUI starts.
    // *** Called multiple times per frame in response to GUI events.
    void OnGUI()
    {
        if (AppManager._photoFrameMode == true)
        {
            if (frame1 == true)
            {
                GUI.DrawTexture(AppsUILayout._lblFrame1,
appsUILayout._txtrFrame1);
            }
            //------------------------------------------------------------------

            if (frame2 == true)
            {
                GUI.DrawTexture(AppsUILayout._lblFrame2,
appsUILayout._txtrFrame2);
            }
            //------------------------------------------------------------------
            if (frame3 == true)
            {
                GUI.DrawTexture(AppsUILayout._lblFrame3,
appsUILayout._txtrFrame3);
            }
            //------------------------------------------------------------------

            // *** Starts watermark area.
            if (_waterMarks == true)
            {
                // Draw watermarks if "_watermarks is true".
                GUI.DrawTexture(AppsUILayout._lblWaterMarkLogo,
appsUILayout._txtrWaterMarkLogo);
                GUI.DrawTexture(AppsUILayout._lblWaterMarkAddress,
appsUILayout._txtrWaterMarkAddress);
            }
            // *** Ends watermark area.
            // ------------------------------------------------------------------

            if (_photoMode == false) // hide the menu while user takesa picture.
            {
                GUI.DrawTexture(AppsUILayout._lblMenuBackground,
appsUILayout._txtrBtnMenuBackground); // creates a transparent area behind the menu buttons.


                //------------------------------------------------------------------
                // Selection Frames
                if (frame1 == true)
                {
                    GUI.DrawTexture(AppsUILayout._lblSelectionFrame1,
appsUILayout._txtrSelectionFrame);
                }
                if (frame2 == true)
                {
                    GUI.DrawTexture(AppsUILayout._lblSelectionFrame2,
appsUILayout._txtrSelectionFrame);
                }
                if (frame3 == true)
                {
                    GUI.DrawTexture(AppsUILayout._lblSelectionFrame3,
appsUILayout._txtrSelectionFrame);
                }

                //------------------------------------------------------------------

                // *** Starts menu buttons area.
                // Draw button for first frame.
                GUI.DrawTexture(AppsUILayout._btnFrame1,
appsUILayout._txtrBtnFrame1);
                if (GUI.Button(AppsUILayout._btnFrame1, "", new
GUIStyle()))// checks, if button is pressed.
                {
                    // enable the first photo frame and disables the rest.
                    frame1 = true;
                    frame2 = false;
                    frame3 = false;
                    appManager._buttonClickSound.Play();
                }
                //------------------------------------------------------------------------------
                // Draw button for second frame.
                GUI.DrawTexture(AppsUILayout._btnFrame2, appsUILayout._txtrBtnFrame2);
                if (GUI.Button(AppsUILayout._btnFrame2, "", new GUIStyle()))// checks, if button is pressed.
                {
                    // enable the second photo frame and disables the rest.
                    frame1 = false;
                    frame2 = true;
                    frame3 = false;
                    appManager._buttonClickSound.Play();
                }

                //-------------------------------------------------------------------------------

                // Draw button for third frame.
                GUI.DrawTexture(AppsUILayout._btnFrame3, appsUILayout._txtrBtnFrame3);
                if (GUI.Button(AppsUILayout._btnFrame3, "", new
GUIStyle()))// checks, if button is pressed.
                {
                    // enable the third photo frame and disables the rest.
                    frame1 = false;
                    frame2 = false;
                    frame3 = true;
                    appManager._buttonClickSound.Play();
                }
                //-------------------------------------------------------------------------------

                // Draw camera button and call takePicture method.
                GUI.DrawTexture(AppsUILayout._btnCameraFrameMode, appsUILayout._txtrCamera);
                if (GUI.Button(AppsUILayout._btnCameraFrameMode, "", new GUIStyle())) // checks, if button is pressed.
                {
                    StartCoroutine(hideButtons());
                    _waterMarks = true;
                    appManager.takePicture();
                    appManager._camShutterSound.Play();
                    Debug.Log("Screenshot has beed taken in photo mode.");
                }
                //-------------------------------------------------------------------------------

                // Draw back button.
                GUI.DrawTexture(AppsUILayout._btnBackFrameMode, appsUILayout._txtrBtnBackPhotoMode);
                if (GUI.Button(AppsUILayout._btnBackFrameMode, "", new GUIStyle())) // checks, if button is pressed.
                {
                    AppManager._photoFrameMode = false; // enable photo mode.
                    frame1 = false;
                    frame2 = false;
                    frame3 = false;
                    appManager._buttonClickSound.Play();
                    Debug.Log("Exit photo mode.");
                }
            }
            // *** Ends menu buttons area.
            // ------------------------------------------------------------------
        }
    }
}