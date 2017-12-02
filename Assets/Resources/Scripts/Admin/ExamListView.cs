using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DB.Models;
using SLS.Widgets.Table;
using UI.Dates;

// ReSharper disable once CheckNamespace
public class ExamListView : MonoBehaviour
{
    public Button LogoutButton;
    public Button Exit;
    public Button ExamsButton;
    public Button FilterButton;

    public Button PassedFilter;
    public Button NotPassedFilter;
    public Button AllFilter;

    public Text Name;

    public Image DataTable;
    public Sprite TutorialIcon;
    public Sprite PassedIcon;
    public Sprite NotPassedIcon;
    private Table _dataTable;
    private List<Exam> _exams;

    public GameObject Tutorial;
    public Button TutorialCloseButton;
    public TutorialController TutorialControllerObj;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    public DatePicker ExamFromDate;
    public DatePicker ExamToDate;
    public Dropdown ExamsDropdown;

    public Text UsernameFilterText;
    public InputField UsernameFilterInputField;

    public LoaderController Loader;

    private Exam.PassedFilter _passedFilter = Exam.PassedFilter.All;

    private void ReloadData()
    {
        string userName = CurrentUser.User.Role == User.UserRoles.Manager ? UsernameFilterInputField.text : CurrentUser.User.Name;
        string examName = ExamsDropdown.value > 0 ? ExamsDropdown.captionText.text : "";

        _dataTable.data.Clear();

        if (ExamFromDate.SelectedDate.HasValue) Debug.Log(ExamFromDate.SelectedDate.Date);

        _exams = Exam.Find(
            ExamFromDate.SelectedDate.HasValue ? ExamFromDate.SelectedDate.Date.AddDays(-1) : new DateTime(1970, 1, 1),
            ExamToDate.SelectedDate.HasValue ? ExamToDate.SelectedDate.Date.AddDays(1) : DateTime.Now.AddDays(1),
            examName, userName, _passedFilter);

        int index = 0;

        foreach (var exam in _exams)
        {
            Datum d = Datum.Body(index.ToString());
            d.elements.Add(exam.Id ?? 0);
            if (CurrentUser.User.Role == User.UserRoles.Manager) d.elements.Add(exam.User.Name);
            d.elements.Add(exam.Name);
            d.elements.Add(exam.Error);
            if (CurrentUser.User.Role == User.UserRoles.User)
            {
                d.elements.Add("Пройти знову ➦");
                d.elements[3].color = new Color(0, 0, 1f);
            }
            d.elements.Add("Деталізація покроково ➦");
            d.elements[4].color = new Color(0, 0, 1f);
            d.elements.Add("tutorial");
            d.elements.Add(exam.PassedAt.Date.ToString("dd MMMM yyyy"));
            d.elements.Add(exam.Passed ? "passed" : "not_passed");

            _dataTable.data.Add(d);
            index++;
        }
    }

    // Unused Unity Methods
    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    void Update() { }

