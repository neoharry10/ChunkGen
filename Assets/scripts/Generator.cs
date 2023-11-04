using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    [SerializeField]
    private GameObject terrainprefab;

    [SerializeField]
    private GameObject[] decor;

    [SerializeField]
    private Material[] terrainMat;

     [SerializeField]
    private GameObject wallprefab;

    private float chunksize = 1000;

    private Vector3 playerpos;

    //We use a local 2d grid to save the generated chunks, x = globalx - y = globalz
    private Dictionary<Vector2Int, GameObject> grid = new Dictionary<Vector2Int, GameObject>();


    // Start is called before the first frame update
    void Start(){
        // Initialising the chunk
        Chunk.decor = decor;
        Chunk.mats = terrainMat;
        Chunk.wall = wallprefab;

        //Spawning the first 9 chunks
        Vector2Int pos;
        
        for(int i = -1; i <= 1; i ++){
            for(int j = -1; j <= 1; j ++){
                pos = new Vector2Int(i,j);
                GetChunk(pos);
            }
        }

       
    }

    // Update is called once per frame
    void Update()
    {   
        playerpos = GameObject.FindWithTag("Player").transform.position;

        int x = Mathf.FloorToInt(playerpos.x / chunksize);
        int z = Mathf.FloorToInt(playerpos.z / chunksize);


        // Debug.Log("Chunk: "+ x + "," + z);//+ "," + y);

        //When the play is in a chunk we load or generate the 8 around it
        UpdateChunks(new Vector2Int(x,z));
    }

    //Given a 2d point it loads or generates the 8 chunks around that point and unloads all others
    private void UpdateChunks(Vector2Int xy){

        //Spawning/Loading the near chunks
        List<Vector2Int> poslst = new List<Vector2Int>();
        Vector2Int pos; 
        for(int i = -1; i <= 1; i ++){
            for(int j = -1; j <= 1; j ++){
                pos = new Vector2Int(i+xy.x,j+xy.y);
                poslst.Add(pos);
                GetChunk(pos);
            }
        }

        //Unloading
        //Slow, needs to be changed to a check around each element instead
        foreach(Vector2Int entry in grid.Keys){
            if(!poslst.Contains(entry)){
                grid[entry].SetActive(false);
            }
        }

    }
    
    //Given a 2d point it loads or generates its chunk 
    private void GetChunk(Vector2Int xy){

        if(grid.ContainsKey(xy)){
            if(grid[xy].activeSelf == false)
                grid[xy].SetActive(true);
        }
        else{
            GameObject obj;

            obj = Instantiate(terrainprefab, new Vector3(chunksize*xy.x, 0, chunksize*xy.y), Quaternion.identity );
            obj.AddComponent<Chunk>().Populate();
            grid.Add(xy, obj);
        }

    }
}
