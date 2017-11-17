using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndExamControlPanel : MonoBehaviour {


    private string errorMessage;
    private bool PassedExam;
    private bool ActiveEndPanel = false;

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
                GoEndPanel(PassedBackgroundPanel, "Екзамен пройдено успішно", BlueEndPanel);
                ActiveEndPanel = false;
            }
            else
            {
                GoEndPanel(NotPassedBackgroundPanel, "Екзамен не пройден", RedEndPanel);
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
        Transform ButtonInMiniPanel = Instantiate(ButtonPanel);
        ButtonInMiniPanel.transform.SetParent(miniPanel, false);
    }

    public void CloseExam()
    {
        SceneManager.LoadScene("Examining_menu_scene");
    }

    public void EndExam(bool PassedExam, string errorMessage = "")
    {
        this.errorMessage = errorMessage;
        this.PassedExam = PassedExam;
        ActiveEndPanel = true;
    }
}
