using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
class SkinExamView : MonoBehaviour
{
    public Button FinishButton;

    public Text LabelText;

    public Slider ColorSlider;

    public GameObject Skin1;
    public GameObject Skin2;
    public GameObject Skin3;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    private Renderer _rend1;
    private Renderer _rend2;
    private Renderer _rend3;

    // Unused Unity Methods
    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    void Update() { }

    // Initialisation
    // ReSharper disable once UnusedMember.Local
    void Start()
    {
        Button btn = FinishButton.GetComponent<Button>();
        btn.onClick.AddListener(FinishEvent);

        btn = DialogButton.GetComponent<Button>();
        btn.onClick.AddListener(CloseModal);

        Slider sld = ColorSlider.GetComponent<Slider>();
        sld.onValueChanged.AddListener(ColorSliderChange);

        _rend1 = Skin1.GetComponent<Renderer>();

        _rend2 = Skin2.GetComponent<Renderer>();

        _rend3 = Skin3.GetComponent<Renderer>();
    }

    private void ColorSliderChange(float arg0)
    {
        LabelText.text = "Рівень сатурації: " + arg0 + "%";
        _rend1.material.SetColor("_Color", new Color(234 / 255f, 192 / 255f, (134 - arg0) / 255f, 1));
        _rend2.material.SetColor("_Color", new Color(234 / 255f, 192 / 255f, (134 - arg0) / 255f, 1));
        _rend3.material.SetColor("_Color", new Color(234 / 255f, 192 / 255f, (134 - arg0) / 255f, 1));
    }

    public void ReturnDefaultCursor(BaseEventData baseEvent)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void FinishEvent()
    {
        _rend1.material.SetColor("_Color", new Color(234 / 255f, 192 / 255f, 134 / 255f, 1));
        _rend2.material.SetColor("_Color", new Color(234 / 255f, 192 / 255f, 134 / 255f, 1));
        _rend3.material.SetColor("_Color", new Color(234 / 255f, 192 / 255f, 134 / 255f, 1));
        SceneManager.LoadScene("ExamManager_scene");
    }

    void CloseModal()
    {
        Dialog.SetActive(false);
    }
}

