/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
// Updated: 30/01/2016
==============================================================================*/

using System;
using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;
        private AppManager appManager;

        public static bool _objectDetected = false; // lets animation playing, after  language selection, ONLY if target is detected

        #endregion // PRIVATE_MEMBER_VARIABLES

        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            appManager = GameObject.FindObjectOfType<AppManager>();
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();

            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }
        #endregion // UNTIY_MONOBEHAVIOUR_METHODS

        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                                      newStatus == TrackableBehaviour.Status.TRACKED ||
                                      newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                _objectDetected = true;
                OnTrackingFound();
            }
            else
            {
                _objectDetected = false;
                OnTrackingLost();
            }
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS  

        // *** If target has been detected, calls this method, which in turn call play animation and audio methods
        // from ApplicationController Script.
        private void OnTrackingFound()
        {
            if (SceneViewManager._stage == 3 || SceneViewManager._stage == 4)
            {
                Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " is detected.");

                appManager.character.SetActive(true);
                appManager.animationPlay();
                appManager.audioPlay();
                if (AppManager._subtitlesMode == 1)
                {
                    appManager.englishSubtitlesObj.SetActive(true);
                }
                if (AppManager._subtitlesMode == 2)
                {
                    appManager.japaneseSubtitlesObj.SetActive(true);
                }

            }
            else
            {
                Debug.Log("Wait until user define a target and title finished to play.");
            }
        }
        //-------------------------------

        // *** If target object has been lost, calls this method, which in turn call pause animation and audio methods
        // from ApplicationController Script.
        private void OnTrackingLost()
        {
            if (SceneViewManager._stage == 3 || SceneViewManager._stage == 4)
            {

                Debug.Log("Trackable: " + mTrackableBehaviour.TrackableName + " is lost.");
                appManager.character.SetActive(false);
                appManager.japaneseSubtitlesObj.SetActive(false);
                appManager.englishSubtitlesObj.SetActive(false);
                appManager.animationOnPause();
                appManager.audioOnPause();
            }
            else
            {
                Debug.Log("Wait until user define a target and title finished play.");
            }
        }
    }
    #endregion // PRIVATE_METHODS
}

