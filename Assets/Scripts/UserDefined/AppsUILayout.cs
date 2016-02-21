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
using System;

public class AppsUILayout : MonoBehaviour
{
    AppManager appManager;

    // *** Buttons.
    public static Rect _btnEngLanguage;
    public static Rect _btnJapLanguage;
    public static Rect _btnNext;
    public static Rect _btnSkip;
    public static Rect _btnFrameMode;
    public static Rect _btnSouvenir;
    public static Rect _btnRestaurant;
    public static Rect _btnReturn;
    public static Rect _btnExit;
    public static Rect _btnTargetDetection;
    public static Rect _btnCamera;
    public static Rect _btnPause;
    public static Rect _btnPlay;
    public static Rect _btnBackward;
    public static Rect _btnForward;
    public static Rect _btnSpace;
    public static Rect _btnReplay;
    public static Rect _btnInfo;
    public static Rect _btnCloseApp;
    public static Rect _btnBack;
    public static Rect _btnFrame1;
    public static Rect _btnFrame2;
    public static Rect _btnFrame3;
    public static Rect _btnCameraFrameMode;
    public static Rect _btnBackFrameMode;

    // *** Textures.
    public Texture2D _txtrEngLanguage;
    public Texture2D _txtrJapLanguage;
    public Texture2D _txtrNextEng;
    public Texture2D _txtrNextJap;
    public Texture2D _txtrBackEng;
    public Texture2D _txtrBackJap;
    public Texture2D _txtrLblIntroEng;
    public Texture2D _txtrLblIntroJap;
    public Texture2D _txtrLblTitle;
    public Texture2D _txtrBtnSkip;
    public Texture2D _txtrbtnFrameMode;
    public Texture2D _txtrSouvenir;
    public Texture2D _txtrRestaurant;
    public Texture2D _txtrReturn;
    public Texture2D _txtrExit;
    public Texture2D _txtrButtonDetection;
    public Texture2D _txtrPlay;
    public Texture2D _txtrPause;
    public Texture2D _txtrCamera;
    public Texture2D _txtrBackward;
    public Texture2D _txtrForward;
    public Texture2D _txtrReplay;
    public Texture2D _txtrInfo;
    public Texture2D _txtrCloseApp;
    public Texture2D _txtrBtnSpace;
    public Texture2D _txtrTargetGuide;
    public Texture2D _txtrBtnMenuBackground;
    public Texture2D _txtrWaterMarkLogo;
    public Texture2D _txtrWaterMarkAddress;
    public Texture2D _txtrBtnFrame1;
    public Texture2D _txtrBtnFrame2;
    public Texture2D _txtrBtnFrame3;
    public Texture2D _txtrBtnBackPhotoMode;
    public Texture2D _txtrSelectionFrame;
    public Texture2D _txtrFrame1;
    public Texture2D _txtrFrame2;
    public Texture2D _txtrFrame3;
    public Texture2D _txtrCenter;
    public Texture2D _txtrInfoContent;

