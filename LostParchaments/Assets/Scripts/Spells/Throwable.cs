using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    private Transform _target;
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = _target.transform.position;
        _transform.LookAt(targetPos);
        transform.Translate(Vector3.forward * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Particle effect oynat ve pool'a çevir..
        Destroy(this);
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.OnHit(damage);
        }
    }
}
