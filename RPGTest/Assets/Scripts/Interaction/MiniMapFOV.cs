using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class MiniMapFOV : MonoBehaviour
{
    [Header("��������")]
    public RectTransform fovIndicator;  // UIָʾ��
    public float viewAngle = 90f;      // ��Ұ�Ƕ�
    public float viewRadius = 100f;    // ��ʾ�뾶
    public int segments = 60;          // ���ξ�ϸ��

    [Header("�Ӿ�Ч��")]
    public Material fovMaterialTemplate; // Shader����ģ��
    public Color fovColor = new Color(1, 1, 1, 0.3f);
    public float edgeSoftness = 0.1f;

    private Image _fovImage;
    private Material _runtimeMaterial; // ����ʱ����ʵ��
    private Mesh _meshCache;
    private CanvasRenderer _canvasRenderer;
    private Transform _targer;

    void Start()
    {
        InitializeComponents();
        CreateRuntimeResources();
        UpdateVisuals();
    }

    void LateUpdate()
    {
        SyncRotation();
    }

    void InitializeComponents()
    {
        // ��ȡ��Ҫ���
        _fovImage = fovIndicator.GetComponent<Image>();
        _canvasRenderer = fovIndicator.GetComponent<CanvasRenderer>();
        _targer = PlayerManager.Instance.player.transform;

        // ����Ĭ��Image��Ⱦ
        _fovImage.raycastTarget = false;
        _fovImage.sprite = null;
    }

    void CreateRuntimeResources()
    {
        // ��������ʵ��
        _runtimeMaterial = new Material(fovMaterialTemplate);

        // ��ʼ��Mesh
        _meshCache = new Mesh();
        _meshCache.MarkDynamic(); // ���Ϊ��̬����

        // ����CanvasRenderer
        _canvasRenderer.materialCount = 1;
        _canvasRenderer.SetMaterial(_runtimeMaterial, 0);
    }

    void UpdateVisuals()
    {
        GenerateFOVMesh();
        ApplyMaterialSettings();
        UpdateCanvasRenderer();
    }

    void GenerateFOVMesh()
    {
        // ���ɶ�������
        Vector3[] vertices = new Vector3[segments + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        vertices[0] = Vector3.zero;
        uv[0] = new Vector2(0.5f, 0.5f);

        float startAngle = -viewAngle / 2f;
        float angleIncrement = viewAngle / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = startAngle + angleIncrement * i;
            Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.up;
            vertices[i + 1] = dir * viewRadius;

            // ����UV����
            Vector2 uvPos = new Vector2(
                (dir.x + 1) / 2f,
                (dir.y + 1) / 2f
            );
            uv[i + 1] = uvPos;
        }

        // ��������������
        int[] triangles = new int[segments * 3];
        for (int i = 0, ti = 0; i < segments; i++)
        {
            triangles[ti++] = 0;
            triangles[ti++] = i + 1;
            triangles[ti++] = i + 2;
        }

        // ����Mesh
        _meshCache.Clear();
        _meshCache.vertices = vertices;
        _meshCache.uv = uv;
        _meshCache.triangles = triangles;
    }

    void ApplyMaterialSettings()
    {
        // ����Shader����
        _runtimeMaterial.SetFloat("_Angle", viewAngle);
        _runtimeMaterial.SetFloat("_EdgeSoftness", edgeSoftness);
        _runtimeMaterial.SetColor("_Color", fovColor);
    }

    void UpdateCanvasRenderer()
    {
        // ����UI��Ⱦ��
        _canvasRenderer.SetMesh(_meshCache);
        _canvasRenderer.SetColor(Color.white);
    }

    void SyncRotation()
    {
        // ͬ����ҳ���
        fovIndicator.rotation = Quaternion.Euler(
            0,
            0,
            -_targer.eulerAngles.y
        );
    }
}