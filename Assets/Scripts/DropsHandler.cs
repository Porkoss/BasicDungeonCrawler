using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsHandler : MonoBehaviour
{
    // Start is called before the first frame update    
    public GameObject dropPrefabs;
    public bool IsDropping(){
        if(Random.Range(0,1)<=Chance)
        {
            return true;
        }
    }

    public bool GenerateDrops(){
        for drop in dropList:
        {
            Drops dropClass = drop.GetComponent<Drops>();
            if(dropClass.IsDropping()){
                Instantiate(drop,RandomVector3(),drop.transform.rotation);
            }
        }  
    }

    public Vector3 RandomVector3(){
        return new Vector3(Random.Range(0, 3)+transform.position.x,transform.position.y,RandomVector3.Range(0,3)+transform.position.z);
    }
}
