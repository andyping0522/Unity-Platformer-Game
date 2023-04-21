using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public int height = 1;
    public int tiles = 10;
    public MeshFilter wall;
    public SquareGrid squareGrid;
    List<Vector3> vectices;
    List<int> triangles;

    Dictionary<int,List<Triangle>> triangleDictionary = new Dictionary<int, List<Triangle>> ();
	List<List<int>> edges = new List<List<int>> ();
	HashSet<int> checkedVertices = new HashSet<int>();
    HashSet<int> layers = new HashSet<int>();
    public void GenerateMesh(int[,] map, float size) {
        triangleDictionary.Clear();
		edges.Clear();
		checkedVertices.Clear();
        layers.Clear();

        squareGrid = new SquareGrid(map, size);
        vectices = new List<Vector3>();
        triangles = new List<int>();
        for (int x = 0; x < squareGrid.squares.GetLength(0); x++) {
                for (int y = 0; y < squareGrid.squares.GetLength(1); y++) {

                    TriangulateSquare(squareGrid.squares[x,y]);
                }
        }

        edges = CalculateMeshOulines(triangleDictionary);

        //AddLayer(size);
        AddVerticalNoise(size);
        AddHorizonalNoise(size);

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = vectices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        
        Vector2[] uvs = new Vector2[vectices.Count];
        for (int i =0; i < vectices.Count; i++) {
            float percentX = Mathf.InverseLerp(-map.GetLength(0)/2*size, map.GetLength(0)/2*size, vectices[i].x)*tiles;
            float percentY = Mathf.InverseLerp(-map.GetLength(1)/2*size, map.GetLength(1)/2*size, vectices[i].z)*tiles;
            uvs[i] = new Vector2(percentX, percentY);
        }
        mesh.uv = uvs;

        
    }
    /**
    this function apply random displacement on the vertical axis for all vertices
    */
    void AddVerticalNoise(float size){


        for (int i = 0; i < vectices.Count; i++) {
			if (!isInOutline(i, edges)) {
                float noise = Random.Range(0.0f, 2.0f);
                vectices[i] += Vector3.up*Mathf.Sqrt(size*size/2)*noise;
            }
        }
    }
    /**
    this function add layers to the terrain
    */
    void AddLayer(float size){
        HashSet<int> borderVertexIndex = new HashSet<int>();
        for (int x = 0; x < squareGrid.squares.GetLength(0); x++) {
            
            borderVertexIndex.Add(squareGrid.squares[x,0].botLeft.index);
            borderVertexIndex.Add(squareGrid.squares[x,0].centerBot.index);
        
        
            borderVertexIndex.Add(squareGrid.squares[x,squareGrid.squares.GetLength(1)-1].topRight.index);
            borderVertexIndex.Add(squareGrid.squares[x,squareGrid.squares.GetLength(1)-1].centerTop.index);
        
        }
        for (int y = 0; y < squareGrid.squares.GetLength(1); y++) {

            
            borderVertexIndex.Add(squareGrid.squares[0,y].topLeft.index);
            borderVertexIndex.Add(squareGrid.squares[0,y].centerLeft.index);
        
            borderVertexIndex.Add(squareGrid.squares[squareGrid.squares.GetLength(0)-1,y].botRight.index);
            borderVertexIndex.Add(squareGrid.squares[squareGrid.squares.GetLength(0)-1,y].centerRight.index);

        }

        foreach (List<int> edge in edges) {
            foreach (int vertexIndex in edge) {
                if (!borderVertexIndex.Contains(vertexIndex)){
                    layers.Add(vertexIndex);
                }
            }
        }

        int count = 1;
        
        //List<List<int>> newEdges = getNewEdges(edges, triangleDictionary);
        Dictionary<int,List<Triangle>> newTriangleDict = RemoveTriangleFromDictionary(triangleDictionary);
        
        while (layers.Count < vectices.Count) {
        //for (int j = 0; j < 3; j++ ){
            Debug.Log(layers.Count);
            for (int i = 0; i < vectices.Count; i++) {
                if (!layers.Contains(i)) {
                    vectices[i] += Vector3.up*Mathf.Sqrt(size*size/2);
                }
            }
            Debug.Log(edges.Count);
            Debug.Log(count);
            count++;
            checkedVertices.Clear();
            Debug.Log(newTriangleDict.Count);
            List<List<int>> newEdges = CalculateMeshOulines(newTriangleDict);
            
            Debug.Log(newEdges.Count);
            if (newEdges.Count == 0) {
                break;
            }
            
            foreach (List<int> edge in newEdges) {
                foreach(int vertexIndex in edge){
                    layers.Add(vertexIndex);
                }
                
            }
            //newEdges = getNewEdges(newEdges, newTriangleDict);
            newTriangleDict = RemoveTriangleFromDictionary(newTriangleDict);
        //}
        }
        

    }
    /**
    this function apply random displacement on the horizontal axis for all vertices
    */
    void AddHorizonalNoise(float size){
        for (int i = 0; i < vectices.Count; i++) {
			
            float noiseX = Random.Range(-1.0f, 1.0f);
            float noiseY = Random.Range(-1.0f, 1.0f);
            vectices[i] += Vector3.forward*size/4*noiseY;
            vectices[i] += Vector3.right*size/4*noiseX;
            
        }
    }
    /**
    this function calculate the next layer of the edge
    */
    List<List<int>> getNewEdges(List<List<int>> oldEdges, Dictionary<int,List<Triangle>> triangleDict) {
        List<List<int>> newEdges = new List<List<int>>();
        List<int> newEdge = new List<int>();
        List<Triangle> trianglesContainingEdge;
        foreach (List<int> oldEdge in oldEdges) {
            newEdge = new List<int>();
            foreach (int edgeIndex in oldEdge){
                trianglesContainingEdge = triangleDict[edgeIndex];
                foreach(Triangle triangle in trianglesContainingEdge) {
                    for (int i =0; i < 3;i ++ ){
                        if (!isInOutline(triangle[i], oldEdges)) {
                            newEdge.Add(triangle[i]);
                        }
                    }
                }
            }
            if (newEdge.Count > 0) {
                newEdges.Add(newEdge);
            }
        }
        return newEdges;
    }

    bool isInOutline(int vertexIndex, List<List<int>> outlines) {
        foreach (List<int> outline in outlines) {
			if (outline.Contains(vertexIndex)) {
                return true;
            }
        }
        return false;
    }

    
    /**
    this function add the triangle of the given vertex 
    */
    void AddTriangleToDictionary(int vertexIndexKey, Triangle triangle) {
		if (triangleDictionary.ContainsKey(vertexIndexKey)) {
			triangleDictionary[vertexIndexKey].Add(triangle);
		} else {
			List<Triangle> triangleList = new List<Triangle>();
			triangleList.Add(triangle);
			triangleDictionary.Add(vertexIndexKey, triangleList);
		}
	}
    /**
    this function removes the outer triangles in the triangle dictionary
    */
    Dictionary<int,List<Triangle>> RemoveTriangleFromDictionary(Dictionary<int,List<Triangle>> triangleDict) {
        Dictionary<int,List<Triangle>> newDict = new Dictionary<int,List<Triangle>>();
        bool toRemove;
        List<Triangle> newTriangleList;
		foreach (int key in triangleDict.Keys){
            if (layers.Contains(key)) {
                continue;
            }
            newTriangleList = new List<Triangle>();
            //newDict.Add(entry.Key, entry.Value);
            foreach (Triangle triangle in triangleDict[key]) {
                
                toRemove = false;
                for (int i = 0; i < 3; i++){
                    int vertex = triangle[i];
                    if(layers.Contains(vertex)) {
                        toRemove = true;
                    }
                }
                if (!toRemove){
                    newTriangleList.Add(triangle);
                    //newDict[entry.Key].Remove(triangle);
                }
                
            }
            newDict.Add(key, newTriangleList);
        }
        return newDict;
	}

    /**
    this function calculate the edges of the mesh
    */
	List<List<int>> CalculateMeshOulines(Dictionary<int,List<Triangle>> triangleDict) {
        List<List<int>> outlines = new List<List<int>>();
		for (int vertexIndex = 0; vertexIndex < vectices.Count; vertexIndex ++) {
			if (!layers.Contains(vertexIndex) && !checkedVertices.Contains(vertexIndex) ) {
				int newOutlineVertex = GetConnectedOutlineVertex(vertexIndex, triangleDict);
				if (newOutlineVertex != -1) {
					checkedVertices.Add(vertexIndex);

					List<int> newOutline = new List<int>();
					newOutline.Add(vertexIndex);
					outlines.Add(newOutline);
					FollowOutline(newOutlineVertex, outlines.Count-1, outlines, triangleDict);
					outlines[outlines.Count-1].Add(vertexIndex);
				}
			}
		}
        return outlines;
	}

    /**
    this function calculate the vertices on the same outline as the given vertex
    */
	void FollowOutline(int vertexIndex, int outlineIndex, List<List<int>> outlines, Dictionary<int,List<Triangle>> triangleDict) {
		outlines[outlineIndex].Add(vertexIndex);
		checkedVertices.Add(vertexIndex);
		int nextVertexIndex = GetConnectedOutlineVertex(vertexIndex, triangleDict);

		if (nextVertexIndex != -1) {
			FollowOutline(nextVertexIndex, outlineIndex, outlines, triangleDict);
		}
	}

    /**
    this function calculate the next vertex on the same outline as the given vertex
    */
	int GetConnectedOutlineVertex(int vertexIndex, Dictionary<int,List<Triangle>> triangleDict) {
		List<Triangle> trianglesContainingVertex = triangleDict[vertexIndex];

		for (int i = 0; i < trianglesContainingVertex.Count; i ++) {
			Triangle triangle = trianglesContainingVertex[i];

			for (int j = 0; j < 3; j ++) {
				int vertexB = triangle[j];
				if (vertexB != vertexIndex && !checkedVertices.Contains(vertexB) && !layers.Contains(vertexB)) {
					if (IsOutlineEdge(vertexIndex, vertexB, triangleDict)) {
						return vertexB;
					}
				}
			}
		}

		return -1;
	}

	bool IsOutlineEdge(int vertexA, int vertexB, Dictionary<int,List<Triangle>> triangleDict) {
		List<Triangle> trianglesContainingVertexA = triangleDict[vertexA];
		int sharedTriangleCount = 0;

		for (int i = 0; i < trianglesContainingVertexA.Count; i ++) {
			if (trianglesContainingVertexA[i].Contains(vertexB)) {
				sharedTriangleCount ++;
				if (sharedTriangleCount > 1) {
					break;
				}
			}
		}
		return sharedTriangleCount == 1;
	}

	struct Triangle {
		public int vertexIndexA;
		public int vertexIndexB;
		public int vertexIndexC;
		int[] verticesIndex;

		public Triangle (int a, int b, int c) {
			vertexIndexA = a;
			vertexIndexB = b;
			vertexIndexC = c;

			verticesIndex = new int[3];
			verticesIndex[0] = a;
			verticesIndex[1] = b;
			verticesIndex[2] = c;
		}

		public int this[int i] {
			get {
				return verticesIndex[i];
			}
		}


		public bool Contains(int vertexIndex) {
			return vertexIndex == vertexIndexA || vertexIndex == vertexIndexB || vertexIndex == vertexIndexC;
		}
	}

    public class Node {
        public Vector3 position;
        public int index = -1;

        public Node(Vector3 pos){
            position = pos;
        }
    }

    public class ControlNode : Node {

        public bool active;
        public Node top, right;

        public ControlNode(Vector3 pos, bool active, float size) : base(pos) {
            this.active = active;
            top = new Node(position + Vector3.forward*size/2f);
            right = new Node(position + Vector3.right*size/2f);
        }

    }

    public class Square {
        public ControlNode topRight, topLeft, botRight, botLeft;
        public Node centerTop, centerRight, centerLeft, centerBot;
        public int config;
        public Square(ControlNode topLeft,ControlNode topRight,ControlNode botRight,ControlNode botLeft) {
            this.topRight = topRight;
            this.topLeft = topLeft;
            this.botLeft = botLeft;
            this.botRight = botRight;

            this.centerBot = botLeft.right;
            this.centerLeft = botLeft.top;
            this.centerRight = botRight.top;
            this.centerTop = topLeft.right;

            if (topLeft.active) {
                config += 8;
            }
            if (topRight.active) {
                config += 4;
            } 
            if (botRight.active) {
                config += 2;
            }
            if (botLeft.active) {
                config += 1;
            }
        }
    }

    public class SquareGrid {
        public Square[,] squares;
        public SquareGrid(int[,] map, float size) {
            int countNodeX = map.GetLength(0);
            int countNodeY = map.GetLength(1);
            float mapWidth = countNodeX*size;
            float mapHeight = countNodeY*size;

            ControlNode[,] controlNodes = new ControlNode[countNodeX,countNodeY];

            for (int x = 0; x < countNodeX; x++) {
                for (int y = 0; y < countNodeY; y++) {
                    Vector3 pos = new Vector3(-mapWidth/2 + x*size + size/2,0, -mapHeight/2 + y*size + size/2);
                    controlNodes[x,y] = new ControlNode(pos, map[x,y] == 1, size);
                }
            }

            squares = new Square[countNodeX-1, countNodeY-1];
            for (int x = 0; x < countNodeX-1; x++) {
                for (int y = 0; y < countNodeY-1; y++) {
                    squares[x,y] = new Square(controlNodes[x, y+1], controlNodes[x+1, y+1], controlNodes[x+1, y], controlNodes[x, y]);
                }
            }
        }
    }
    /**
    this function draws triangles based on the configuration of the square
    */

    void TriangulateSquare(Square square){
        switch (square.config) {
            case 0:
            break;

            case 1:
            MeshFromPoints(square.centerLeft, square.centerBot, square.botLeft);
            checkedVertices.Add(square.botLeft.index);
            break;
            case 2:
            MeshFromPoints(square.botRight, square.centerBot, square.centerRight);
            checkedVertices.Add(square.botRight.index);
            break;
            case 4:
            MeshFromPoints(square.topRight, square.centerRight, square.centerTop);
            checkedVertices.Add(square.topRight.index);
            break;
            case 8:
            MeshFromPoints(square.topLeft, square.centerTop, square.centerLeft);
            checkedVertices.Add(square.topLeft.index);
            break;

            case 3:
            MeshFromPoints(square.centerRight, square.botRight, square.botLeft, square.centerLeft);
            checkedVertices.Add(square.botLeft.index);
            checkedVertices.Add(square.botRight.index);
            break;
            case 6:
            MeshFromPoints(square.centerTop, square.topRight, square.botRight, square.centerBot);
            checkedVertices.Add(square.topRight.index);
            checkedVertices.Add(square.botRight.index);
            break;
            case 9:
            MeshFromPoints(square.topLeft, square.centerTop, square.centerBot, square.botLeft);
            checkedVertices.Add(square.botLeft.index);
            checkedVertices.Add(square.topLeft.index);
            break;
            case 12:
            MeshFromPoints(square.topLeft, square.topRight, square.centerRight, square.centerLeft);
            checkedVertices.Add(square.topRight.index);
            checkedVertices.Add(square.topLeft.index);
            break;
            
            case 5:
            MeshFromPoints(square.centerTop, square.topRight, square.centerRight, square.centerBot, square.botLeft, square.centerLeft);
            checkedVertices.Add(square.topRight.index);
            checkedVertices.Add(square.botLeft.index);
            break;
            case 10:
            MeshFromPoints(square.topLeft, square.centerTop, square.centerRight, square.botRight, square.centerBot, square.centerLeft);
            checkedVertices.Add(square.topLeft.index);
            checkedVertices.Add(square.botRight.index);
            break;

            case 7:
            MeshFromPoints(square.centerTop, square.topRight, square.botRight, square.botLeft, square.centerLeft);
            checkedVertices.Add(square.botLeft.index);
            checkedVertices.Add(square.botRight.index);
            checkedVertices.Add(square.topRight.index);
            break;
            case 11:
            MeshFromPoints(square.topLeft, square.centerTop, square.centerRight, square.botRight, square.botLeft);
            checkedVertices.Add(square.botLeft.index);
            checkedVertices.Add(square.botRight.index);
            checkedVertices.Add(square.topLeft.index);
            break;
            case 13:
            MeshFromPoints(square.topLeft, square.topRight, square.centerRight, square.centerBot, square.botLeft);
            break;
            case 14:
            MeshFromPoints(square.topLeft, square.topRight, square.botRight, square.centerBot, square.centerLeft);
            checkedVertices.Add(square.topLeft.index);
            checkedVertices.Add(square.topRight.index);
            checkedVertices.Add(square.botRight.index);
            break;

            case 15:
            MeshFromPoints(square.topLeft, square.topRight, square.botRight, square.botLeft);
            checkedVertices.Add(square.topLeft.index);
			checkedVertices.Add(square.topRight.index);
			checkedVertices.Add(square.botRight.index);
			checkedVertices.Add(square.botLeft.index);
            break;
        }
    }
    /**
    this function draws triangles with the specified points
    */

    void MeshFromPoints(params Node[] nodes){
        for (int i = 0;i< nodes.Length; i++) {
            if (nodes[i].index == -1) {
                nodes[i].index = vectices.Count;
                vectices.Add(nodes[i].position);
            }
        }

        if (nodes.Length >= 3)
			CreateTriangle(nodes[0], nodes[1], nodes[2]);
		if (nodes.Length >= 4)
			CreateTriangle(nodes[0], nodes[2], nodes[3]);
		if (nodes.Length >= 5) 
			CreateTriangle(nodes[0], nodes[3], nodes[4]);
		if (nodes.Length >= 6)
			CreateTriangle(nodes[0], nodes[4], nodes[5]);
    }
    /**
    this function create a triangle given the nodes and store it in the triangle dictionary
    */
    void CreateTriangle(Node a, Node b, Node c) {
		triangles.Add(a.index);
		triangles.Add(b.index);
		triangles.Add(c.index);

        Triangle triangle = new Triangle (a.index, b.index, c.index);
		AddTriangleToDictionary(triangle.vertexIndexA, triangle);
		AddTriangleToDictionary(triangle.vertexIndexB, triangle);
		AddTriangleToDictionary(triangle.vertexIndexC, triangle);
	}
    
    
}
