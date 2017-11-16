using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
class LungsAuscultationView : MonoBehaviour
{
    public Button FinishButton;
    public Camera Сamera;

    public Texture2D CursorTexture;

    public GameObject StatusDisplay;

    public GameObject Body;

    public Button RotateBodyButton;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    public AudioSource VezycularBreathing;
    public AudioSource CavernousBreathing;
    public AudioSource BronchialBreathing;

    private bool _rotateBody;

    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    void Update()
    {
        if (Math.Abs(Body.transform.rotation.y + 1f) < 0.01f || Math.Abs(Body.transform.rotation.y - 1f) < 0.01f || Math.Abs(Body.transform.rotation.y) < 0.01f)
            _rotateBody = false;

        if (_rotateBody)
            Body.transform.RotateAround(Body.transform.position, Body.transform.up, 40f * Time.deltaTime);

        if (!Input.GetMouseButtonDown(0))
            return;

        RaycastHit hit;

        Ray ray = Сamera.ScreenPointToRay(Input.mousePosition);
        bool hitted = Physics.Raycast(ray, out hit);
        if (!hitted)
            return;

        Transform objectHit = hit.transform;

        MuteAllSound();

        if (objectHit.tag == "VezycularBreathing")
        {
            AudioSource heartAudio = VezycularBreathing.GetComponent<AudioSource>();
            heartAudio.Play();
        }

        if (objectHit.tag == "BronchialBreathing")
        {
            AudioSource heartAudio = BronchialBreathing.GetComponent<AudioSource>();
            heartAudio.Play();
        }

        if (objectHit.tag == "CavernousBreathing")
        {
            AudioSource heartAudio = CavernousBreathing.GetComponent<AudioSource>();
            heartAudio.Play();
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

        btn = RotateBodyButton.GetComponent<Button>();
        btn.onClick.AddListener(RotateBody);

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

    void RotateBody()
    {
        while(Math.Abs(Body.transform.rotation.y + 1f) < 0.01f || Math.Abs(Body.transform.rotation.y - 1f) < 0.01f || Math.Abs(Body.transform.rotation.y) < 0.01f)
            Body.transform.RotateAround(Body.transform.position, Body.transform.up, 40f * Time.deltaTime);

        _rotateBody = true;
    }

    void FinishEvent()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene("ExamManager_scene");
    }

    void CloseModal()
    {
        Dialog.SetActive(false);
        //SceneManager.LoadScene("Examining_menu_scene");
    }

    private void MuteAllSound()
    {
        AudioSource heartAudio = VezycularBreathing.GetComponent<AudioSource>();
        heartAudio.Stop();

        heartAudio = BronchialBreathing.GetComponent<AudioSource>();
        heartAudio.Stop();

        heartAudio = CavernousBreathing.GetComponent<AudioSource>();
        heartAudio.Stop();
    }
}