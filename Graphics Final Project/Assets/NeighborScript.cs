﻿//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class NeighborScript : MonoBehaviour {

    private Terrain park;

    private Terrain PosXSW;
    private Terrain NegXSW;
    private Terrain PosZSW;
    private Terrain NegZSW;

    private Terrain SWCP1;
    private Terrain SWCP2;
    private Terrain SWCP3;

//    public ArrayList<GameObject> TerrainPositions;
    private Terrain SWCP4;

    
    void Start () {
        //TerrainPositions = new ArrayList<GameObject>();

        park = (Terrain) GetComponent("Park");
        PosXSW = (Terrain) GetComponent("PosXSW");
        NegXSW = (Terrain) GetComponent("NegXSW");
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

        SWCP1 = (Terrain) GetComponent("SWCP1");
        SWCP2 = (Terrain) GetComponent("SWCP2");
        SWCP3 = (Terrain) GetComponent("SWCP3");
        SWCP4 = (Terrain) GetComponent("SWCP4");

        //new terrain() is put in place for null, because null causes nullException to be thrown

        //left, top, right, bottom (that's the order)
        park.SetNeighbors(NegXSW, PosZSW, PosXSW, NegZSW);
        NegZSW.SetNeighbors(SWCP1, park, SWCP2, new Terrain());
        PosXSW.SetNeighbors(park, SWCP3, new Terrain(), SWCP2);
        PosZSW.SetNeighbors(SWCP4, new Terrain(), SWCP3, park);
        NegXSW.SetNeighbors(new Terrain(), SWCP4, park, SWCP1);
        
        SWCP1.SetNeighbors(new Terrain(), NegXSW, NegZSW, new Terrain());
        SWCP2.SetNeighbors(NegZSW, PosXSW, new Terrain(), new Terrain());
        SWCP3.SetNeighbors(PosZSW, new Terrain(), new Terrain(), PosXSW);
        SWCP4.SetNeighbors(new Terrain(), new Terrain(), PosZSW, NegXSW);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
