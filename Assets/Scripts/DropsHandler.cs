using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsHandler : MonoBehaviour
{
    // Start is called before the first frame update    
    public GameObject[] dropPrefabs;

    public bool IsDropping(Drops drop){
        float dropRandom=Random.Range(0f,1);
        if(dropRandom<=drop.Chance)
        {
            return true;
        }
        return false;
    }

    public void GenerateDrops(){
        foreach (GameObject  drop in dropPrefabs)
        {
            Drops dropClass = drop.GetComponent<Drops>();
            if(IsDropping(dropClass)){
                Instantiate(drop,RandomVector3(),drop.transform.rotation);
            }
        }  
    }

    public Vector3 RandomVector3(){
        return new Vector3(Random.Range(0, 3)+transform.position.x,transform.position.y,Random.Range(0,3)+transform.position.z);
    }
}
