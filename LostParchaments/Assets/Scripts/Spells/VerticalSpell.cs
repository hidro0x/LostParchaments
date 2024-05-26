using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSpell : MonoBehaviour, ICastable
{
    [SerializeField] private int damage;
    public LayerMask targetableLayer;
    public GameObject impactParticle;
    
    private Transform _transform;
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    

    public void Cast(Vector3 point)
    {
        _transform.position = point;
        _transform.rotation = Quaternion.Euler(-90, 0, 0);
        var temp =Physics.OverlapBox(_transform.position, _transform.localScale, Quaternion.identity,targetableLayer);
        foreach (var hitable in temp)
        {
            if(hitable.transform.TryGetComponent(out IDamageable hit)) hit.OnHit(damage);
        }

        var impact =Instantiate(impactParticle, point, Quaternion.identity);
        Destroy(gameObject, 2f);
    }
}
