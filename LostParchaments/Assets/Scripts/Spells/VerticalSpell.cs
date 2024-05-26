using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSpell : MonoBehaviour, ICastable
{
    [SerializeField] private int damage;
    [SerializeField] private float bufferTime;
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
        StartCoroutine(SpellBuffer(point));
        Destroy(gameObject, 2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_transform.position, _transform.localScale);
    }

    IEnumerator SpellBuffer(Vector3 point)
    {
        yield return new WaitForSeconds(bufferTime);
        var temp =Physics.OverlapBox(_transform.position, _transform.localScale, Quaternion.identity,targetableLayer);
        foreach (var hitable in temp)
        {
            if(hitable.TryGetComponent(out IDamageable hit)) hit.OnHit(damage);
        }
        
        var impact =Instantiate(impactParticle, point, Quaternion.identity);
        Destroy(impact, 2f);
    }
}