    // *** Labels.
    public static Rect _lblIntro;
    public static Rect _lblTitle;
    public static Rect _lblTargetGuide;
    public static Rect _lblMenuBackground;
    public static Rect _lblSelectionFrame1;
    public static Rect _lblSelectionFrame2;
    public static Rect _lblSelectionFrame3;
    public static Rect _lblFrame1;
    public static Rect _lblFrame2;
    public static Rect _lblFrame3;
    public static Rect _lblWaterMarkLogo;
    public static Rect _lblWaterMarkAddress;
    public static Rect _lblInfoContent;
    public static Rect _lblCenter;
    // ---------------------------------------------------
    void Start()
    {
        appManager = GameObject.FindObjectOfType<AppManager>();
    }
    public void layoutMyLaptop()
    {
        Debug.Log("my laptop layout");

        appManager.character.SetActive(true);
        // *** Buttons
        _btnEngLanguage = new Rect(505, 418, 100, 47);
        _btnJapLanguage = new Rect(338, 418, 100, 47);
        _btnNext = new Rect(535, 460, 75, 40);
        _btnFrameMode = new Rect(860, 20, 55, 40);
        _btnSkip = new Rect(800, 15, 101, 40);
        _btnBack = new Rect(340, 460, 75, 40);
        _btnSouvenir = new Rect(130, 425, 70, 75);
        _btnRestaurant = new Rect(325, 425, 70, 75);
        _btnReturn = new Rect(540, 425, 70, 75);
        _btnExit = new Rect(750, 425, 70, 75);
        _btnSpace = new Rect(0, 0, Screen.width, Screen.height);
        _btnInfo = new Rect(860, 20, 40, 40);
        _btnCamera = new Rect(860, 90, 40, 40);
        _btnForward = new Rect(860, 170, 40, 40);
        _btnPlay = new Rect(860, 240, 37, 40);
        _btnPause = new Rect(860, 240, 37, 40);
        _btnBackward = new Rect(860, 320, 40, 40);
        _btnReplay = new Rect(860, 400, 40, 40);
        _btnCloseApp = new Rect(860, 480, 40, 40);
        _btnCameraFrameMode = new Rect(860, 380, 50, 50);
        _btnBackFrameMode = new Rect(860, 450, 50, 50);
        _btnFrame1 = new Rect(850, 25, 76, 72);
        _btnFrame2 = new Rect(850, 125, 76, 72);
        _btnFrame3 = new Rect(850, 225, 76, 72);

        // *** Labels
        _lblTargetGuide = new Rect(160, 100, 640, 420);
        _lblMenuBackground = new Rect(830, 0, 122, Screen.height);
        _lblIntro = new Rect(65, 100, 840, 168);
        _lblTitle = new Rect(185, 115, 590, 370);
        _lblWaterMarkLogo = new Rect(700, 15, 200, 50);
        _lblWaterMarkAddress = new Rect(25, 510, 650, 25);
        _btnTargetDetection = new Rect(160, 100, 640, 420);
        _lblCenter = new Rect(472, 0, 4, Screen.height);
        _lblInfoContent = new Rect(0, 0, Screen.width, Screen.height);

        // *** Frames
        _lblFrame1 = new Rect(275, 75, 350, 425);
        _lblFrame2 = new Rect(150, 125, 600, 350);
        _lblFrame3 = new Rect(325, 175, 325, 325);

        // *** Selection frames
        _lblSelectionFrame1 = new Rect(848, 22, 80, 75);
        _lblSelectionFrame2 = new Rect(848, 122, 80, 75);
        _lblSelectionFrame3 = new Rect(848, 221, 80, 75);
    }

    // *** TABLETS/SMARTPHONES LAYOUTS

    public void layout2560x1600()
    {
        Debug.Log("2560 x 1600 pixels");
        Debug.Log("not implemented.");
    }
    // ----------------------------------

    public void layout2560x1440()
    {
        Debug.Log("2560 x 1440 pixels");
        Debug.Log("not implemented.");
    }
    // ----------------------------------

    public void layout2048x1536()
    {
        Debug.Log("2048 x 1536 pixels");
        Debug.Log("not implemented.");
    }
    // ----------------------------------

    public void layout1920x1200()
    {
        Debug.Log("1920 x 1200 pixels");

        appManager.character.SetActive(true);

        // *** Buttons
        _btnEngLanguage = new Rect(1010, 836, 240, 114);
        _btnJapLanguage = new Rect(670, 836, 240, 114);
        _btnNext = new Rect(1210, 920, 150, 80);
        _btnFrameMode = new Rect(1720, 50, 130, 100);
        _btnSkip = new Rect(1687, 30, 203, 81);
        _btnBack = new Rect(680, 920, 150, 80);
        _btnSouvenir = new Rect(264, 950, 150, 160);
        _btnRestaurant = new Rect(678, 950, 150, 160);
        _btnReturn = new Rect(1092, 950, 150, 160);
        _btnExit = new Rect(1506, 950, 150, 160);
        _btnSpace = new Rect(0, 0, Screen.width, Screen.height);
        _btnInfo = new Rect(1785, 90, 75, 75);
        _btnCamera = new Rect(1785, 245, 75, 73);
        _btnForward = new Rect(1790, 400, 75, 72);
        _btnPlay = new Rect(1785, 555, 75, 70);
        _btnPause = new Rect(1785, 555, 75, 70);
        _btnBackward = new Rect(1790, 710, 75, 73);
        _btnReplay = new Rect(1790, 865, 75, 73);
        _btnCloseApp = new Rect(1790, 1020, 75, 73);
        _btnCameraFrameMode = new Rect(1775, 800, 93, 93);
        _btnBackFrameMode = new Rect(1775, 1000, 93, 93);
        _btnFrame1 = new Rect(1750, 50, 150, 150);
        _btnFrame2 = new Rect(1750, 250, 150, 150);
        _btnFrame3 = new Rect(1750, 450, 150, 150);

        // *** Labels
        _lblTargetGuide = new Rect(322, 200, 1280, 840);
        _lblMenuBackground = new Rect(1720, 0, 200, 1200);
        _lblIntro = new Rect(130, 200, 1660, 336);
        _lblTitle = new Rect(370, 230, 1180, 740);
        _lblWaterMarkLogo = new Rect(1480, 30, 400, 100);
        _lblWaterMarkAddress = new Rect(50, 1100, 1300, 50);
        _btnTargetDetection = new Rect(322, 200, 1280, 840);
        _lblInfoContent = new Rect(0, 0, Screen.width+100, Screen.height);
        //_lblCenter = new Rect(958, 0, 4, Screen.height);

        // *** Frames 
        _lblFrame1 = new Rect(550, 150, 700, 850);
        _lblFrame2 = new Rect(300, 500, 1200, 600);
        _lblFrame3 = new Rect(650, 350, 650, 650);

        // *** Selection frames
        _lblSelectionFrame1 = new Rect(1745, 45, 160, 155);
        _lblSelectionFrame2 = new Rect(1745, 245, 160, 155);
        _lblSelectionFrame3 = new Rect(1745, 445, 160, 155);
    }
    // -----------------------------------

