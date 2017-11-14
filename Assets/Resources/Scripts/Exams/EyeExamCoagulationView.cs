using System;
using DB.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
class EyeExamCoagulationView : MonoBehaviour
{
    public Button FinishButton;
    public Camera Сamera;

    public Image ImageCraters;
    public Image ImageCoagulation;

    public Texture2D CursorTexture;

    public Dropdown LaserWaveLength;
    public Dropdown LaserExposition;
    public InputField Diameter;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    private bool _finished;

    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, -Vector2.up);
        if (hit.collider == null)
            return;

        Debug.Log(hit.collider.tag);

        if (hit.collider.tag == "EyeMacula" || hit.collider.tag == "EyeNervus")
        {
            Finish(false, "Тест не пройдено, попадання в макулу або зоровий нерв");
            return;
        }
            
        if (hit.collider.tag == "EyeCraters")
        {
            if (!CheckLaser())
            {
                Finish(false, "Тест не пройдено, перепалення сітківки");
                return;
            }
                
            
        }
    }

    // Initialisation
    // ReSharper disable once UnusedMember.Local
    void Start()
    {
        Button btn = FinishButton.GetComponent<Button>();
        btn.onClick.AddListener(FinishEvent);

        btn = DialogButton.GetComponent<Button>();
        btn.onClick.AddListener(CloseModal);

        EventTrigger trigger = ImageCraters.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter,
            callback = new EventTrigger.TriggerEvent()
        };
        UnityEngine.Events.UnityAction<BaseEventData> call = ChangeCursorToSight;
        entry.callback.AddListener(call);
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit,
            callback = new EventTrigger.TriggerEvent()
        };
        call = ReturnDefaultCursor;
        entry.callback.AddListener(call);
        trigger.triggers.Add(entry);
    }

    public void ChangeCursorToSight(BaseEventData baseEvent)
    {
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void ReturnDefaultCursor(BaseEventData baseEvent)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void Finish(bool examResult, string errorMessage = "")
    {
        _finished = true;
        Exam exam = new Exam(CurrentUser.User, "Фотокоагуляція", examResult ? "" : errorMessage, examResult);
        exam.Save();
        CurrentAdminExam.Exam = exam;
        GeneralSceneHelper.ShowMessage(examResult ? "Вітаємо з успішним проходженням" : errorMessage,
            Dialog, DialogText);
    }

    void FinishEvent()
    {
        bool examResult = true;
        
    }

    void CloseModal()
    {
        if (_finished)
            SceneManager.LoadScene("Examining_menu_scene");
        else
            Dialog.SetActive(false);
    }

    private bool CheckLaser()
    {
        int diameter;
        if (Int32.TryParse(Diameter.text, out diameter))
            if (diameter.CheckRange(200, 600) && LaserWaveLength.value == 1 && LaserExposition.value == 2)
                return true;

        return false;
    }
}

