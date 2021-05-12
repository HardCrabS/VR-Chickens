using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 50;
    [SerializeField] float damage = 30;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity *= bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            print("I hit damageable " + other.gameObject.name);
        }
        Destroy(gameObject);
    }
}
