//using System.Collections;
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

    private Terrain SWCP4;

    
    void Start () {

        park = (Terrain) GetComponent("Park");
        PosXSW = (Terrain) GetComponent("PosXSW");
        NegXSW = (Terrain) GetComponent("NegXSW");
        PosZSW = (Terrain) GetComponent("PosZSW");
        NegZSW = (Terrain) GetComponent("NegZSW");


        SWCP1 = (Terrain) GetComponent("SWCP1");
        SWCP2 = (Terrain) GetComponent("SWCP2");
        SWCP3 = (Terrain) GetComponent("SWCP3");
        SWCP4 = (Terrain) GetComponent("SWCP4");

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
	
}
