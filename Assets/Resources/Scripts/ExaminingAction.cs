using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


[Serializable]
public class ExaminingAction : MonoBehaviour {



    public GameObject HeaderPanel;

    public GameObject ResultListPanel;
        public Text ViewNumText;
        public Text ViewNameTestText;
        public Text ViewResultText;
        public Text ViewDateTestText;
        public Button ViewDetailingButton;

    public GameObject DetailingListPanel;
        public Text ViewNameTestTitleText;
        public Text ViewDateTestTitleText;
        public Text ViewResultTitleText;
        public Text ViewDetailingNumText;
        public Text ViewDetailingNameStepText;
        public Text ViewDetailingResultStepText;

    // Use this for initialization
    void Start () {
        HeaderPanel.SetActive(true);
        ResultListPanel.SetActive(true);
        DetailingListPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
