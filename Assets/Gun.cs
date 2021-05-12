using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] float timeBetweenShots = 1;
    [SerializeField] int maxAmmo;

    [SerializeField] Text ammoText;

    int currAmmo;
    float lastTimeShot = 0;
    float centerX, centerY;

    MicrophoneInput microphoneInput;
    Animator animator;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        microphoneInput = GetComponentInParent<MicrophoneInput>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        microphoneInput.OnScream += ShootBullet;
        currAmmo = maxAmmo;
        ammoText.text = maxAmmo.ToString();

        centerX = Screen.width * 0.5f;
        centerY = Screen.height * 0.5f;

        //scope.localPosition = new Vector3(centerX, centerY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        lastTimeShot += Time.deltaTime;
    }

    Vector3 currShotPoint;
    private void ShootBullet()
    {
        if (lastTimeShot >= timeBetweenShots)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            // Check whether your are pointing to something so as to adjust the direction
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(1000); // You may need to change this value according to your needs

            audioSource.Play();

            currShotPoint = targetPoint;

            GameObject projectile = Instantiate(bullet, shotPoint.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().velocity = 
                (targetPoint - shotPoint.position).normalized;
            Destroy(projectile, 5);

            animator.SetTrigger("Shoot");
            currAmmo--;
            if(currAmmo <= 0)
            {
                ammoText.text = currAmmo.ToString();
                animator.SetTrigger("Reload");
                currAmmo = maxAmmo;
                lastTimeShot = 0;
                return;
            }
            ammoText.text = currAmmo.ToString();
            lastTimeShot = 0;
        }
    }

    public void ResetReloadText()
    {
        ammoText.text = currAmmo.ToString();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currShotPoint, 0.5f);
    }
}
