using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ConsumeItem2 : MonoBehaviour, IPointerDownHandler
{
    public Item item;
    private static Tooltip tooltip;
    public ItemType[] itemTypeOfSlot;
    public static EquipmentSystem eS2;
    public GameObject duplication;
    public static GameObject mainInventory2;

    void Start()
    {
        item = GetComponent<ItemOnObject>().item;
        if (GameObject.FindGameObjectWithTag("Tooltip") != null)
            tooltip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
        if (GameObject.FindGameObjectWithTag("EquipmentSystem2") != null)
			eS2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2>().characterSystem.GetComponent<EquipmentSystem>();

        if (GameObject.FindGameObjectWithTag("MainInventory2") != null)
            mainInventory2 = GameObject.FindGameObjectWithTag("MainInventory2");
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (this.gameObject.transform.parent.parent.parent.GetComponent<EquipmentSystem>() == null)
        {
            bool gearable = false;
            Inventory inventory = transform.parent.parent.parent.GetComponent<Inventory>();

            if (eS2 != null)
                itemTypeOfSlot = eS2.itemTypeOfSlots;

			if (data.button == PointerEventData.InputButton.Right)
            {
                //item from craft system to inventory
                if (transform.parent.GetComponent<CraftResultSlot>() != null)
                {
					bool check = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2>().inventory.GetComponent<Inventory>().checkIfItemAllreadyExist(item.itemID, item.itemValue);

                    if (!check)
                    {
                        GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2>().inventory.GetComponent<Inventory>().addItemToInventory(item.itemID, item.itemValue);
                        GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2>().inventory.GetComponent<Inventory>().stackableSettings();
                    }
                    CraftSystem cS = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2>().craftSystem.GetComponent<CraftSystem>();
                    cS.deleteItems(item);
                    CraftResultSlot result = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2>().craftSystem.transform.GetChild(3).GetComponent<CraftResultSlot>();
                    result.temp = 0;
                    tooltip.deactivateTooltip();
                    gearable = true;
                    GameObject.FindGameObjectWithTag("MainInventory2").GetComponent<Inventory>().updateItemList();
                }
                else
                {
                    bool stop = false;
                    if (eS2 != null)
                    {
                        for (int i = 0; i < eS2.slotsInTotal; i++)
                        {
                            if (itemTypeOfSlot[i].Equals(item.itemType))
                            {
                                if (eS2.transform.GetChild(1).GetChild(i).childCount == 0)
                                {
                                    stop = true;
                                    if (eS2.transform.GetChild(1).GetChild(i).parent.parent.GetComponent<EquipmentSystem>() != null && this.gameObject.transform.parent.parent.parent.GetComponent<EquipmentSystem>() != null) { }
                                    else                                    
                                        inventory.EquiptItem(item);
                                    
                                    this.gameObject.transform.SetParent(eS2.transform.GetChild(1).GetChild(i));
                                    this.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
                                    eS2.gameObject.GetComponent<Inventory>().updateItemList();
                                    inventory.updateItemList();
                                    gearable = true;
                                    if (duplication != null)
                                        Destroy(duplication.gameObject);
                                    break;
                                }
                            }
                        }


                        if (!stop)
                        {
                            for (int i = 0; i < eS2.slotsInTotal; i++)
                            {
                                if (itemTypeOfSlot[i].Equals(item.itemType))
                                {
                                    if (eS2.transform.GetChild(1).GetChild(i).childCount != 0)
                                    {
                                        GameObject otherItemFromCharacterSystem = eS2.transform.GetChild(1).GetChild(i).GetChild(0).gameObject;
                                        Item otherSlotItem = otherItemFromCharacterSystem.GetComponent<ItemOnObject>().item;
                                        if (item.itemType == ItemType.UFPS_Weapon)
                                        {
                                            inventory.UnEquipItem3(otherItemFromCharacterSystem.GetComponent<ItemOnObject>().item);
                                            inventory.EquiptItem2(item);
                                        }
                                        else
                                        {
                                            inventory.EquiptItem2(item);
                                            if (item.itemType != ItemType.Backpack)
                                                inventory.UnEquipItem3(otherItemFromCharacterSystem.GetComponent<ItemOnObject>().item);
                                        }
                                        if (this == null)
                                        {
                                            GameObject dropItem = (GameObject)Instantiate(otherSlotItem.itemModel);
                                            dropItem.AddComponent<PickUpItem>();
                                            dropItem.GetComponent<PickUpItem>().item = otherSlotItem;
                                            dropItem.transform.localPosition = GameObject.FindGameObjectWithTag("Player2").transform.localPosition;
                                            inventory.OnUpdateItemList();
                                        }
                                        else
                                        {
                                            otherItemFromCharacterSystem.transform.SetParent(this.transform.parent);
                                            otherItemFromCharacterSystem.GetComponent<RectTransform>().localPosition = Vector3.zero;
                                            if (this.gameObject.transform.parent.parent.parent.GetComponent<Hotbar>() != null)
                                                createDuplication(otherItemFromCharacterSystem);

                                            this.gameObject.transform.SetParent(eS2.transform.GetChild(1).GetChild(i));
                                            this.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
                                        }                                        
                                        
                                        gearable = true;                                        
                                        if (duplication != null)
                                            Destroy(duplication.gameObject);
                                        eS2.gameObject.GetComponent<Inventory>().updateItemList();
                                        inventory.OnUpdateItemList();
                                        break;
                                    }
                                }
                            }
                        }

                    }

                }
                if (!gearable && item.itemType != ItemType.UFPS_Ammo && item.itemType != ItemType.UFPS_Grenade)
                {

                    Item itemFromDup = null;
                    if (duplication != null)
                        itemFromDup = duplication.GetComponent<ItemOnObject>().item;

                    inventory.ConsumeItem2(item);

                    item.itemValue--;
                    if (itemFromDup != null)
                    {
                        duplication.GetComponent<ItemOnObject>().item.itemValue--;
                        if (itemFromDup.itemValue <= 0)
                        {
                            if (tooltip != null)
                                tooltip.deactivateTooltip();
                            inventory.deleteItemFromInventory(item);
                            Destroy(duplication.gameObject); 
                        }
                    }
                    if (item.itemValue <= 0)
                    {
                        if (tooltip != null)
                            tooltip.deactivateTooltip();
                        inventory.deleteItemFromInventory(item);
                        Destroy(this.gameObject);                        
                    }

                }
                
            }
            

        }
    }    

    public void consumeIt2()
    {
        Inventory inventory2 = transform.parent.parent.parent.GetComponent<Inventory>();

        bool gearable = false;
        if (GameObject.FindGameObjectWithTag("EquipmentSystem2") != null)
			eS2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2>().characterSystem.GetComponent<EquipmentSystem>();

        if (eS2 != null)
            itemTypeOfSlot = eS2.itemTypeOfSlots;

        Item itemFromDup = null;
        if (duplication != null)
            itemFromDup = duplication.GetComponent<ItemOnObject>().item;       

        bool stop = false;
        if (eS2 != null)
        {
            
            for (int i = 0; i < eS2.slotsInTotal; i++)
            {
                if (itemTypeOfSlot[i].Equals(item.itemType))
                {
                    if (eS2.transform.GetChild(1).GetChild(i).childCount == 0)
                    {

                        stop = true;
                        this.gameObject.transform.SetParent(eS2.transform.GetChild(1).GetChild(i));
                        this.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
                        eS2.gameObject.GetComponent<Inventory>().updateItemList();
                        inventory2.updateItemList();
                        inventory2.EquiptItem2(item);
                        gearable = true;
                        if (duplication != null)
                            Destroy(duplication.gameObject);
                        break;
                    }
                }
            }

            if (!stop)
            {
                for (int i = 0; i < eS2.slotsInTotal; i++)
                {
                    if (itemTypeOfSlot[i].Equals(item.itemType))
                    {
                        if (eS2.transform.GetChild(1).GetChild(i).childCount != 0)
                        {
                            GameObject otherItemFromCharacterSystem = eS2.transform.GetChild(1).GetChild(i).GetChild(0).gameObject;
                            Item otherSlotItem = otherItemFromCharacterSystem.GetComponent<ItemOnObject>().item;
                            if (item.itemType == ItemType.UFPS_Weapon)
                            {
                                inventory2.UnEquipItem1(otherItemFromCharacterSystem.GetComponent<ItemOnObject>().item);
                                inventory2.EquiptItem(item);
                            }
                            else
                            {
                                inventory2.EquiptItem(item);
                                if (item.itemType != ItemType.Backpack)
                                    inventory2.UnEquipItem1(otherItemFromCharacterSystem.GetComponent<ItemOnObject>().item);
                            }
                            if (this == null)
                            {
                                GameObject dropItem = (GameObject)Instantiate(otherSlotItem.itemModel);
                                dropItem.AddComponent<PickUpItem>();
                                dropItem.GetComponent<PickUpItem>().item = otherSlotItem;
                                dropItem.transform.localPosition = GameObject.FindGameObjectWithTag("Player2").transform.localPosition;
                                inventory2.OnUpdateItemList();
                            }
                            else
                            {
                                otherItemFromCharacterSystem.transform.SetParent(this.transform.parent);
                                otherItemFromCharacterSystem.GetComponent<RectTransform>().localPosition = Vector3.zero;
                                if (this.gameObject.transform.parent.parent.parent.GetComponent<Hotbar>() != null)
                                    createDuplication(otherItemFromCharacterSystem);

                                this.gameObject.transform.SetParent(eS2.transform.GetChild(1).GetChild(i));
                                this.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
                            }

                            gearable = true;
                            if (duplication != null)
                                Destroy(duplication.gameObject);
                            eS2.gameObject.GetComponent<Inventory>().updateItemList();
                            inventory2.OnUpdateItemList();
                            break;                           
                        }
                    }
                }
            }


        }
        if (!gearable && item.itemType != ItemType.UFPS_Ammo && item.itemType != ItemType.UFPS_Grenade)
        {

            if (duplication != null)
                itemFromDup = duplication.GetComponent<ItemOnObject>().item;

           inventory2.ConsumeItem2(item);


            item.itemValue--;
            if (itemFromDup != null)
            {
                duplication.GetComponent<ItemOnObject>().item.itemValue--;
                if (itemFromDup.itemValue <= 0)
                {
                    if (tooltip != null)
                        tooltip.deactivateTooltip();
                    inventory2.deleteItemFromInventory(item);
                    Destroy(duplication.gameObject);

                }
            }
            if (item.itemValue <= 0)
            {
                if (tooltip != null)
                    tooltip.deactivateTooltip();
                inventory2.deleteItemFromInventory(item);
                Destroy(this.gameObject); 
            }

        }        
    }

    public void createDuplication(GameObject Item)
    {
        Item item = Item.GetComponent<ItemOnObject>().item;
        GameObject dup = mainInventory2.GetComponent<Inventory>().addItemToInventory(item.itemID, item.itemValue);
       Item.GetComponent<ConsumeItem>().duplication = dup;
       dup.GetComponent<ConsumeItem>().duplication = Item;
    }
}
