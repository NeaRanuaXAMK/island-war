using UnityEngine;

public class Troop : MonoBehaviour
{
    public float speed = 2f;
    public float stealTime = 1f;
    public int loot = 20;

    public Transform lootPoint;
    public GameManager gameManager;

    bool going = false;
    bool stealing = false;
    bool returning = false;

    Vector3 home;
    float timer;

    void Start()
    {
        home = transform.position;
    }

    void Update()
    {
        if (going)
        {
            transform.position = Vector3.MoveTowards(transform.position, lootPoint.position, speed * Time.deltaTime);

            if (transform.position == lootPoint.position)
            {
                going = false;
                stealing = true;
                timer = stealTime;
            }
        }
        else if (stealing)
        {
            timer = timer - Time.deltaTime;

            if (timer <= 0)
            {
                stealing = false;
                returning = true;
            }
        }
        else if (returning)
        {
            transform.position = Vector3.MoveTowards(transform.position, home, speed * Time.deltaTime);

            if (transform.position == home)
            {
                returning = false;
                gameManager.AddMoney(loot);
                Destroy(gameObject);
            }
        }
    }

    public void StartMoving()
    {
        if (!going && !stealing && !returning)
        {
            going = true;
        }
    }

}