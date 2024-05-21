using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    

    private void FixedUpdate()
    {
        _transform.Translate(Vector3.forward * (Time.deltaTime * speed * 2));
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
}
