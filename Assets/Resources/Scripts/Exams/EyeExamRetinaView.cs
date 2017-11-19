using System.Collections.Generic;
using DB.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
class EyeExamRetinaView : MonoBehaviour
{
    public Button NextButton;
    public Button PrevButton;
    public Button FinishButton;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    public GameObject Step1;
    public GameObject Step2;
    public GameObject Step3;
    public GameObject Step4;

    public Toggle Symptom1;
    public Toggle Symptom2;
    public Toggle Symptom3;
    public Toggle Symptom4;
    public Toggle Symptom5;
    public Toggle Symptom6;
    public Toggle Symptom7;
    public Toggle Symptom8;
    public Toggle Symptom9;
    public Toggle Symptom10;
    public Toggle Symptom11;
    public Toggle Symptom12;
    public Toggle Symptom13;
    public Toggle Symptom14;
    public Toggle Symptom15;

    public Toggle Diagnosys1;
    public Toggle Diagnosys2;
    public Toggle Diagnosys3;
    public Toggle Diagnosys4;
    public Toggle Diagnosys5;
    public Toggle Diagnosys6;
    public Toggle Diagnosys7;
    public Toggle Diagnosys8;
    public Toggle Diagnosys9;
    public Toggle Diagnosys10;
    public Toggle Diagnosys11;
    public Toggle Diagnosys12;

    public Toggle Result1;
    public Toggle Result2;
    public Toggle Result3;
    public Toggle Result4;
    public Toggle Result5;
    public Toggle Result6;
    public Toggle Result7;
    public Toggle Result8;
    public Toggle Result9;
    public Toggle Result10;

    public Toggle Treatment1;
    public Toggle Treatment2;
    public Toggle Treatment3;
    public Toggle Treatment4;
    public Toggle Treatment5;
    public Toggle Treatment6;
    public Toggle Treatment7;
    public Toggle Treatment8;
    public Toggle Treatment9;
    public Toggle Treatment10;

    private List<GameObject> _steps;

    private int _currentStep;
    private bool _finished;

    // Unused Unity Methods
    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    void Update() { }

    // Initialisation
    // ReSharper disable once UnusedMember.Local
    void Start()
    {
        Button btn = NextButton.GetComponent<Button>();
        btn.onClick.AddListener(NextStepEvent);

        btn = PrevButton.GetComponent<Button>();
        btn.onClick.AddListener(PrevStepEvent);

        btn = FinishButton.GetComponent<Button>();
        btn.onClick.AddListener(FinishEvent);

        btn = DialogButton.GetComponent<Button>();
        btn.onClick.AddListener(CloseModal);

        _steps = new List<GameObject>
        {
            Step1,
            Step2,
            Step3,
            Step4
        };
    }

    void NextStepEvent()
    {
        if (_currentStep < 3)
        {
            _steps[_currentStep].SetActive(false);
            _currentStep++;
            _steps[_currentStep].SetActive(true);
        }
    }

    void PrevStepEvent()
    {
        if (_currentStep > 0)
        {
            _steps[_currentStep].SetActive(false);
            _currentStep--;
            _steps[_currentStep].SetActive(true);
        }
    }

    void FinishEvent()
    {
        if (_currentStep != 3)
            GeneralSceneHelper.ShowMessage("Спочатку пройдіть всі 4 кроки", Dialog, DialogText);
        else
        {
            bool examResult = CheckExam();
            _finished = true;
            Exam exam = new Exam(CurrentUser.User, "Відшарування сітківки", examResult ? "" : "Тест не пройдено, перевірте свої відповіді на кожному кроці", examResult);
            exam.Save();
            CurrentAdminExam.Exam = exam;
            GeneralSceneHelper.ShowMessage(examResult ? "Вітаємо з успішним проходженням" : "Тест не пройдено, перевірте свої відповіді на кожному кроці",
                Dialog, DialogText);
        }
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
        if (Symptom1.isOn && Symptom2.isOn && Symptom3.isOn && Symptom4.isOn && Symptom5.isOn && Symptom6.isOn && Symptom7.isOn && Symptom8.isOn &&
            Diagnosys1.isOn && Diagnosys2.isOn && Diagnosys3.isOn && Diagnosys4.isOn && Diagnosys5.isOn && Diagnosys6.isOn &&
            Result1.isOn && Result2.isOn && Result3.isOn && Result4.isOn && Result5.isOn && Result6.isOn && Result7.isOn &&
            Treatment1.isOn && Treatment2.isOn && Treatment3.isOn && Treatment4.isOn)
            return true;

        return false;
    }
}

