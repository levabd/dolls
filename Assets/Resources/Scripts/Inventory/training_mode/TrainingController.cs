using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class TrainingController : MonoBehaviour
{
    [Header("Генерований лист")]
    public List<string> listItems;
    [Header("Активація")]
    public bool action = true;
    [Header("Поточний крок")]
    public int step = 0;
    [Header("Контейнери")]
    public Transform MainDisplayTransform;
    public ItemMaskDisplay IMD;
    public ActionMaskDisplay AMD;
    [Header("Панель інформації")]
    public GameObject PanelInfo;
    public Text HeadInfo;
    public Text MessageInfo;
    public Image ImageInfo;
    public Button ButtonInfo;
    [Header("Ціль на тілі")]
    public GameObject BodyTargettingPoint;
    [Header("Маска для панелі дій голки")]
    public GameObject FingerСoveringMask;
    public GameObject NeedleRemovingMask;

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
        string buttonText = "ПОЧАТИ";
        if (step > 0 )
        {
            buttonText = "ДАЛІ";
        }
        ButtonInfo.GetComponentInChildren<Text>().text = buttonText;
    }

    void TrainingControl(int step)
    {
        switch (step)
        {
            case 0:
                InfoPanelCreate(true, "Ласкаво просимо в режим навчання", "Пройшовши даний сценарій Ви навчитися працювати з програмою. Познайомитеся з інтерфейсом. Освоїте навички роботи з інструментами в програмі.", "tr_1", true);
                StartCoroutine(ItemMaskCreate(0, 8));
                break;
            case 1:
                InfoPanelCreate(true, "Крок 1. Панель інструментів", "Ви бачите панель інструментів. Клацніть лівою кнопкою миші по інструменту, щоб активувати панель дій інструментів", "tr_2");
                StartCoroutine(ItemMaskCreate(1, 8));
                break;
            case 2:
                InfoPanelCreate(true, "Крок 2. Панель дій", "Ви бачите панель дій. Клацніть лівою кнопкою миші по дії зі списку, щоб зробити дію з інструментом", "tr_3");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(1, 0, 2));
                break;
            case 3:
                StartCoroutine(ActionMaskCreate(0, 0, 2));
                InfoPanelCreate(true, "Крок 3. Активний інструмент", "Ви можете побачити який інструмент Ви зараз використовуєте в середині верхньої частини екрану", "tr_4", true);
                break;
            case 4:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Вибираючи активні інструменти і дії, Ви зробите дезінфекцію.", "tr_5", true);
                StartCoroutine(ActionMaskCreate(0, 0, 2));
                StartCoroutine(ItemMaskCreate(0, 8));
                break;
            case 5:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Виберіть стерильні марлеві кульки", "tr_6");
                StartCoroutine(ActionMaskCreate(0, 0, 2));
                StartCoroutine(ItemMaskCreate(3, 8));
                break;
            case 6:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Промокніть стерильні марлеві кульки в 70% розчині спирту.", "tr_7");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(2, 0, 6));
                break;
            case 7:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Виберіть пінцет", "tr_8");
                StartCoroutine(ActionMaskCreate(0, 0, 6));
                StartCoroutine(ItemMaskCreate(4, 8));
                break;
            case 8:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Візьміть ним змочені марлеві кульки. Обратите внимание на иконку цели на кнопке действия. При выборе таких действий изменится курсор для позиционирования.", "tr_9");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(1, 0, 3));
                break;
            case 9:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Вкажіть місце дезінфекції на тілі", "tr_10");
                BodyTargettingPoint.SetActive(true);
                StartCoroutine(ActionMaskCreate(0, 0, 3));
                break;
            case 10:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Протріть раніше зазначену область", "tr_11");
                BodyTargettingPoint.SetActive(false);
                StartCoroutine(ActionMaskCreate(3, 0, 3));
                break;
            case 11:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Вітаємо, Ви успішно провели дезінфекцію спиртом", "tr_5", true);
                StartCoroutine(ActionMaskCreate(0, 0, 3));
                break;
            case 12:
                InfoPanelCreate(true, "Крок 5. Анестезія", "Вибираючи активні інструменти і дії, Ви зробите анестезію.", "tr_12", true);
                break;
            case 13:
                InfoPanelCreate(true, "Крок 5. Анестезія", "Виберіть шприц без голки", "tr_13");
                StartCoroutine(ItemMaskCreate(2, 8));
                break;
            case 14:
                InfoPanelCreate(true, "Крок 5. Анестезія", "Візьміть голку для анестезії і наповніть шприц анестетиком", "tr_14");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(4, 4, 10));
                break;
            case 15:
                InfoPanelCreate(true, "Крок 5. Анестезія", "Зробіть місцеву анестезію", "tr_15");
                StartCoroutine(ActionMaskCreate(2, 4, 10));
                break;
            case 16:
                InfoPanelCreate(true, "Крок 5. Анестезія", "Вітаємо, Ви успішно провели анестезію", "tr_12", true);
                StartCoroutine(ActionMaskCreate(0, 4, 10));
                break;
            case 17:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Ви ознайомитеся з порядком дій установки катетера", "tr_16", true);
                StartCoroutine(ActionMaskCreate(0, 4, 10));
                break;
            case 18:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Від'єднайте шприц від голки", "tr_17");
                StartCoroutine(ActionMaskCreate(1, 4, 10));
                break;
            case 19:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Візьміть голку для пункції вени довжиною 10 см, внутрішнім просвітом каналу 1,7 мм і зрізом вістря голки під кутом 45 °", "tr_18");
                StartCoroutine(ActionMaskCreate(6, 4, 10));
                break;
            case 20:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Наповніть шприц наполовину 0,25% розчином новокаїну", "tr_19");
                StartCoroutine(ActionMaskCreate(8, 4, 10));
                break;
            case 21:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Вкажіть місце уколу", "tr_20");
                BodyTargettingPoint.SetActive(true);
                StartCoroutine(ActionMaskCreate(0, 4, 10));
                break;
            case 22:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "З'явився шприц. У правому нижньому кутку Ви бачите підказку, по управлінню шприцом", "tr_21", true);
                BodyTargettingPoint.SetActive(false);
                break;
            case 23:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Встановіть кут нахилу шприца використовуючи клавіші з підказки на 36 °. Если угол не изменяется, значит Вы уже начали вводить шприц. Выполните движение шприца назад, а затем меняйте угол", "tr_21", true);                
                break;
            case 24:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Перед тим як почнете вводити шприц в тіло, активуйте потягування поршня шприца", "tr_22");
                StartCoroutine(ActionMaskCreate(3, 4, 10));
                break;
            case 25:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Вводьте шприц з голкою в тіло до появи в ньому крові, використовуючи клавіші з підказки", "tr_23", true);
                StartCoroutine(ActionMaskCreate(0, 4, 10));
                break;
            case 26:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Від'єднайте шприц від голки і менше ніж за 5 секунд натисніть 'Прикрити голку пальцем' на панелі, що з'явилася", "tr_24");
                StartCoroutine(ActionMaskCreate(1, 4, 10));
                break;
            case 27:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Прикрийте голку пальцем, у Вас є 5 секунд", "tr_25");
                StartCoroutine(ActionMaskCreate(0, 4, 10));
                NeedleRemovingMask.SetActive(true);                
                break;
            case 28:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Ви впоралися. При прикритті голки після закінчення 5 секунд є ризик виникнення повітряної емболії. Будьте уважні.", "tr_25", true);
                FingerСoveringMask.SetActive(true);
                break;
            case 29:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Необхідно вставити провідник в голку. Активуйте провідник", "tr_24");                
                StartCoroutine(ItemMaskCreate(6, 8));
                FingerСoveringMask.SetActive(true);
                break;
            case 30:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Вставте провідник", "tr_24");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(1, 0, 2));
                break;
            case 31:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Видаліть голку через провідник", "tr_24");
                StartCoroutine(ActionMaskCreate(0, 0, 2));
                NeedleRemovingMask.SetActive(false);
                break;
            case 32:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Голка успішно видалена", "tr_24", true);
                
                break;
            case 33:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Катетер с канюлей и заглушкой", "tr_24");
                StartCoroutine(ItemMaskCreate(7, 8));
                break;
            case 34:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Вставить по проводнику", "tr_24");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(1, 0, 5));
                break;
            case 35:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Ушлубить вразательными двиениями", "tr_24");
                StartCoroutine(ActionMaskCreate(4, 0, 5));
                break;
            case 36:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Активировать проводник", "tr_24");
                StartCoroutine(ActionMaskCreate(0, 0, 5));
                StartCoroutine(ItemMaskCreate(6, 8));
                break;
            case 37:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Удалить проводник", "tr_24");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(2, 0, 2));                
                break;
            case 38:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Продолжаем действия с катетером", "tr_24");
                StartCoroutine(ItemMaskCreate(7, 8));
                StartCoroutine(ActionMaskCreate(0, 0, 2));
                break;
            case 39:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Соединяем катетер с системой переливания жидкостей", "tr_24");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(3, 0, 5));
                break;
            case 40:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Нам необходимо закрепить катетер на теле, берем пластырь", "tr_24");
                StartCoroutine(ItemMaskCreate(8, 8));
                StartCoroutine(ActionMaskCreate(0, 0, 5));
                break;
            case 41:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Активируем позиционирование пластыря", "tr_24");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(1, 0, 1));
                break;
            case 42:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Активируем позиционирование пластыря", "tr_24");
                StartCoroutine(ActionMaskCreate(0, 0, 1));
                break;
            case 43:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Поздавляем Вы справились", "tr_24", true);
                
                break;
            case 44:
                InfoPanelCreate(true, "Крок 7. Рука для дополнительных действий", "В рамках обучающего сценария нет возможности провести демонстрацию руки для дополнительных действий. Но вы можете посмотреть список действий", "tr_24", true);

                break;
            case 45:
                InfoPanelCreate(true, "Крок 7. Рука для дополнительных действий", "Активируйте руку для дополнительных действий", "tr_24", true);
                StartCoroutine(ItemMaskCreate(5, 8));
                break;
            case 46:
                InfoPanelCreate(true, "Крок 7. Рука для дополнительных действий", "Вы можете ознакомиться с перечнем. Демонстрация доступна в действующих сценариях", "tr_24", true);
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(0, 0, 4));
                break;
            case 47:
                InfoPanelCreate(true, "Крок 8. История действий", "На панели в правом нижнем углу экрана Вы можете ознакомиться с историей ваших действий в течении всего экзамена", "tr_24", true);
                break;
            case 48:
                InfoPanelCreate(true, "Крок 9. Инструкция к сценарию", "В левом верхнем углу экрана есть кнопка с вопросительным знаком. Нажав ее Вы сможете ознакомиться с подробной работой в сценарии", "tr_24", true);
                break;
            case 49:
                InfoPanelCreate(true, "Крок 10. Завершение сценария", "Поздравляем. Ваше обучение завершено. В правом верхнем углу экрана есть кнопка Завершити сцену. Нажав ее Вы закончите сценарий и сможете ознакомиться с результатом", "tr_24");
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
