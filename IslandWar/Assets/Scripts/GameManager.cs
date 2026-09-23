using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    
    [SerializeField] private int money;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] float x;
    public Tikku_ukko tikku_Ukko;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        x = tikku_Ukko.transform.position.x;

        if (x <= -6)
        {
            moneyText.text = "100$";
        }
    }

    void addMoney()
    {
        if(x == -6)
        {
            moneyText.text = money.ToString() + "$";
        }
    }
}
