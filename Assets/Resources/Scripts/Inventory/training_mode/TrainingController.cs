using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class TrainingController : MonoBehaviour
{
    [Header("Генерируемый лист")]
    public List<string> listItems;
    [Header("Активация")]
    public bool action = true;
    [Header("Текущий шаг")]
    public int step = 0;
    [Header("Контейнеры")]
    public Transform MainDisplayTransform;
    public ItemMaskDisplay IMD;
    public ActionMaskDisplay AMD;
    [Header("Панель информации")]
    public GameObject PanelInfo;
    public Text HeadInfo;
    public Text MessageInfo;
    public Image ImageInfo;
    public Button ButtonInfo;
    [Header("Цель на теле")]
    public GameObject BodyTargettingPoint;

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (action)
        {            
            TrainingControl(step);
            action = false;
        }
    }

    public void IsActive()
    {
        step = step + 1;
        action = true;
    }

    //генерирует маски для инструментов   
    IEnumerator ItemMaskCreate(int trueItemN, int listCount, float waitTime = .1f)
    {
        yield return new WaitForSeconds(waitTime);
        if (GameObject.Find("ItemMaskDisplay"))
        {
            Destroy(GameObject.Find("ItemMaskDisplay"));
        }
        MaskListGenerate("itemMask", trueItemN, 0, listCount);
        ItemMaskDisplay itemMaskDisplay = Instantiate(IMD);
        itemMaskDisplay.name = IMD.name;
        itemMaskDisplay.transform.SetParent(MainDisplayTransform, false);
        SetMask(listItems, itemMaskDisplay.gameObject);

    }
    //генерирует маски для действий

    IEnumerator ActionMaskCreate(int trueItemN,int separator, int listCount, float waitTime = .1f)
    {
        yield return new WaitForSeconds(waitTime);
        if (GameObject.Find("ActionMaskDisplay"))
        {
            Destroy(GameObject.Find("ActionMaskDisplay"));
        }
        MaskListGenerate("actionMask", trueItemN, separator, listCount);
        ActionMaskDisplay actionMaskDisplay = Instantiate(AMD);
        actionMaskDisplay.name = AMD.name;
        actionMaskDisplay.transform.SetParent(MainDisplayTransform, false);
        SetMask(listItems, actionMaskDisplay.gameObject);
    }


    void InfoPanelCreate(bool panel, string headText, string messageText, string spriteSourse, bool button = false)
    {
        PanelInfo.SetActive(panel);
        HeadInfo.text = headText;
        MessageInfo.text = messageText;
        ImageInfo.sprite = Resources.Load<Sprite>("Images/training/" + spriteSourse);
        ButtonInfo.gameObject.SetActive(button);
        string buttonText = "НАЧАТЬ";
        if (step > 0 )
        {
            buttonText = "ДАЛЕЕ";
        }
        ButtonInfo.GetComponentInChildren<Text>().text = buttonText;
    }

    void TrainingControl(int step)
    {
        switch (step)
        {
            case 0:
                InfoPanelCreate(true, "Добро пожаловать в режим обучения", "Пройдя данный курс Вы научитесь работать с программой.Познакомитесь с интерфейсом.Освоите навыки работы с инструментами в программе.", "tr_1", true);
                StartCoroutine(ItemMaskCreate(0, 9));
                break;
            case 1:
                InfoPanelCreate(true, "Шаг 1. Панель инструментов", "Вы видите панель инструментов. Щелкните левой кнопкой мыши по инструменту, чтобы активировать панель действий инструментов", "tr_2");
                StartCoroutine(ItemMaskCreate(1, 9));
                break;
            case 2:
                InfoPanelCreate(true, "Шаг 2. Панель действий", "Вы видите панель действий. Щелкните левой кнопкой мыши по действию из списка, чтобы совершить действие с инструментом", "tr_3");
                StartCoroutine(ItemMaskCreate(0, 9));
                StartCoroutine(ActionMaskCreate(1, 0, 2));
                break;
            case 3:
                InfoPanelCreate(true, "Шаг 3. Выбранный инструмент", "Вы можете увидеть активный инструмент в центре вверху экрана", "tr_4", true);
                break;
            case 4:
                InfoPanelCreate(true, "Шаг 4. Дезинфекция", "Выбирая активные инструменты и действия, Вы сделаете дещинфекцию", "tr_4", true);
                StartCoroutine(ActionMaskCreate(0, 0, 2));
                StartCoroutine(ItemMaskCreate(3, 9));
                break;
            case 5:
                StartCoroutine(ItemMaskCreate(0, 9));
                StartCoroutine(ActionMaskCreate(2, 0, 6));
                break;
            case 6:
                StartCoroutine(ActionMaskCreate(0, 0, 6));
                StartCoroutine(ItemMaskCreate(4, 9));
                break;
            case 7:
                StartCoroutine(ItemMaskCreate(0, 9));
                StartCoroutine(ActionMaskCreate(1, 3, 5));
                break;
            case 8:
                StartCoroutine(ActionMaskCreate(0, 3, 5));
                break;
            case 9:
                StartCoroutine(ActionMaskCreate(4, 3, 5));
                break;
            case 10:
                StartCoroutine(ActionMaskCreate(0, 3, 5));
                break;


            default:
                break;
        }
    } 

    //герерирует лист для маски
    void MaskListGenerate(string itemMask, int itemN = 0, int separator = 0, int listCount = 8)
    {
        listItems = new List<string>();
        for (int i = 0; i < (separator != 0 ? listCount - 2: listCount - 1); i++)
        {
            listItems.Add(itemMask);
        }
        if (itemN != 0)
        {
            if (itemMask.Contains("item"))
            {
                listItems.Insert(itemN - 1, "trueItem");
            }
            if (itemMask.Contains("action"))
            {
                listItems.Insert(itemN - 1, "trueAction");
            }
        }
        else
        {
            listItems.Add(itemMask);
        }       
        if (separator != 0)
        {
            listItems.Insert(separator-1, "Separator");
        }
    }
    

    void SetMask(List<string> items, GameObject gameObject) {
        foreach (var item in items)
        {
            if (item.Contains("item"))
            {
                gameObject.GetComponent<ItemMaskDisplay>().Prime("Item");
            }
            if (item.Contains("action"))
            {
                gameObject.GetComponent<ActionMaskDisplay>().Prime("Action");                
            }
            if (item.Contains("trueItem"))
            {
                gameObject.GetComponent<ItemMaskDisplay>().Prime("trueItem");
            }
            if (item.Contains("trueAction"))
            {
                gameObject.GetComponent<ActionMaskDisplay>().Prime("trueAction");
            }
            if (item.Contains("Separator"))
            {
                gameObject.GetComponent<ActionMaskDisplay>().Prime("Separator");
            }
        }
        
    }
    void DestroyChildren(GameObject go)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform tran in go.transform)
        {
            children.Add(tran.gameObject);
        }
        children.ForEach(child => GameObject.Destroy(child));
    }
}
