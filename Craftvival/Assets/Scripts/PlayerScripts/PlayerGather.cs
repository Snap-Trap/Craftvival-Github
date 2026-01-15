using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGather : MonoBehaviour
{
    // Script made by Charly
    public InputAction gatherAction;

    public float playerGatherRange = 1f;

    private LayerMask objectLayer;

    public float gatherCooldown = 1f; // Cooldown upon hitting correct target
    public float resourceDamage = 1f;
    private bool canGather = true;
    public enum ToolType
    {
        None,
        Axe,
        Pickaxe,
        Universal_Gatherer
    }
    public ToolType toolType;
    private string targetTag; // What said tool targets
    public Vector3 boxSize = new Vector3(0.5f, 1f, 1f); // Size of the boxcast

    public void Awake()
    {
        objectLayer = LayerMask.GetMask("objectLayer");
        SetTargetTag(); //Sets target of whatever tool you're using
    }

    void SetTargetTag()
    {
        if (toolType == ToolType.Axe)
        {
            targetTag = "Tree";
        }
        else if (toolType == ToolType.Pickaxe)
        {
            targetTag = "Rock";
        }
        else if (toolType == ToolType.Universal_Gatherer)
        {
            targetTag = "All";
        }
        else
        {
            targetTag = "None";
        }
    }

    public void Update()
    {
        if (gatherAction.triggered)
        {
            Debug.Log(toolType + " triggered");
            PerformGathering();
        }
    }

    private void PerformGathering()
    {
        if (canGather)
        {
            // Perform the BoxCast
            RaycastHit[] hits = Physics.BoxCastAll(transform.position, boxSize, transform.forward, Quaternion.identity, playerGatherRange, objectLayer);

            // Check if we hit any objects
            if (hits.Length > 0)
            {
                bool hitTarget = false;
                foreach (var hit in hits)
                {
                    // Check if the object has the correct tag based on the tool type
                    if (targetTag == "All" || hit.collider.CompareTag(targetTag))
                    {
                        Debug.Log(gameObject.name + " hit " + hit.collider.gameObject.name + " with a " + toolType);
                        hit.collider.gameObject.GetComponent<IDamagable>().TakeDamage(resourceDamage); // Deal damage to the resource
                        hitTarget = true;
                    }
                }
                if (hitTarget) // If you hit a correct target
                {
                    StartCoroutine(GatherCooldown()); // Start cooldown
                }
            }
            else
            {
                // If no objects were hit
                Debug.Log("Missed swing");
            }          
        }
    }

    private IEnumerator GatherCooldown()
    {
        canGather = false;
        yield return new WaitForSeconds(gatherCooldown);
        Debug.Log(gameObject.name + " gather cooldown has ended");
        canGather = true;
    }

    private void OnDrawGizmos()
    {    
        // Set the color for the gizmos
        Gizmos.color = Color.magenta;

        // Calculate the center of the boxcast
        Vector3 boxCenter = transform.position + transform.forward * playerGatherRange;

        // Set the matrix to apply rotation based on the player's rotation.
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, Quaternion.LookRotation(transform.forward), Vector3.one);

        // Draw the boxcast
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }

    public void OnEnable()
    {
        gatherAction.Enable();
    }

    public void OnDisable()
    {
        gatherAction.Disable();
    }
}
