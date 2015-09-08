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

    }

    void GenerateVoxels() {

        //First test with perlin noise
        //TODO: Implement it

        //TODO: Remove this 
        for (var x = 1; x < width - 1; x++)
        {

            for (var y = 1; y < height - 1; y++)
            {

                //Set it to something random
                voxels[x, y] = (Random.Range(0, 2) == 0 ? true : false);

            }

        }

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

					GameObject newCase = (GameObject)Instantiate((GameObject)Resources.Load (caseNumber.ToString()), 
                                                                 new Vector3(x * 0.5f, y * 0.5f), 
                                                                 Quaternion.identity);

				}

            }

        }

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

}
