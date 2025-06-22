using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint; // Optional: specify a launch point if needed
    public GameObject projectilePrefab;
    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        Debug.Log("→ Flecha creada: " + projectile.name);

        Vector3 origScale =projectile.transform.localScale;

        float direction = transform.localScale.x >= 0 ? 1f : -1f;
        projectile.transform.localScale = new Vector3(
            origScale.x * direction,
            origScale.y,
            origScale.z
        );
    }
}
