using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int money;
    [SerializeField] private TextMeshProUGUI moneyText;

    public Troop troop;

    public int troopCount = 1;
    public int troopCost = 100;
    public Transform pointA;
    public Transform pointB;
    bool troopsAway = false;
    bool choosing = false;

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

    public void SendTroops(Transform point)
    {
        choosing = false;
        troopsAway = true;

        for (int i = 0; i < troopCount; i++)
        {
            Troop copy = Instantiate(troop);
            copy.lootPoint = point;
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

    public void sendToA()
    {
        if (choosing)
            SendTroops(pointA);
    }
    public void sendToB()
    {
        if (choosing)
            SendTroops(pointB);
    }
    public void PressSend()
    {
        if (troopsAway) return;
        choosing = true;
    }


}