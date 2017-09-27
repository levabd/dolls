using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolItem : MonoBehaviour {

    [Header("Иконка предмета")]
    public string sprite;
    [Header("Физический объект")]
    public string prefab;
    [Header("Текущее состояние обьекта")]
    public Dictionary<string, string> stateParams;
    [Header("Код инструмента")]
    public string codeName;
    [Header("Подпись инструмента(может быть изменена)")]
    public string title;
}
