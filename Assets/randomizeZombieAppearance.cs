using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizeZombieAppearance : MonoBehaviour
{
    public Material[] zombieMaterials;
    
    // Start is called before the first frame update
    void Start()
    {
        SkinnedMeshRenderer myRenderer = GetComponent<SkinnedMeshRenderer>();
        myRenderer.material = zombieMaterials[Random.Range(0, zombieMaterials.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
