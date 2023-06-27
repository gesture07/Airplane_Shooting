using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform bulletSpawnPoint; // Reference to the spawn point of the bullet
    public float bulletSpeed = 10f;

    public void FireBullet()
    {
        // Instantiate a new bullet at the bullet spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        transform.Translate(Vector3.down * bulletSpeed);
        // Add velocity to the bullet in the forward direction of the spawn point
        // Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        // bulletRigidbody.velocity = bulletSpawnPoint.up * bulletSpeed;
    }
}



