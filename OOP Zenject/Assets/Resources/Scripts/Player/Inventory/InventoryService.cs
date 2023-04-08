using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryService : MonoBehaviour,IInventoryService
{
    [SerializeField]private List<Item> items;

    public InventoryService()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log($"Добавлен предмет {item.name} в инвентарь.");
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(items.Count != 0)
            {
                RemoveItem(items[0]);
            }
            else
            {
                Debug.LogWarning("Больше нечего выбрасывать!");
            }
        }
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        Debug.Log($"Удален предмет {item.name} из инвентаря.");
    }

    public Item PickUpNearestItem(Vector3 playerPosition)
    {
        Item nearestItem = null;
        float nearestDistance = Mathf.Infinity;

        // Найти все объекты с тегом "Item" в сцене
        GameObject[] foundItems = GameObject.FindGameObjectsWithTag("Item");

        // Пройтись по всем найденным объектам и выбрать ближайший к игроку
        foreach (GameObject itemObject in foundItems)
        {
            // Получить компонент Item из объекта
            Item item = itemObject.GetComponent<Item>();

            if (item != null)
            {
                // Вычислить расстояние между игроком и предметом
                float distance = Vector3.Distance(playerPosition, itemObject.transform.position);

                // Если предмет ближе к игроку, чем предыдущий ближайший предмет, то выбрать его
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestItem = item;
                }
            }
        }
        items.Add(nearestItem);
        
        return nearestItem;
      
    }

    public void UseItem(Item item)
    {
        // Логика использования предмета
        // ...
        Debug.Log($"Использован предмет {item.name}.");
    }

    public List<Item> GetItems()
    {
        return items;
    }


}
