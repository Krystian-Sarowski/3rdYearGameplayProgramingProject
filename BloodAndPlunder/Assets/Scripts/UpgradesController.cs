using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesController : MonoBehaviour
{
    [SerializeField]
    Text costText = null;

    [SerializeField]
    Button purchaseButton = null;

    [SerializeField]
    Transform costTextTransform = null;

    [SerializeField]
    List<int> upgradeCosts = new List<int>();

    [SerializeField]
    Vector3 buttonOffset = Vector3.zero;

    [SerializeField]
    AxeController axeScript = null;

    // Start is called before the first frame update
    void Awake()
    {
        HideUpgradeUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(costText.enabled)
        {
            Vector3 costTextPos = Camera.main.WorldToScreenPoint(costTextTransform.position);
            costText.transform.position = costTextPos;
        }

        if(purchaseButton.gameObject.activeSelf)
        {
            Vector3 buttonPos = Camera.main.WorldToScreenPoint(costTextTransform.position + buttonOffset);
            purchaseButton.transform.position = buttonPos;
        }
    }

    public void PurchaseUpgrade()
    {
        Inventory.gold -= upgradeCosts[Inventory.axeLevel];
        Inventory.axeLevel++;

        axeScript.SetAxeStats();
        //Play purchase sound.

        if (Inventory.axeLevel >= upgradeCosts.Count)
        {
            costText.enabled = false;
            purchaseButton.gameObject.SetActive(false);
        }

        else
        {
            costText.text = "Cost: " + upgradeCosts[Inventory.axeLevel].ToString();

            if (Inventory.gold < upgradeCosts[Inventory.axeLevel])
            {
                purchaseButton.interactable = false;
            }
        }
    }

    void DisplayUpgradeUI()
    {
        if (Inventory.axeLevel >= upgradeCosts.Count)
        {
            costText.enabled = false;
            purchaseButton.gameObject.SetActive(false);
        }

        else
        {
            costText.enabled = true;
            purchaseButton.gameObject.SetActive(true);

            costText.text = "Cost: " + upgradeCosts[Inventory.axeLevel].ToString();

            if (Inventory.gold < upgradeCosts[Inventory.axeLevel])
            {
                purchaseButton.interactable = false;
            }
        }
    }

    void HideUpgradeUI()
    {
        costText.enabled = false;
        purchaseButton.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            DisplayUpgradeUI();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            HideUpgradeUI();
        }
    }
}
