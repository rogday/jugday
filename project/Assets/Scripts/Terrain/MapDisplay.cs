using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    GameObject meshObject;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    Material meshMaterial;

    public void DrawMesh(MeshData meshData, Vector2 position, Transform parent) { 
        meshObject = new GameObject("Chunk");
        meshObject.transform.parent = parent;

        meshRenderer = meshObject.AddComponent<MeshRenderer>();
        meshFilter = meshObject.AddComponent<MeshFilter>();
        meshCollider = meshObject.AddComponent<MeshCollider>();

        meshRenderer.material = meshMaterial;

        meshFilter.sharedMesh = meshData.CreateMesh();
        meshCollider.sharedMesh = meshFilter.sharedMesh;
        meshRenderer.transform.position = new Vector3(position.x, 0, position.y);
        //colorize the shit
    }

    public void UpdateHeights(Material material, float minHeight, float maxHeight) {
        meshMaterial = material;
        meshMaterial.SetFloat("lowestPoint", minHeight);
        meshMaterial.SetFloat("highestPoint", maxHeight);
    }
}
