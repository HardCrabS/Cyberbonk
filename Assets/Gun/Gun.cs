using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] int maxAmmo;
    [SerializeField] Text ammoText;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject shootParticle;

    int currAmmo;

    Animator animator;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currAmmo = maxAmmo;
        ammoText.text = maxAmmo.ToString();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && currAmmo < maxAmmo)
        {
            ammoText.text = currAmmo.ToString();
            animator.SetTrigger("Reload");
            currAmmo = maxAmmo;
        }
    }
    public void ShootBullet()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
            return;
        audioSource.Play();
        animator.SetTrigger("Shoot");
        currAmmo--;
        Quaternion quaternion = new Quaternion();
        var part = Instantiate(shootParticle, shootPoint.position, quaternion, transform);
        part.transform.Rotate(90, 0, 0);
        Destroy(part, 3);
        if (currAmmo <= 0)
        {
            ammoText.text = currAmmo.ToString();
            animator.SetTrigger("Reload");
            currAmmo = maxAmmo;
            return;
        }
        ammoText.text = currAmmo.ToString();
    }

    public void ResetReloadText()
    {
        ammoText.text = currAmmo.ToString();
    }
}