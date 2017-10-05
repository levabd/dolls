using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        //ActiveEndPanel = true;
        //PassedExam = false;
        //errorMessage = "fgfgfgfgfg";
	}
	

	void Update () {

        if (ActiveEndPanel)
        {
            if (PassedExam)
            {
                ActivePassedPanel();
                ActiveEndPanel = false;
            }
            else
            {
                ActiveNotPassedPanel();
                ActiveEndPanel = false;
            }
        }

	}
    void ActivePassedPanel()
    {
        Transform panel = Instantiate(PassedBackgroundPanel);
        panel.transform.SetParent(TargetTransform, false);
        //panel.transform.SetParent(gameObject.transform, false);
        Transform miniPanel = Instantiate(BlueEndPanel);
        miniPanel.transform.SetParent(panel, false);
        Transform HeadInMiniPanel = Instantiate(HeadPanel);
        HeadInMiniPanel.transform.SetParent(miniPanel, false);
        HeadPanelText.text = "Экзамен пройден"; 
        Transform ButtonInMiniPanel = Instantiate(ButtonPanel);
        ButtonInMiniPanel.transform.SetParent(miniPanel, false);
        

        
    }
    void ActiveNotPassedPanel()
    {
        Transform panel = Instantiate(NotPassedBackgroundPanel);
        panel.transform.SetParent(TargetTransform, false);
        //panel.transform.SetParent(gameObject.transform, false);
        Transform miniPanel = Instantiate(RedEndPanel);
        miniPanel.transform.SetParent(panel, false);
        Transform HeadInMiniPanel = Instantiate(HeadPanel);
        HeadInMiniPanel.transform.SetParent(miniPanel, false);
        HeadPanelText.text = "Экзамен не пройден";
        Transform ErrorMessageInMiniPanel = Instantiate(ErrorMessagePanel);
        ErrorMessageInMiniPanel.transform.SetParent(miniPanel, false);
        ErrorMessagePanelText.text = errorMessage;
        Transform ButtonInMiniPanel = Instantiate(ButtonPanel);
        ButtonInMiniPanel.transform.SetParent(miniPanel, false);
        
    }
    public void GoToExaminingMenuScene()
    {
        Application.LoadLevel("Examining_menu_scene");
    }
    public void EndExam(bool PassedExam, string errorMessage = "")
    {
        this.errorMessage = errorMessage;
        this.PassedExam = PassedExam;
        ActiveEndPanel = true;
    }
}
