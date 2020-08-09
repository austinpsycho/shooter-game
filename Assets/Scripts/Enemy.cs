using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public int Health = 3;
    [SerializeField] private float speed = 4f;
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y <= -5) 
            transform.position = new Vector3(Random.Range(-10f,10f), 7);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null || triggers?.Any() != true) return;
        triggers.Where(t=>other.CompareTag(t.Key)).ToList().ForEach(t=>t.Value(other));
    }

    private Dictionary<string, Action<Collider2D>> triggers;

    private void Start()
    {
        transform.position = new Vector3(Random.Range(-10f,10f), 7);
        triggers = new Dictionary<string, Action<Collider2D>>
        {
            {"Player", PlayerHit},
            {"Laser", LaserHit}
        };
    }

    private void PlayerHit(Collider2D other)
    {
        other.GetComponent<Player>()?.ReduceLives();
        Destroy(gameObject);
    }

    private void LaserHit(Collider2D other)
    {
        Destroy(other.gameObject);
        if (--Health == 0) Destroy(gameObject);
    }
}
