using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chicken : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 300;
    [SerializeField] float speed = 300f;
    [SerializeField] float spotPlayerRadius = 10;
    [SerializeField] float attackRadius = 2f;
    [SerializeField] float damage = 50;
    [SerializeField] float attackSpeed = 1;
    [SerializeField] int angerPossibility = 80;

    [SerializeField] AudioClip[] hitAudio;

    GameObject player;
    Animator animator;
    Rigidbody myRb;
    Slider healthSlider;

    float lastAttack = 0;
    float currHealth;
    bool isAngry = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        myRb = GetComponent<Rigidbody>();
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        currHealth = maxHealth;

        SetAnger();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAngry)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < spotPlayerRadius)
            {
                animator.SetBool("Run", true);
                if (Vector3.Distance(player.transform.position, transform.position) > attackRadius)
                {
                    myRb.velocity = (player.transform.position - transform.position).normalized * speed * Time.deltaTime;
                }
                else
                    AttackPlayer();

                transform.LookAt(player.transform);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        int randIndex = Random.Range(0, hitAudio.Length);
        AudioSource.PlayClipAtPoint(hitAudio[randIndex], transform.position);

        animator.SetBool("Run", true);
        isAngry = true;
        currHealth -= damage;

        healthSlider.value = currHealth;
        spotPlayerRadius += spotPlayerRadius;

        if (currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void AttackPlayer()
    {
        if (lastAttack > attackSpeed)
        {
            player.GetComponent<IDamageable>().TakeDamage(damage);
            lastAttack = 0;
        }
        lastAttack += Time.deltaTime;
    }

    void SetAnger()
    {
        int chance = Random.Range(0, 101);

        isAngry = chance < angerPossibility ? true : false;
    }

    /*void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, spotPlayerRadius);
    }*/
}