    public void layout1920x1080()
    {
        Debug.Log("1920 x 1080 pixels");
        appManager.character.SetActive(true);

        // *** Buttons
        _btnEngLanguage = new Rect(1010, 836, 240, 114);
        _btnJapLanguage = new Rect(670, 836, 240, 114);
        _btnNext = new Rect(1210, 920, 150, 80);
        _btnFrameMode = new Rect(1720, 50, 130, 100);
        _btnSkip = new Rect(1687, 30, 203, 81);
        _btnBack = new Rect(680, 920, 150, 80);
        _btnSouvenir = new Rect(264, 850, 150, 160);
        _btnRestaurant = new Rect(678, 850, 150, 160);
        _btnReturn = new Rect(1092, 850, 150, 160);
        _btnExit = new Rect(1506, 850, 150, 160);
        _btnSpace = new Rect(0, 0, Screen.width, Screen.height);
        _btnCamera = new Rect(1785, 200, 83, 83);
        _btnForward = new Rect(1790, 384, 79, 83);
        _btnPlay = new Rect(1785, 528, 79, 72);
        _btnPause = new Rect(1785, 528, 79, 72);
        _btnBackward = new Rect(1790, 702, 83, 83);
        _btnReplay = new Rect(1790, 876, 83, 83);
        _btnCameraFrameMode = new Rect(1775, 800, 93, 93);
        _btnBackFrameMode = new Rect(1775, 1000, 93, 93);
        _btnFrame1 = new Rect(1750, 50, 150, 150);
        _btnFrame2 = new Rect(1750, 250, 150, 150);
        _btnFrame3 = new Rect(1750, 450, 150, 150);

        // *** Labels
        _lblTargetGuide = new Rect(322, 200, 1280, 840);
        _lblMenuBackground = new Rect(1720, 0, 200, 1200);
        _lblIntro = new Rect(130, 200, 1660, 336);
        _lblTitle = new Rect(370, 230, 1180, 740);
        _lblWaterMarkLogo = new Rect(1480, 30, 400, 100);
        _lblWaterMarkAddress = new Rect(50, 1100, 1300, 50);
        _btnTargetDetection = new Rect(322, 200, 1280, 840);
        _lblInfoContent = new Rect(0, 0, Screen.width, Screen.height);
        //_lblCenter = new Rect(958, 0, 4, Screen.height);

        // *** Frames
        _lblFrame1 = new Rect(550, 150, 700, 850);
        _lblFrame2 = new Rect(300, 500, 1200, 600);
        _lblFrame3 = new Rect(650, 350, 650, 650);

        // *** Selection frames
        _lblSelectionFrame1 = new Rect(1745, 45, 160, 155);
        _lblSelectionFrame2 = new Rect(1745, 245, 160, 155);
        _lblSelectionFrame3 = new Rect(1745, 445, 160, 155);
    }
    // ----------------------------------

    public void layout1280x800()
    {
        Debug.Log("1280 x 800 pixels");
        Debug.Log("not implemented.");
    }
    // ----------------------------------

    public void layout1280x720()
    {
        Debug.Log("1280 x 720 pixels");
        Debug.Log("not implemented.");
    }
    // -----------------------------------

    public void layout1024x600()
    {
        Debug.Log("1024 x 600 pixels");
        Debug.Log("not implemented.");
    }
    // ----------------------------------

    public void layout540x960()
    {
        Debug.Log("540 x 960 pixels");
        Debug.Log("not implemented.");
    }
    // -----------------------------------

    public void layout480x800()
    {
        Debug.Log("480 x 800 pixels");
        Debug.Log("not implemented.");
    }
    // -----------------------------------

    public void layout480x854()
    {
        Debug.Log("480 x 854 pixels");
        Debug.Log("not implemented.");
    }
    // -----------------------------------

    public void layout320x480()
    {
        Debug.Log("320 x 480 pixels");
        Debug.Log("not implemented.");
    }
    // -----------------------------------
}
