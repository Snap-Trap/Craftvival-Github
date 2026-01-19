using UnityEngine;

public class Drops : MonoBehaviour
{
    public float foodAmount = 10f;
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
            if (gameObject.tag == "Food")
            {
                PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
                if (playerStatus != null)
                {
                    playerStatus.AddStatus(foodAmount, "Food");
                }
            }
            Destroy(gameObject);
            Debug.Log("Gained " + gameObject.name);
        }
    }
}
