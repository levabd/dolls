using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DB.Models;
using SLS.Widgets.Table;

// ReSharper disable once CheckNamespace
public class StepListView : MonoBehaviour {

    public Button LogoutButton;
    public Button Exit;
    public Button BackButton;
    public Button RepeatButton;
    public Button ExamsButton;
    public Button TutorialButton;

    public Text Name;
    public Text ExamDescription;

    public Image DataTable;
    public Sprite PassedIcon;
    public Sprite NotPassedIcon;
    private Table _dataTable;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    public GameObject Tutorial;
    public Button TutorialCloseButton;
    public TutorialController TutorialControllerObj;

    public LoaderController Loader;

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
        Button btn = Exit.GetComponent<Button>();
        btn.onClick.AddListener(CloseApp);

        btn = LogoutButton.GetComponent<Button>();
        btn.onClick.AddListener(Logout);

        btn = BackButton.GetComponent<Button>();
        btn.onClick.AddListener(OpenExamList);

        btn = RepeatButton.GetComponent<Button>();
        btn.onClick.AddListener(RepeatExam);

        btn = TutorialButton.GetComponent<Button>();
        btn.onClick.AddListener(OpenTutorial);

        btn = TutorialCloseButton.GetComponent<Button>();
        btn.onClick.AddListener(TutorialClose);

        btn = ExamsButton.GetComponent<Button>();
        btn.onClick.AddListener(OpenExams);

        btn = DialogButton.GetComponent<Button>();
        btn.onClick.AddListener(CloseModal);

        Name.text = CurrentUser.User.Name;

        if (CurrentUser.User.Role == User.UserRoles.Manager)
            ExamDescription.text = "Користувачем " + CurrentAdminExam.Exam.User.Name + " було ";
        if (CurrentUser.User.Role == User.UserRoles.User)
            ExamDescription.text = "Було ";
        ExamDescription.text = ExamDescription.text + (CurrentAdminExam.Exam.Passed ? "пройдено" : "провалено") + " сценарій «" + CurrentAdminExam.Exam.Name + "»";

        ExamDescription.color = CurrentAdminExam.Exam.Passed ? new Color(7f / 256f, 122f / 256f, 63f / 256f, 1f) : Color.red;

        ExamsButton.gameObject.SetActive(CurrentUser.User.Role == User.UserRoles.User);
        RepeatButton.gameObject.SetActive(CurrentUser.User.Role == User.UserRoles.User);

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

    void RepeatExam()
    {
        Type examType = Type.GetType(CurrentAdminExam.Exam.Class);
        if (examType != null)
        {
            CurrentExam.Instance.Exam = null;
            CurrentExam.Instance.Exam = (BaseExam) Activator.CreateInstance(examType);
            Loader.ActiveLoader(CurrentExam.Instance.Exam.LoadName);
            //SceneManager.LoadScene(CurrentExam.Instance.Exam.LoadName);
        }
    }

    void OpenTutorial()
    {
        Type examType = Type.GetType(CurrentAdminExam.Exam.Class);
        if (examType != null)
        {
            BaseExam currentExam = (BaseExam)Activator.CreateInstance(examType);
            TutorialControllerObj.TutorialCreate(currentExam.LoadName);
            Tutorial.SetActive(true);
        }
    }

    void TutorialClose()
    {
        Debug.Log("Tutorial Close");
        Tutorial.SetActive(false);
    }


    void CloseApp()
    {
        GeneralSceneHelper.QuitGame();
    }

    void Logout()
    {
        SceneManager.LoadScene("Autorization_first_scene");
    }

    void OpenExamList()
    {
        SceneManager.LoadScene("ExamList");
    }

    void OpenExams()
    {
        SceneManager.LoadScene("Exams_choice");
    }

    private void OnTableSelectedWithCol(Datum datum, Column column) { }
}
