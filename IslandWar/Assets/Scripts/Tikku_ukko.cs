using UnityEngine;

public class Tikku_ukko : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed = 0.5f;
    public Vector2 position;
    bool isMoving = false;
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && transform.position.x > -6)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

    }


    public void StartMoving()
    {
        isMoving = true;
    }
}
