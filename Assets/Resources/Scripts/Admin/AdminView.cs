using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DB.Models;
using SLS.Widgets.Table;
using Debug = UnityEngine.Debug;

// ReSharper disable once CheckNamespace
public class AdminView : MonoBehaviour {

    public Button NewUser;
    public Button LogoutButton;
    public Button Exit;

    public Text Name;

    public Image DataTable;
    public Sprite EditIcon;
    public Sprite ChangePasswdIcon;
    public Sprite DeleteIcon;
    private Table _dataTable;
    private List<User> _users;

    public GameObject Dialog;
    public Button DialogButton;
    public Text DialogText;

    public GameObject ApplyDeleteUserAccountPanel;
    public Button ApplyDeleteCancel;
    public Button ApplyDeleteUserButton;
    public Text ApplyDeleteUserName;

    public GameObject ChangeUserPasswordPanel;
    public Button ChangeUserPasswordCancel;
    public Button ChangePasswordButton;
    public InputField AddNewPasswordInputField;

    public GameObject EditUserAccountPanel;
    public Button EditUserCancel;
    public Button EditUserButton;
    public InputField EditUserNameInputField;
    public Dropdown EditUserRoleDropdown;

    public GameObject CreateUserAccountPanel;
    public Button CreateUserCancel;
    public Button CreateUserButton;
    public InputField AddUserNameInputField;
    public InputField AddLoginInputField;
    public InputField AddPasswordInputField;
    public InputField RepeatPasswordInputField;
    public Dropdown AddUserRoleDropdown;

    private int _currentTableIndex;

