using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemAction : PlayerAction
{
    public override void Execute(GameObject playerObject)
    {
        // Получить компонент IInventoryService установленный на объекте Player
        IInventoryService inventoryService = playerObject.GetComponent<IInventoryService>();

        if (inventoryService != null)
        {
            // Попытаться подобрать ближайший предмет
            Item item = inventoryService.PickUpNearestItem(playerObject.transform.position);
            if (item != null)
            {

                // Использовать предмет
                inventoryService.UseItem(item);
                Debug.Log("Игрок использовал предмет");
                item.gameObject.SetActive(false);
                
            }
        }
        else
        {
            Debug.LogError("Компонент IInventoryService не найден на объекте Player");
        }
    }
}
