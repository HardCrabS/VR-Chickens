using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
}

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] float health = 100;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootPoint;
    [SerializeField] float attackRadius = 10f;
    [SerializeField] float minTimeShot = 2;
    [SerializeField] float maxTimeShot = 5;

    float currBetweenShots = 3;
    float lastTimeShot = 0;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RotateOnPlayer();
        lastTimeShot += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if(distanceToPlayer < attackRadius && lastTimeShot > currBetweenShots)
        {
            ShootAtPlayer();
            lastTimeShot = 0;
            currBetweenShots = Random.Range(minTimeShot, maxTimeShot);
        }
    }

    void ShootAtPlayer()
    {
        GameObject go = Instantiate(projectile, shootPoint.position, transform.rotation);
        Vector3 playerDir = player.transform.position - transform.position;
        //go.GetComponent<Rigidbody>().velocity = shootPoint.transform.TransformDirection(playerDir).normalized;
        go.GetComponent<Rigidbody>().velocity = playerDir.normalized;
        Destroy(go, 4);
    }

    void RotateOnPlayer()
    {
        transform.LookAt(player.transform);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}