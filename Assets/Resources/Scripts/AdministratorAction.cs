using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


[Serializable]
public class AdministratorAction : MonoBehaviour {
	
	public GameObject HeaderPanel;

	public GameObject UserListPanel;
		public Text ViewNumText;
		public Text ViewUsernameText;
		public Text ViewLoginText;
		public Dropdown ViewRoleDropdown;

	public GameObject CreateUserAccountPanel;
		public Input AddUserNameInput;
		public Input AddLoginInput;
		public Dropdown AddRoleDropdown;
		public Input AddPasswordInput;

	public GameObject EditUserAccountPanel;
		public Input EditUserNameInput;
		public Input EditLoginInput;
		public Dropdown EditRoleDropdown;

	public GameObject DeleteUserAccountPanel;
	public GameObject ApplyDeleteUserAccountPanel;
	public GameObject ChangeUserPasswordAccountPanel;
		public Input UserNameInput;
		public Input LoginInput;
		public Dropdown RoleDropdown;
		public Input NewPasswordInput;

	// Use this for initialization
	void Start () {
		HeaderPanel.SetActive (true);
		UserListPanel.SetActive (true);
		EditUserAccountPanel.SetActive (false);
		DeleteUserAccountPanel.SetActive (false);
		ApplyDeleteUserAccountPanel.SetActive (false);
		ChangeUserPasswordAccountPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

[Serializable]
public class AdministratorPageProperties  {
	
}