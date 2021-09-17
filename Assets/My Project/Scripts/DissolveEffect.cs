using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] Material dissolveMat;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = dissolveMat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
