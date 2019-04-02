using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
	public int mapSize = 30;
	[SerializeField]
	Vector2 posOffset;
	[SerializeField]
	float mapHeight = 3.0f;
	[SerializeField]
	float mapStretch = 1.0f;
	[SerializeField]
	float lakeStart = 0.4f;
	[SerializeField]
	float lakeDepth = 2.0f;
	[SerializeField]
	GameObject[] trees;
	[SerializeField]
	[Range(1,100)]
	int treeCount = 20;
	[SerializeField]
	int treeDensity = 16;

    // Start is called before the first frame update
    public void Start()
    {
		Random.InitState((int)(posOffset.x + posOffset.y * transform.position.x + transform.position.y));

		Mesh mesh = GetComponent<MeshFilter>().mesh;

		Vector3 objPos = transform.position;
		int[] triangles = new int[(mapSize-1) * (mapSize-1) * 6];
		Vector3[] verts = new Vector3[mapSize * mapSize];
		Vector2[] uv = new Vector2[verts.Length];
		int numTri = 0;

		transform.position = new Vector3(transform.position.x, lakeDepth*(-mapHeight/2),transform.position.z);

		for(int y = 0;y<mapSize;y+=1){
			for(int x = 0;x<mapSize;x+=1){
				float perlin = Mathf.PerlinNoise((x+posOffset.x+objPos.x)*mapStretch,(y+posOffset.y+objPos.z)*mapStretch);

				if(perlin>lakeStart){
					perlin*=lakeDepth;
				}

				perlin*=mapHeight;

				if(x%treeDensity == 0 && y%treeDensity == 0 && perlin / mapHeight > lakeStart + 1.0f){
					int treePer = Random.Range(0,101);

					if(treePer<treeCount){
						Instantiate(trees[Random.Range(0,trees.Length)], new Vector3(objPos.x + x, objPos.y + perlin, objPos.z + y ), Quaternion.identity, transform);
					}
				}

				verts[y * mapSize + x] = new Vector3(x, perlin, y);
				uv[y * mapSize + x] = new Vector2((objPos.x + Mathf.Clamp(x,0.0001f,Mathf.Infinity)) / mapSize, (objPos.z + Mathf.Clamp(y,0.0001f,Mathf.Infinity)) / mapSize);

				if(x != mapSize-1 && y != mapSize-1){
					triangles[numTri] = y * mapSize + x;
					triangles[numTri+1] = (y+1)*mapSize + x;
					triangles[numTri+2] = (y+1)*mapSize + x + 1;

					triangles[numTri+3] = (y+1)*mapSize + x + 1;
					triangles[numTri+4] = y*mapSize + x + 1;
					triangles[numTri+5] = y*mapSize + x;

					numTri += 6;
				}
			}
		}

		mesh.vertices = verts;
		mesh.uv = uv;
		mesh.triangles = triangles;
		GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
