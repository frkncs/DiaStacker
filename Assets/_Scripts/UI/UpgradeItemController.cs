using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeItemController : MonoBehaviour
{
    public enum BuyItemType
    {
        StartStackCount,
    }

    #region Variables

    // Public Variables

    // Private Variables
    [Header("Upgrade Item Objects")]
    [SerializeField] private TextMeshProUGUI txtCost;
    [SerializeField] private BuyItemType itemType;
    [SerializeField] private int defaultCost;
    [SerializeField] private int maxBuyCount = 4;

    [Header("StartStackCount Item Objects")] [SerializeField]
    private TextMeshProUGUI txtStartItemCount;

    #endregion Variables

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(itemType + "Cost"))
        {
            PlayerPrefs.SetInt(itemType + "Cost", defaultCost);
        }
        
        txtCost.text = PlayerPrefs.GetInt(itemType + "Cost").ToString();

        if (PlayerPrefs.GetInt(itemType + "BuyCount") >= maxBuyCount)
        {
            txtCost.text = "Max";
        }
        
        if (txtStartItemCount != null)
        {
            txtStartItemCount.text = PlayerPrefs.GetInt("StartStackCount").ToString();
        }
    }

    public void BuyItem()
    {
        var currentMoney = PlayerPrefs.GetInt("Money");
        var currentCost = PlayerPrefs.GetInt(itemType + "Cost");

        if (currentMoney - currentCost < 0) return;
        if (PlayerPrefs.GetInt(itemType + "BuyCount") >= maxBuyCount) return;

        PlayerPrefs.SetInt("Money", currentMoney - currentCost);
        PlayerPrefs.SetInt(itemType + "Cost", (int)(currentCost * 1.2f));

        txtCost.text = PlayerPrefs.GetInt(itemType + "Cost").ToString();
        GameEvents.UpdateCurrentMoneyEvent?.Invoke();

        PlayerPrefs.SetInt(itemType + "BuyCount", PlayerPrefs.GetInt(itemType + "BuyCount") + 1);

        if (PlayerPrefs.GetInt(itemType + "BuyCount") >= maxBuyCount)
        {
            txtCost.text = "Max";
        }

        switch (itemType)
        {
            case BuyItemType.StartStackCount:
                
                PlayerPrefs.SetInt("StartStackCount", PlayerPrefs.GetInt("StartStackCount") + 1);
                txtStartItemCount.text = PlayerPrefs.GetInt("StartStackCount").ToString();

                GameEvents.UpdateStartStackEvent?.Invoke();
                
                break;
        }
    }
}