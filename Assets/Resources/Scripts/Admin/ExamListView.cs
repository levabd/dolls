using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DB.Models;
using SLS.Widgets.Table;
using UI.Dates;
using Debug = UnityEngine.Debug;

// ReSharper disable once CheckNamespace
public class ExamListView : MonoBehaviour {

    public Button LogoutButton;
    public Button Exit;
    public Button ExamsButton;
    public Button FilterButton;

    public Button PassedFilter;
    public Button NotPassedFilter;
    public Button AllFilter;

    public Text Name;

    public Image DataTable;
    public Sprite PassedIcon;
    public Sprite NotPassedIcon;
    private Table _dataTable;
    private List<Exam> _exams;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    public DatePicker ExamFromDate;
    public DatePicker ExamToDate;
    public Dropdown ExamsDropdown;
    public InputField UsernameFilterInputField;

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
            d.elements.Add(exam.PassedAt.Date.ToString("dd MMMM yyyy"));
            d.elements.Add("Детальніше");
            d.elements[CurrentUser.User.Role == User.UserRoles.Manager ? 5 : 4].color = new Color(0, 0, 1f);
            d.elements.Add(exam.Passed ? "passed" : "not_passed");

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
        ExamsButton.gameObject.SetActive(CurrentUser.User.Role == User.UserRoles.User);

        // Initialize Table
        _dataTable = DataTable.GetComponent<Table>();
        _dataTable.ResetTable();
        _dataTable.AddTextColumn("№", null, 20f, 20f);
        if (CurrentUser.User.Role == User.UserRoles.Manager) _dataTable.AddTextColumn("ПІБ", null, 300f, 300f);
        _dataTable.AddTextColumn("Назва тесту", null, 500f, 500f);
        _dataTable.AddTextColumn("Помилка", null, 400f, 400f);
        _dataTable.AddTextColumn("Дата прохоження", null, 150f, 150f);
        _dataTable.AddTextColumn("Детальніше", null, 150f, 150f);
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

    void CloseApp()
    {
        GeneralSceneHelper.QuitGame();
    }

    void CloseModal()
    {
        Dialog.SetActive(false);
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
            Debug.Log("You Clicked: " + datum.uid + " Column: " + column.idx);
            if (Convert.ToInt32(CurrentUser.User.Role == User.UserRoles.User) + column.idx == 5)
            {
                CurrentAdminExam.Exam = _exams[Int32.Parse(datum.uid)];
                SceneManager.LoadScene("Examining_menu_scene");
            }
        }
    }
}
