using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// Loads chunks around another chunk
public class loadSurroundingChunks : MonoBehaviour
{
    [SerializeField]
    GameObject chunk; // The chunk prefab to spawn
    [SerializeField]
    int chunksToLoad = 2; // How many chunks spawn either side

    // Start is called before the first frame update
    void Start()
    {
        int mapScale = chunk.GetComponent<TerrainGenerator>().mapSize - 1; //  We work out the scale of each chunk

		// We loop through each chunk
        for(int x = -chunksToLoad;x<=chunksToLoad;x+=1){
            for(int y = -chunksToLoad;y<=chunksToLoad;y+=1){
                // We ensure we don't respawn the chunk we are loading around
				if(x != 0 || y != 0){
                    Instantiate(chunk, transform.position + new Vector3(x * mapScale, 0.0f, y * mapScale), Quaternion.identity); // We create the chunk
                }
            }
        }
    }
}
