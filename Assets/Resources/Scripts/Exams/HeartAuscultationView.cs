using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
class HeartAuscultationView : MonoBehaviour
{
    public Button FinishButton;
    public Camera Сamera;

    public Texture2D CursorTexture;

    public GameObject StatusDisplay;

    public GameObject Equalizer;

    public Dropdown Condition;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    public AudioSource NormHeart;
    public AudioSource TricuspidValve;
    public AudioSource Aorta;
    public AudioSource PulmonaryTrunk;

    public AudioSource MitralStenosis;
    public AudioSource MitralValve;
    public AudioSource AorticStenosis;
    public AudioSource AorticValve;

    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;
            RaycastHit hit;

        Ray ray = Сamera.ScreenPointToRay(Input.mousePosition);
        bool hitted = Physics.Raycast(ray, out hit);
        if (!hitted)
            return;

        Transform objectHit = hit.transform;

        Debug.Log(objectHit.tag);

        MuteAllSound();

        if (Condition.value == 0) // Normal
        {
            if (objectHit.tag == "PinkAuscultation" || objectHit.tag == "PurpleAuscultation")
            {
                AudioSource heartAudio = NormHeart.GetComponent<AudioSource>();
                heartAudio.Play();
            }
            if (objectHit.tag == "GreenAuscultation")
            {
                AudioSource heartAudio = TricuspidValve.GetComponent<AudioSource>();
                heartAudio.Play();
            }
            if (objectHit.tag == "RedAuscultation")
            {
                AudioSource heartAudio = Aorta.GetComponent<AudioSource>();
                heartAudio.Play();
            }
            if (objectHit.tag == "BlueAuscultation" || objectHit.tag == "YellowAuscultation")
            {
                AudioSource heartAudio = PulmonaryTrunk.GetComponent<AudioSource>();
                heartAudio.Play();
            }
        }

        if (Condition.value == 1) // Мітральний стеноз
        {
            if (objectHit.tag == "PinkAuscultation" || objectHit.tag == "PurpleAuscultation")
            {
                AudioSource heartAudio = MitralStenosis.GetComponent<AudioSource>();
                heartAudio.Play();
            }
        }

        if (Condition.value == 2) // Недостатність мітрального клапана
        {
            if (objectHit.tag == "PinkAuscultation" || objectHit.tag == "PurpleAuscultation")
            {
                AudioSource heartAudio = MitralValve.GetComponent<AudioSource>();
                heartAudio.Play();
            }
        }

        if (Condition.value == 3) // Стеноз вістя аорти
        {
            if (objectHit.tag == "RedAuscultation")
            {
                AudioSource heartAudio = AorticStenosis.GetComponent<AudioSource>();
                heartAudio.Play();
            }
        }

        if (Condition.value == 4) // Шум на аортальному клапані
        {
            if (objectHit.tag == "RedAuscultation")
            {
                AudioSource heartAudio = AorticValve.GetComponent<AudioSource>();
                heartAudio.Play();
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
        SceneManager.LoadScene("ExamList");
    }

    void CloseModal()
    {
        Dialog.SetActive(false);
        //SceneManager.LoadScene("StepList");
    }

    private void MuteAllSound()
    {
        AudioSource heartAudio = NormHeart.GetComponent<AudioSource>();
        heartAudio.Stop();

        heartAudio = TricuspidValve.GetComponent<AudioSource>();
        heartAudio.Stop();

        heartAudio = Aorta.GetComponent<AudioSource>();
        heartAudio.Stop();

        heartAudio = PulmonaryTrunk.GetComponent<AudioSource>();
        heartAudio.Stop();

        heartAudio = MitralStenosis.GetComponent<AudioSource>();
        heartAudio.Stop();

        heartAudio = MitralValve.GetComponent<AudioSource>();
        heartAudio.Stop();

        heartAudio = AorticStenosis.GetComponent<AudioSource>();
        heartAudio.Stop();

        heartAudio = AorticValve.GetComponent<AudioSource>();
        heartAudio.Stop();
    }
}

