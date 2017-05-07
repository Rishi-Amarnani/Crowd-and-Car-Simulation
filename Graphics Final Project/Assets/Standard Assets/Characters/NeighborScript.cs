//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class NeighborScript : MonoBehaviour {

    private Terrain park;

    private Terrain PosXSW;
    private Terrain PosZSW;
    private Terrain NegZSW;

//    private Terrain SWCP1;
    private Terrain SWCP2;
    private Terrain SWCP3;

//    public ArrayList<GameObject> TerrainPositions;
//    private Terrain SWCP4;

    //private Terrain PosXSWGuard;

/*
    // Use this for initialization
    void Start () {
        park = (Terrain) GetComponent("Park");
        PosXSW = (Terrain) GetComponent("PosXSW");
        PosZSW = (Terrain)GetComponent("PosZSW");
        NegZSW = (Terrain)GetComponent("NegZSW");
    
        SWCP1 = (Terrain) GetComponent("SWCP1");
        SWCP2 = (Terrain)GetComponent("SWCP2");
        SWCP3 = (Terrain)GetComponent("SWCP3");
        SWCP4 = (Terrain)GetComponent("SWCP4");

        PosXSWGuard = (Terrain) GetComponent("PosXSWGuard");


        //new terrain() is put in place for null, because null causes nullException to be thrown
        park.SetNeighbors(new Terrain(), PosZSW, PosXSW, NegZSW);
        PosXSW.SetNeighbors(park, SWCP3, new Terrain(), SWCP2);
        PosZSW.SetNeighbors(SWCP4, new Terrain(), SWCP3, park);
        NegZSW.SetNeighbors(SWCP1, park, SWCP2, new Terrain());

        SWCP1.SetNeighbors(new Terrain(), park, NegZSW, new Terrain());
        SWCP2.SetNeighbors(NegZSW, PosXSW, PosXSWGuard, new Terrain());
        SWCP3.SetNeighbors(PosZSW, new Terrain(), PosXSWGuard, PosXSW);
        SWCP4.SetNeighbors(new Terrain(), new Terrain(), PosZSW, park);

        PosXSWGuard.SetNeighbors(PosXSW, new Terrain(), new Terrain(), new Terrain());
    }
    */
    
    void Start () {
        //TerrainPositions = new ArrayList<GameObject>();

        park = (Terrain) GetComponent("Park");
        PosXSW = (Terrain) GetComponent("PosXSW");
        PosZSW = (Terrain) GetComponent("PosZSW");
        NegZSW = (Terrain) GetComponent("NegZSW");

/*
        TerrainData parkdata = (TerrainData)Resources.Load("Terrain/Park");
        GameObject ParkPosition = Terrain.CreateTerrainGameObject(parkdata);
        TerrainData posxswdata = (TerrainData)Resources.Load("Terrain/PosXSW");
        GameObject PosXSWPosition = Terrain.CreateTerrainGameObject(posxswdata);
        TerrainData poszswdata = (TerrainData)Resources.Load("Terrain/PosZSW");
        GameObject PosZSWPosition = Terrain.CreateTerrainGameObject(poszswdata);
        TerrainData negzsw = (TerrainData)Resources.Load("Terrain/NegZSW");
        GameObject NegZSWPosition = Terrain.CreateTerrainGameObject(negzsw);

        TerrainPositions.Add(ParkPosition);
        TerrainPositions.Add(PosXSWPosition);
        TerrainPositions.Add(PosZSWPosition);
        TerrainPositions.Add(NegZSWPosition);
*/

        SWCP2 = (Terrain) GetComponent("SWCP2");
        SWCP3 = (Terrain) GetComponent("SWCP3");

        //new terrain() is put in place for null, because null causes nullException to be thrown

        //left, top, right, bottom (that's the order)
        park.SetNeighbors(new Terrain(), PosZSW, PosXSW, NegZSW);
        NegZSW.SetNeighbors(new Terrain(), park, SWCP2, new Terrain());
        PosXSW.SetNeighbors(park, SWCP3, new Terrain(), SWCP2);
        PosZSW.SetNeighbors(new Terrain(), new Terrain(), SWCP3, park);
        

        SWCP2.SetNeighbors(NegZSW, PosXSW, new Terrain(), new Terrain());
        SWCP3.SetNeighbors(PosZSW, new Terrain(), new Terrain(), PosXSW);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
