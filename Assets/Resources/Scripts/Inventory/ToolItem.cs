﻿using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class ToolItem : MonoBehaviour {
    [Header("Иконка предмета")]
    public Sprite Sprite;
    [Header("Физический объект")]
    public string Prefab;
    [Header("Текущее состояние обьекта")]
    public Dictionary<string, string> StateParams;
    [Header("Код инструмента")]
    public string CodeName;
    [Header("Подпись инструмента(может быть изменена)")]
    [Multiline]
    public string Title;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
