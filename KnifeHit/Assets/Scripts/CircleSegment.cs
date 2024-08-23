using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CircleSegment : MonoBehaviour
{
    public int segments = 8;
    public Material segmentMaterial;

    [Button()]
    void segement()
    {
        float angle = 0f;
        float angleStep = 360f / segments;
        Vector2 center = transform.position;

        for (int i = 0; i < segments; i++)
        {
            GameObject segment = new GameObject("Segment" + i);
            segment.transform.parent = transform;

            var segmentCollider = segment.AddComponent<PolygonCollider2D>();
            var segmentRenderer = segment.AddComponent<SpriteRenderer>();

            Vector2[] segmentVertices = new Vector2[3];
            segmentVertices[0] = center;
            segmentVertices[1] = center + new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            angle += angleStep;
            segmentVertices[2] = center + new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));

            segmentCollider.points = segmentVertices;

            segmentRenderer.material = segmentMaterial;

            segment.transform.position = transform.position;
            segment.transform.rotation = transform.rotation;
        }
    }
}
