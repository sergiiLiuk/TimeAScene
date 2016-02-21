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
using System;

/// <summary>
/// All of its Init, Update and Draw calls take place via SceneManager's Monobehaviour calls to ensure proper sync across all updates
/// </summary>
public class AppManager : MonoBehaviour
{
    private CaptureAndSave snapShot;
    private UserDefinedTargetEventHandler mTargetHandler;

    // *** Game Objects.
    public GameObject character;
    public GameObject subtitlesSet;
    public GameObject normSettings;
    public GameObject fadedSettings;
    public GameObject charactersVoiceObj;
    public GameObject clockTicObj;
    public GameObject clockBongObj;
    public GameObject englishSubtitlesObj;
    public GameObject japaneseSubtitlesObj;
    public GameObject openingAnimationObj;
    public GameObject endingJapAnimationObj;
    public GameObject endingEngAnimationObj;
    public GameObject lastClipOpeningAnimationObj;

    // *** Animator.
    public Animator japSubtitlesAnimator;
    public Animator engSubtitlesAnimator;
    public Animator endingJapaneseAnimator;
    public Animator endingEnglishAnimator;

    // *** Audio Sources.
    public AudioSource _charactersVoice;
    public AudioSource _camShutterSound;
    public AudioSource _buttonClickSound;

    // *** Other variables.
    public static string screenShotName;
    public static bool _switchPlayPauseBtn = false; // change visibility  beetwen stop/play buttons. false is make visible PAUSE button by default.
    public static int _subtitlesMode = 0; // 0 - show the menu to select Japanese or English language. 1 - english language, 2 japanese language
                                          //  public Animation anim;
    public static float _currentAnimTime; // the current time in the animation at the current moment.
    public static float _playPosition; // the current position in the animation at the current moment.
    private float _animationLength;
    private string myPath;
    public static bool _photoFrameMode = false;
    public static bool _hideTargetGuide = false;
    public static bool _toFadeOut = false;
    public static bool _toFadeIn = false;
    public static bool _areObjectFaded = false;
    public static bool _pauseMode = false;
    public static bool _lastStage;
    public static bool _tartgetDetectionMode = true;
    public static bool _infoPage = false;
    public static bool _executeOnReStartApp = true;
    public static bool _executeOnce = false;

    // *** Timers
    private float _startFadingTime;
    public static float _timeEndAnim = 8.0f;
    private float _countDownTime = 3.5f;
    float _refreshSubtitlesFreq = 0.1f; // with this frequency subtitles shows on display while subtitles playing backwards. 

    // ----------------------------------------------------------------------------------------

    // *** Stats start method.
    // *** Start is called before the first frame update only if the script instance is enabled.
    // *** Starts initialization.   
    void Start()
    {
        // Disable both settings characters on application start.
        normSettings.SetActive(false);
        fadedSettings.SetActive(false);

        // Disable subtitles on  application start.
        japaneseSubtitlesObj.SetActive(false);
        englishSubtitlesObj.SetActive(false);
        // -----------------------------------------------

        // Enable opening animation on application start.
        openingAnimationObj.SetActive(true);
        // -----------------------------------------------

        // Disable ending animation on application start.
        endingJapAnimationObj.SetActive(false);
        endingEngAnimationObj.SetActive(false);
        // -----------------------------------------------

        // Disable last clip opening animation on application start.
        lastClipOpeningAnimationObj.SetActive(false);
        // -----------------------------------------------

        // Disable clock bong on application start.
        clockBongObj.SetActive(false);

        snapShot = GameObject.FindObjectOfType<CaptureAndSave>();
    }
    // *** Ends start method.
    //---------------------------------------------------

    // *** Starts method Update.
    // *** Update is called once per frame. It is the main workhorse function for frame updates.
    public void Update()
    {
        animController();  // Checks if animation playing or finished.
        getCurrentAnimPos(); // Get current animation position.
        playSubtitles(); // Update subtitles position.
    }
    // *** Ends method Update.
    // --------------------------------------------------------------

