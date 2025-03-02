using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public float speedBullet = 10f; 
    public GameObject bulletPrefab; 
    public Transform spawnBullet; // O ponto de spawn DEVE seguir a rotação do SimpleAim
    public SimpleAim simpleAim;

    public float fireRate = 0.2f; 
    private float nextFireTime = 0; 

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // Remove a condição desnecessária da posição do mouse
        
        var bullet = Instantiate(bulletPrefab, spawnBullet.position, spawnBullet.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        // Usa a direção do ponto de spawn (que deve estar controlado pelo SimpleAim)
        rb.linearVelocity = spawnBullet.up * speedBullet; 
    }
}