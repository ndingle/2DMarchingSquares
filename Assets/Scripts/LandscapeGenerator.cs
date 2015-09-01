using UnityEngine;
using System.Collections.Generic;

public class LandscapeGenerator : MonoBehaviour {

    //Public variables
    public Sprite[] cases;
    public float resolution = 1f;
    public int width = 20;
    public int height = 20;

    //Private variables
    private bool[,] voxels;
    

    void Awake() {

        //Load the images to use
        cases = Resources.LoadAll<Sprite>("cases");

        //Create the empty array for the data
        voxels = new bool[width, height];

        //Start her up
        GenerateGrid();

    }

    void GenerateGrid() {

        //TODO: Remove this 
        for(var x = 1; x < width-1; x++) {

            for (var y = 1; y < height-1; y++)
            {

                //Set it to something random
                voxels[x, y] = (Random.Range(0, 2) == 0 ? true : false);

            }

        }
        //END TODO

        //CreateCase(0);

        //Start by generating the cases
        for (var x = 0; x < width - 1; x++)
        {

            for (var y = 0; y < height - 1; y++)
            {

                int caseNumber = GetCaseNumber(x, y);

                GameObject newCase = new GameObject();
                Vector3 pos = newCase.transform.position;
                pos.x = x * 1.45f;
                pos.y = y * 1.45f;
                newCase.transform.position = pos;

                Vector3 scale = newCase.transform.localScale;
                scale.x = 2f;
                scale.y = 2f;
                newCase.transform.localScale = scale;

                SpriteRenderer renderer = newCase.AddComponent<SpriteRenderer>();
                Sprite newSprite = cases[caseNumber];
                renderer.sprite = newSprite;

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

    void CreateCase(int caseNumber) { 

        //Check what case it was then make it
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        vertices.Add(new Vector3(0f, 0f));
        vertices.Add(new Vector3(0.5f, 0f));
        vertices.Add(new Vector3(0f, 0.5f));

        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(1);

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();

        print(vertices.Count);

    }

}
