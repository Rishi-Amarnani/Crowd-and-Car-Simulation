using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborScript : MonoBehaviour {

    private Terrain park;
    private Terrain otherTerrain;

	// Use this for initialization
	void Start () {
        park = (Terrain) GetComponent("Park");
        otherTerrain = (Terrain) GetComponent("OtherTerrain");
        park.SetNeighbors(new Terrain(), otherTerrain, new Terrain(), new Terrain());
        otherTerrain.SetNeighbors(new Terrain(), new Terrain(), park, new Terrain());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
