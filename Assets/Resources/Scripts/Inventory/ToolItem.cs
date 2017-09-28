using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolItem : MonoBehaviour {
    [Header("Иконка предмета")]
    public Sprite sprite;
    [Header("Физический объект")]
    public string prefab;
    [Header("Текущее состояние обьекта")]
    public Dictionary<string, string> stateParams;
    [Header("Код инструмента")]
    public string codeName;
    [Header("Подпись инструмента(может быть изменена)")]
    [Multiline]
    public string title;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
