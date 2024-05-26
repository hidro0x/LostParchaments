using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Throwable : MonoBehaviour, ICastable
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        //Particle effect oynat ve pool'a çevir..
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.OnHit(damage);
        }
        Destroy(gameObject);
    }

    public void Cast(Vector3 point)
    {
        _transform.LookAt(point); //Sets the projectiles rotation to look at the point clicked
        GetComponent<Rigidbody>().AddForce(_transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
        _transform.SetParent(null);
    }
}
