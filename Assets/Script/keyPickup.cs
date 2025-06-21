using UnityEngine;

public class keyPickup : MonoBehaviour
{
    public static int keyCount = 0;

    void Awake()
    {
        keyCount = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyCount++;
            Debug.Log("Total keys: " + keyCount);

            Destroy(gameObject);

            if (keyCount >= 4)
            {
                Time.timeScale = 0f;
                Debug.Log("You got out of the room");
            }
        }
    }
}
