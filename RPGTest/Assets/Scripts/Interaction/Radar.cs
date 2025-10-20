using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    public RectTransform miniMap;
    public float radius;
    public RadarPool pool;
    public float scanTime = 0.2f;
    [SerializeField] private LayerMask radarLayers ;

    private Dictionary<Collider, GameObject> activeIcon = new Dictionary<Collider, GameObject>();

    private void Start()
    {
        StartCoroutine(CheckObjects());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private IEnumerator CheckObjects()
    {
        while (true)
        {
            RaderCheck();
            yield return new WaitForSeconds(scanTime);
        }
    }

    private void RaderCheck()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, radarLayers);
        HashSet<Collider> currentColliders = new HashSet<Collider>(colliders);
        List<Collider> toRemove = new List<Collider>();
        foreach (var icon in activeIcon)
        {
            Collider col = icon.Key;
            if (col == null || !currentColliders.Contains(col))
            {
                pool.ReleaseIcon(icon.Value);
                toRemove.Add(col);
            }
        }

        foreach (var col in toRemove)
        {
            activeIcon.Remove(col);
        }

        foreach (Collider collider in colliders)
        {
            if (collider == null) continue;
            Vector3 offset = collider.transform.position - transform.position;
            Vector2 dis = NormalizePos(offset);
            Vector2 uv = ToUV(dis);
            Vector2 uiPos = GetUIPos(uv);
            Color color = collider.CompareTag("Enemy") ? Color.red : Color.yellow;
            if (activeIcon.TryGetValue(collider, out GameObject icon))
            {
                Image image = pool.GetIconImage(icon);
                if (image != null)
                {
                    image.rectTransform.anchoredPosition = uiPos;
                    image.color = color;
                }
            }
            else
            {
                GameObject newIcon = pool.GetIcon(color, uiPos);
                activeIcon.Add(collider, newIcon);
            }
        }
    }



    private Vector2 NormalizePos(Vector3 worldPos)
    {
        return new Vector2(worldPos.x / radius, worldPos.z / radius);
    }

    private Vector2 ToUV(Vector2 dis)
    {
        float u = (dis.x + 1f) * 0.5f;
        float v = (dis.y + 1f) * 0.5f;
        return new Vector2(u, v);
    }

    private Vector2 GetUIPos(Vector2 uv)
    {
        float x = uv.x * miniMap.rect.width - miniMap.rect.width * 0.5f;
        float y = uv.y * miniMap.rect.height - miniMap.rect.height * 0.5f;
        return new Vector2(x, y);
    }
}
