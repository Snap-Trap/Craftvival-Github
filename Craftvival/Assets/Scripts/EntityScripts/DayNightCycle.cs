using UnityEngine;

// Creator: Luca
public class DayNightCycle : MonoBehaviour
{
    public float DaySpeed;
    public float NightSpeed;

    public bool isDay = true;
    public bool isNight = false;
    private Vector3 rotation = Vector3.zero;

    public void Update()
    {
        CycleCheck();
        rotation.x = (isDay ? DaySpeed : NightSpeed) * Time.deltaTime;
        transform.Rotate(rotation, Space.World);
    }

    public void CycleCheck()
    {
        if (transform.rotation.eulerAngles.x >= 180f && isDay)
        {
            isDay = false;
            isNight = true;
        }
        else if (transform.rotation.eulerAngles.x < 180f && isNight)
        {
            isDay = true;
            isNight = false;
        }
    }
}
