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
    private Rigidbody _rb;
    private bool _casting;
    public GameObject projectileParticle;
    public GameObject impactParticle;
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        projectileParticle = Instantiate(projectileParticle, _transform.position, _transform.rotation);
        projectileParticle.transform.SetParent(_transform);
    }

    private void FixedUpdate()
    {
        //if(_casting) _transform.Translate(Vector3.forward * (Time.deltaTime * speed * 2));
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Particle effect oynat ve pool'a çevir..
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.OnHit(damage);
        }
        
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        var particle =Instantiate(impactParticle, pos, rot);
        particle.transform.SetParent(null);
        Destroy(particle, 1f);
        
        Destroy(gameObject);
    }

    public void Cast(Vector3 point)
    {
        transform.LookAt(point);
        //_transform.DOLookAt(point, 0f, AxisConstraint.Y); //Sets the projectiles rotation to look at the point clicked
        GetComponent<Rigidbody>().AddForce(_transform.forward * speed * 100); //Set the speed of the projectile by applying force to the rigidbody
        _transform.SetParent(null);
        _casting = true;
    }
}
