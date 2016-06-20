using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CloseInventory : MonoBehaviour, IPointerDownHandler
{
	private GameObject player2;
	private GameObject pla2;

	private GameObject pla22;
	public GameObject invent2;
	private GameObject inv2;
	private GameObject ci2;
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
