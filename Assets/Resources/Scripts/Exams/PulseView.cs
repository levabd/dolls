using System.Collections.Generic;
using DB.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
class PulseView : MonoBehaviour
{
    public Button FinishButton;
    public Camera Сamera;

    public Texture2D CursorTexture;

    public GameObject StatusDisplay;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    public Text HeartText;

    public AudioSource Heart;

    private bool _playHeart;

    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Ray ray = Сamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                List<string> rightCollider = new List<string>
                {
                    "PoplitealPulse",
                    "PosteriorTibialPulse",
                    "DorsalisPedisPulse",
                    "FemoralPulse",
                    "UlnarPulse",
                    "RadialPulse",
                    "CubitalPulse",
                    "BrachialPulse",
                    "CarotidPulse",
                    "FacialPulse",
                    "PulseExamController",
                };

                foreach (var rightTag in rightCollider)
                {
                    if (objectHit.tag == rightTag)
                    {
                        if (!_playHeart)
                        {
                            AudioSource heartAudio = Heart.GetComponent<AudioSource>();
                            heartAudio.Play();
                            HeartText?.gameObject.SetActive(true);
                            _playHeart = true;
                        }
                        else
                        {
                            AudioSource heartAudio = Heart.GetComponent<AudioSource>();
                            heartAudio.Stop();
                            HeartText?.gameObject.SetActive(false);
                            _playHeart = false;
                        }
                    }
                }
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

        EventTrigger trigger = StatusDisplay.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter,
            callback = new EventTrigger.TriggerEvent()
        };
        UnityEngine.Events.UnityAction<BaseEventData> call = ChangeCursorToDefault;
        entry.callback.AddListener(call);
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit,
            callback = new EventTrigger.TriggerEvent()
        };
        call = SetCustomCursor;
        entry.callback.AddListener(call);
        trigger.triggers.Add(entry);

        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void SetCustomCursor(BaseEventData baseEvent)
    {
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void ChangeCursorToDefault(BaseEventData baseEvent)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void FinishEvent()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        new Exam(CurrentUser.User, "PulseExam", "Наявність пульсу на артеріях", "", true).Save();
        SceneManager.LoadScene("ExamList");
    }

    void CloseModal()
    {
        Dialog.SetActive(false);
        //SceneManager.LoadScene("StepList");
    }
}

