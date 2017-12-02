﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndExamControlPanel : MonoBehaviour {


    private string errorMessage;
    private bool PassedExam;
    private bool ActiveEndPanel;

    public Transform TargetTransform;
    public Transform PassedBackgroundPanel;
    public Transform NotPassedBackgroundPanel;
    public Transform BlueEndPanel;
    public Transform RedEndPanel;
    public Transform HeadPanel;
    public Text HeadPanelText;
    public Transform ErrorMessagePanel;
    public Text ErrorMessagePanelText;
    public Transform ButtonPanel;
    private Sprite ChekedSprite;


    void Start () {
        if (ErrorMessagePanelText)
        {
            errorMessage = "";
            ErrorMessagePanelText.text = "";
        }
	}

	void Update () {

        if (ActiveEndPanel)
        {
            if (PassedExam)
            {
                ChekedSprite = Resources.Load<Sprite>("Textures/true");
                GoEndPanel(PassedBackgroundPanel, "Сценарій пройдено", BlueEndPanel);
                ActiveEndPanel = false;
            }
            else
            {
                ChekedSprite = Resources.Load<Sprite>("Textures/false");
                GoEndPanel(NotPassedBackgroundPanel, "Сценарій не пройдено", RedEndPanel);
                ActiveEndPanel = false;
            }
        }

	}

    void GoEndPanel(Transform backgroundPanel, string headPanelText, Transform EndPanel)
    {
        Transform panel = Instantiate(backgroundPanel);
        panel.transform.SetParent(TargetTransform, false);

        ErrorMessagePanelText.text = errorMessage;
        HeadPanelText.text = headPanelText;
        Transform miniPanel = Instantiate(EndPanel);
        miniPanel.transform.SetParent(panel, false);
        Transform HeadInMiniPanel = Instantiate(HeadPanel);
        HeadInMiniPanel.transform.SetParent(miniPanel, false);
        Transform ErrorMessageInMiniPanel = Instantiate(ErrorMessagePanel);
        ErrorMessageInMiniPanel.transform.SetParent(miniPanel, false);
        ErrorMessageInMiniPanel.GetChild(1).GetComponent<Image>().sprite = ChekedSprite;
        Transform ButtonInMiniPanel = Instantiate(ButtonPanel);
        ButtonInMiniPanel.transform.SetParent(miniPanel, false);
    }

    public void CloseExam()
    { 
        SceneManager.LoadScene("StepList");
    }

    public void CloseExamToExamList()
    {
        SceneManager.LoadScene("ExamList");
    }

    public void EndExam(bool PassedExam, string errorMessage = "")
    {
        this.errorMessage = errorMessage + (PassedExam ? "" : " Ви можете подивитися правильний порядок дій в інструкції до сценарію. Кнопка «?»");
        this.PassedExam = PassedExam;
        ActiveEndPanel = true;
    }
}
