using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadSurroundingChunks : MonoBehaviour
{
    [SerializeField]
    GameObject chunk;
    [SerializeField]
    int chunksToLoad = 2;

    // Start is called before the first frame update
    void Start()
    {
        int mapScale = chunk.GetComponent<TerrainGenerator>().mapSize - 1;

        for(int x = -chunksToLoad;x<=chunksToLoad;x+=1){
            for(int y = -chunksToLoad;y<=chunksToLoad;y+=1){
                if(x != 0 || y != 0){
                    Instantiate(chunk, transform.position + new Vector3(x * mapScale, 0.0f, y * mapScale), Quaternion.identity);
                }
            }
        }
    }
}
