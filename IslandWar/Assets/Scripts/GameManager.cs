using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int money;
    public int goalMoney;
    public int happiness;
    public int goalHappiness;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timerTextA;
    public TextMeshProUGUI timerTextB;
    public TextMeshProUGUI runtimeTimer;
    public TextMeshProUGUI goalText;
    public TextMeshProUGUI goalAchievedTimer;

    public GameObject troopPrefab;
    public Transform homePoint;
    public Transform pointA;
    public Transform pointB;

    public int troopCount = 1;
    public int troopCost = 100;

    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject goalAchievedPanel;
    public GameObject upgradePanel;
    public GameObject statsPanel;
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI troopCostText;
    public TextMeshProUGUI speedCostText;
    public TextMeshProUGUI lootCostText;

    public Button pointAButton;
    public Button pointBButton;
    public Sprite spriteBaseNormal;
    public Sprite spriteBaseHighlight;

    public float troopSpeed = 2f;
    public int troopLoot = 25;
    public float timer;
    public int timerSeconds;
    public int timerMinutes;
    public int timerHours;
    public bool isRunning = false;

    public int speedCost = 50;
    public int lootCost = 50;
    
    public InputActionReference pause;

    // Happiness
    public GameObject buildingsPanel;
    public TextMeshProUGUI happinessText;
    public TextMeshProUGUI waterCostText;
    public TextMeshProUGUI parkCostText;
    public TextMeshProUGUI schoolCostText;

    public float baseStealTime = 3f;

    public int waterCost = 100;
    public int parkCost = 250;
    public int schoolCost = 500;

    int speedBought = 0;
    int lootBought = 0;
    int waterBought = 0;
    int parkBought = 0;
    int schoolBought = 0;

    int troopsOut = 0;
    bool choosing = false;

    void Start()
    {
        timerTextA.text = "";
        timerTextB.text = "";
        upgradePanel.SetActive(false);
        buildingsPanel.SetActive(false);
        statsPanel.SetActive(false);
        UpdateUI();
        InvokeRepeating("UpdateTimer", .01f, 1.0f);
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        timer = 0f;
        isRunning = true;
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        isRunning = false;
    }

    public void ExitToMenu()
    {
        pauseMenuPanel.SetActive(false);
        goalAchievedPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        isRunning = false;
        money = 0;
        happiness = 1;
        timer = 0f;
        timerSeconds = 0;
        timerMinutes = 0;
        timerHours = 0;
        troopCount = 1;
        troopSpeed = 2f;
        troopLoot = 20;
        UpdateUI();
    }

    public void Update()
    {
        if (isRunning == true)
        {
            timer = timer + Time.deltaTime;
        }

        timerSeconds = (int)timer;
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

    public void UpdateTimer()
    {
        if(timerMinutes < 1)
        {
            runtimeTimer.text = timerSeconds.ToString() + "s";
        }

        if(timerSeconds %  60 == 0 && timerSeconds > 0)
        {
            timerMinutes++;
        }

        if(timerMinutes > 0 && timerMinutes < 60)
        {
            timerSeconds = timerSeconds - (timerMinutes * 60);
            runtimeTimer.text = timerMinutes.ToString() + "min " + timerSeconds.ToString() + "s";
        }

        if(timerMinutes % 60 == 0 && timerMinutes > 0)
        {
            timerHours++;
        }

        if(timerHours > 0)
        {
            timerMinutes = timerMinutes - (timerHours * 60);
            runtimeTimer.text = timerHours.ToString() + "h " + timerMinutes.ToString() + "min " + timerSeconds.ToString() + "s";
        }
    }

    public void PressContinue()
    {
        pauseMenuPanel.SetActive(false);
        isRunning = true;
    }
    public void ToggleBuildings()
    {
        buildingsPanel.SetActive(!buildingsPanel.activeSelf);
    }
    public void ToggleStats()
{
    statsPanel.SetActive(!statsPanel.activeSelf);
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
            speedBought++;
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
            lootBought++;
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
            waterBought++;
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
            parkBought++;
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
            schoolBought++;
            UpdateUI();
        }
    }

    // Trooppien lähettäminen

    public void PressSend()
    {
        if (troopsOut > 0) return;
        choosing = true;
        HighlightBases();
    }

    public void HighlightBases()
    {
        pointAButton.GetComponent<Image>().sprite = spriteBaseHighlight;
        pointBButton.GetComponent<Image>().sprite = spriteBaseHighlight;
    }

    public void NonHighlightBases()
    {
        pointAButton.GetComponent<Image>().sprite = spriteBaseNormal;
        pointBButton.GetComponent<Image>().sprite = spriteBaseNormal;
    }

    public void SendToA()
    {
        if (choosing) SendTroops(pointA, timerTextA);
        NonHighlightBases();
    }

    public void SendToB()
    {
        if (choosing) SendTroops(pointB, timerTextB);
        NonHighlightBases();
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
        IsGoalAchieved();
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
        goalText.text = "Goal: Happiness - " + happiness + "% \n Money - " + money + "$/" + goalMoney + "$";
        statsText.text =
        "Troops: " + troopCount + "\n" +
        "Speed upgrades: " + speedBought + "\n" +
        "Loot upgrades: " + lootBought + "\n" +
        "\n" +
        "Water purifiers: " + waterBought + "\n" +
        "Parks: " + parkBought + "\n" +
        "Schools: " + schoolBought + "\n" +
        "\n" +
        "Happiness: " + happiness + "/100";
    }

    private void IsGoalAchieved()
    {
        if(timerHours > 0)
        {
            goalAchievedTimer.text = timerHours + "h " + timerMinutes + "min " + timerSeconds + "s";
        }
        else
        {
            goalAchievedTimer.text = timerMinutes + "min " + timerSeconds + "s";
        }

        if (happiness >= goalHappiness && money >= goalMoney)
        {
            GoalAchieved();
        }
    }

    private void OnEnable()
    {
        pause.action.started += Pause;
    }

    public void GoalAchieved()
    {
        isRunning = false;
        goalAchievedPanel.SetActive(true);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    private void Pause(InputAction.CallbackContext context)
    {
        PauseGame();
    }
}