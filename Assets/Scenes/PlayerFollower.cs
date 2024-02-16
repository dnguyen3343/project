using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    //An offset from player
    public GameObject Square;
    private Vector3 offset = new Vector3(0, 0, -10);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //follow player
        transform.position = Square.transform.position + offset;
    }
}
