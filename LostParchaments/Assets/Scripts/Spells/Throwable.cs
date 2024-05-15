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

    private void OnEnable()
    {
        _target = TargetSelector.Instance._targetInfo.Transform;
        Vector3 targetPostition = new Vector3( _target.position.x, transform.position.y, _target.position.z ) ;
        transform.LookAt( targetPostition ) ;
    }

    private void OnDisable()
    {
        _target = null;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        _transform.Translate(Vector3.forward * speed);
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
