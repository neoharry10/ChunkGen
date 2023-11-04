using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Terrain))]
public class Chunk : MonoBehaviour {

    public static GameObject[] decor;
    public static GameObject wall;
    public static Material[] mats;
    

    private List<GameObject> spawnedobjs = new List<GameObject>(); 

    public void Populate(){

        int mt = Random.Range(0, mats.Length);
        GetComponent<Terrain>().materialTemplate = mats[mt];
        
        //Spawning random objects on each chunk
        int Num = decor.Length, J;
        GameObject obj;
        Vector3 pos;
        int size = 1000;

        for(int i = 0; i < Num; i ++){
            J = Random.Range(0, i+Num);
            for(int j = 0; j < J; j++){
                pos = transform.position;
                
                pos.x = Random.Range(pos.x, pos.x+size);
                pos.z = Random.Range(pos.z, pos.z+size);
                obj = Instantiate(decor[i], pos, Quaternion.identity, transform);
                spawnedobjs.Add(obj);
            }
            
        }

        //Randomly spawn a wall to block a direction
        if(Random.Range(0, 20) >= 15){ //1/4
    
                obj = Instantiate(wall, transform.position, Quaternion.identity, transform);
            }

    }


}