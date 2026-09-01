using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(EdgeCollider2D))]
public class TerrainGenerator : MonoBehaviour
{
    [Header("Terrain Dimensions")]
    public int width = 100;               // Total horizontal segments
    public float segmentWidth = 0.5f;     // Distance between each point
    public float baseHeight = 2f;         // Minimum height of the ground
    
    [Header("Wave Settings")]
    public float waveFrequency = 0.1f;    // Controls how wide the hills are
    public float waveAmplitude = 1.5f;    // Controls how high the hills are

    private MeshFilter meshFilter;
    private EdgeCollider2D edgeCollider;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        // 1. Calculate how many vertices we need (Top row + Bottom row)
        int vertexCount = (width + 1) * 2;
        Vector3[] vertices = new Vector3[vertexCount];
        Vector2[] uv = new Vector2[vertexCount];
        
        // This array tracks just the top surface points for our 2D physics collider
        Vector2[] colliderPoints = new Vector2[width + 1];

        // 2. Generate vertex positions using a Sine Wave
        for (int i = 0; i <= width; i++)
        {
            float x = i * segmentWidth;
            
            // The classic math function to make smooth, clean hills
            float yTop = baseHeight + Mathf.Sin(x * waveFrequency) * waveAmplitude;
            float yBottom = 0f; // Flat bottom at the very base of the screen

            // Top Vertex
            vertices[i * 2] = new Vector3(x, yTop, 0);
            uv[i * 2] = new Vector2((float)i / width, 1);
            colliderPoints[i] = new Vector2(x, yTop); // Save for physics

            // Bottom Vertex directly underneath
            vertices[i * 2 + 1] = new Vector3(x, yBottom, 0);
            uv[i * 2 + 1] = new Vector2((float)i / width, 0);
        }

        // 3. Stitch the vertices together into triangles (2 triangles per square segment)
        int triangleCount = width * 6;
        int[] triangles = new int[triangleCount];
        int tIdx = 0;

        for (int i = 0; i < width; i++)
        {
            int topLeft = i * 2;
            int bottomLeft = i * 2 + 1;
            int topRight = (i + 1) * 2;
            int bottomRight = (i + 1) * 2 + 1;

            // Triangle 1
            triangles[tIdx++] = topLeft;
            triangles[tIdx++] = topRight;
            triangles[tIdx++] = bottomLeft;

            // Triangle 2
            triangles[tIdx++] = bottomLeft;
            triangles[tIdx++] = topRight;
            triangles[tIdx++] = bottomRight;
        }

        // 4. Create and assign the final Mesh object
        Mesh mesh = new Mesh();
        mesh.name = "GeneratedTerrain";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;

        // 5. Update the solid physical boundary line
        edgeCollider.points = colliderPoints;
    }
}
