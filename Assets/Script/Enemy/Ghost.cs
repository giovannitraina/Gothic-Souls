using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Ghost : Enemy
{
    private Vector2 posizioneFinale1, posizioneFinale2;
    private bool andata = true, ritorno = true, check=false;
    [SerializeField] float speed;

    protected override void Start()
    {
        base.Start();
        posizioneFinale1 = transform.GetChild(0).position;
        posizioneFinale2 = transform.GetChild(1).position;
        StartCoroutine("GhostMove");
    }

    void FixedUpdate()
    {
        if (check)
        {
            check = false;
            andata = true;
            ritorno = true;
            StartCoroutine("GhostMove");
        }
    }
  
    IEnumerator GhostMove()
    {
        while (andata)
        {
            transform.position = Vector2.MoveTowards(transform.position, posizioneFinale1, speed*Time.deltaTime);
      
            if(transform.position.y == posizioneFinale1.y)
            {
                andata = false;
            }
            yield return null;
        }

        while (ritorno)
        {
            transform.position = Vector2.MoveTowards(transform.position, posizioneFinale2, speed*Time.deltaTime);

            if(transform.position.y == posizioneFinale2.y)
            {
                ritorno = false;
            }
            yield return null;
        }

        check = true;
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            SoundManager.instance.Play("GhostDead");
            StartCoroutine("Die");
        }
    }
}
