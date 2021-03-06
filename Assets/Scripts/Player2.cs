using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Player2 : MonoBehaviour {

	public GameObject inventory;
	public GameObject characterSystem;
	public GameObject craftSystem;
	private Inventory craftSystemInventory;
	private CraftSystem cS;
	private Inventory mainInventory2;
	private Inventory characterSystemInventory;
	private Tooltip toolTip;
	private InputManager inputManagerDatabase;


	int normalSize = 10; 
	public void OnEnable()
	{

		Inventory.ItemEquip += OnBackpack2;
		Inventory.UnEquipItem += UnEquipBackpack2;

		Inventory.ItemEquip += OnGearItem2;
		Inventory.ItemConsumed2 += OnConsumeItem2;
		Inventory.UnEquipItem += OnUnEquipItem2;

		Inventory.ItemEquip += EquipWeapon2;
		Inventory.UnEquipItem += UnEquipWeapon2;
	}

	public void OnDisable()
	{
		Inventory.ItemEquip -= OnBackpack2;
		Inventory.UnEquipItem -= UnEquipBackpack2;

		Inventory.ItemEquip -= OnGearItem2;
		Inventory.UnEquipItem -= OnUnEquipItem2;

		Inventory.UnEquipItem -= UnEquipWeapon2;
		Inventory.ItemEquip-= EquipWeapon2;
	}

	void EquipWeapon2(Item item)
	{
		if (item.itemType == ItemType.Weapon)
		{
			//add the weapon if you unequip the weapon
		}
	}

	void UnEquipWeapon2(Item item)
	{
		if (item.itemType == ItemType.Weapon)
		{
			//delete the weapon if you unequip the weapon
		}
	}

	void OnBackpack2(Item item)
	{
		if (item.itemType == ItemType.Backpack)
		{
			for (int i = 0; i < item.itemAttributes.Count; i++)
			{
				
				if (mainInventory2 == null)
					mainInventory2 = inventory.GetComponent<Inventory>();
				mainInventory2.sortItems();
				if (item.itemAttributes[i].attributeName == "Slots")
					changeInventorySize(item.itemAttributes[i].attributeValue);
			}
		}
	}

	void UnEquipBackpack2(Item item)
	{
		if (item.itemType == ItemType.Backpack)
			changeInventorySize(normalSize);
	}

	void changeInventorySize(int size)
	{
		dropTheRestItems(size);

		if (mainInventory2 == null)
			mainInventory2 = inventory.GetComponent<Inventory>();
		if (size == 3)
		{
			mainInventory2.width = 3;
			mainInventory2.height = 1;
			mainInventory2.updateSlotAmount();
			mainInventory2.adjustInventorySize();
		}
		if (size == 6)
		{
			mainInventory2.width = 3;
			mainInventory2.height = 2;
			mainInventory2.updateSlotAmount();
			mainInventory2.adjustInventorySize();
		}
		else if (size == 12)
		{
			mainInventory2.width = 4;
			mainInventory2.height = 3;
			mainInventory2.updateSlotAmount();
			mainInventory2.adjustInventorySize();
		}
		else if (size == 16)
		{
			mainInventory2.width = 4;
			mainInventory2.height = 4;
			mainInventory2.updateSlotAmount();
			mainInventory2.adjustInventorySize();
		}
		else if (size == 24)
		{
			mainInventory2.width = 6;
			mainInventory2.height = 4;
			mainInventory2.updateSlotAmount();
			mainInventory2.adjustInventorySize();
		}
	}

	void dropTheRestItems(int size)
	{
		if (size < mainInventory2.ItemsInInventory.Count)
		{
			for (int i = size; i < mainInventory2.ItemsInInventory.Count; i++)
			{
				GameObject dropItem = (GameObject)Instantiate(mainInventory2.ItemsInInventory[i].itemModel);
				dropItem.AddComponent<PickUpItem>();
				dropItem.GetComponent<PickUpItem>().item = mainInventory2.ItemsInInventory[i];
				dropItem.transform.localPosition = GameObject.FindGameObjectWithTag("Player2").transform.localPosition;
			}
		}
	}

	void Start () 
	{
		if (inputManagerDatabase == null)
			inputManagerDatabase = (InputManager)Resources.Load("InputManager");

		if (craftSystem != null)
			cS = craftSystem.GetComponent<CraftSystem>();

		if (GameObject.FindGameObjectWithTag("Tooltip") != null)
			toolTip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
		if (inventory != null)
			mainInventory2 = inventory.GetComponent<Inventory>();
		if (characterSystem != null)
			characterSystemInventory = characterSystem.GetComponent<Inventory>();
		if (craftSystem != null)
			craftSystemInventory = craftSystem.GetComponent<Inventory>();
    }

    public void OnConsumeItem2(Item item)
    {
		for (int i = 0; i < item.itemAttributes.Count; i++) 
		{

			if (item.itemAttributes [i].attributeName == "Health") //Если мы используем предмет
			{
				if ((gameObject.GetComponent<HP2> ().cur_Health + item.itemAttributes[i].attributeValue) >= gameObject.GetComponent<HP2> ().max_Health) 
				{
					gameObject.GetComponent<HP2> ().cur_Health= gameObject.GetComponent<HP2> ().max_Health;
					gameObject.GetComponent<HP2> ().SetHealthBar (1f);
				} 
				else // То прибовляем определенное кол-во ХП соответствующее 
				{
					gameObject.GetComponent<HP2> ().cur_Health += item.itemAttributes[i].attributeValue;
					float calc_Health = gameObject.GetComponent<HP2> ().cur_Health / gameObject.GetComponent<HP2> ().max_Health;
					gameObject.GetComponent<HP2> ().SetHealthBar (calc_Health);
				}
				Debug.Log (item.itemAttributes[i].attributeValue);
			}

		}
    }
    	public void OnGearItem2(Item item) //надеваем шмот
    	{
    		for (int i = 0; i < item.itemAttributes.Count; i++)
    		{

				if (item.itemAttributes [i].attributeName == "Armor") 
				{
				gameObject.GetComponent<HP2> ().max_Health += item.itemAttributes [i].attributeValue; //прибавляем значение "Attribute Value"
				}

//				if (item.itemAttributes [i].attributeName == "Damage") //если мы одеваем предмет с аттрибутом "Damage"
//				{
//				gameObject.GetComponentInChildren<SwordDemag>().damag += item.itemAttributes [i].attributeValue; //прибавляем значение "Attribute Value"
//				}
				if (item.itemAttributes [i].attributeName == "MoveSpeed") //если мы одеваем предмет с аттрибутом "MoveSpeed"
				{
				gameObject.GetComponent<PlayerMovement2>().moveSpeed += item.itemAttributes [i].attributeValue; //прибавляем значение "Attribute Value"
					
				}

    		}

    	}

    	public void OnUnEquipItem2(Item item) // снимаем шмот
    	{
    		for (int i = 0; i < item.itemAttributes.Count; i++)
    		{
				if (item.itemAttributes [i].attributeName == "Armor") 
				{
					gameObject.GetComponent<HP2> ().max_Health -= item.itemAttributes [i].attributeValue;
				}
				
//				if (item.itemAttributes [i].attributeName == "Damage") //если мы снимаем предмет с аттрибутом "Damage"
//				{
//					gameObject.GetComponentInChildren<SwordDemag>().damag -= item.itemAttributes [i].attributeValue; //вычитаем значение "Attribute Value"
//				}

				if (item.itemAttributes [i].attributeName == "MoveSpeed") //если мы одеваем предмет с аттрибутом "MoveSpeed"
				{
					gameObject.GetComponent<PlayerMovement2>().moveSpeed -= item.itemAttributes [i].attributeValue; //вычитаем значение "Attribute Value"
				}

			}
    
    	}

    void Update () {



		if (Input.GetKeyDown(inputManagerDatabase.CharacterSystemKeyCode2))
		{
			if (!characterSystem.activeSelf)
			{
				characterSystemInventory.openInventory();
			}
			else
			{
				if (toolTip != null)
					toolTip.deactivateTooltip();
					characterSystemInventory.closeInventory();
			}
		}

		if (Input.GetKeyDown(inputManagerDatabase.InventoryKeyCode2))
		{
			if (!inventory.activeSelf)
			{
				mainInventory2.openInventory();
			}
			else
			{
				if (toolTip != null)
					toolTip.deactivateTooltip();
					mainInventory2.closeInventory();
			}
		}

		if (Input.GetKeyDown(inputManagerDatabase.CraftSystemKeyCode2))
		{
			if (!craftSystem.activeSelf)
				craftSystemInventory.openInventory();
			else
			{
				if (cS != null)
					cS.backToInventory();
				if (toolTip != null)
					toolTip.deactivateTooltip();
				craftSystemInventory.closeInventory();
			}
		}

    }
}
