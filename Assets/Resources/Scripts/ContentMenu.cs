using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contentmenu : MonoBehaviour
{
    [Header("Заполните список кнопок")]
    public List<Action> list;
    [Header("Укажите панель основную панель меню")]
    public GameObject inventory;
    public int colummnCount = 1;
    public Vector2 containerSize;
    [Header("Укажите дочерний контейнер для кнопки")]
    public GameObject cellspace;
    [Header("Укажите кнопку")]
    public GameObject container;


    // Use this for initialization
    void Start()
    {
        containerSize = new Vector2(100, 100);
    }

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.I))
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
                for (int i = 0; i < inventory.transform.childCount; i++)
                {
                    if (inventory.transform.GetChild(i).transform.childCount > 0)
                    {
                        Destroy(inventory.transform.GetChild(i).transform.GetChild(0).gameObject);
                    }
                }
            }
            else
            {
                inventory.SetActive(true);
                int cellCount = list.Count;
                int invChildCount = inventory.transform.childCount;

                inventory.GetComponent<GridLayoutGroup>().cellSize = containerSize;
                AddCell(cellCount, invChildCount);
                ItemToInventory(cellCount);
                RowControl(cellCount, colummnCount);
            }
        }
    }

    private void AddCell(int cellCount, int invChildCount)
    {
        for (int i = 0; i < cellCount; i++)
        {
            if (inventory.transform.childCount <= i)
            {
                GameObject cell = Instantiate(cellspace);

                cellspace.GetComponent<GridLayoutGroup>().cellSize = containerSize;
                cell.transform.SetParent(inventory.transform);
                cell.transform.localScale = inventory.transform.localScale;
            }
        }
    }
    private void ItemToInventory(int cellCount)
    {
        for (int i = 0; i < cellCount; i++)
        {
            Action it = list[i];
            ControlActivateMethod activMethod = new ControlActivateMethod();

            if (inventory.transform.childCount > i)
            {
                GameObject img = Instantiate(container);

                container.GetComponent<RectTransform>().sizeDelta = containerSize;

                img.transform.SetParent(inventory.transform.GetChild(i).transform);
                //img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.sprite);
                //img.transform.localScale = inventory.transform.GetChild(i).transform.localScale;
                //img.AddComponent<Button>().onClick.AddListener(() => activMethod.ControlItems(it.action));
            }
            else break;
        }
    }

    private void RowControl(int cellCount, int colummnCount)
    {
        int rowCount = (cellCount / colummnCount) + 1;
        float width = ((cellCount - 1) * 10) + (containerSize.x * colummnCount);
        float height = 20 + ((rowCount - 1) * 10) + (containerSize.y * rowCount);
        if ((cellCount % colummnCount) != 0)
        {
            for (int i = 0; i < (cellCount / colummnCount); i++)
            {
                inventory.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            }
        }
        else
        {
            height = 50 + (100 * (rowCount - 1));
            for (int i = 0; i < (cellCount / colummnCount); i++)
            {
                inventory.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            }
        }



    }


}
