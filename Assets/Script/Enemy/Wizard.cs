using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    [SerializeField] Transform firePoint; 
    [SerializeField] GameObject fireball;
    [SerializeField] float fireRate;
    [SerializeField] float shootingTime;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if(Mathf.Abs(direzioneDopo) < 20) //Per farlo sparare solo se è ad una certa distanza
            checkFire();
    }

    private void checkFire()
    {
        if (Time.time > shootingTime)
        {
            shootingTime = Time.time + fireRate / 1000;
            StartCoroutine("Fire");
        }
    }

    protected override IEnumerator Die()
    {
        StopCoroutine("Fire");
        return base.Die();
    }

    IEnumerator Fire()
    {
        animator.SetTrigger("IsFiring");
        yield return new WaitForSeconds(0.8f);
        SoundManager.instance.Play("Fireball");
        Vector2 myPos = new Vector2(firePoint.position.x, firePoint.position.y); 
        Instantiate(fireball, myPos, Quaternion.identity);
    }

}
