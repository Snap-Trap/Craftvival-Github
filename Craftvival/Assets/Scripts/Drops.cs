using UnityEngine;

public class Drops : MonoBehaviour
{
    // Script made by Charly
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.name == "Food")
            {
                PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
                if (playerStatus != null)
                {
                    playerStatus.AddStatus(10, "Food");
                }
            }
            Destroy(gameObject);
            Debug.Log("Gained " + gameObject.name);
        }
    }
}
