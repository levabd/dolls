using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DB.Models;

// ReSharper disable once CheckNamespace
public class AuthView : MonoBehaviour {

    public Button CloseButton;
    public Button EnterButton;

    public Toggle SaveToggle;

    public InputField LoginInput;
    public InputField PasswordInput;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    private readonly Perferences _perferences = new Perferences();

    private void TrySaveLogin()
    {
        if (SaveToggle.isOn)
        {
            _perferences.CurrentLogin = LoginInput.text;
            _perferences.CurrentPasswordHash = PasswordInput.text;
            _perferences.SaveLogin();
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
        Button btn = CloseButton.GetComponent<Button>();
        btn.onClick.AddListener(CloseApp);

        btn = EnterButton.GetComponent<Button>();
        btn.onClick.AddListener(EnterApp);

        btn = DialogButton.GetComponent<Button>();
        btn.onClick.AddListener(CloseModal);

        if (_perferences.LoadLogin())
        {
            LoginInput.text = _perferences.CurrentLogin;
            PasswordInput.text = _perferences.CurrentPasswordHash;
        }
    }

    void CloseApp()
    {
        TrySaveLogin();
        GeneralSceneHelper.QuitGame();
    }

    void CloseModal()
    {
        Dialog.SetActive(false);
    }

    void EnterApp()
    {
        try
        {
            User currentUser = User.FindByLogin(LoginInput.text);
            if (!currentUser.CheckPassword(PasswordInput.text))
                GeneralSceneHelper.ShowMessage("Хибний пароль", Dialog, DialogText);
            else
            {
                CurrentUser.User = currentUser;
                TrySaveLogin();
                switch (currentUser.Role)
                {
                    case User.UserRoles.Admin: SceneManager.LoadScene("Administrator_menu_scene"); break;
                    case User.UserRoles.Manager: SceneManager.LoadScene("ExamManager_scene"); break;
                    case User.UserRoles.User: SceneManager.LoadScene("ExamManager_scene"); break;
                    default: SceneManager.LoadScene("ExamManager_scene"); break;
                }
            }
        }
        catch (ArgumentException ex)
        {
            if (ex.ParamName == "login")
                GeneralSceneHelper.ShowMessage("Такий логін не було знайдено", Dialog, DialogText);
        }
    }
}
