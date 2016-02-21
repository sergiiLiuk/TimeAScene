//############################################
// Created: 15/09/2015
// Updated: 30/01/2016
// Author: Sergii Liuk
// Author's e-mail: sergiiliuk@yahoo.dk
// Company: Copyright (c) Kuadd Corporation All Rights Reserved
// Company's e-mail: contact@kuadd.com
// version: 1.0.0
//############################################

/// <summary>
/// All Initializations, Draw Calls and Update Calls go through here.
/// </summary>
using UnityEngine;
using System.Collections;
using Vuforia;

public class SceneViewManager : MonoBehaviour
{
    public AppManager appManager;
    private AppsUILayout appsUILayout;

    // *** Set timers time.
    private float _timePlayTitle = 6.0f;
    private float _timeOpenAnim = 20.0f;

    // *** Others.
    public static int _stage = 0; // if _stage == 0 - appears and starts playing opening animation; 
                                  // if _stage == 1 - appears language menu; 
                                  // if _stage == 2 - appears scene's introduction with language depend on chosen in a previous menu;
                                  // if _stage == 3 - appears title;
                                  // if _stage == 4 - all previous menus hides, and animation is ready to play, appears main menu buttons;
                                  // if _stage == 5 - appears and starts playing ending animation, another control options are disabled;

    void Start()
    {
        appManager.InitManager();

        // Disable screen rotation.
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        appsUILayout = GameObject.FindObjectOfType<AppsUILayout>();
        appManager = GameObject.FindObjectOfType<AppManager>();

        Debug.Log(SystemInfo.deviceModel);

        // The next statment check, the application run on Unity editor or Android device. If application run on Android device, than the program search for a rigth screen resoulution and calls needed layout.
#if UNITY_EDITOR

        appsUILayout.layoutMyLaptop();
        Debug.Log("Unity Editor");

#elif UNITY_ANDROID && !UNITY_EDITOR
        if (Screen.width == 2560 && Screen.height == 1600)
        {
            appsUILayout.layout2560x1600();
        }
        else if (Screen.width == 2560 && Screen.height == 1440)
        {
            appsUILayout.layout2560x1440();
        }
        else if (Screen.width == 2048 && Screen.height == 1536)
        {
            appsUILayout.layout2048x1536();
        }
        else if (Screen.width == 1920 && Screen.height == 1200)
        {
            appsUILayout.layout1920x1200();
        }
        else if (Screen.width == 1920 && Screen.height == 1080)
        {
            appsUILayout.layout1920x1080();
        }
        else if (Screen.width == 1280 && Screen.height == 800)
        {
            appsUILayout.layout1280x800();
        }
        else if (Screen.width == 1280 && Screen.height == 720)
        {
            appsUILayout.layout1280x720();
        }
        else if (Screen.width == 1024 && Screen.height == 600)
        {
            appsUILayout.layout1024x600();
        }
        else if (Screen.width == 540 && Screen.height == 960)
        {
            appsUILayout.layout540x960();
        }
        else if (Screen.width == 480 && Screen.height == 800)
        {
            appsUILayout.layout480x800();
        }
        else if (Screen.width == 480 && Screen.height == 854)
        {
            appsUILayout.layout480x854();
        }
        else if (Screen.width == 320 && Screen.height == 480)
        {
            appsUILayout.layout320x480();
        }
#endif
    }

