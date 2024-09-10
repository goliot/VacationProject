using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScope : MonoBehaviour
{
    List<Item> ItemsInScope;

    public Item nearestItem;

    private void Awake()
    {
        ItemsInScope = new List<Item>();
    }

    private void Update()
    {
        if(ItemsInScope.Count == 0)
        {
            //player.LetterF.SetActive(false);
        }
        else
        {
            //player.LetterF.SetActive(true);

            if(Input.GetKeyDown(KeyCode.F))
            {
                Vector3 playerPos = gameObject.transform.position;

                foreach(Item item in ItemsInScope)
                {
                    if(Vector3.Distance(playerPos, nearestItem.transform.position) >
                        Vector3.Distance(playerPos, item.transform.position))
                    {
                        nearestItem.canPickUpThis = false;
                        nearestItem = item;
                        nearestItem.canPickUpThis = true;
                    }
                }
                nearestItem.PickUp(gameObject.GetComponent<PlayerItems>());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Item") return;

        Item item = other.GetComponent<Item>();
        if(item != null)
        {
            ItemsInScope.Add(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Item") return;
        Item item = other.GetComponent<Item>();
        if(item != null) 
            ItemsInScope.Remove(item);
    }
}
