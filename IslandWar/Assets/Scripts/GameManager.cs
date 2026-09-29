using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int money;
    [SerializeField] private TextMeshProUGUI moneyText;

    public Troop troop;

    public int troopCount = 1;
    public int troopCost = 100;

    bool troopsAway = false;

    void Start()
    {
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        troopsAway = false;
        UpdateUI();
    }

    public void SendTroops()
    {
        if (troopsAway) return;
        troopsAway = true;

        for (int i = 0; i < troopCount; i++)
        {
            Troop copy = Instantiate(troop);
            copy.StartMoving();
        }
    }

    public void BuyTroop()
    {
        if (money >= troopCost)
        {
            money = money - troopCost;
            troopCount = troopCount + 1;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        moneyText.text = money + "$";
    }
}