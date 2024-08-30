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
    public ItemEffect itemEffect;  // ���⿡ ScriptableObject�� �Ҵ�

    // �������� ����ϴ� �޼���
    public void UseItem()
    {
        PlayerItems playerItems = GameManager.Instance.player.GetComponent<PlayerItems>();

        if (itemType == ItemType.Consumable)
        {
            if (itemEffect != null)
            {
                itemEffect.ExecuteRole(); // ������ ȿ�� ����
                playerItems.RemoveItem(this);
            }
        }
        else if (IsInEquipmentRange(itemType))
        {
            isEquipped = true;
            playerItems.EquipItem(itemType, this);
            playerItems.RemoveItem(this); //�κ��丮���� ����
        }
    }

    // Consumable ���������� Ȯ���ϴ� �޼���
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

    // Equals �޼��� ������
    public override bool Equals(object obj)
    {
        if (obj is Item item)
        {
            return itemName == item.itemName && itemType == item.itemType;
        }
        return false;
    }

    // GetHashCode �޼��� ������
    public override int GetHashCode()
    {
        return (itemName, itemType).GetHashCode();
    }

    // ItemType�� Equipment ������ �ִ��� Ȯ���ϴ� �޼���
    public static bool IsInEquipmentRange(ItemType itemType)
    {
        return itemType switch
        {
            ItemType.Weapon or ItemType.Armor or ItemType.Glove or ItemType.Ring or ItemType.Shoe or ItemType.Helmet => true,
            _ => false
        };
    }
}
