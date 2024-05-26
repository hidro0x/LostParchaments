using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSpell : MonoBehaviour, ICastable
{
    [SerializeField] private int damage;
    public LayerMask targetableLayer;
    public GameObject impactParticle;
    
    public float radius = 5.0F;
    public float power = 10.0F;
    
    private Transform _transform;
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    

    public void Cast(Entity entity,Vector3 point)
    {
        _transform.position = point;
        _transform.rotation = Quaternion.Euler(-90, 0, 0);
        var temp =Physics.OverlapBox(_transform.position, _transform.localScale*3, Quaternion.identity,targetableLayer);
        foreach (var hitable in temp)
        {
            if(hitable.TryGetComponent(out IDamageable hit)) hit.OnHit(damage);
        }

        
        var impact =Instantiate(impactParticle, point, Quaternion.identity);
        Destroy(impact, 2f);
        Destroy(gameObject, 2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_transform.position, _transform.localScale);
    }
}
