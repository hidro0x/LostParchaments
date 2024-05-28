using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSpell : MonoBehaviour, ICastable
{
    [SerializeField] private int damage;
    [SerializeField] private float bufferTime;
    public LayerMask targetableLayer;
    public LayerMask castLayer;
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
        _transform.rotation = Quaternion.Euler(-90, 0, 0);
        Debug.DrawRay(point, -Vector3.up, Color.red, 100f);
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(point.x,point.y+1, point.z), -Vector3.up, out hit, 500f, castLayer))
        {
            _transform.position = hit.point;
            StartCoroutine(SpellBuffer(point));
            Destroy(gameObject, 2f);
        }
        
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
        
        var impact =Instantiate(impactParticle, new Vector3(point.x,point.y+1, point.z), Quaternion.identity);
        Destroy(impact, 2f);
    }
}