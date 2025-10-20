using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class RadarPool : MonoBehaviour
{
    [SerializeField] private GameObject iconPrefab;
    private ObjectPool<GameObject> pool;

    private Dictionary<GameObject, Image> cache = new Dictionary<GameObject, Image>();

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(createFunc: () =>
        {
            GameObject icon = Instantiate(iconPrefab, transform);
            cache.Add(icon, icon.GetComponent<Image>());
            icon.SetActive(false);
            return icon;
        },
            actionOnGet: (icon) =>
            {
                icon.SetActive(true);
            },
            actionOnRelease: (icon) =>
            {
                icon.SetActive(false);
            },
            actionOnDestroy: (icon) => Destroy(icon),
            defaultCapacity: 20
        );
            }

    public GameObject GetIcon(Color color, Vector2 Pos)
    {
        GameObject icon = pool.Get();
        Image image = cache[icon];
        image.color = color;
        image.rectTransform.anchoredPosition = Pos;
        return icon;
    }

    public void ReleaseIcon(GameObject icon)
    {
        pool.Release(icon);
    }

    public Image GetIconImage(GameObject icon)
    {
        if (cache.TryGetValue(icon, out Image image))
        {
            return image;
        }
        return null;
    }
}
