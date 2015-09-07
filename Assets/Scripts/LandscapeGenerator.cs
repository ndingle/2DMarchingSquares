using UnityEngine;
using System.Collections.Generic;

public class LandscapeGenerator : MonoBehaviour {

    //Public variables
    //public Sprite[] cases;
    public float resolution = 1f;
    public int width = 6;
    public int height = 6;

    //Private variables
    private bool[,] voxels;

    private Mesh mesh;
    private List<Vector3> vertices;
    private List<int> triangles;

    private int triangleCount;

    public int test = 2;
    
    void Awake() {

        //Load the images to use
        //cases = Resources.LoadAll<Sprite>("cases");

        //Create the empty array for the data
        voxels = new bool[width, height];

        //Create the variables for our mesh
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = new List<Vector3>();
        triangles = new List<int>();
        triangleCount = 0;

        //Start her up
        GenerateVoxels();
        CreateMesh();
		//OptimiseVertices();
        //UpdateMesh();

    }

    void GenerateVoxels() {

        //TODO: Remove this 
        for(var x = 1; x < width-1; x++) {

            for (var y = 1; y < height-1; y++)
            {

                //Set it to something random
                voxels[x, y] = (Random.Range(0, 2) == 0 ? true : false);

            }

        }
        //END TODO

    }

    void CreateMesh() {

        //Start by generating the cases
        for (var x = 0; x < width - 1; x++)
        {

            for (var y = 0; y < height - 1; y++)
            {

				int caseNumber = GetCaseNumber(x,y);
                //CreateCase(caseNumber, x, y);
				if(caseNumber > 0) {

					GameObject newCase = (GameObject)Instantiate((GameObject)Resources.Load (caseNumber.ToString().Trim()), Vector3.zero, Quaternion.identity);
					Vector3 pos = newCase.transform.position;
					pos.x = x * 0.5f;
					pos.y = y * 0.5f;
					newCase.transform.position = pos;

				}

            }

        }

    }

	void OptimiseVertices() {

		for (int x = 0; x < vertices.Count; x++){
			Vector3 curVert = vertices[x]; // get vertex [i]
			// compare it to the next vertices:
			for (int y = x+1; y < vertices.Count; y++){
				// if any one inside limit distance...
				if (Vector3.Distance(curVert, vertices[y]) < 0.0001f){
					vertices[y] = curVert; // make it equal to vertex [i]
				}
			}
		}

	}

    void UpdateMesh() {

        //Setup the mesh
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();

    }

    int GetCaseNumber(int x, int y) {

        //Calculate the case number and return
        int caseNumber = (voxels[x, y + 1] ? 1 : 0);
        caseNumber += (voxels[x + 1, y + 1] ? 2 : 0);
        caseNumber += (voxels[x + 1, y] ? 4 : 0);
        caseNumber += (voxels[x, y] ? 8 : 0);

        //Return the final case
        return caseNumber;

    }

    void AddTriangle() {

        triangles.Add(3 * triangleCount);
        triangles.Add(1 + (3 * triangleCount));
        triangles.Add(2 + (3 * triangleCount));

        triangleCount++;

    }

    void CreateCase(int caseNumber, 
                    int x, 
                    int y) { 

        //Create the mesh
        switch(caseNumber) {

            case 1:
                GenCase1(x, y);
                break;

            case 2:
                GenCase2(x, y);
                break;

            case 3:
                GenCase3(x, y);
                break;

            case 4:
                GenCase4(x, y);
                break;

            case 5:
                GenCase5(x, y);
                break;

            case 6:
                GenCase6(x, y);
                break;

            case 7:
                GenCase7(x, y);
                break;

            case 8:
                GenCase8(x, y);
                break;

            case 9:
                GenCase9(x, y);
                break;

            case 10:
                GenCase10(x, y);
                break;

            case 11:
                GenCase11(x, y);
                break;

            case 12:
                GenCase12(x, y);
                break;

            case 13:
                GenCase13(x, y);
                break;

            case 14:
                GenCase14(x, y);
                break;

            case 15:
                GenCase15(x, y);
                break;
        }

    }

    void GenCase1(int x, int y) {

        //Vertices for case 1
        vertices.Add(new Vector3(x, y));
        vertices.Add(new Vector3(x + 0.5f, y));
        vertices.Add(new Vector3(x, y - 0.5f));

        AddTriangle();

    }

    void GenCase2(int x, int y) {

        vertices.Add(new Vector3(x + 0.5f, y));
        vertices.Add(new Vector3(x + 1f, y));
        vertices.Add(new Vector3(x + 1f, y - 0.5f));

        AddTriangle();

    }

    void GenCase3(int x, int y) {

        vertices.Add(new Vector3(x, y));
        vertices.Add(new Vector3(x + 1f, y));
        vertices.Add(new Vector3(x + 1f, y - 0.5f));

        AddTriangle();

        vertices.Add(new Vector3(x, y));
        vertices.Add(new Vector3(x + 1f, y - 0.5f));
        vertices.Add(new Vector3(x, y - 0.5f));

        AddTriangle();

    }

