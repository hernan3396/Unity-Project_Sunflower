using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class FieldOfView : MonoBehaviour
{
    #region Components
    [SerializeField] LayerMask layerMask;
    private Mesh mesh;
    #endregion

    #region FOVConfig
    [SerializeField] float viewDistance = 9f;
    [SerializeField] int rayCount = 100;
    [SerializeField] float fov = 360f;
    float angleIncrease => fov / rayCount;
    Vector3 origin = Vector3.zero;
    float currentAngle = 0f;
    #endregion

    private void Start()
    {
        // create & assign mesh
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void LateUpdate()
    {
        origin = transform.position;
        currentAngle = 0f;

        // create triangle properties
        Vector3[] vertices = new Vector3[rayCount + 2]; // +1 for origin +1 for ray 0
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D ray = Physics2D.Raycast(origin, GetVectorFromAngle(currentAngle), viewDistance, layerMask);
            // Debug.DrawRay(origin, GetVectorFromAngle(currentAngle) * viewDistance, Color.blue);

            if (ray.collider != null)
            {
                vertex = ray.point;
            }
            else
            {
                vertex = origin + (GetVectorFromAngle(currentAngle) * viewDistance);
            }

            vertices[vertexIndex] = transform.InverseTransformPoint(vertex.x, vertex.y, vertex.z);

            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            currentAngle -= angleIncrease; // lo restas para que sea sentido horario
            vertexIndex += 1;
        }

        // assign values
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * Mathf.PI / 180; // cos and sin needs radians instead of degrees
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad)); ;
    }

    public void SetOrigin(Vector3 newOrigin)
    {
        origin = newOrigin;
    }
}
