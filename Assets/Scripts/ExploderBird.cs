using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderBird : Bird
{
    public bool CanExplode = false;
    public LayerMask LayerToHit;

    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void ExplodeBirdAt()
    {
        if (State == BirdState.HitSomething && !CanExplode)
        {
            CanExplode = true;
        }

        Collider2D[] objectsInRadius = Physics2D.OverlapCircleAll(transform.position,
            _explosionRadius, LayerToHit);

        foreach (Collider2D collider in objectsInRadius)
        {
            Vector2 direction = collider.transform.position - transform.position;
            collider.GetComponent<Rigidbody2D>().AddForce(direction * _explosionForce);
        }
        
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }

    public override void OnHitSomething()
    {
        ExplodeBirdAt();
    }
}
