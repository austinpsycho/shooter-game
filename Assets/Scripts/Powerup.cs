using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Powerup : MonoBehaviour
{
    [SerializeField] private GameObject tripleShot;
    [SerializeField] private float speed = 3f;

    void Start()
    {
        transform.position = new Vector3(Random.Range(-10f,10f), 7);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.CompareTag("Player")) return;
        transform.Translate(Vector3.left*15);
        var player = other.transform.GetComponent<Player>();
        player.SetWeapon(tripleShot);
        StartCoroutine(StartCooldown(player));
    }

    private IEnumerator StartCooldown(Player player)
    {
        yield return new WaitForSeconds(10f);
        player.UnsetWeapon();
        yield return null;
        Destroy(gameObject);
    }
    
}
