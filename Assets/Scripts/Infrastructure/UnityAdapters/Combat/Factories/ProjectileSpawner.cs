using UnityEngine;

/// <summary>
/// ProjectileSpawner is responsible for instantiating and launching projectiles from a specified launch point.
/// </summary>

public class ProjectileSpawner : MonoBehaviour, IProjectileSpawner
{
    [SerializeField] public Transform launchPoint;
    [SerializeField] public GameObject projectilePrefab;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        //Debug.Log("[SPAWNER]→ Projectile created: " + projectile.name);
        float direction = transform.localScale.x >= 0 ? 1f : -1f;

        Vector3 scale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(scale.x * direction, scale.y, scale.z);

        if (projectile.TryGetComponent(out IProjectile logic))
        {
            logic.Launch(direction);
        }
    }
}
