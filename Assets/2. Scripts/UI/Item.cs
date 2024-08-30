using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    Glove,
    Ring,
    Shoe,
    Helmet,
    Consumable,
    Etc,
}

[System.Serializable]
public class Item : MonoBehaviour
{
    public ItemType itemType;
    public bool isEquipped = false;
    public string itemName;
    public Sprite itemImage;
    public ItemEffect itemEffect;  // 여기에 ScriptableObject를 할당

    // 아이템을 사용하는 메서드
    public void UseItem()
    {
        PlayerItems playerItems = GameManager.Instance.player.GetComponent<PlayerItems>();

        if (itemType == ItemType.Consumable)
        {
            if (itemEffect != null)
            {
                itemEffect.ExecuteRole(); // 아이템 효과 실행
                playerItems.RemoveItem(this);
            }
        }
        else if (IsInEquipmentRange(itemType))
        {
            isEquipped = true;
            playerItems.EquipItem(itemType, this);
            playerItems.RemoveItem(this); //인벤토리에서 삭제
        }
    }

    // Consumable 아이템인지 확인하는 메서드
    public bool Use()
    {
        return itemType == ItemType.Consumable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerItems>().InsertItem(this);
            gameObject.SetActive(false);
        }
    }

    // Equals 메서드 재정의
    public override bool Equals(object obj)
    {
        if (obj is Item item)
        {
            return itemName == item.itemName && itemType == item.itemType;
        }
        return false;
    }

    // GetHashCode 메서드 재정의
    public override int GetHashCode()
    {
        return (itemName, itemType).GetHashCode();
    }

    // ItemType이 Equipment 범위에 있는지 확인하는 메서드
    public static bool IsInEquipmentRange(ItemType itemType)
    {
        return itemType switch
        {
            ItemType.Weapon or ItemType.Armor or ItemType.Glove or ItemType.Ring or ItemType.Shoe or ItemType.Helmet => true,
            _ => false
        };
    }
}
