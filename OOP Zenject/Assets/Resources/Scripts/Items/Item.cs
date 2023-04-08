using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public int count;
    public Sprite icon;
    public GameObject itemobj;
}

public interface IInventoryService
{
    void AddItem(Item item);
    void RemoveItem(Item item);
    void UseItem(Item item);
    Item PickUpNearestItem(Vector3 playerPosition);

}
