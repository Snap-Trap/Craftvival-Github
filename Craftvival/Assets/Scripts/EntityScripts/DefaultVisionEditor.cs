using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DefaultVision))]

// Creator: Luca
// L: Vraag me NIET waarom dit werkt, maar het werkt wel
public class DefaultVisionEditor : Editor
{
    private void OnSceneGUI()
    {
        DefaultVision fov = (DefaultVision)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.visionRange);

        Vector3 viewAngleA = DirectionAngle(fov.coneAngle / 2, fov.transform.eulerAngles.y);
        Vector3 viewAngleB = DirectionAngle(-fov.coneAngle / 2, fov.transform.eulerAngles.y);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.visionRange);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.visionRange);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.transform.position + (fov.transform.forward * fov.visionRange));
        }
    }

    private Vector3 DirectionAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
