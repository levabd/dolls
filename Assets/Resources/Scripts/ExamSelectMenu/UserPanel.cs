using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DB.Models;

// ReSharper disable once CheckNamespace
public class UserPanel : MonoBehaviour
{

    public Button LogoutButton;
    public Button Exit;
    public Button BackButton;

    public Text Name;
    
    // Unused Unity Methods
    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    void Update() { }

    // Initialisation
    // ReSharper disable once UnusedMember.Local
    void Start()
    {
        // Event listeners
        Button btn = Exit.GetComponent<Button>();
        btn.onClick.AddListener(CloseApp);

        btn = LogoutButton.GetComponent<Button>();
        btn.onClick.AddListener(Logout);

        btn = BackButton.GetComponent<Button>();
        btn.onClick.AddListener(OpenExamList);

        Name.text = CurrentUser.User.Name;       
    }
    
    void CloseApp()
    {
        GeneralSceneHelper.QuitGame();
    }

    void Logout()
    {
        SceneManager.LoadScene("Autorization_first_scene");
    }

    void OpenExamList()
    {
        SceneManager.LoadScene("ExamList");
    }
    
}

