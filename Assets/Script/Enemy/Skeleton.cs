using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] float speed;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Moving"))
        {

            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            base.Update();
        }
    }
}
