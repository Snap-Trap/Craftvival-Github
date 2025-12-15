using UnityEngine;

// Creator: Luca
public class DayNightCycle : MonoBehaviour
{
    public float DayDuration = 3f;
    public Vector3 rotation = Vector3.zero;

    public void Update()
    {
        rotation.x = DayDuration * Time.deltaTime;
        transform.Rotate(rotation, Space.World);
    }
}