    private void ReloadData()
    {
        _dataTable.data.Clear();

        _users = User.FindAll();

        int index = 0;

        foreach (var user in _users)
        {
            Datum d = Datum.Body(index.ToString());
            d.elements.Add(user.Id ?? 0);
            d.elements.Add(user.Name);
            d.elements.Add(user.Login);
            switch (user.Role)
            {
                case User.UserRoles.Admin: d.elements.Add("Адміністратор"); break;
                case User.UserRoles.Manager: d.elements.Add("Перевіряючий"); break;
                case User.UserRoles.User: d.elements.Add("Користувач"); break;
                default: d.elements.Add("Користувач"); break;
            }

            d.elements.Add("edit");
            d.elements.Add("change_passwd");
            d.elements.Add("delete");

            _dataTable.data.Add(d);
            index++;
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
        Button btn = Exit.GetComponent<Button>();
        btn.onClick.AddListener(CloseApp);

        btn = LogoutButton.GetComponent<Button>();
        btn.onClick.AddListener(Logout);
        
        btn = NewUser.GetComponent<Button>();
        btn.onClick.AddListener(OpenCreateUser);

        btn = DialogButton.GetComponent<Button>();
        btn.onClick.AddListener(CloseModal);

        btn = ApplyDeleteCancel.GetComponent<Button>();
        btn.onClick.AddListener(CloseDeleteUserAccount);

        btn = ChangeUserPasswordCancel.GetComponent<Button>();
        btn.onClick.AddListener(CloseChangeUserPassword);

        btn = EditUserCancel.GetComponent<Button>();
        btn.onClick.AddListener(CloseEditUserAccount);

        btn = CreateUserCancel.GetComponent<Button>();
        btn.onClick.AddListener(CloseUserAccount);

        btn = EditUserButton.GetComponent<Button>();
        btn.onClick.AddListener(EditUser);

        btn = ChangePasswordButton.GetComponent<Button>();
        btn.onClick.AddListener(ChangePassword);

        btn = CreateUserButton.GetComponent<Button>();
        btn.onClick.AddListener(CreateUser);

        btn = ApplyDeleteUserButton.GetComponent<Button>();
        btn.onClick.AddListener(DeleteUser);

        Name.text = CurrentUser.User.Name;

        // Initialize Table
        _dataTable = DataTable.GetComponent<Table>();
        _dataTable.ResetTable();
        _dataTable.AddTextColumn("№", null, 20f, 20f);
        _dataTable.AddTextColumn("ПІБ", null, 300f, 300f);
        _dataTable.AddTextColumn("Логін", null, 100f, 100f);
        _dataTable.AddTextColumn("Роль", null, 150f, 150f);
        _dataTable.AddImageColumn("Редагувати");
        _dataTable.AddImageColumn("Змінити пароль");
        _dataTable.AddImageColumn("Видалити");

        Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>
        {
            {"edit", EditIcon},
            {"change_passwd", ChangePasswdIcon},
            {"delete", DeleteIcon}
        };

        _dataTable.Initialize(OnTableSelectedWithCol, spriteDict);

        // Fill the data
        ReloadData();

        _dataTable.StartRenderEngine();
    }

    void CloseApp()
    {
        GeneralSceneHelper.QuitGame();
    }

    void CloseModal()
    {
        Dialog.SetActive(false);
    }

    void CloseUserAccount()
    {
        CreateUserAccountPanel.SetActive(false);
    }

    void CloseEditUserAccount()
    {
        EditUserAccountPanel.SetActive(false);
    }

    void CloseChangeUserPassword()
    {
        ChangeUserPasswordPanel.SetActive(false);
    }

    void CloseDeleteUserAccount()
    {
        ApplyDeleteUserAccountPanel.SetActive(false);
    }

    void Logout()
    {
        SceneManager.LoadScene("Autorization_first_scene");
    }

    private void OnTableSelectedWithCol(Datum datum, Column column)
    {
        if (datum == null) return;
        _currentTableIndex = Int32.Parse(datum.uid);
        if (column != null)
        {
            Debug.Log("You Clicked: " + datum.uid + " Column: " + column.idx);
            switch (column.idx)
            {
                case 4:
                {
                    EditUserNameInputField.text = _users[_currentTableIndex].Name;
                    EditUserRoleDropdown.value = (int)_users[_currentTableIndex].Role;
                    EditUserAccountPanel.SetActive(true);
                    break;
                }
                case 5:
                {
                    ChangeUserPasswordPanel.SetActive(true);
                    break;
                }
                case 6:
                {
                    ApplyDeleteUserName.text = _users[_currentTableIndex].Name;
                    ApplyDeleteUserAccountPanel.SetActive(true);
                    break;
                }
            }
        }
    }

    void OpenCreateUser()
    {
        CreateUserAccountPanel.SetActive(true);
    }

    void CreateUser()
    {
        if (AddPasswordInputField.text != RepeatPasswordInputField.text)
        {
            GeneralSceneHelper.ShowMessage("Паролі не співпадають.", Dialog, DialogText);
            CreateUserAccountPanel.SetActive(true);
            return;
        }

        try
        {
            User newUser = new User(AddLoginInputField.text, AddPasswordInputField.text, AddUserNameInputField.text,
                AddUserRoleDropdown.value);
            newUser.Save();

            CreateUserAccountPanel.SetActive(false);
            GeneralSceneHelper.ShowMessage("Користувача було успішно додано.", Dialog, DialogText);
            ReloadData();
        }
        catch (ArgumentException ex)
        {
            GeneralSceneHelper.ShowMessage(ex.Message, Dialog, DialogText);
            CreateUserAccountPanel.SetActive(true);
        }
        catch (Exception ex)
        {
            GeneralSceneHelper.ShowMessage("Загальна помилка. Не вдалося додати користувача.", Dialog, DialogText);
            CreateUserAccountPanel.SetActive(true);
            Debug.LogWarning("Oh Crap! " + ex.Message);
        }
    }

    void EditUser()
    {
        try
        {
            User currentUser = User.FindById(_users[_currentTableIndex].Id);
            currentUser.Role = (User.UserRoles)EditUserRoleDropdown.value;
            currentUser.Name = EditUserNameInputField.text;
            currentUser.Save();

            EditUserAccountPanel.SetActive(false);
            GeneralSceneHelper.ShowMessage("Користувача було успішно відредаговано.", Dialog, DialogText);
            ReloadData();
        }
        catch (ArgumentException ex)
        {
            GeneralSceneHelper.ShowMessage(ex.Message, Dialog, DialogText);
            EditUserAccountPanel.SetActive(true);
        }
        catch (Exception ex)
        {
            GeneralSceneHelper.ShowMessage("Загальна помилка. Не вдалося відредагувати користувача.", Dialog, DialogText);
            EditUserAccountPanel.SetActive(true);
            Debug.LogWarning("Oh Crap! " + ex.Message);
        }
    }

    void ChangePassword()
    {
        try
        {
            User.FindById(_users[_currentTableIndex].Id).Save();

            ChangeUserPasswordPanel.SetActive(false);
            GeneralSceneHelper.ShowMessage("Пароль користувача було успішно відредаговано.", Dialog, DialogText);
            ReloadData();
        }
        catch (ConstraintException ex)
        {
            GeneralSceneHelper.ShowMessage("Можна змінювати пароль тільки у існуючого користувача", Dialog, DialogText);
            ChangeUserPasswordPanel.SetActive(true);
            Debug.LogWarning("Oh Crap! " + ex.Message);
        }
        catch (Exception ex)
        {
            GeneralSceneHelper.ShowMessage("Загальна помилка. Не вдалося відредагувати пароль користувача.", Dialog, DialogText);
            ChangeUserPasswordPanel.SetActive(true);
            Debug.LogWarning("Oh Crap! " + ex.Message);
        }
    }

    void DeleteUser()
    {
        try
        {
            if (_users[_currentTableIndex].Role == 0)
            {
                GeneralSceneHelper.ShowMessage("Не дозволено видаляти користувачів з правами адміністратора", Dialog,
                    DialogText);
                return;
            }

            User.FindById(_users[_currentTableIndex].Id).Delete();

            ApplyDeleteUserAccountPanel.SetActive(false);
            GeneralSceneHelper.ShowMessage("Користувача було успішно видалено.", Dialog, DialogText);
            ReloadData();
        }
        catch (Exception ex)
        {
            GeneralSceneHelper.ShowMessage("Загальна помилка. Не вдалося видалити користувача.", Dialog, DialogText);
            Debug.LogWarning("Oh Crap! " + ex.Message);
        }
    }
}
