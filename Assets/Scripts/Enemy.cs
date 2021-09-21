using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float Health = 50f;
    
    public UnityAction<GameObject> OnEnemyDestroyed = delegate {  };

    private bool _isHit = false;

    private void OnDestroy()
    {
        if (_isHit)
        {
            OnEnemyDestroyed(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.GetComponent<Rigidbody2D>() == null)
        {
            return;
        }

        if (collider.gameObject.tag == "Bird")
        {
            _isHit = true;
            Destroy(gameObject);
        }
        else if (collider.gameObject.tag == "Obstacle")
        {
            // Hitung damage yang diperoleh
            float damage = collider.gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude * 10;

            Health -= damage;

            if (Health <= 0)
            {
                _isHit = true;
                Destroy(gameObject);
            }
        }
    }
}
