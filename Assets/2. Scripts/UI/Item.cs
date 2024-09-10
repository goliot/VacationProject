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
    public bool isEquipmentItem = false;

    public bool canPickUpThis = false;

    private void Awake()
    {
        isEquipmentItem = IsEquipment(itemType);
    }

    // �������� ����ϴ� �޼���
    public void UseItem()
    {
        PlayerItems playerItems = GameManager.Instance.player.GetComponent<PlayerItems>();

        if (itemType == ItemType.Consumable)
        {
            if (itemEffect != null)
            {
                itemEffect.ExecuteEffect(); // ������ ȿ�� ����
                playerItems.RemoveItem(this);
            }
        }
        else if (isEquipmentItem)
        {
            isEquipped = true;
            itemEffect.ExecuteEffect();
            playerItems.EquipItem(itemType, this);
            playerItems.RemoveItem(this); //�κ��丮���� ����
        }
    }

    public void UnEquipItem()
    {
        isEquipped = false;
        itemEffect.CancelEffect();
    }

    // Consumable ���������� Ȯ���ϴ� �޼���
    public bool Use()
    {
        return itemType == ItemType.Consumable;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canPickUpThis = true;


            other.gameObject.GetComponent<PlayerItems>().InsertItem(this);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canPickUpThis = false;
        }
    }*/

    public void PickUp(PlayerItems player)
    {
        if (canPickUpThis)
        {
            player.InsertItem(this);
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
    public static bool IsEquipment(ItemType itemType)
    {
        return itemType switch
        {
            ItemType.Weapon or ItemType.Armor or ItemType.Glove or ItemType.Ring or ItemType.Shoe or ItemType.Helmet => true,
            _ => false
        };
    }
}
