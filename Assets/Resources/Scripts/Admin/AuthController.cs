using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public class AuthController : MonoBehaviour {

    public Button CloseButton;
    public Button EnterButton;

    public Toggle SaveToggle;

    public InputField LoginInput;
    public InputField PasswordInput;

    // Unused Unity Methods
    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    void Update() {  }


    // Initialisation
    // ReSharper disable once UnusedMember.Local
    void Start()
    {
        // Event listeners
        Button btn = CloseButton.GetComponent<Button>();
        btn.onClick.AddListener(CloseApp);

        btn = EnterButton.GetComponent<Button>();
        btn.onClick.AddListener(EnterApp);
    }

    void CloseApp()
    {
        Application.Quit();
    }

    void EnterApp()
    {
        Debug.Log("You have clicked the Enter button!");
    }
}
