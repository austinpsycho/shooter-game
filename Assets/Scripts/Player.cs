using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float fireRate = 0.15f;
    [SerializeField] 
    private float speed = 15f;
    [SerializeField]
    private GameObject laser;
    [SerializeField] private int lives = 3;
    private float nextFire = 0;
    private GameObject weapon;
    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    void Update()
    {
        CalculateMovement();
        UpdateLaser();
        if (lives == 0) Destroy(gameObject);
    }

    private void UpdateLaser()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || !(Time.time > nextFire)) return;
        Instantiate(weapon ?? laser, transform.position + new Vector3(0,.8f,0), Quaternion.identity);
        nextFire = Time.time + fireRate;
    }

    private void CalculateMovement()
    {
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");
        if (vInput > 0 && transform.position.y >= 0) vInput = 0;
        if (vInput < 0 && transform.position.y <= -4) vInput = 0;
        var position = transform.position;
        var x = position.x;
        if (x < -11.3 || x > 11.3) x =-1f * x / Mathf.Abs(x) * 11.3f;
        var y = Mathf.Clamp(position.y, -4, 0);
        transform.position = new Vector3(x,y,0);
        transform.Translate(new Vector3(hInput, vInput, 0) * speed * Time.deltaTime);
    }

    public void ReduceLives()
    {
        lives--;
    }

    public void SetWeapon(GameObject weapon)
    {
        this.weapon = weapon;
    }

    public void UnsetWeapon()
    {
        this.weapon = null;
    }
}
