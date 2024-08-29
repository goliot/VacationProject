using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumable,
    Etc,
}

[System.Serializable]
public class Item : MonoBehaviour
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public ItemEffect itemEffect;  // ���⿡ ScriptableObject�� �Ҵ�

    // �������� ����ϴ� �޼���
    public void UseItem()
    {
        if (itemEffect != null)
        {
            itemEffect.ExecuteRole(); // ������ ȿ�� ����
            GameManager.Instance.player.GetComponent<PlayerItems>().RemoveItem(this);
        }
    }

    public bool Use()
    {
        if (itemType != ItemType.Consumable)
            return false;
        else
        {
            return true;
        }
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
}

