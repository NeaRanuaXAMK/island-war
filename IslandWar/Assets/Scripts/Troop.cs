using TMPro;
using UnityEngine;

public class Troop : MonoBehaviour
{
    public float speed = 2f;
    public float stealTime = 1f;
    public int loot = 20;

    public GameManager gameManager;
    public Vector3 target;
    public Vector3 home;
    public TextMeshProUGUI timerText;

    int phase = 0; // 0 = menossa 1= varastaa 2 = palaa takas
    float timer;

    public float animSpeed = 0.5f;
    SpriteRenderer sr;
    Animator anim;
    AudioSource audioSource;
    public AudioClip stealSound;
    public bool playSound = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        anim.speed = animSpeed;
        if (playSound) audioSource.PlayOneShot(stealSound);
    }

    void Update()
    {
        if (phase == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position == target)
            {
                phase = 1;
                timer = stealTime;
                anim.speed = 0;
                
            }
        }
        else if (phase == 1)
        {
            timer -= Time.deltaTime;
            timerText.text = Mathf.Max(timer, 0).ToString("F1") + "s";
            if (timer <= 0)
            {
                timerText.text = "";
                phase = 2;
                anim.speed = animSpeed;
                sr.flipX = true;
                if (playSound) audioSource.PlayOneShot(stealSound);
            }
        }
        else if (phase == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, home, speed * Time.deltaTime);
            if (transform.position == home)
            {
                gameManager.TroopReturned(loot);
                Destroy(gameObject);
            }
        }
    }
}