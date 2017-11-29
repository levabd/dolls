using DB.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
class EyeExamDiabeticView : MonoBehaviour
{
    public Button FinishButton;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    public EndExamControlPanel EndExam;

    public Toggle Retina1;
    public Toggle Retina2;
    public Toggle Retina3;
    public Toggle Retina4;
    public Toggle Retina5;
    public Toggle Retina6;
    public Toggle Retina7;
    public Toggle Retina8;
    public Toggle Retina9;
    public Toggle Retina10;
    public Toggle Retina11;
    public Toggle Retina12;

    public Image ImageEye1;
    public Image ImageEye2;
    public Image ImageEye3;
    public Image ImageEye4;
    public Image ImageEye5;
    public Image ImageEye6;
    public Image ImageEye7;
    public Image ImageEye8;
    public Image ImageEye9;
    public Image ImageEye10;
    public Image ImageEye11;
    public Image ImageEye12;
    
    private bool _finished;

    // Unused Unity Methods
    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    void Update() { }

    // Initialisation
    // ReSharper disable once UnusedMember.Local
    void Start()
    {
        Toggle tgl = Retina1.GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(ChangeToggle);

        tgl = Retina2.GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(ChangeToggle);

        tgl = Retina3.GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(ChangeToggle);

        tgl = Retina4.GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(ChangeToggle);

        tgl = Retina5.GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(ChangeToggle);

        tgl = Retina6.GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(ChangeToggle);

        tgl = Retina7.GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(ChangeToggle);

        tgl = Retina8.GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(ChangeToggle);

        tgl = Retina9.GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(ChangeToggle);

        tgl = Retina10.GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(ChangeToggle);

        tgl = Retina11.GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(ChangeToggle);

        tgl = Retina12.GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(ChangeToggle);

        Button btn = FinishButton.GetComponent<Button>();
        btn.onClick.AddListener(FinishEvent);

        btn = DialogButton.GetComponent<Button>();
        btn.onClick.AddListener(CloseModal);
    }

    private void ChangeToggle(bool arg0)
    {
        ImageEye1.gameObject.SetActive(Retina1.isOn);
        ImageEye2.gameObject.SetActive(Retina2.isOn);
        ImageEye3.gameObject.SetActive(Retina3.isOn);
        ImageEye4.gameObject.SetActive(Retina4.isOn);
        ImageEye5.gameObject.SetActive(Retina5.isOn);
        ImageEye6.gameObject.SetActive(Retina6.isOn);
        ImageEye7.gameObject.SetActive(Retina7.isOn);
        ImageEye8.gameObject.SetActive(Retina8.isOn);
        ImageEye9.gameObject.SetActive(Retina9.isOn);
        ImageEye10.gameObject.SetActive(Retina10.isOn);
        ImageEye11.gameObject.SetActive(Retina11.isOn);
        ImageEye12.gameObject.SetActive(Retina12.isOn);
    }

    void FinishEvent()
    {
        bool examResult = CheckExam();
        _finished = true;
        Exam exam = new Exam(CurrentUser.User, "EyeDiabeticExam", "Препроліферативна діабетична ретинопатія", examResult ? "" : "Тест не пройдено, перевірте свої відповіді.", examResult);
        exam.Save();
        CurrentAdminExam.Exam = exam;
        GeneralSceneHelper.ShowMessage(examResult ? "Вітаємо з успішним проходженням" : "Тест не пройдено, перевірте свої відповіді.", Dialog, DialogText);
        //EndExam.EndExam(examResult, examResult ? "Вітаємо з успішним проходженням" : "Тест не пройдено, перевірте свої відповіді.");

    }

    void CloseModal()
    {
        if (_finished)
            SceneManager.LoadScene("ExamList");
        else
            Dialog.SetActive(false);
    }

    private bool CheckExam()
    {
        if (Retina9.isOn || Retina10.isOn || Retina11.isOn || Retina12.isOn)
            return false;

        int rightAnsversCount = 0;

        if (Retina1.isOn)
            rightAnsversCount++;
        if (Retina2.isOn)
            rightAnsversCount++;
        if (Retina3.isOn)
            rightAnsversCount++;
        if (Retina4.isOn)
            rightAnsversCount++;
        if (Retina5.isOn)
            rightAnsversCount++;
        if (Retina6.isOn)
            rightAnsversCount++;
        if (Retina7.isOn)
            rightAnsversCount++;
        if (Retina8.isOn)
            rightAnsversCount++;

        if (rightAnsversCount > 5)
            return true;

        return false;
    }
}

