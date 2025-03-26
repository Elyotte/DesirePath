using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class GrassBunch : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] bool showVertices;
    [SerializeField] bool showVerticesCoordinates;

    [Header("Grass bunch settings")]
    [SerializeField] float height = 1f;
    [SerializeField] float width = 1f;
    [SerializeField] int segmentNum = 3;

    Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {
        // Adjust height of the grass
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit lRayInfo);

        if (lRayInfo.collider != null)
            transform.position = lRayInfo.point;

        // generate new mesh
        Mesh lMesh = new Mesh();
        MeshFilter lMeshFilter = GetComponent<MeshFilter>();

        int[] lVertices = new int[15];
        Vector3[] lVerticesCoordinates = new Vector3[ 1 + (segmentNum *2)];

        Vector3 lStartPosition = Vector3.right * (-.5f * width);
        Vector3 lOffset = lStartPosition;
        float lheigthStep = height / segmentNum;

        int lPlaceCoordinateIteration = segmentNum * 2;

        // Populate vertices coordinates vector array
        for (int i = 0; i < lPlaceCoordinateIteration; i+= 2)
        {
            // Place triangles
            lVerticesCoordinates[i] = lOffset;
            lVerticesCoordinates[i + 1] = lOffset + (transform.right * width);

            // Change offset position
            lOffset += transform.up * lheigthStep;

            print(i);
            print(lOffset);
        }

        // Place last coordinate
        lVerticesCoordinates[segmentNum * 2] = transform.up * height;

        vertices = lVerticesCoordinates;

        //lVertices[0] = 0;
        //lVertices[1] = 1;
        //lVertices[2] = 2;

        // Trace triangles using vertices coordinates array
        for (int i = 0; i < 5; i++)
        {
            lVertices[i] = i;
            lVertices[i +1] = i + 1;
            lVertices[i +2] = i + 2;
            //print(i);
        }


        lMesh.vertices = lVerticesCoordinates;
        lMesh.triangles = lVertices;


        lMesh.RecalculateBounds();
        lMesh.RecalculateNormals();
        lMesh.RecalculateTangents();

        lMeshFilter.mesh = lMesh;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnDrawGizmos()
    {
        if (!showVertices || vertices == null)
            return;

        Vector3 vertex;

        for (int i = 0; i < vertices.Length; i++)
        {

            vertex = transform.TransformPoint(vertices[i]);
            if (showVerticesCoordinates)
                UnityEditor.Handles.Label(vertex, "V" + i.ToString() + "  " + vertex.ToString(), new GUIStyle() { fontSize = 15 });
            else
                UnityEditor.Handles.Label(vertex, "V" + i.ToString(), new GUIStyle() { fontSize = 15 });
        }
    }
}
