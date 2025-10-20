using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class MiniMapFOV : MonoBehaviour
{
    [Header("基础设置")]
    public RectTransform fovIndicator;  // UI指示器
    public float viewAngle = 90f;      // 视野角度
    public float viewRadius = 100f;    // 显示半径
    public int segments = 60;          // 扇形精细度

    [Header("视觉效果")]
    public Material fovMaterialTemplate; // Shader材质模板
    public Color fovColor = new Color(1, 1, 1, 0.3f);
    public float edgeSoftness = 0.1f;

    private Image _fovImage;
    private Material _runtimeMaterial; // 运行时材质实例
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
        // 获取必要组件
        _fovImage = fovIndicator.GetComponent<Image>();
        _canvasRenderer = fovIndicator.GetComponent<CanvasRenderer>();
        _targer = PlayerManager.Instance.player.transform;

        // 禁用默认Image渲染
        _fovImage.raycastTarget = false;
        _fovImage.sprite = null;
    }

    void CreateRuntimeResources()
    {
        // 创建材质实例
        _runtimeMaterial = new Material(fovMaterialTemplate);

        // 初始化Mesh
        _meshCache = new Mesh();
        _meshCache.MarkDynamic(); // 标记为动态更新

        // 配置CanvasRenderer
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
        // 生成顶点数据
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

            // 计算UV坐标
            Vector2 uvPos = new Vector2(
                (dir.x + 1) / 2f,
                (dir.y + 1) / 2f
            );
            uv[i + 1] = uvPos;
        }

        // 生成三角形索引
        int[] triangles = new int[segments * 3];
        for (int i = 0, ti = 0; i < segments; i++)
        {
            triangles[ti++] = 0;
            triangles[ti++] = i + 1;
            triangles[ti++] = i + 2;
        }

        // 更新Mesh
        _meshCache.Clear();
        _meshCache.vertices = vertices;
        _meshCache.uv = uv;
        _meshCache.triangles = triangles;
    }

    void ApplyMaterialSettings()
    {
        // 设置Shader参数
        _runtimeMaterial.SetFloat("_Angle", viewAngle);
        _runtimeMaterial.SetFloat("_EdgeSoftness", edgeSoftness);
        _runtimeMaterial.SetColor("_Color", fovColor);
    }

    void UpdateCanvasRenderer()
    {
        // 更新UI渲染器
        _canvasRenderer.SetMesh(_meshCache);
        _canvasRenderer.SetColor(Color.white);
    }

    void SyncRotation()
    {
        // 同步玩家朝向
        fovIndicator.rotation = Quaternion.Euler(
            0,
            0,
            -_targer.eulerAngles.y
        );
    }
}