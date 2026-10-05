using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timerTextA;
    public TextMeshProUGUI timerTextB;

    public GameObject troopPrefab;
    public Transform homePoint;
    public Transform pointA;
    public Transform pointB;

    public int troopCount = 1;
    public int troopCost = 100;

    public GameObject upgradePanel;
    public TextMeshProUGUI troopCostText;
    public TextMeshProUGUI speedCostText;
    public TextMeshProUGUI lootCostText;

    public float troopSpeed = 2f;
    public int troopLoot = 20;

    public int speedCost = 50;
    public int lootCost = 50;

    int troopsOut = 0;
    bool choosing = false;

    void Start()
    {
        timerTextA.text = "";
        timerTextB.text = "";
        upgradePanel.SetActive(false);
        UpdateUI();
    }

    public void ToggleUpgrades()
    {
        upgradePanel.SetActive(!upgradePanel.activeSelf);
    }

    public void BuyTroop()
    {
        if (money >= troopCost)
        {
            money -= troopCost;
            troopCount++;
            troopCost = Mathf.CeilToInt(troopCost * 1.15f);
            UpdateUI();
        }
    }

    public void BuySpeed()
    {
        if (money >= speedCost)
        {
            money -= speedCost;
            troopSpeed += 0.5f;
            speedCost = Mathf.CeilToInt(speedCost * 1.15f);
            UpdateUI();
        }
    }

    public void BuyLoot()
    {
        if (money >= lootCost)
        {
            money -= lootCost;
            troopLoot += 5;
            lootCost = Mathf.CeilToInt(lootCost * 1.15f);
            UpdateUI();
        }
    }

    public void PressSend()
    {
        if (troopsOut > 0) return;
        choosing = true;
    }

    public void SendToA()
    {
        if (choosing) SendTroops(pointA, timerTextA);
    }

    public void SendToB()
    {
        if (choosing) SendTroops(pointB, timerTextB);
    }

    void SendTroops(Transform target, TextMeshProUGUI timerText)
    {
        choosing = false;

        for (int i = 0; i < troopCount; i++)
        {
            GameObject newTroop = Instantiate(troopPrefab);
            newTroop.transform.position = homePoint.position;

            Troop troop = newTroop.GetComponent<Troop>();
            troop.gameManager = this;
            troop.target = target.position;
            troop.home = homePoint.position;
            troop.timerText = timerText;
            troop.speed = troopSpeed;
            troop.loot = troopLoot;
            troopsOut++;
        }
    }

    public void TroopReturned(int loot)
    {
        money += loot;
        troopsOut--;
        UpdateUI();
    }

    void UpdateUI()
    {
        moneyText.text = money + "$";
        troopCostText.text = "Troop +1  (" + troopCost + "$)";
        speedCostText.text = "Speed +0.5  (" + speedCost + "$)";
        lootCostText.text = "Loot +5  (" + lootCost + "$)";
    }
}