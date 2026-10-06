using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int money;
    public int happiness;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timerTextA;
    public TextMeshProUGUI timerTextB;
    public TextMeshProUGUI runtimeTimer;

    public GameObject troopPrefab;
    public Transform homePoint;
    public Transform pointA;
    public Transform pointB;

    public int troopCount = 1;
    public int troopCost = 100;

    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject upgradePanel;
    public TextMeshProUGUI troopCostText;
    public TextMeshProUGUI speedCostText;
    public TextMeshProUGUI lootCostText;

    public Button pointAButton;
    public Button pointBButton;
    public Sprite spriteBaseNormal;
    public Sprite spriteBaseHighlight;

    public float troopSpeed = 2f;
    public int troopLoot = 20;
    public float timer;
    public int timerSeconds;
    public int timerMinutes;
    public int timerHours;
    public bool isRunning = false;

    public int speedCost = 50;
    public int lootCost = 50;

    public InputActionReference pause;

    int troopsOut = 0;
    bool choosing = false;

    void Start()
    {
        timerTextA.text = "";
        timerTextB.text = "";
        upgradePanel.SetActive(false);
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
        mainMenuPanel.SetActive(true);
        isRunning = false;
        money = 0;
        happiness = 0;
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

    private void OnEnable()
    {
        pause.action.started += Pause;
    }
    private void Pause(InputAction.CallbackContext context)
    {
        PauseGame();
    }
}