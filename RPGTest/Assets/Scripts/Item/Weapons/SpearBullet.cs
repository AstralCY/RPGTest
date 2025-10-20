using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBullet : WeaponSpear
{
    private Rigidbody rb;
    private Collider col;
    private int speed;


    public void InitSpearBullet(int _speed)
    {
        this.speed = _speed;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rb.velocity = transform.forward * speed;
        Destroy(this.gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            rb.Sleep();
            col.enabled = false;

            Destroy(this.gameObject, 1f);
        }
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            PlayerManager.Instance.status.DoDamage(enemy.status);
        }
    }
}
