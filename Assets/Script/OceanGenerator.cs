using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanGenerator : MonoBehaviour {

	int realSize = 64;
	int meshSize = 150;
	float noiseScale = 1;
	float waveScale = 1;
	List<Vector3> vertices;
	List<int> triangles;
	Vector3[,] posArray;
	// Use this for initialization
	void Start () {
		vertices = new List<Vector3>();
		triangles = new List<int>();
		posArray = new Vector3[meshSize,meshSize];

		arrangeVertex(transform.position);
		generateMesh();

		Mesh mesh = new Mesh();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();

		GetComponent<MeshFilter>().mesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}

	void arrangeVertex(Vector3 center){
		float x = center.x - realSize/2;
		float z = center.z - realSize/2;
		for(int i = 0; i < meshSize; i++){
			for(int j = 0; j < meshSize; j++){

				float height = Mathf.PerlinNoise(x*noiseScale,z*noiseScale);
				posArray[i,j] = new Vector3(x, height*waveScale, z);
				x += realSize/(float)meshSize;
			}
			z += realSize/(float)meshSize;
			x = center.x - realSize/2;
		}
	}

	void generateMesh(){
		for(int i =0; i < meshSize-1; i++){
			for(int j=0; j < meshSize-1; j++){
				createSquare(i,j);
			}
		}
	}
	Vertex createVertex(int i, int j){
		Vertex v = new Vertex(posArray[i,j]);
		v.vertexIndex = vertices.Count;
		vertices.Add(v.position);
		return v;
	}

	void createSquare(int i, int j){
		Vertex a = createVertex(i,j);
		Vertex b = createVertex(i+1,j);
		Vertex c = createVertex(i+1,j+1);
		Vertex d = createVertex(i,j+1);
		createTriangle(a,b,c);
		createTriangle(a,c,d);
	}
	void createTriangle(Vertex a, Vertex b, Vertex c){
		triangles.Add(a.vertexIndex);
		triangles.Add(b.vertexIndex);
		triangles.Add(c.vertexIndex);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
