using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RakeRing : WeaponRake
{
    private int speed;

    public void InitRing(int _speed)
    {
        this.speed = _speed;
        this.transform.localScale = Vector3.one * 0.1f;
    }
    private void Update()
    {
        this.transform.localScale += new Vector3(speed * Time.deltaTime, 0, speed * Time.deltaTime);

        if (transform.localScale.x > 10f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            PlayerManager.Instance.status.DoDamage(enemy.status);
        }
    }
}
