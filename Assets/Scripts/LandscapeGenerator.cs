using UnityEngine;
using System.Collections.Generic;

public class LandscapeGenerator : MonoBehaviour {

    //Public variables
    //public Sprite[] cases;
    public float cellScale = 1f;
    public int width = 6;
    public int height = 6;

    //Private variables
    private float[,] cells;
    
    void Awake() {

        //Create the empty array for the data
        cells = new float[width, height];

        //Start her up
        GenerateCells();
        CreateMesh();

    }

    int NoiseInt(int x, int y, float scale, float mag, float exp)
    {

        return (int)(Mathf.Pow((Mathf.PerlinNoise(x / scale, y / scale) * mag), (exp)));


    }

    void GenerateCells()
    {

        //First test with perlin noise
        for (var x = 1; x < width - 1; x++)
        {

            int heightY = NoiseInt(x, 0, 30, height, 0.8f);
          
            for (int y = 0; y < heightY && y < height; y++)
            {

                cells[x, y] = 1;

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

				if(caseNumber > 0) {

					GameObject newCase = (GameObject)Instantiate((GameObject)Resources.Load (caseNumber.ToString()), 
					                                             new Vector3((x * 0.5f) * cellScale, (y * 0.5f) * cellScale), 
					                                             Quaternion.identity);

					//Change the size of the cases
					Vector3 scale = newCase.transform.localScale;
					scale.x = cellScale;
					scale.y = cellScale;
					newCase.transform.localScale = scale;

				}

            }

        }

    }

    int GetCaseNumber(int x, int y) {

        //Calculate the case number and return
        int caseNumber = (cells[x, y + 1] > 0.5f ? 1 : 0);
        caseNumber += (cells[x + 1, y + 1] > 0.5f ? 2 : 0);
        caseNumber += (cells[x + 1, y] > 0.5f ? 4 : 0);
        caseNumber += (cells[x, y] > 0.5f ? 8 : 0);

        //Return the final case
        return caseNumber;

    }

}
