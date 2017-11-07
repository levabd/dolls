﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DB.Models;
using SLS.Widgets.Table;

// ReSharper disable once CheckNamespace
public class StepListView : MonoBehaviour {

    public Button BackButton;
    public Button ExamsButton;

    public Text Name;
    public Text ExamDescription;

    public Image DataTable;
    public Sprite PassedIcon;
    public Sprite NotPassedIcon;
    private Table _dataTable;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    private void ReloadData()
    {
        _dataTable.data.Clear();

        int index = 0;

        foreach (var step in Step.FindByExam(CurrentAdminExam.Exam))
        {
            Datum d = Datum.Body(index.ToString());
            d.elements.Add(step.OrderedAt);
            d.elements.Add(step.Name);
            d.elements.Add(step.Error);
            d.elements.Add(step.OrderNumber);
            d.elements.Add(step.Passed ? "passed" : "not_passed");

            _dataTable.data.Add(d);
            index++;
        }
    }

    // Unused Unity Methods
    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    void Update() {  }

    // Initialisation
    // ReSharper disable once UnusedMember.Local
    void Start()
    {
        // Event listeners
        Button btn = BackButton.GetComponent<Button>();
        btn.onClick.AddListener(OpenExamList);

        btn = ExamsButton.GetComponent<Button>();
        btn.onClick.AddListener(OpenExams);

        btn = DialogButton.GetComponent<Button>();
        btn.onClick.AddListener(CloseModal);

        Name.text = CurrentUser.User.Name;
        ExamDescription.text = "Користувачем " + CurrentAdminExam.Exam.User.Name + " було пройдено сценарій «" + CurrentAdminExam.Exam.Name + "»";

        // Initialize Table
        _dataTable = DataTable.GetComponent<Table>();
        _dataTable.ResetTable();
        _dataTable.AddTextColumn("Номер кроку", null, 100f, 100f);
        _dataTable.AddTextColumn("Назва тесту", null, 500f, 500f);
        _dataTable.AddTextColumn("Помилка", null, 400f, 400f);
        _dataTable.AddTextColumn("Коректний номер кроку", null, 200f, 200f);
        _dataTable.AddImageColumn("Результат");

        Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>
        {
            {"passed", PassedIcon},
            {"not_passed", NotPassedIcon}
        };

        _dataTable.Initialize(OnTableSelectedWithCol, spriteDict);

        // Fill the data
        ReloadData();

        _dataTable.StartRenderEngine();
    }

    void CloseModal()
    {
        Dialog.SetActive(false);
    }

    void OpenExamList()
    {
        SceneManager.LoadScene("ExamManager_scene");
    }

    void OpenExams()
    {
        SceneManager.LoadScene("Exams_choice");
    }

    private void OnTableSelectedWithCol(Datum datum, Column column) { }
}