using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CloseInventoryP2 : MonoBehaviour, IPointerDownHandler
{
	


    Inventory inv;

    void Start()
    {

        inv = transform.parent.GetComponent<Inventory>();

    }
    public void OnPointerDown(PointerEventData eventData)
    {

		if (eventData.button == PointerEventData.InputButton.Left)
        {
            inv.closeInventory();

        }
    }
}