    void GenCase4(int x, int y) {

        vertices.Add(new Vector3(x + 1f, y - 0.5f));
        vertices.Add(new Vector3(x + 1f, y - 1f));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));

        AddTriangle();

    }

    void GenCase5(int x, int y) {

        GenCase1(x, y);

        vertices.Add(new Vector3(x, y - 0.5f));
        vertices.Add(new Vector3(x + 0.5f, y));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));
         
        AddTriangle();

        vertices.Add(new Vector3(x + 0.5f, y - 1f));
        vertices.Add(new Vector3(x + 0.5f, y));
        vertices.Add(new Vector3(x + 1f, y - 0.5f));
        
        AddTriangle();

        GenCase4(x, y);

    }

    void GenCase6(int x, int y) {

        vertices.Add(new Vector3(x + 0.5f, y));
        vertices.Add(new Vector3(x + 1f, y));
        vertices.Add(new Vector3(x + 1f, y - 1f));

        AddTriangle();

        vertices.Add(new Vector3(x + 0.5f, y));
        vertices.Add(new Vector3(x + 1f, y - 1f));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));
         
        AddTriangle();

    }

    void GenCase7(int x, int y) {

        GenCase1(x, y);

        vertices.Add(new Vector3(x + 0.5f, y));
        vertices.Add(new Vector3(x + 1f, y - 0.5f));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));

        AddTriangle();

        vertices.Add(new Vector3(x + 0.5f, y));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));
        vertices.Add(new Vector3(x, y - 0.5f));

        AddTriangle();

        GenCase2(x, y);

        GenCase4(x, y);

    }

    void GenCase8(int x, int y) {

        vertices.Add(new Vector3(x, y - 0.5f));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));
        vertices.Add(new Vector3(x, y - 1f));

        AddTriangle();

    }

    void GenCase9(int x, int y) {

        vertices.Add(new Vector3(x, y));
        vertices.Add(new Vector3(x + 0.5f, y));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));

        AddTriangle();

        vertices.Add(new Vector3(x, y));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));
        vertices.Add(new Vector3(x, y - 1f));

        AddTriangle();

    }

    void GenCase10(int x, int y) {

        GenCase2(x, y);

        vertices.Add(new Vector3(x + 0.5f, y));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));
        vertices.Add(new Vector3(x, y - 0.5f));

        AddTriangle();

        vertices.Add(new Vector3(x + 0.5f, y));
        vertices.Add(new Vector3(x + 1f, y - 0.5f));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));

        AddTriangle();

        GenCase8(x, y);

    }

    void GenCase11(int x, int y) {

        GenCase1(x, y);

        GenCase2(x, y);
        
        vertices.Add(new Vector3(x + 0.5f, y));
        vertices.Add(new Vector3(x + 1f, y - 0.5f));
        vertices.Add(new Vector3(x, y - 0.5f));

        AddTriangle();

        vertices.Add(new Vector3(x + 1f, y - 0.5f));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));
        vertices.Add(new Vector3(x, y - 0.5f));

        AddTriangle();

        GenCase8(x, y);

    }

    void GenCase12(int x, int y) {

        vertices.Add(new Vector3(x, y - 0.5f));
        vertices.Add(new Vector3(x + 1f, y - 0.5f));
        vertices.Add(new Vector3(x, y - 1f));

        AddTriangle();

        vertices.Add(new Vector3(x, y - 1f));
        vertices.Add(new Vector3(x + 1f, y - 0.5f));
        vertices.Add(new Vector3(x + 1f, y - 1f));

        AddTriangle();

    }

    void GenCase13(int x, int y) {

        GenCase1(x, y);

        GenCase4(x, y);

        vertices.Add(new Vector3(x, y - 0.5f));
        vertices.Add(new Vector3(x + 0.5f, y));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));

        AddTriangle();

        vertices.Add(new Vector3(x + 1f, y - 0.5f));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));
        vertices.Add(new Vector3(x + 0.5f, y));

        AddTriangle();

        GenCase8(x, y);

    }

    void GenCase14(int x, int y) {

        GenCase2(x, y);

        GenCase4(x, y);

        vertices.Add(new Vector3(x, y - 0.5f));
        vertices.Add(new Vector3(x + 1f, y - 0.5f));
        vertices.Add(new Vector3(x + 0.5f, y - 1f));

        AddTriangle();

        vertices.Add(new Vector3(x + 1f, y - 0.5f));
        vertices.Add(new Vector3(x, y - 0.5f));
        vertices.Add(new Vector3(x + 0.5f, y));

        AddTriangle();

        GenCase8(x, y);

    }

    void GenCase15(int x, int y) {

        vertices.Add(new Vector3(x, y));
        vertices.Add(new Vector3(x + 1f, y));
        vertices.Add(new Vector3(x, y - 1f));

        AddTriangle();

        vertices.Add(new Vector3(x, y - 1f));
        vertices.Add(new Vector3(x + 1f, y));
        vertices.Add(new Vector3(x + 1f, y - 1f));

        AddTriangle();

    }

}
