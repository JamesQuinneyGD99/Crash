using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// Generates a terrain mesh on a gameobject
public class TerrainGenerator : MonoBehaviour
{
	public int mapSize = 30;
	[SerializeField]
	Vector2 posOffset; // Essentially a "seed", for perlin noise
	[SerializeField]
	float mapHeight = 3.0f; // The maximum height (and minimum) height for hills
	[SerializeField]
	float mapStretch = 1.0f; // Higher values squeeze parts of the map together, lower do the opposite
	[SerializeField]
	float lakeStart = 0.4f; // The highest height for a lake, a lake is just a hole in the ground which is intersected by the water plane
	[SerializeField]
	float lakeDepth = 2.0f; // How deep lakes are, this also affects the height of hills
	[SerializeField]
	GameObject[] trees; // We store all of the different tree prefabs, this adds variety
	[SerializeField]
	[Range(1,100)]
	int treeCount = 20; // From 1-100, how populated is the map with trees
	[SerializeField]
	int treeDensity = 16; // How close the trees are together, lower = more dense

    // Start is called before the first frame update
    public void Start()
    {
		Random.InitState((int)(posOffset.x + posOffset.y * transform.position.x + transform.position.y)); // We set the map seed

		Mesh mesh = GetComponent<MeshFilter>().mesh; // We grab the mesh from the gameobject

		Vector3 objPos = transform.position; // We store the current position of this part of the map
		int[] triangles = new int[(mapSize-1) * (mapSize-1) * 6]; // We work out how many triangles are in the mesh
		Vector3[] verts = new Vector3[mapSize * mapSize]; // We work out of many vertices are in the mesh
		Vector2[] uv = new Vector2[verts.Length]; // The number of UV (texture mapping) points are the same as the number of vertices
		int numTri = 0; // How many triangles have been added, this increases for each point

		transform.position = new Vector3(transform.position.x, lakeDepth*(-mapHeight/2),transform.position.z); // We normalise the ground height, this stops the gameobject from spawning based on the map height

		// We loop through each vertex
		for(int y = 0;y<mapSize;y+=1){
			for(int x = 0;x<mapSize;x+=1){
				float perlin = Mathf.PerlinNoise((x+posOffset.x+objPos.x)*mapStretch,(y+posOffset.y+objPos.z)*mapStretch); // We work out the height of the current vertex

				// We check whether the current point is outside of a lake
				if(perlin>lakeStart){
					perlin*=lakeDepth; // We push the point upwards
				}

				perlin*=mapHeight; // We push the point upwards based on the map height

				// We check if a tree can spawn at the current point
				if(x%treeDensity == 0 && y%treeDensity == 0 && perlin / mapHeight > lakeStart + 1.0f){
					int treePer = Random.Range(0,101); // We get a random number between 0 and 100

					// We do a percentage check, whether we should spawn a tree at the current point
					if(treePer<treeCount){
						Instantiate(trees[Random.Range(0,trees.Length)], new Vector3(objPos.x + x, objPos.y + perlin, objPos.z + y ), Quaternion.identity, transform); // We create the tree, and place it on the ground
					}
				}

				verts[y * mapSize + x] = new Vector3(x, perlin, y); // We set the current vertex's position
				uv[y * mapSize + x] = new Vector2((objPos.x + Mathf.Clamp(x,0.0001f,Mathf.Infinity)) / mapSize, (objPos.z + Mathf.Clamp(y,0.0001f,Mathf.Infinity)) / mapSize); // We set where the current point is on the texture

				if(x != mapSize-1 && y != mapSize-1){ // We check to ensure we are not on the last point on either axis
					// We set the top left triangles
					triangles[numTri] = y * mapSize + x; 
					triangles[numTri+1] = (y+1)*mapSize + x;
					triangles[numTri+2] = (y+1)*mapSize + x + 1;

					// We set the bottom right triangle
					triangles[numTri+3] = (y+1)*mapSize + x + 1;
					triangles[numTri+4] = y*mapSize + x + 1;
					triangles[numTri+5] = y*mapSize + x;

					numTri += 6; // 6 points means we have added two triangles, since each triangle has 3 points
				}
			}
		}

		// We reload the object's mesh with the data we just calculated
		mesh.vertices = verts;
		mesh.uv = uv;
		mesh.triangles = triangles;
		GetComponent<MeshCollider>().sharedMesh = mesh; // We wrap the mesh collider around our current mesh
    }
}
