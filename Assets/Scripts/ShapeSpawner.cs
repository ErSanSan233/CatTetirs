using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{

    public GameObject[] shapes;
    public void SpawnShape ()
    {
        int shapeIndex = Random.Range(0, 7);
        //Debug.Log(shapeIndex);
        Instantiate(shapes[shapeIndex],
        transform.position,
            Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnShape();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
