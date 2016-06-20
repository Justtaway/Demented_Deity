using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Player : MonoBehaviour {

	public GameObject inventory;
	public GameObject characterSystem;
	public GameObject craftSystem;
	private Inventory craftSystemInventory;
	private CraftSystem cS;
	private Inventory mainInventory;
	private Inventory characterSystemInventory;
	private Tooltip toolTip;
	private InputManager inputManagerDatabase;
	public GameObject sword;
	public GameObject heal;
	public GameObject light;
	public GameObject fire;


	int normalSize = 10; 

	public void OnEnable()
	{
		Inventory.ItemEquip += OnBackpack;
		Inventory.UnEquipItem += UnEquipBackpack;

		Inventory.ItemEquip += OnGearItem;

		Inventory.ItemConsumed += OnConsumeItem;
		Inventory.UnEquipItem += OnUnEquipItem;

		Inventory.ItemEquip += EquipWeapon;
		Inventory.UnEquipItem += UnEquipWeapon;
	}

	public void OnDisable()
	{
		Inventory.ItemEquip -= OnBackpack;
		Inventory.UnEquipItem -= UnEquipBackpack;

		Inventory.ItemEquip -= OnGearItem;
		Inventory.UnEquipItem -= OnUnEquipItem;

		Inventory.UnEquipItem -= UnEquipWeapon;
		Inventory.ItemEquip -= EquipWeapon;
	}

	void EquipWeapon(Item item)
	{
		if (item.itemType == ItemType.Weapon)
		{
			//add the weapon if you unequip the weapon
		}
	}

	void UnEquipWeapon(Item item)
	{
		if (item.itemType == ItemType.Weapon)
		{
			//delete the weapon if you unequip the weapon
		}
	}

	void OnBackpack(Item item)
	{
		if (item.itemType == ItemType.Backpack)
		{
			for (int i = 0; i < item.itemAttributes.Count; i++)
			{
				if (mainInventory == null)
					mainInventory = inventory.GetComponent<Inventory>();
				mainInventory.sortItems();
				if (item.itemAttributes[i].attributeName == "Slots")
					changeInventorySize(item.itemAttributes[i].attributeValue);
			}
		}
	}

	void UnEquipBackpack(Item item)
	{
		if (item.itemType == ItemType.Backpack)
			changeInventorySize(normalSize);
	}

	void changeInventorySize(int size)
	{
		dropTheRestItems(size);

		if (mainInventory == null)
			mainInventory = inventory.GetComponent<Inventory>();
		if (size == 3)
		{
			mainInventory.width = 3;
			mainInventory.height = 1;
			mainInventory.updateSlotAmount();
			mainInventory.adjustInventorySize();
		}
		if (size == 6)
		{
			mainInventory.width = 3;
			mainInventory.height = 2;
			mainInventory.updateSlotAmount();
			mainInventory.adjustInventorySize();
		}
		else if (size == 12)
		{
			mainInventory.width = 4;
			mainInventory.height = 3;
			mainInventory.updateSlotAmount();
			mainInventory.adjustInventorySize();
		}
		else if (size == 16)
		{
			mainInventory.width = 4;
			mainInventory.height = 4;
			mainInventory.updateSlotAmount();
			mainInventory.adjustInventorySize();
		}
		else if (size == 24)
		{
			mainInventory.width = 6;
			mainInventory.height = 4;
			mainInventory.updateSlotAmount();
			mainInventory.adjustInventorySize();
		}
	}

	void dropTheRestItems(int size)
	{
		if (size < mainInventory.ItemsInInventory.Count)
		{
			for (int i = size; i < mainInventory.ItemsInInventory.Count; i++)
			{
				GameObject dropItem = (GameObject)Instantiate(mainInventory.ItemsInInventory[i].itemModel);
				dropItem.AddComponent<PickUpItem>();
				dropItem.GetComponent<PickUpItem>().item = mainInventory.ItemsInInventory[i];
				dropItem.transform.localPosition = GameObject.FindGameObjectWithTag("Player").transform.localPosition;
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
			mainInventory = inventory.GetComponent<Inventory>();
		if (characterSystem != null)
			characterSystemInventory = characterSystem.GetComponent<Inventory>();
		if (craftSystem != null)
			craftSystemInventory = craftSystem.GetComponent<Inventory>();

    }

    public void OnConsumeItem(Item item)
    {
		for (int i = 0; i < item.itemAttributes.Count; i++) 
		{
			if (item.itemAttributes [i].attributeName == "Health") //Если мы используем предмет
			{
				if ((gameObject.GetComponent<HP> ().cur_Health + item.itemAttributes[i].attributeValue) >= gameObject.GetComponent<HP> ().max_Health) 
				{
					gameObject.GetComponent<HP> ().cur_Health = gameObject.GetComponent<HP> ().max_Health;
					gameObject.GetComponent<HP> ().SetHealthBar (1f);
				} 
				else // То прибовляем определенное кол-во ХП соответствующее 
				{
					gameObject.GetComponent<HP> ().cur_Health += item.itemAttributes[i].attributeValue;
					float calc_Health = gameObject.GetComponent<HP> ().cur_Health / gameObject.GetComponent<HP> ().max_Health;
					gameObject.GetComponent<HP> ().SetHealthBar (calc_Health);
				}
				Debug.Log (item.itemAttributes[i].attributeValue);
			}

		}
    }
    	public void OnGearItem(Item item) //надеваем шмот
    	{
			
    		for (int i = 0; i < item.itemAttributes.Count; i++)
    		{
				if (item.itemAttributes [i].attributeName == "Armor") 
				{
					gameObject.GetComponent<HP> ().max_Health += item.itemAttributes [i].attributeValue; //прибавляем значение "Attribute Value"
				}

				if (item.itemAttributes [i].attributeName == "Damage") //если мы одеваем предмет с аттрибутом "Damage"
				{
					sword.GetComponent<Damage>().damage+= item.itemAttributes [i].attributeValue; //прибавляем значение "Attribute Value"
				}
				if (item.itemAttributes [i].attributeName == "Move Speed") //если мы одеваем предмет с аттрибутом "MoveSpeed"
				{
					gameObject.GetComponent<PlayerMovement>().moveSpeed += item.itemAttributes [i].attributeValue / 100f; //прибавляем значение "Attribute Value"
					
				}

			if (item.itemAttributes [i].attributeName == "lightning") //если мы одеваем предмет с аттрибутом "MoveSpeed"
				{
					light.GetComponent<Damage>().damage += item.itemAttributes [i].attributeValue; //прибавляем значение "Attribute Value"
				}
    		}

    	}

    	public void OnUnEquipItem(Item item) // снимаем шмот
    	{
    		for (int i = 0; i < item.itemAttributes.Count; i++)
    		{
				if (item.itemAttributes [i].attributeName == "Armor") 
				{
					gameObject.GetComponent<HP> ().max_Health -= item.itemAttributes [i].attributeValue;
				}
				
				if (item.itemAttributes [i].attributeName == "Damage") //если мы снимаем предмет с аттрибутом "Damage"
				{
					sword.GetComponent<Damage>().damage -= item.itemAttributes [i].attributeValue; //вычитаем значение "Attribute Value"
				}

				if (item.itemAttributes [i].attributeName == "MoveSpeed") //если мы одеваем предмет с аттрибутом "MoveSpeed"
				{
				gameObject.GetComponent<PlayerMovement>().moveSpeed -= item.itemAttributes [i].attributeValue; //вычитаем значение "Attribute Value"
				}

				if (item.itemAttributes [i].attributeName == "lightning") //если мы одеваем предмет с аттрибутом "MoveSpeed"
				{
					light.GetComponent<Damage>().damage -= item.itemAttributes [i].attributeValue; //прибавляем значение "Attribute Value"
				}

			}
    
    	}

    void Update () {



		if (Input.GetKeyDown(inputManagerDatabase.CharacterSystemKeyCode))
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

		if (Input.GetKeyDown(inputManagerDatabase.InventoryKeyCode))
		{
			if (!inventory.activeSelf)
			{
				mainInventory.openInventory();
			
			}
			else
			{
				if (toolTip != null)
					toolTip.deactivateTooltip();
					mainInventory.closeInventory ();
			}
		}

		if (Input.GetKeyDown(inputManagerDatabase.CraftSystemKeyCode))
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
