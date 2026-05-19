using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VisionConeRenderer : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private int rayCount = 30;   
    [SerializeField] private float coneAlphaColor = 0.3f;

    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private Renderer enemyRenderer;

    private void Awake()
    {
        mesh = new Mesh();
        mesh.name = "VisionCone";
        GetComponent<MeshFilter>().mesh = mesh;
        meshRenderer = GetComponent<MeshRenderer>();

        Material mat = new Material(Shader.Find("Sprites/Default"));
        mat.color = new Color(0f, 1f, 0f, coneAlphaColor);
        meshRenderer.material = mat;

        enemyRenderer = enemy.GetComponent<Renderer>();
        RebuildMesh();
    }

    private void LateUpdate()
    {
        SyncColor();
    }

    private void RebuildMesh()
    {
        float halfAngle = enemy.ViewAngle;  
        float distance = enemy.ViewDistance;
        float stepAngle = (halfAngle * 2f) / rayCount;

        Vector3[] vertices  = new Vector3[rayCount + 2];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i <= rayCount; i++)
        {
            float angle = (-halfAngle + stepAngle * i) * Mathf.Deg2Rad;
            vertices[i + 1] = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * distance;
        }

        for (int i = 0; i < rayCount; i++)
        {
            triangles[i * 3 + 0] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void SyncColor()
    {
        Color bodyColor = enemyRenderer.material.color;
        bodyColor.a = coneAlphaColor;
        meshRenderer.material.color = bodyColor;
    }
}
