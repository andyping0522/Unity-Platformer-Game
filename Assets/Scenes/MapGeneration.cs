using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGeneration : MonoBehaviour
{
    public int width;
    public int length;

    public int TrackSize = 0;

    public string seed;
    public bool useRandomSeed;

    [Range(0,100)]
    public int randomFillPercent;

    public int iteration = 5;
    int[,] map;

    public float size = 1;

    public int borderSize = 1;

    public bool clickToRegenerate = false;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    void Update() {
        if (clickToRegenerate){
            if (Input.GetMouseButtonDown(0)) {
			    GenerateMap();
		    }
        }
        
    }

    // Update is called once per frame
    void GenerateMap()
    {
        map = new int[width, length];
        RandomFillMap();

        

        
        for (int i = 0; i < iteration; i ++) {
            map = CellularAutomata();
        }

        

        int adjustedBorderSize = (int)Mathf.Ceil(borderSize/size);
        int[,] border = new int[width+ adjustedBorderSize*2, length + adjustedBorderSize*2];
        for (int x = 0; x < border.GetLength(0); x ++) {
            for (int y = 0; y <border.GetLength(1); y ++) {
                if ((x >= adjustedBorderSize) && (x < (width + adjustedBorderSize)) && (y >= adjustedBorderSize) && (y < (length + adjustedBorderSize))) {
                    border[x, y] = map[x-adjustedBorderSize, y-adjustedBorderSize];
                } else {
                    border[x,y] = 1;
                }
            }
        }
        
        MeshGenerator meshGen = gameObject.GetComponent<MeshGenerator>();
		meshGen.GenerateMesh(border, size);
    }

    
    /**
    this function randomly fill a map with spefiied width and length of 1s and 0s, and with 
    speicified ratio of 1 and 0, it also avoid to fill the specified area
    */
    void RandomFillMap() {
        System.Random pseudoRandom = new System.Random();
        if (!useRandomSeed) {
            pseudoRandom = new System.Random(seed.GetHashCode());
            
        } 
        int avoid = (int)Mathf.Ceil(TrackSize/size);
        for (int x = 0; x < width; x ++) {
            for (int y = 0; y < length; y ++) {
                if (x == 0 || x == width-1 || y ==0 || y == length - 1) {
                    map[x,y] = 1;
                } else if ((y >= (length/2-avoid)) && (y <= length/2+avoid)){
                    map[x,y] = 0;
                }
                else{
                    map[x,y] = (pseudoRandom.Next(0, 100) < randomFillPercent)? 1: 0;
                }
                
            }
        }
    }
    
    /**
    this function implements the cellular automata process on the map
    */
    int[,] CellularAutomata() {
        int[,] newMap = new int[width,length];
        for (int x = 0; x < width; x ++) {
            for (int y = 0; y < length; y ++) {
                int neighbourCell = GetSurroundingCellCount(x, y);

                if (neighbourCell > 4) {
                    newMap[x,y] = 1;
                } else if (neighbourCell < 4) {
                    newMap[x,y] = 0;
                }

                
            }
        }
        return newMap;
    }
    /**
    this function calculate the number of surrounding cell of a specified cell
    */
    int GetSurroundingCellCount(int gridX, int gridY) {
        int CellCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++){
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++){
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < length) {
                    if ((neighbourX != gridX) || (neighbourY != gridY)){
                        CellCount += map[neighbourX, neighbourY];
                    }
                } else {
                    CellCount += 1;
                }
                
            }
        }

        return CellCount;
    }

}
