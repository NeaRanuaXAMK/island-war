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

    // Upgrade window
    public GameObject upgradePanel;
    public TextMeshProUGUI troopCostText;
    public TextMeshProUGUI speedCostText;
    public TextMeshProUGUI lootCostText;

    public float troopSpeed = 2f;
    public int troopLoot = 25;
    public int speedCost = 50;
    public int lootCost = 50;

    // Happiness
    public GameObject buildingsPanel;
    public TextMeshProUGUI happinessText;
    public TextMeshProUGUI waterCostText;
    public TextMeshProUGUI parkCostText;
    public TextMeshProUGUI schoolCostText;

    public int happiness = 0;
    public float baseStealTime = 3f;

    public int waterCost = 100;
    public int parkCost = 250;
    public int schoolCost = 500;

    int troopsOut = 0;
    bool choosing = false;

    void Start()
    {
        timerTextA.text = "";
        timerTextB.text = "";
        upgradePanel.SetActive(false);
        buildingsPanel.SetActive(false);
        UpdateUI();
    }

    // Happiness raha multiplier
    float GetMultiplier()
    {
        return 1f + happiness / 100f;
    }

    // Happiness (lyhyempi steal time)
    float GetStealTime()
    {
        return baseStealTime * (1f - happiness * 0.005f);
    }

    public void ToggleUpgrades()
    {
        upgradePanel.SetActive(!upgradePanel.activeSelf);
    }

    public void ToggleBuildings()
    {
        buildingsPanel.SetActive(!buildingsPanel.activeSelf);
    }

    // troop upgradet

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

    // Buildingssit

    public void BuyWater()
    {
        if (money >= waterCost)
        {
            money -= waterCost;
            happiness = Mathf.Min(happiness + 5, 100);
            waterCost = Mathf.CeilToInt(waterCost * 1.15f);
            UpdateUI();
        }
    }

    public void BuyPark()
    {
        if (money >= parkCost)
        {
            money -= parkCost;
            happiness = Mathf.Min(happiness + 10, 100);
            parkCost = Mathf.CeilToInt(parkCost * 1.15f);
            UpdateUI();
        }
    }

    public void BuySchool()
    {
        if (money >= schoolCost)
        {
            money -= schoolCost;
            happiness = Mathf.Min(happiness + 15, 100);
            schoolCost = Mathf.CeilToInt(schoolCost * 1.15f);
            UpdateUI();
        }
    }

    // Trooppien lähettäminen

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
            troop.stealTime = GetStealTime();
            troopsOut++;
        }
    }

    public void TroopReturned(int loot)
    {
        money += Mathf.RoundToInt(loot * GetMultiplier());
        troopsOut--;
        UpdateUI();
    }

    void UpdateUI()
    {
        moneyText.text = money + "$";
        troopCostText.text = "Troop +1  (" + troopCost + "$)";
        speedCostText.text = "Speed +0.5  (" + speedCost + "$)";
        lootCostText.text = "Loot +5  (" + lootCost + "$)";

        waterCostText.text = "Water purifier +5  (" + waterCost + "$)";
        parkCostText.text = "Park +10  (" + parkCost + "$)";
        schoolCostText.text = "School +15  (" + schoolCost + "$)";
        happinessText.text = "Happiness " + happiness + "/100\n"
            + "Money x" + GetMultiplier().ToString("F2")
            + "  Steal time " + GetStealTime().ToString("F1") + "s";
    }
}