    public void InitManager()
    {
        mTargetHandler = GameObject.FindObjectOfType(typeof(UserDefinedTargetEventHandler)) as UserDefinedTargetEventHandler;
        mTargetHandler.Init();
    }
    // ***Starts methods getCurrentAnimPos.
    // This method update current position in the animation at the current moment and pass the value to "_playPosition" varible, as well this method return a length of animation and animation state.
    public void getCurrentAnimPos()
    {
        Animation[] animationComponents = GetComponentsInChildren<Animation>();
        foreach (Animation animation in animationComponents)
        {
            foreach (AnimationState animState in animation)
            {
                _animationLength = animState.clip.length; // length of animation.
                animState.time = _currentAnimTime; // animation state.
                _playPosition = (_currentAnimTime / _animationLength); // playing position, value beetween 0...1.
            }
        }
    }
    // *** Ends methods getCurrentAnimPos.
    //--------------------------------------------------------------

    // This method play subtitles (subtitles represents as animation using Animator) backwards, forwards by refreshing animation position. 
    public void playSubtitles()
    {
        if (DefaultTrackableEventHandler._objectDetected == true)
        {
            _refreshSubtitlesFreq -= Time.deltaTime; // updating frequecy.
            if (_refreshSubtitlesFreq <= 0 && _subtitlesMode == 1) // refresh english subtitles.
            {
                engSubtitlesAnimator.speed = 1f; // animation speed.                
                engSubtitlesAnimator.Play("animEng", 0, _playPosition); // current animation name and playing position.
                _refreshSubtitlesFreq = 0.1f; // updating frequecy.
            }

            if (_refreshSubtitlesFreq <= 0 && _subtitlesMode == 2) // refresh japanese subtitles.
            {
                japSubtitlesAnimator.speed = 1f; // animation speed.
                japSubtitlesAnimator.Play("japSubtAnim", 0, _playPosition); // current animation name and playing position.
                _refreshSubtitlesFreq = 0.1f; // updating frequecy.
            }
        }
    }
    // *** Ends _rewindAnimation subtitles  method.
    //--------------------------------------------------------------

