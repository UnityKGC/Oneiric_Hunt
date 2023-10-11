using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombine : MonoBehaviour
{
    [SerializeField] MeshFilter[] meshFilters = null;
    [SerializeField] CombineInstance[] combine = null;
    void Start()
    {
        meshFilters = GetComponentsInChildren<MeshFilter>();
        combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            // Optionally, you can disable the original renderers of the child objects
            meshFilters[i].gameObject.SetActive(false);
        }

        // Create a new mesh to hold the combined mesh data
        Mesh combinedMesh = new Mesh();

        // Combine the meshes into the new mesh
        combinedMesh.CombineMeshes(combine, true, true);

        // Optionally, optimize the mesh for better performance
        combinedMesh.Optimize();

        // Create a new GameObject to hold the combined mesh
        GameObject combinedObject = new GameObject("CombinedRoads");

        // Add MeshFilter and MeshRenderer components to the new GameObject
        MeshFilter meshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = combinedObject.AddComponent<MeshRenderer>();

        // Assign the combined mesh to the MeshFilter component
        meshFilter.mesh = combinedMesh;

        // Optionally, assign a material to the MeshRenderer component
        meshRenderer.material = meshFilters[0].GetComponent<MeshRenderer>().sharedMaterial;

        combinedObject.isStatic = true; // 정적으로 했는데, 의미가 있나? 오클루전은 Bake를 해야 진행되는데, 그래도 일단은 static으로 설정.
    }
}
