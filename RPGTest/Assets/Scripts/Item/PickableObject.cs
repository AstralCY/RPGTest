using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : InteractableObject
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private ItemData itemData;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

    }

    public void CopyDetail(ItemData targerItemData)
    {
        itemData = targerItemData;

        MeshFilter meshFilterSource = targerItemData.itemPrefab.GetComponentInChildren<MeshFilter>();
        MeshRenderer meshRendererSource = targerItemData.itemPrefab.GetComponentInChildren<MeshRenderer>();
        Transform transformSource = targerItemData.itemPrefab.GetComponentInChildren<Transform>();

        meshFilter.sharedMesh = meshFilterSource.sharedMesh;
        meshRenderer.materials = meshRendererSource.sharedMaterials;
        meshCollider.sharedMesh = meshFilterSource.sharedMesh;
        transform.localScale = transformSource.localScale * 2;

    }

    public override void Interact()
    {
        Pickup();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        if (itemData == null) { print("没有物品"); return; }
        Inventory.Instance.AddItem(itemData);
        Destroy(this.gameObject);
    }
}
