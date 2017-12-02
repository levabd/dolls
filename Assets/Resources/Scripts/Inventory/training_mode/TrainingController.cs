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
    public GameObject CatheterTargettingPoint;
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
                InfoPanelCreate(true, "Ласкаво просимо в режим навчання", "Пройшовши даний сценарій Ви навчитися працювати з програмою. Познайомитеся з інтерфейсом. Освоїте навички роботи з інструментами в програмі.", "0", true);
                StartCoroutine(ItemMaskCreate(0, 8));
                break;
            case 1:
                InfoPanelCreate(true, "Крок 1. Панель інструментів", "Ви бачите панель інструментів. Клацніть лівою кнопкою миші по інструменту, щоб активувати панель дій інструментів", "1");
                StartCoroutine(ItemMaskCreate(1, 8));
                break;
            case 2:
                InfoPanelCreate(true, "Крок 2. Панель дій", "Ви бачите панель дій. Клацніть лівою кнопкою миші по дії зі списку, щоб зробити дію з інструментом", "2");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(1, 0, 2));
                break;
            case 3:
                StartCoroutine(ActionMaskCreate(0, 0, 2));
                InfoPanelCreate(true, "Крок 3. Активний інструмент", "Ви можете побачити який інструмент Ви зараз використовуєте в середині верхньої частини екрану", "3", true);
                break;
            case 4:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Вибираючи активні інструменти і дії, Ви зробите дезінфекцію", "4", true);
                StartCoroutine(ActionMaskCreate(0, 0, 2));
                StartCoroutine(ItemMaskCreate(0, 8));
                break;
            case 5:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Виберіть стерильні марлеві кульки", "5");
                StartCoroutine(ActionMaskCreate(0, 0, 2));
                StartCoroutine(ItemMaskCreate(3, 8));
                break;
            case 6:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Промокніть стерильні марлеві кульки в 70% розчині спирту", "6");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(2, 0, 6));
                break;
            case 7:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Виберіть пінцет", "7");
                StartCoroutine(ActionMaskCreate(0, 0, 6));
                StartCoroutine(ItemMaskCreate(4, 8));
                break;
            case 8:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Візьміть ним змочені марлеві кульки. Зверніть увагу на іконку цілі на кнопці дії. При виборі таких дій зміниться курсор для позиціонування.", "8");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(1, 0, 3));
                break;
            case 9:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Вкажіть місце дезінфекції на тілі", "9");
                BodyTargettingPoint.SetActive(true);
                StartCoroutine(ActionMaskCreate(0, 0, 3));
                break;
            case 10:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Протріть раніше зазначену область", "10");
                BodyTargettingPoint.SetActive(false);
                StartCoroutine(ActionMaskCreate(3, 0, 3));
                break;
            case 11:
                InfoPanelCreate(true, "Крок 4. Дезінфекція", "Вітаємо, Ви успішно провели дезінфекцію спиртом", "11", true);
                StartCoroutine(ActionMaskCreate(0, 0, 3));
                break;
            case 12:
                InfoPanelCreate(true, "Крок 5. Анестезія", "Вибираючи активні інструменти і дії, Ви зробите анестезію.", "12", true);
                break;
            case 13:
                InfoPanelCreate(true, "Крок 5. Анестезія", "Виберіть шприц без голки", "13");
                StartCoroutine(ItemMaskCreate(2, 8));
                break;
            case 14:
                InfoPanelCreate(true, "Крок 5. Анестезія", "Візьміть голку для анестезії і наповніть шприц анестетиком", "14");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(4, 4, 10));
                break;
            case 15:
                InfoPanelCreate(true, "Крок 5. Анестезія", "Зробіть місцеву анестезію", "15");
                StartCoroutine(ActionMaskCreate(2, 4, 10));
                break;
            case 16:
                InfoPanelCreate(true, "Крок 5. Анестезія", "Вітаємо, Ви успішно провели анестезію", "16", true);
                StartCoroutine(ActionMaskCreate(0, 4, 10));
                break;
            case 17:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Ви ознайомитеся з порядком дій установки катетера", "17", true);
                StartCoroutine(ActionMaskCreate(0, 4, 10));
                break;
            case 18:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Від'єднайте шприц від голки", "18");
                StartCoroutine(ActionMaskCreate(1, 4, 10));
                break;
            case 19:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Візьміть голку для пункції вени довжиною 10 см, внутрішнім просвітом каналу 1,7 мм і зрізом вістря голки під кутом 45 °", "19");
                StartCoroutine(ActionMaskCreate(6, 4, 10));
                break;
            case 20:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Наповніть шприц наполовину 0,25% розчином новокаїну", "20");
                StartCoroutine(ActionMaskCreate(8, 4, 10));
                break;
            case 21:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Вкажіть місце уколу", "21");
                BodyTargettingPoint.SetActive(true);
                StartCoroutine(ActionMaskCreate(0, 4, 10));
                break;
            case 22:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "З'явився шприц. У правому нижньому кутку Ви бачите підказку, по управлінню шприцом", "22", true);
                BodyTargettingPoint.SetActive(false);
                break;
            case 23:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Встановіть кут нахилу шприца використовуючи клавіші з підказки на 36°. Якщо значення кута не змінюється, значить Ви вже почали вводити шприц. Виконайте рух шприца назад, а потім міняйте кут", "23", true);                
                break;
            case 24:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Перед тим як почнете вводити шприц в тіло, активуйте потягування поршня шприца", "24");
                StartCoroutine(ActionMaskCreate(3, 4, 10));
                break;
            case 25:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Вводьте шприц з голкою в тіло до появи в ньому крові, використовуючи клавіші з підказки", "25", true);
                StartCoroutine(ActionMaskCreate(0, 4, 10));
                break;
            case 26:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Від'єднайте шприц від голки і менше ніж за 5 секунд натисніть 'Прикрити голку пальцем' на панелі, що з'явилася", "26");
                StartCoroutine(ActionMaskCreate(1, 4, 10));
                break;
            case 27:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Прикрийте голку пальцем, у Вас є 5 секунд", "27");
                StartCoroutine(ActionMaskCreate(0, 4, 10));
                NeedleRemovingMask.SetActive(true);                
                break;
            case 28:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Ви впоралися. При прикритті голки після закінчення 5 секунд є ризик виникнення повітряної емболії. Будьте уважні.", "28", true);
                FingerСoveringMask.SetActive(true);
                break;
            case 29:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Необхідно вставити провідник в голку. Активуйте провідник", "29");                
                StartCoroutine(ItemMaskCreate(6, 8));
                FingerСoveringMask.SetActive(true);
                break;
            case 30:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Вставте провідник", "30");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(1, 0, 2));
                break;
            case 31:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Видаліть голку через провідник", "31");
                StartCoroutine(ActionMaskCreate(0, 0, 2));
                NeedleRemovingMask.SetActive(false);
                break;
            case 32:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Голка успішно видалена", "32", true);
                
                break;
            case 33:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Катетер з канюлею і заглушкой", "33");
                StartCoroutine(ItemMaskCreate(7, 8));
                break;
            case 34:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Вставити по провіднику", "34");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(1, 0, 5));
                break;
            case 35:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Поглибити обертальними рухами", "35");
                StartCoroutine(ActionMaskCreate(4, 0, 5));
                break;
            case 36:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Активувати провідник", "36");
                StartCoroutine(ActionMaskCreate(0, 0, 5));
                StartCoroutine(ItemMaskCreate(6, 8));
                break;
            case 37:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Видалити провідник", "37");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(2, 0, 2));                
                break;
            case 38:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Продовжуємо дії з катетером", "38");
                StartCoroutine(ItemMaskCreate(7, 8));
                StartCoroutine(ActionMaskCreate(0, 0, 2));
                break;
            case 39:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "З'єднуємо з системою переливання рідин", "39");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(3, 0, 5));
                break;
            case 40:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Нам потрібно зафіксувати катетер на тілі, беремо пластир", "40");
                StartCoroutine(ItemMaskCreate(8, 8));
                StartCoroutine(ActionMaskCreate(0, 0, 5));
                break;
            case 41:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Активуємо позиціонування пластира", "41");
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(1, 0, 1));
                break;
            case 42:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Фіксуємо катетер пластирем до шкіри", "42");
                StartCoroutine(ActionMaskCreate(0, 0, 1));
                CatheterTargettingPoint.SetActive(true);
                break;
            case 43:
                InfoPanelCreate(true, "Крок 6. Постановка катетера в підключичну вену", "Вітаємо, Ви впорались", "43", true);
                CatheterTargettingPoint.SetActive(false);
                break;
            case 44:
                InfoPanelCreate(true, "Крок 7. Рука для додаткових дій", "В рамках навчального сценарію немає можливості провести демонстрацію використання руки для додаткових дій. Але ви можете подивитися список можливих дій", "44", true);

                break;
            case 45:
                InfoPanelCreate(true, "Крок 7. Рука для додаткових дій", "Активуйте руку для додаткових дій", "45");
                StartCoroutine(ItemMaskCreate(5, 8));
                break;
            case 46:
                InfoPanelCreate(true, "Крок 7. Рука для додаткових дій", "Ви можете ознайомитися з переліком. Демонстрація доступна в діючих сценаріях", "46", true);
                StartCoroutine(ItemMaskCreate(0, 8));
                StartCoroutine(ActionMaskCreate(0, 0, 4));
                break;
            case 47:
                InfoPanelCreate(true, "Крок 8. Історія дій", "На панелі в правому нижньому кутку екрану Ви можете ознайомитися з історією ваших дій протягом всього іспиту", "47", true);
                break;
            case 48:
                InfoPanelCreate(true, "Крок 9. Інструкція до сценарію", "У лівому верхньому кутку екрану є кнопка зі знаком питання. Натиснувши її Ви зможете ознайомитися з послідовністю дій в сценарії", "48", true);
                break;
            case 49:
                InfoPanelCreate(true, "Крок 10. Завершення сценарію", "Вітаємо. Ваше навчання завершено. У правому верхньому куті екрану є кнопка Завершити сцену. Натиснувши її Ви закінчите сценарій і зможете ознайомитися з результатом", "49");
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