    // Initialisation
    // ReSharper disable once UnusedMember.Local
    void Start()
    {
        // Event listeners
        Button btn = Exit.GetComponent<Button>();
        btn.onClick.AddListener(CloseApp);

        btn = LogoutButton.GetComponent<Button>();
        btn.onClick.AddListener(Logout);

        btn = TutorialCloseButton.GetComponent<Button>();
        btn.onClick.AddListener(TutorialClose);

        btn = ExamsButton.GetComponent<Button>();
        btn.onClick.AddListener(OpenExams);

        btn = FilterButton.GetComponent<Button>();
        btn.onClick.AddListener(Filter);

        btn = DialogButton.GetComponent<Button>();
        btn.onClick.AddListener(CloseModal);

        btn = PassedFilter.GetComponent<Button>();
        btn.onClick.AddListener(PassedFilterClick);

        btn = NotPassedFilter.GetComponent<Button>();
        btn.onClick.AddListener(NotPassedFilterClick);

        btn = AllFilter.GetComponent<Button>();
        btn.onClick.AddListener(AllFilterClick);

        Name.text = CurrentUser.User.Name;

        System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("uk-UA");

        // if user not manager and admin he cannot filter by user

        UsernameFilterInputField.gameObject.SetActive(CurrentUser.User.Role == User.UserRoles.Manager);
        UsernameFilterText.gameObject.SetActive(CurrentUser.User.Role == User.UserRoles.Manager);
        ExamsButton.gameObject.SetActive(CurrentUser.User.Role == User.UserRoles.User);

        // Initialize Table
        _dataTable = DataTable.GetComponent<Table>();
        _dataTable.ResetTable();
        Column numberColumn = _dataTable.AddTextColumn("ID сценарію", null, 100f, 100f);
        numberColumn.horAlignment = Column.HorAlignment.RIGHT;
        if (CurrentUser.User.Role == User.UserRoles.Manager) _dataTable.AddTextColumn("ПІБ", null, 300f, 300f);
        _dataTable.AddTextColumn("Назва тесту", null, 380f, 380f);
        _dataTable.AddTextColumn("Помилка", null, 380f, 380f);
        if (CurrentUser.User.Role == User.UserRoles.User) _dataTable.AddTextColumn("", null, 180f, 180f);
        _dataTable.AddTextColumn("", null, 200f, 200f);
        _dataTable.AddImageColumn("Інструкція");
        _dataTable.AddTextColumn("Дата проходження", null, 150f, 150f);
        _dataTable.AddImageColumn("Результат");

        Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>
        {
            { "tutorial", TutorialIcon},
            { "passed", PassedIcon},
            { "not_passed", NotPassedIcon}
        };

        _dataTable.Initialize(OnTableSelectedWithCol, spriteDict);

        // Fill the data
        ReloadData();

        _dataTable.StartRenderEngine();
    }

    void CloseApp()
    {
        GeneralSceneHelper.QuitGame();
    }

    void CloseModal()
    {
        Dialog.SetActive(false);
    }

    void TutorialClose()
    {
        Debug.Log("Tutorial Close");
        Tutorial.SetActive(false);
    }

    void PassedFilterClick()
    {
        _passedFilter = Exam.PassedFilter.Passed;
        ReloadData();
    }

    void NotPassedFilterClick()
    {
        _passedFilter = Exam.PassedFilter.NotPassed;
        ReloadData();
    }

    void AllFilterClick()
    {
        _passedFilter = Exam.PassedFilter.All;
        ReloadData();
    }

    void Filter()
    {
        ReloadData();
    }

    void Logout()
    {
        SceneManager.LoadScene("Autorization_first_scene");
    }

    void OpenExams()
    {
        SceneManager.LoadScene("Exams_choice");
    }

    private void OnTableSelectedWithCol(Datum datum, Column column)
    {
        if (datum == null) return;
        if (column != null)
        {
            if (CurrentUser.User.Role == User.UserRoles.User && column.idx == 3)
            {
                Type examType = Type.GetType(_exams[Int32.Parse(datum.uid)].Class);
                if (examType != null)
                {
                    CurrentExam.Instance.Exam = null;
                    CurrentExam.Instance.Exam = (BaseExam)Activator.CreateInstance(examType);
                    Loader.ActiveLoader(CurrentExam.Instance.Exam.LoadName);
                    //SceneManager.LoadScene(CurrentExam.Instance.Exam.LoadName);
                }
            }

            if (column.idx == 4)
            {
                CurrentAdminExam.Exam = _exams[Int32.Parse(datum.uid)];
                SceneManager.LoadScene("StepList");
            }

            if (column.idx == 5)
            {
                Type examType = Type.GetType(_exams[Int32.Parse(datum.uid)].Class);
                if (examType != null)
                {
                    BaseExam currentExam = (BaseExam)Activator.CreateInstance(examType);
                    TutorialControllerObj.TutorialCreate(currentExam.LoadName);
                    Tutorial.SetActive(true);
                }
            }
        }
    }
}
