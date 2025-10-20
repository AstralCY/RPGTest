using UnityEngine;

public class WeaponSpear : Weapon
{
    private GameObject bulletPrefab;
    private MeshRenderer meshRenderer;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Weapons/SpearBullet");
    }

    public override void Attack()
    {
        if (meshRenderer.enabled)
        {
            meshRenderer.enabled = false;
            GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<SpearBullet>().InitSpearBullet(this.GetValue(ItemData.ItemPropertyType.Speed));
            Invoke(nameof(Show), 0.5f);
        }
    }

    private void Show()
    {
        meshRenderer.enabled = true;
    }
}
