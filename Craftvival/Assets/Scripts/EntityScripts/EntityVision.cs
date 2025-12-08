using System.Collections;
using UnityEngine;

public class EntityVision : BaseEntity, IDetect, IEntity
{
    // Luca

    // Inherit van BaseEntity om toegang te krijgen tot vrijwel alles
    private BaseEntity parent;
    private BaseEntitySO entityStats;

    public void Initialize(BaseEntitySO entityData)
    {
        entityStats = entityData;
    }

    void Awake()
    {
        parent = GetComponent<BaseEntity>();
        StartCoroutine(VisionRoutine());
    }

    // IEnumerator zodat dit niet elke frame wordt gedaan om performance te sparen
    public IEnumerator VisionRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            VisionCheck();
        }
    }

    public void VisionCheck()
    {
        // Pakt alle colliders die op de "playerLayer" zit
        Collider[] rangeChecks = Physics.OverlapSphere(parent.transform.position, entityStats.visionRange, playerLayer);

        // Als er NIET niks is, gaat ie verder. De player hoort als ENIGSTE op de playerLayer te zijn
        if (rangeChecks.Length != 0)
        {
            // Target transform wordt het eerste in de rangeChecks lijst, wat ALTIJD de player hoort te zijn
            Transform target = rangeChecks[0].transform;

            // Pakt de richting van de player
            Vector3 directionTarget = (target.position - parent.transform.position).normalized;

            // Pakt de Angle via WISKUNDE en doet het gedeeld door 2 want links + rechts = cone
            // Heeft 2 canSeePlayer = false nodig (IN DEZE BLOK) omdat de speler in de Angle kan zijn maar alsnog kan er een object tussen de player en entity zijn
            if (Vector3.Angle(parent.transform.position, directionTarget) < entityStats.coneAngle / 2)
            {
                // Berekent de distance tussen zichzelf en de target, de target moet de player zijn
                float distanceTarget = Vector3.Distance(parent.transform.position, target.position);

                // Maakt een Raycast, als de Raycast NIET buiten de distance is, of iets aanraakt vanuit de objectLayer, dan kan die de player zien.
                if (!Physics.Raycast(parent.transform.position, directionTarget, distanceTarget, objectLayer))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        // Als de entity de player zag, maar nu is de player buiten range, dan kan die de player niet meer zien
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
}