    // *** Starts animController method.
    // Method which is checks if animation is playing now and do specific action depend on animation time.
    public void animController()
    {
        if (SceneViewManager._stage == 4 || SceneViewManager._stage == 3)
        {
            //Debug.Log(_currentAnimTime);
            Animation[] animationComponents = GetComponentsInChildren<Animation>();
            foreach (Animation animation in animationComponents)
            {
                foreach (AnimationState animState in animation)
                {
                    _startFadingTime = (_animationLength - 3.0f);
                    _currentAnimTime = animState.time % animState.clip.length; // Get the current animation playing position in seconds.

                    if (_currentAnimTime <= 1) // This statement check if the current animation playing  
                                               //  not less than 1, if less than that value, than animation set on pause mode.
                    {
                        _pauseMode = true;
                    }

                    if (_currentAnimTime > (_startFadingTime - 0.2f) && _currentAnimTime <= _startFadingTime)// At this moment of time animations back to playback mode, f.x. after rewinding. It's time 3.5 seconds before and of animation.
                    {
                        animationOnPause();
                        audioOnPause();
                        animationPlay();
                        audioPlay();
                        _hideTargetGuide = true;

                        if (_subtitlesMode == 1)
                        {
                            endingEngAnimationObj.SetActive(true); // Activate english ending animation.
                            endingEnglishAnimator.speed = 1f; // animation speed.
                            endingEnglishAnimator.Play("endingAnimEnglish", 0);
                        }
                        if (_subtitlesMode == 2)
                        {
                            endingJapAnimationObj.SetActive(true); // Activate japanese ending animation.
                            endingJapaneseAnimator.speed = 1f; // animation speed.
                            endingJapaneseAnimator.Play("endingAnimJapanese", 0);
                        }
                        _lastStage = true; // last stage activating.
                        MainMenuManager._triggerMenu = false; // hide main menu.
                        Debug.Log("1");
                    }
                }
            }
            if (_lastStage == true)
            {
                _countDownTime -= Time.deltaTime;
                if (_countDownTime <= 3.1f && _countDownTime > 3.0f)
                {
                    fadedSettings.SetActive(true); // activate faded settings character object.
                    foreach (Animation animation in animationComponents)
                    {
                        foreach (AnimationState animState in animation)
                        {
                            animState.time = _currentAnimTime;
                        }
                    }
                    _toFadeOut = true; // starts fading.
                    japaneseSubtitlesObj.SetActive(false); // deactivate japanese subtitles.
                    englishSubtitlesObj.SetActive(false); // deactivate english subtitles.
                    charactersVoiceObj.SetActive(false); // deactivate character voice.                   
                    Debug.Log("2");
                }

                if (_countDownTime <= 2.7f && _countDownTime > 2.6f)
                {
                    normSettings.SetActive(false); // deactivate normal settings character object.
                    Debug.Log("3.1");
                }
                if (_countDownTime <= 0.2f && _countDownTime > 0.1f)
                {
                    fadedSettings.SetActive(false); // deactivate faded settings character object.
                    Debug.Log("3.2");
                }
            }

            if (_hideTargetGuide == true && _lastStage == true)
            {
                _timeEndAnim -= Time.deltaTime;
                if (_timeEndAnim < 0.5f)
                {
                    // Set ending animation on pause.
                    if (_subtitlesMode == 1)
                    {
                        endingEnglishAnimator.speed = 0f; // animation speed.
                        endingEnglishAnimator.Play("endingAnimEnglish", 0, 0.9f);
                    }
                    if (_subtitlesMode == 2)
                    {
                        endingJapaneseAnimator.speed = 0f; // animation speed.
                        endingJapaneseAnimator.Play("endingAnimJapanese", 0, 0.9f);
                    }
                    _lastStage = false;
                    SceneViewManager._stage = 5; // activate 5'th stage.
                    Debug.Log("4");
                }
            }
        }
    }
    // *** Ends animController method.
    //--------------------------------------------------------------

    // *** Starts method animationBackward.
    // Makes possible to play animation backward with estamated speed.
    public void animationBackward()
    {
        _switchPlayPauseBtn = true;
        Animation[] animationComponents = GetComponentsInChildren<Animation>();
        foreach (Animation animation in animationComponents)
        {
            foreach (AnimationState animState in animation)
            {
                animState.speed = -4f;
                animation.Play();
            }
        }
        Debug.Log("Animation playing backwards.");
    }
    // *** Ends method animation backward.
    //--------------------------------------------------------------

    // *** Starts method animation forward. Makes possible to play animation forward with estamated speed.
    public void animationForward()
    {
        _switchPlayPauseBtn = true;
        Animation[] animationComponents = GetComponentsInChildren<Animation>();
        foreach (Animation animation in animationComponents)
        {
            foreach (AnimationState animState in animation)
            {
                animState.speed = 5f;
                animation.Play();
                Debug.Log("Animation playing forwards.");
            }
        }
    }
    // *** Ends the method animation forward.
    //--------------------------------------------------------------

    // *** Starts method of animation on pause.
    public void animationOnPause()
    {
        _switchPlayPauseBtn = true; // Switch from pause button to play button on screen.
        Animation[] animationComponents = GetComponentsInChildren<Animation>();
        foreach (Animation animation in animationComponents)
        {
            foreach (AnimationState animState in animation)
            {
                animState.speed = 0f; // Animation playing speed;
                animation.Play();
            }
        }
        Debug.Log("Animation  has been paused.");
    }
    //*** Ends method of animation on pause.
    //--------------------------------------------------------------

