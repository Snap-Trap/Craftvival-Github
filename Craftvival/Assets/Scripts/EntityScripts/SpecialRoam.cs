using UnityEngine;

public class SpecialRoam : DefaultRoam
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    override public void Update()
    {
        Debug.Log("Special Roam Update called");
    }
}
