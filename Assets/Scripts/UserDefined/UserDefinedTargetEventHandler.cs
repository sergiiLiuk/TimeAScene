/*===============================================================================
Copyright (c) 2012-2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
 
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
Vuforia is a trademark of QUALCOMM Incorporated, registered in the United States 
and other countries. Trademarks of QUALCOMM Incorporated are used with permission.
// Updated: 30/01/2016
===============================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

public class UserDefinedTargetEventHandler : MonoBehaviour, IUserDefinedTargetEventHandler
{
    #region PUBLIC_MEMBERS
    /// <summary>
    /// Can be set in the Unity inspector to reference a ImageTargetBehaviour that is instanciated for augmentations of new user defined targets.
    /// </summary>
    public ImageTargetBehaviour ImageTargetTemplate;
    public int IndexForMostRecentlyAddedTrackable
    {
        get
        {
            return (mTargetCounter - 1) % 5;
        }
    }

    #endregion PUBLIC_MEMBERS

    #region PRIVATE_MEMBERS

    private UserDefinedTargetBuildingBehaviour mTargetBuildingBehaviour;
    private ObjectTracker mObjectTracker;
    private AppManager appManager;
    // DataSet that newly defined targets are added to
    private DataSet mBuiltDataSet;
    // currently observed frame quality
    private ImageTargetBuilder.FrameQuality mFrameQuality = ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE;
    // counter variable used to name duplicates of the image target template
    private int mTargetCounter;
    public static bool _readyToTake = false;
    #endregion PRIVATE_MEMBERS
    void Start()
    {
        appManager = GameObject.FindObjectOfType<AppManager>();
    }
    /// <summary>
    /// Registers this component as a handler for UserDefinedTargetBuildingBehaviour events
    /// </summary>
    void Update()
    {
        if (_readyToTake == true)
        {
            Debug.Log("Build New Target.");
            BuildNewTarget();
        }

    }/// 

    public void Init()
    {
        mTargetBuildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour>();
        if (mTargetBuildingBehaviour)
        {
            mTargetBuildingBehaviour.RegisterEventHandler(this);
            Debug.Log("Registering to the events of IUserDefinedTargetEventHandler");
        }
    }

    #region IUserDefinedTargetEventHandler implementation
    /// <summary>
    /// Called when UserDefinedTargetBuildingBehaviour has been initialized successfully
    /// </summary>
    public void OnInitialized()
    {
        mObjectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (mObjectTracker != null)
        {
            // create a new dataset
            mBuiltDataSet = mObjectTracker.CreateDataSet();
            mObjectTracker.ActivateDataSet(mBuiltDataSet);
        }
    }

    /// <summary>
    /// Updates the current frame quality
    /// </summary>
    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {
        mFrameQuality = frameQuality;
    }

    /// <summary>
    /// Takes a new trackable source and adds it to the dataset
    /// This gets called automatically as soon as you 'BuildNewTarget with UserDefinedTargetBuildingBehaviour
    /// </summary>
    public void OnNewTrackableSource(TrackableSource trackableSource)
    {
        mTargetCounter++;

        // deactivates the dataset first
        mObjectTracker.DeactivateDataSet(mBuiltDataSet);

        // Destroy the oldest target if the dataset is full or the dataset 
        // already contains five user-defined targets.
        if (mBuiltDataSet.HasReachedTrackableLimit() || mBuiltDataSet.GetTrackables().Count() >= 5)
        {
            IEnumerable<Trackable> trackables = mBuiltDataSet.GetTrackables();
            Trackable oldest = null;
            foreach (Trackable trackable in trackables)
                if (oldest == null || trackable.ID < oldest.ID)
                    oldest = trackable;

            if (oldest != null)
            {
                Debug.Log("Destroying oldest trackable in UDT dataset: " + oldest.Name);
                mBuiltDataSet.Destroy(oldest, true);
            }
        }

        // get predefined trackable and instantiate it
        ImageTargetBehaviour imageTargetCopy = (ImageTargetBehaviour)Instantiate(ImageTargetTemplate);
        imageTargetCopy.gameObject.name = "UserDefinedTarget-" + mTargetCounter;

        // add the duplicated trackable to the data set and activate it
        mBuiltDataSet.CreateTrackable(trackableSource, imageTargetCopy.gameObject);
        // call animatioPlay and audioPlay methods.

        // activate the dataset again
        mObjectTracker.ActivateDataSet(mBuiltDataSet);
        StartCoroutine(hideForShotButtons());


    }
    #endregion IUserDefinedTargetEventHandler implementation
    private IEnumerator hideForShotButtons()
    {
        yield return new WaitForSeconds(0.2f);
       // ----------
        appManager.normSettings.SetActive(true); // activate a chracter with normall settings.
        appManager.charactersVoiceObj.SetActive(true); // enable voice audio.
        AppManager._currentAnimTime = 0.0f; // set animation playing time to 0. 
        appManager.animationPlay();
        appManager.audioPlay();
        appManager.subtitlesSet.SetActive(true);
        AppManager._executeOnce = false;
        AppManager._tartgetDetectionMode = false;
        //----------
    }
    private void BuildNewTarget()
    {
        // create the name of the next target.
        // the TrackableName of the original, linked ImageTargetBehaviour is extended with a continuous number to ensure unique names
        string targetName = string.Format("{0}-{1}", ImageTargetTemplate.TrackableName, mTargetCounter);

        // generate a new target name:

        mTargetBuildingBehaviour.BuildNewTarget(targetName, ImageTargetTemplate.GetSize().x);

        _readyToTake = false;
    }
}