    // *** Starts method of animation play.
    public void animationPlay()
    {
        _switchPlayPauseBtn = false;// Switch from play button to pause button on screen.
        _pauseMode = false; //Switch off pause mode;                   

        Animation[] animationComponents = GetComponentsInChildren<Animation>();
        foreach (Animation animation in animationComponents)
        {
            foreach (AnimationState animState in animation)
            {
                animState.time = _currentAnimTime;
                animState.speed = 1f;
                animation.Play();
            }
        }
        Debug.Log("Animation is playing.");
    }
    // *** Ends method of animation play.
    //--------------------------------------------------------------

    // *** Starts the method of scene replay.
    public void sceneReplay()
    {
        // Below reset all setting and prepare aplication replay a scene.
        _switchPlayPauseBtn = false;
        _subtitlesMode = 0;// Switch to default language select menu mode.
        normSettings.SetActive(false);
        fadedSettings.SetActive(false);
        charactersVoiceObj.SetActive(false);
        japaneseSubtitlesObj.SetActive(false);
        englishSubtitlesObj.SetActive(false);
        endingJapAnimationObj.SetActive(false);
        endingEngAnimationObj.SetActive(false);
        MainMenuManager._triggerMenu = false;
        _hideTargetGuide = false;
        _timeEndAnim = 8.0f;
        _countDownTime = 3.5f;
        SceneViewManager._stage = 1;
        Debug.Log("Scene has been replayed.");
    }
    // *** Ends method of scene on replay.
    //------------------------------------

    // ***Starts method to take a picture.
    public void takePicture()
    {
        DateTime dt = DateTime.Now;
        String strDate = "";
        strDate = dt.ToString("yyyy.MM.dd HH.mm.ss");
        screenShotName = String.Format("Time A Scene." + strDate + ".jpg");
        try
        {
            myPath = "/storage/emulated/0/Pictures/" + screenShotName; // my path android
            snapShot.CaptureAndSaveAtPath(myPath); // Call method to take a screen shot and save it in to early chosen path.
            Debug.Log("Screenshot has been taken and saved.");
        }
        catch
        {
            Debug.Log("Screenshot cannot be save in to: " + myPath);
        }
    }
    // ***Ends method to take a picture.
    //------------------------------------

    //**********************
    //Audio contol
    //**********************

    // Start audio play method.
    public void audioPlay()
    {
        if (_charactersVoice != null)
        {
            _charactersVoice.mute = false;
            _charactersVoice.pitch = 1f; // Audio speed.
            _charactersVoice.Play();
            Debug.Log("Audio is playing.");
        }
        else
        {
            Debug.Log("Audioclip doesn't exist.");
        }
    }
    // Ends audio play method.
    //------------------------------------ 

    // Starts audio pause method.
    public void audioOnPause()
    {
        if (_charactersVoice != null)
        {
            _charactersVoice.Pause();
            Debug.Log("Audio has been paused.");
        }
        else
        {
            Debug.Log("Audioclip doesn't exist.");
        }
    }
    // ***Ends audio pause method.
    //------------------------------------ 

    // ***Starts audio backward method.
    public void audioBackward()
    {
        if (_charactersVoice != null)
        {
            _charactersVoice.Pause();
            _charactersVoice.mute = true;
            _charactersVoice.pitch = -4f; // Audio Speed.
            _charactersVoice.Play();
            Debug.Log("Audio is rewinding backwards.");
        }
        else
        {
            Debug.Log("Audioclip doesn't exist.");
        }
    }
    // ***Ends audio backward method.
    //------------------------------------ 
    // ***Starts audio forwards method.
    public void audioForward()
    {
        if (_charactersVoice != null)
        {
            _charactersVoice.Pause();
            _charactersVoice.mute = true;
            _charactersVoice.pitch = 5f;
            _charactersVoice.Play();
            Debug.Log("Audio is rewinding forwards");
        }
        else
        {
            Debug.Log("Audioclip doesn't exist.");
        }
    }
    // ***Ends audio forward method.
    //------------------------------------
}