    void OnGUI()
    {
        // *** This statement checks if a state of "stage" variable, if the current variable equal 0 - starts opening animation play.
        // *** This animation is playing only ONCE, on application start first time.
        if (AppManager._photoFrameMode == false)
        {
            if (_stage == 0)
            {
                GUI.DrawTexture(AppsUILayout._btnSkip, appsUILayout._txtrBtnSkip); // draw skip button.
                if (GUI.Button(AppsUILayout._btnSkip, "", new GUIStyle())) // checks, if button is pressed.
                {
                    appManager.openingAnimationObj.SetActive(false); // disable opening animation, if skip button was pressed.
                    appManager.clockTicObj.SetActive(false); // disable clock tics.
                    appManager.clockBongObj.SetActive(false); // disable clock bong.
                    _stage = 1; // if "_stage" is "1", than program run to next stage.
                    appManager._buttonClickSound.Play();
                }
                _timeOpenAnim -= Time.deltaTime;
                if (_timeOpenAnim < 11 && _timeOpenAnim > 10.5f) // the bong begins to sound at a specific time period.
                {
                    appManager.clockBongObj.SetActive(true); // enable clock bong.
                }
                if (_timeOpenAnim <= 0)
                {
                    _stage = 1; // program run to to next stage.
                    appManager.openingAnimationObj.SetActive(false); // disable opening animation.               
                    appManager.clockTicObj.SetActive(false); // disable clock tics.
                }
            }
            // ----------------------------------------------------------------

            // *** Starts select language menu buttons area.
            // *** Starts english language button        
            if (AppManager._subtitlesMode == 0 && _stage == 1)
            {
                // if languange mode == 0, that means language is not choosen yet and languages menu is visible.
                appManager.lastClipOpeningAnimationObj.SetActive(true); // enable the last slide from opening 
                                                                        //animation on screen, until any of languages will be chosen.
                GUI.DrawTexture(AppsUILayout._btnEngLanguage, appsUILayout._txtrEngLanguage); // draw english button.
                if (GUI.Button(AppsUILayout._btnEngLanguage, "", new GUIStyle())) // Checks, if button is pressed.
                {
                    AppManager._subtitlesMode = 1;
                    _stage = 2;
                    appManager._buttonClickSound.Play();
                    Debug.Log("English version has been selected");
                }
                // *** Ends english language button
                // *** Ends select language menu buttons area.
                //------------------------------------------------------------      

                // *** Starts japanese language button.
                GUI.DrawTexture(AppsUILayout._btnJapLanguage, appsUILayout._txtrJapLanguage); // draw japanese button.
                if (GUI.Button(AppsUILayout._btnJapLanguage, "", new GUIStyle())) // Checks, if button is pressed.
                {
                    AppManager._subtitlesMode = 2;
                    _stage = 2;
                    appManager._buttonClickSound.Play();
                    Debug.Log("Japanese version has been selected");
                }
                // *** Ends japanese language button.
                //-------------------------------------------------------------

                // *** Starts photo mode button.
                GUI.DrawTexture(AppsUILayout._btnFrameMode, appsUILayout._txtrbtnFrameMode); // draw japanese button.
                if (GUI.Button(AppsUILayout._btnFrameMode, "", new GUIStyle())) // Checks, if button is pressed.
                {
                    AppManager._photoFrameMode = true;
                    appManager._buttonClickSound.Play();
                    Debug.Log("Photo mode has been selected");
                }
                // *** Ends photo mode button.
            }
            //------------------------------------------------------------

            // *** Starts introdaction, when language is selcted   
            // *** After language is selected, on screen appears scene introduction and button "Next", 
            // this button need to press to continue play scene.         
            if (_stage == 2)
            {
                appManager.lastClipOpeningAnimationObj.SetActive(false); // disable last clip from opening animation.

                if (AppManager._subtitlesMode == 1)
                {
                    GUI.DrawTexture(AppsUILayout._lblIntro, appsUILayout._txtrLblIntroEng); // draw button with english texture.
                    GUI.DrawTexture(AppsUILayout._btnNext, appsUILayout._txtrNextEng); // draw next button with english texture.  
                    GUI.DrawTexture(AppsUILayout._btnBack, appsUILayout._txtrBackEng); // draw back button with english texture. 
                }
                if (AppManager._subtitlesMode == 2)
                {
                    GUI.DrawTexture(AppsUILayout._lblIntro, appsUILayout._txtrLblIntroJap); // draw button with japanese texture.
                    GUI.DrawTexture(AppsUILayout._btnNext, appsUILayout._txtrNextJap); // draw next button with japanese texture. 
                    GUI.DrawTexture(AppsUILayout._btnBack, appsUILayout._txtrBackJap); // draw back button with japanese texture.
                }
                if (GUI.Button(AppsUILayout._btnNext, "", new GUIStyle())) // Checks, if button is pressed.
                {
                    _stage = 3;
                    appManager._buttonClickSound.Play();
                    AppManager._executeOnReStartApp = true;
                    AppManager._executeOnce = true;
                    appManager._buttonClickSound.Play();
                    Debug.Log("Introdaction has been finished.");
                }

                // if button "back" has been pressed, than program goes to previous menu.
                if (GUI.Button(AppsUILayout._btnBack, "", new GUIStyle())) // Checks, if button is pressed.
                {
                    _stage = 1;
                    AppManager._subtitlesMode = 0;
                    appManager._buttonClickSound.Play();
                }
            }
            // *** Ends introdaction.
            // ----------------------------------------------------------------

            // *** Starts Title.
            // *** Title appear after user passed introduction and clicked "Next" and after target object was detected.
            if (_stage == 3 && AppManager._tartgetDetectionMode == false && _stage != 5)
            {
                AppManager._hideTargetGuide = true;
                // Statement below checks if faded character has been faded out before, if YES, than program make a faded character ACTIVE and fade it in.
                if (AppManager._areObjectFaded == true)
                {
                    appManager.fadedSettings.SetActive(true);
                    AppManager._toFadeIn = true;
                    AppManager._executeOnReStartApp = false;
                }
                //Starts a countdown. Make title appear for _currentAnimTime has been set in to varible "_timePlayTitle"
                // When countdown is expired, title will be unvisible. 
                _timePlayTitle -= Time.deltaTime; // countdown for Title.
                
                if (AppManager._executeOnce == true && AppManager._tartgetDetectionMode == false)
                {
                    // call animatioPlay and audioPlay methods.
                    //---------- 
                    appManager.normSettings.SetActive(true); // activate a chracter with normall settings.
                    appManager.charactersVoiceObj.SetActive(true); // enable voice audio.
                    AppManager._currentAnimTime = 0.0f; // set animation playing time to 0. 
                    appManager.animationPlay();
                    appManager.audioPlay();
                    appManager.subtitlesSet.SetActive(true);
                    AppManager._executeOnce = false;
                    //----------
                }
                if (_timePlayTitle < 0.07f && _timePlayTitle > 0.0f)
                {
                    Debug.Log("Title has been finished.");
                }
                if (_timePlayTitle <= 0)
                {
                    _stage = 4; // hide title after it's finished playing  and goes to next stage. 
                    _timePlayTitle = 6;   // reset countdown.  
                    AppManager._hideTargetGuide = false;
                    appManager.fadedSettings.SetActive(false);
                    //----------
                    if (AppManager._subtitlesMode == 2)
                    {
                        appManager.japaneseSubtitlesObj.SetActive(true); // enable japanese subtitles.
                    }
                    if (AppManager._subtitlesMode == 1)
                    {
                        appManager.englishSubtitlesObj.SetActive(true); // enable english subtitles. 
                    }
                }
                else
                {
                    GUI.DrawTexture(AppsUILayout._lblTitle, appsUILayout._txtrLblTitle); // draw  label title.
                }
            }

            // ----------------------------------------------------------------   

            // *** This statment check if "_stage" value is equal "5", if yes - than draws ending menu.
            if (_stage == 5)
            {
                // *** Ending menu starts.
                GUI.DrawTexture(AppsUILayout._btnSouvenir, appsUILayout._txtrSouvenir); // Draw replay button.
                if (GUI.Button(AppsUILayout._btnSouvenir, "", new GUIStyle())) // Checks, if button is pressed.
                {
                    Application.OpenURL("http://www.yoshidaya.ne.jp/");
                    appManager._buttonClickSound.Play();
                }
                GUI.DrawTexture(AppsUILayout._btnRestaurant, appsUILayout._txtrRestaurant); // Draw replay button.
                if (GUI.Button(AppsUILayout._btnRestaurant, "", new GUIStyle())) // Checks, if button is pressed.
                {
                    appManager._buttonClickSound.Play();
                }
                GUI.DrawTexture(AppsUILayout._btnReturn, appsUILayout._txtrReturn); // Draw replay button.
                if (GUI.Button(AppsUILayout._btnReturn, "", new GUIStyle())) // Checks, if button is pressed.
                {
                    StartCoroutine(delay(0.2f));
                    if (AppManager._subtitlesMode == 1) // refresh english subtitles.
                    {
                        appManager.endingEnglishAnimator.speed = 1f; // animation speed.
                        appManager.endingEnglishAnimator.Play("endingAnimEnglish", 0, 0.990f);
                    }
                    if (AppManager._subtitlesMode == 2) // refresh english subtitles.
                    {
                        appManager.endingJapaneseAnimator.speed = 1f; // animation speed.
                        appManager.endingJapaneseAnimator.Play("endingAnimJapanese", 0, 0.990f);
                    }

                    appManager._buttonClickSound.Play();
                }
                GUI.DrawTexture(AppsUILayout._btnExit, appsUILayout._txtrExit); // Draw replay button.
                if (GUI.Button(AppsUILayout._btnExit, "", new GUIStyle())) // Checks, if button is pressed.
                {
                    Application.Quit();
                    appManager._buttonClickSound.Play();
                }
            }
            // *** Ending menu ends.        
        }
    }

    // Calls delay and replay scene after it.
    IEnumerator delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (AppManager._subtitlesMode == 1) // refresh english subtitles.
        {
            appManager.endingEnglishAnimator.speed = 0f; // animation speed.
            appManager.endingEnglishAnimator.Play("endingAnimEnglish", 0, 0.999f);
        }
        if (AppManager._subtitlesMode == 2) // refresh english subtitles.
        {
            appManager.endingJapaneseAnimator.speed = 0f; // animation speed.
            appManager.endingJapaneseAnimator.Play("endingAnimJapanese", 0, 0.999f);
        }
        appManager.sceneReplay();
    }
}
