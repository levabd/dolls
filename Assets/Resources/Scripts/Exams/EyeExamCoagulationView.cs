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

    public Button EnableLaserButton;
    public Image RedLight;
    public Image BlueLight;

    public Texture2D CursorTexture;

    public Dropdown LaserWaveLength;
    public Dropdown LaserExposition;
    public InputField Diameter;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    private Texture2D _texture;

    private float _imageWidth;
    private float _imageHeight;

    private float _imageLeft;
    private float _imageBottom;

    private bool _finished;
    private bool _laserEnabled;

    private int _cratersCount;
    const int CratersRadius = 6;

    private bool CheckCrater(int cx, int cy)
    {
        for (int y = cy - (int)(CratersRadius * 1.8); y < cy + CratersRadius * 1.8; y++)
        {
            for (int x = cx - (int)(CratersRadius * 1.8); x < cx + CratersRadius * 1.8; x++)
                if (_texture.GetPixel(x, y) != Color.clear)
                    return false;
        }

        return true;
    }

    private void DrawCrater(int cx, int cy)
    {
        for (int x = 0; x <= CratersRadius; x++)
        {
            var d = (int)Mathf.Ceil(Mathf.Sqrt(CratersRadius * CratersRadius - x * x));
            int y;
            for (y = 0; y <= d; y++)
            {
                var px = cx + x;
                var nx = cx - x;
                var py = cy + y;
                var ny = cy - y;

                _texture.SetPixel(px, py, Color.white);
                _texture.SetPixel(nx, py, Color.white);

                _texture.SetPixel(px, ny, Color.white);
                _texture.SetPixel(nx, ny, Color.white);

            }
        }

        _texture.Apply();
    }

    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    void Update()
    {
        Vector3[] v = new Vector3[4];
        ImageCraters.rectTransform.GetWorldCorners(v);

        _imageWidth = v[2].x - v[0].x;
        _imageHeight = v[1].y - v[0].y;

        _imageLeft = v[0].x;
        _imageBottom = v[0].y;


        if (!Input.GetMouseButtonDown(0))
            return;

        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, -Vector2.up);

        if (hit.collider == null)
            return;

        if (!_laserEnabled)
            return;

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

            int cx = (int)((hit.point.x - _imageLeft + 17) * 800 / _imageWidth);
            int cy = (int)((hit.point.y - _imageBottom - 17) * 680 / _imageHeight);

            if (!CheckCrater(cx, cy))
            {
                Finish(false, "Тест не пройдено, відстань між спайками замаленька");
                return;
            }

            DrawCrater(cx, cy);
            _cratersCount++;
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

        btn = EnableLaserButton.GetComponent<Button>();
        btn.onClick.AddListener(EnableLaser);

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
        
        _texture = new Texture2D(800, 680);
        ImageCraters.GetComponent<Image>().material.mainTexture = _texture;

        for (int y = 0; y < _texture.height; y++)
        {
            for (int x = 0; x < _texture.width; x++)
            {
                Color color = Color.clear;
                _texture.SetPixel(x, y, color);
            }
        }

        _texture.Apply();
    }

    void EnableLaser()
    {
        _laserEnabled = true;
        RedLight.gameObject.SetActive(true);
        BlueLight.gameObject.SetActive(false);
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
        if (_cratersCount < 300)
            Finish(false, "Тест не пройдено, не оброблена вся площа сітківки ");

        Finish(true);
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

