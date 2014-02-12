using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateLevel : MonoBehaviour {

    private Mesh mesh;

    public List<Vector3> newVertices = new List<Vector3>();
    public List<Color> newColors = new List<Color>();
    public List<int> newTriangles = new List<int>();


    private Mesh mesh2;
    public List<Vector3> newVertices2 = new List<Vector3>();
    public List<Color> newColors2 = new List<Color>();
    public List<int> newTriangles2 = new List<int>();

    public MeshFilter secondMeshFilter;
    public MeshCollider secondMeshCollider;

    public List<Vector2> newUV = new List<Vector2>();

    private Color earth = new Color(139f / 256f, 69f / 256f, 19f / 256f);
    private Color grass = new Color(124f / 256f, 252f / 256f, 0f / 256f);

    private Color TopBad = new Color(200f / 256f, 30f / 256f, 20f / 256f);

    [Range(1, 25)]
    public int resolutionY = 5;

    [Range(0.1f, 20.0f)]
    public float resolutionX = 1.0f;

    [Range(1, 1000)]
    public int StripeCount = 5;

    [Range(0.01f, 2.0f)]
    public float maxRandomPositionZ = 1.0f;

    [Range(2f, 400f)]
    public float height = 2.0f;

    [Range(0f, 2f)]
    public float maxHeightChange = 0.5f;

    [Range(-3f, 3f)]
    public float minHeightChangePerStripe = 0.5f;
    [Range(-3f, 3f)]
    public float maxHeightChangePerStripe = 0.5f;

    private float currentYDiff = 0.0f;

    private float lastHeightChange = 0.0f;

    [Range(0.005f, 0.2f)]
    public float smoothing = 0.02f;

    [Range(0f, 10.0f)]
    public float minSplit = 2.0f;
    [Range(0f, 10.0f)]
    public float maxSplit = 2.0f;

    float lastYDiffChange = 0.0f;

    void BuildMesh()
    {

        Random.seed = 0;


        minHeightChangePerStripe *= resolutionX;
        maxHeightChangePerStripe *= resolutionX;

        mesh = GetComponent<MeshFilter>().mesh;
        mesh2 = secondMeshFilter.mesh;

        BuildStripeBottom(transform.position, 0, height);
        BuildStripeTop(transform.position, 0, height);

        for (int i = 1; i < StripeCount; i++)
        {
            AddOneStrip(i);
        }

        UpdateMeshs();
    }

    private void AddOneStrip(int i)
    {

        float currentYDiffChange = 0.0f;

        if (lastYDiffChange > smoothing)
        {
            currentYDiffChange = Random.Range(minHeightChangePerStripe * smoothing, maxHeightChangePerStripe);
        }
        else if (lastYDiffChange < -smoothing)
        {
            currentYDiffChange = Random.Range(minHeightChangePerStripe, maxHeightChangePerStripe * smoothing);
        }
        else
        {
            currentYDiffChange = Random.Range(minHeightChangePerStripe, maxHeightChangePerStripe);
        }

        currentYDiff += currentYDiffChange;
        lastYDiffChange = currentYDiffChange;

        BuildStripeSecondBottom(transform.position, i, height);


        BuildStripeSecondTop(transform.position, i, height);

        UpdateMeshs();
    }

    private void UpdateMeshs()
    {
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.colors = newColors.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();

        mesh2.Clear();
        mesh2.vertices = newVertices2.ToArray();
        mesh2.colors = newColors2.ToArray();
        mesh2.triangles = newTriangles2.ToArray();
        mesh2.Optimize();
        mesh2.RecalculateNormals();

        secondMeshFilter.mesh = mesh2;
        secondMeshCollider.sharedMesh = mesh2;

        secondMeshCollider.enabled = false;
        secondMeshCollider.enabled = true;

        GetComponent<MeshCollider>().sharedMesh = mesh;

        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshCollider>().enabled = true;
    }

    void BuildStripeTop(Vector3 startingPosition, int startIndex, float height)
    {
        float vertDiffY = height / (float)resolutionY;

        float x = startingPosition.x;
        float y = startingPosition.y;
        float z = startingPosition.z;

        y = newVertices[startIndex * 4 * resolutionY].y + 1.5f * height  + Random.Range(minSplit, maxSplit);

        for (int i = 0; i < resolutionY; i++)
        {
            float diffY = vertDiffY * (float)i;

            if (i == 0)
            {
                //Nearly flat but a bit thingy in z
                newVertices2.Add(new Vector3(x, y - diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ)));
                newVertices2.Add(new Vector3(x, y - vertDiffY - diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ)));
                newVertices2.Add(new Vector3(x + resolutionX, y - vertDiffY - diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ)));
                newVertices2.Add(new Vector3(x + resolutionX, y - diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ)));

                newColors2.Add(earth);
                newColors2.Add(earth);
                newColors2.Add(earth);
                newColors2.Add(earth);
            }
            else if (i == resolutionY - 1)
            {
                newVertices2.Add(newVertices2[startIndex + i * 4 - 3]);
                newVertices2.Add(new Vector3(x, y - vertDiffY * 0.2f - diffY + vertDiffY, z + 1));
                newVertices2.Add(new Vector3(x + resolutionX, y - vertDiffY * 0.2f - diffY + vertDiffY, z + 1));
                newVertices2.Add(newVertices2[startIndex + i * 4 - 2]);

                newColors2.Add(TopBad);
                newColors2.Add(TopBad);
                newColors2.Add(TopBad);
                newColors2.Add(TopBad);
            }
            else if (i == resolutionY - 2)
            {
                newVertices2.Add(newVertices2[startIndex + i * 4 - 3]);
                newVertices2.Add(new Vector3(x, y - vertDiffY * 0.2f - diffY, z + Random.Range(maxRandomPositionZ + 0.1f, maxRandomPositionZ + maxRandomPositionZ))); //Links Unten
                newVertices2.Add(new Vector3(x + resolutionX, y - vertDiffY * 0.2f - diffY, z + Random.Range(maxRandomPositionZ + 0.1f, maxRandomPositionZ + maxRandomPositionZ))); //Rechts Unten
                newVertices2.Add(newVertices2[startIndex + i * 4 - 2]);

                newColors2.Add(earth);
                newColors2.Add(TopBad);
                newColors2.Add(TopBad);
                newColors2.Add(earth);
            }
            else
            {
                newVertices2.Add(newVertices2[startIndex + i * 4 - 3]);//Links Oben
                newVertices2.Add(new Vector3(x, y - vertDiffY - diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ))); //Links Unten
                newVertices2.Add(new Vector3(x + resolutionX, y - vertDiffY - diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ))); //Rechts Unten
                newVertices2.Add(newVertices2[startIndex + i * 4 - 2]);//Rechts Oben

                newColors2.Add(earth);
                newColors2.Add(earth);
                newColors2.Add(earth);
                newColors2.Add(earth);
            }


            newTriangles2.Add(i * 4);
            newTriangles2.Add(i * 4 + 3);
            newTriangles2.Add(i * 4 + 2);

            newTriangles2.Add(i * 4);
            newTriangles2.Add(i * 4 + 2);
            newTriangles2.Add(i * 4 + 1);
        }
    }

    void BuildStripeSecondTop(Vector3 startingPosition, int startIndex, float height)
    {
        float vertDiffY = height / (float)resolutionY;

        float x = startingPosition.x + startIndex * resolutionX;
        float y = startingPosition.y + currentYDiff;
        float z = startingPosition.z;

        Debug.Log("Vertex Position of ID: " + (startIndex * 4 * resolutionY).ToString());

        y = newVertices[startIndex * 4 * resolutionY + 3].y + 1.5f * height + Random.Range(minSplit, maxSplit);

        x = newVertices[startIndex * 4 * resolutionY].x;

        startIndex = startIndex * (resolutionY * 4);

        for (int i = 0; i < resolutionY; i++)
        {
            float diffY = vertDiffY * (float)i;

            if (i == 0)
            {
                //Nearly flat but a bit thingy in z
                newVertices2.Add(newVertices2[startIndex - (resolutionY * 4) + 3 + (i * 4)]); //Links Unten
                newVertices2.Add(newVertices2[startIndex - (resolutionY * 4) + 2 + (i * 4)]); //Links Oben
                newVertices2.Add(new Vector3(x + resolutionX, y - vertDiffY, 0)); //Rechts Oben
                newVertices2.Add(new Vector3(x + resolutionX, y, 0)); //Rechts Unten

                newColors2.Add(earth);
                newColors2.Add(earth);
                newColors2.Add(earth);
                newColors2.Add(earth);
            }
            else if (i == resolutionY - 1)
            {
                newVertices2.Add(newVertices2[startIndex - (resolutionY * 4) + 3 + (i * 4)]); //Links Unten
                newVertices2.Add(newVertices2[startIndex - (resolutionY * 4) + 3 + (i * 4) - 1]); //Links Oben
                newVertices2.Add(new Vector3(x + resolutionX, y - vertDiffY * 0.2f - diffY + vertDiffY, z + 1));
                newVertices2.Add(newVertices2[startIndex + i * 4 - 2]);

                newColors2.Add(TopBad);
                newColors2.Add(TopBad);
                newColors2.Add(TopBad);
                newColors2.Add(TopBad);
            }
            else if (i == resolutionY - 2)
            {
                newVertices2.Add(newVertices2[startIndex - (resolutionY * 4) + 3 + (i * 4)]); //Links Unten
                newVertices2.Add(newVertices2[startIndex - (resolutionY * 4) + 3 + (i * 4) - 1]); //Links Oben
                newVertices2.Add(new Vector3(x + resolutionX, y - vertDiffY * 0.2f - diffY, z + Random.Range(maxRandomPositionZ + 0.1f, maxRandomPositionZ + maxRandomPositionZ))); //Rechts Unten
                newVertices2.Add(newVertices2[startIndex + i * 4 - 2]); //Rechts Unten

                newColors2.Add(earth);
                newColors2.Add(TopBad);
                newColors2.Add(TopBad);
                newColors2.Add(earth);
            }
            else
            {
                newVertices2.Add(newVertices2[startIndex - (resolutionY * 4) + 3 + (i * 4)]); //Links Unten
                newVertices2.Add(newVertices2[startIndex - (resolutionY * 4) + 3 + (i * 4) - 1]); //Links Oben
                newVertices2.Add(new Vector3(x + resolutionX, y - vertDiffY - diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ)));
                newVertices2.Add(newVertices2[startIndex + i * 4 - 2]); //Rechts Unten

                newColors2.Add(earth);
                newColors2.Add(earth);
                newColors2.Add(earth);
                newColors2.Add(earth);
            }

            newTriangles2.Add(startIndex + i * 4);
            newTriangles2.Add(startIndex + i * 4 + 3);
            newTriangles2.Add(startIndex + i * 4 + 2);

            newTriangles2.Add(startIndex + i * 4);
            newTriangles2.Add(startIndex + i * 4 + 2);
            newTriangles2.Add(startIndex + i * 4 + 1);
        }
    }

    void BuildStripeBottom(Vector3 startingPosition, int startIndex, float height)
    {
        float vertDiffY = height / (float)resolutionY;

        float x = startingPosition.x;
        float y = startingPosition.y;
        float z = startingPosition.z;

        for (int i = 0; i < resolutionY; i++)
        {
            float diffY = vertDiffY * (float)i;

            if(i == 0)
            {
                //Nearly flat but a bit thingy in z
                newVertices.Add(new Vector3(x, y + diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ)));
                newVertices.Add(new Vector3(x, y + vertDiffY + diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ)));
                newVertices.Add(new Vector3(x + resolutionX, y + vertDiffY + diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ)));
                newVertices.Add(new Vector3(x + resolutionX, y + diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ)));

                newColors.Add(earth);
                newColors.Add(earth);
                newColors.Add(earth);
                newColors.Add(earth);
            }
            else if (i == resolutionY - 1)
            {
                newVertices.Add(newVertices[i * 4 - 3]);
                newVertices.Add(new Vector3(x, y + vertDiffY * 0.2f + diffY - vertDiffY, z + 1));
                newVertices.Add(new Vector3(x + resolutionX, y + vertDiffY * 0.2f + diffY - vertDiffY, z + 1));
                newVertices.Add(newVertices[i * 4 - 2]);

                newColors.Add(grass);
                newColors.Add(grass);
                newColors.Add(grass);
                newColors.Add(grass);
            }
            else if (i == resolutionY - 2)
            {
                newVertices.Add(newVertices[i * 4 - 3]); //Links Unten
                newVertices.Add(new Vector3(x, y + vertDiffY * 0.2f + diffY, z + Random.Range(maxRandomPositionZ+0.1f, maxRandomPositionZ + maxRandomPositionZ))); //Links Unten
                newVertices.Add(new Vector3(x + resolutionX, y + vertDiffY * 0.2f + diffY, z + Random.Range(maxRandomPositionZ + 0.1f, maxRandomPositionZ + maxRandomPositionZ))); //Rechts Unten
                newVertices.Add(newVertices[i * 4 - 2]); //Rechts Unten

                newColors.Add(earth);
                newColors.Add(grass);
                newColors.Add(grass);
                newColors.Add(earth);
            }
            else
            {
                newVertices.Add(newVertices[i*4 - 3]);
                newVertices.Add(new Vector3(x, y + vertDiffY + diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ)));
                newVertices.Add(new Vector3(x + resolutionX, y + vertDiffY + diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ)));
                newVertices.Add(newVertices[i * 4 - 2]);

                newColors.Add(earth);
                newColors.Add(earth);
                newColors.Add(earth);
                newColors.Add(earth);
            }

            

            newTriangles.Add(i*4);
            newTriangles.Add(i*4 + 1);
            newTriangles.Add(i*4 + 2);

            newTriangles.Add(i*4);
            newTriangles.Add(i*4 + 2);
            newTriangles.Add(i*4 + 3);
        }
    }

    void BuildStripeSecondBottom(Vector3 startingPosition, int startIndex, float height)
    {
        float vertDiffY = height / (float)resolutionY;

        float x = startingPosition.x + startIndex * resolutionX;
        float y = startingPosition.y + currentYDiff;
        float z = startingPosition.z;

        x = newVertices[startIndex * 4 * resolutionY - 1].x;

        startIndex = startIndex * (resolutionY * 4);

        for (int i = 0; i < resolutionY; i++)
        {
            float diffY = vertDiffY * (float)i;

            if (i == 0)
            {
                //Nearly flat but a bit thingy in z
                newVertices.Add(newVertices[startIndex - (resolutionY * 4) + 3 + (i * 4)]); //Links Unten
                newVertices.Add(newVertices[startIndex - (resolutionY * 4) + 3 + (i * 4) - 1]); //Links Oben
                newVertices.Add(new Vector3(x + resolutionX, y + vertDiffY + diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ))); //Rechts Oben
                newVertices.Add(new Vector3(x + resolutionX, y + diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ))); //Rechts Unten

                newColors.Add(earth);
                newColors.Add(earth);
                newColors.Add(earth);
                newColors.Add(earth);
            }
            else if (i == resolutionY - 1)
            {
                newVertices.Add(newVertices[startIndex - (resolutionY * 4) + 3 + (i * 4)]); //Links Unten
                newVertices.Add(newVertices[startIndex - (resolutionY * 4) + 3 + (i * 4) - 1]); //Links Oben
                newVertices.Add(new Vector3(x + resolutionX, y + vertDiffY * 0.2f + diffY - vertDiffY, z + 1));
                newVertices.Add(newVertices[startIndex + i * 4 - 2]);

                newColors.Add(grass);
                newColors.Add(grass);
                newColors.Add(grass);
                newColors.Add(grass);
            }
            else if (i == resolutionY - 2)
            {
                newVertices.Add(newVertices[startIndex - (resolutionY * 4) + 3 + (i * 4)]); //Links Unten
                newVertices.Add(newVertices[startIndex - (resolutionY * 4) + 3 + (i * 4) - 1]); //Links Oben
                newVertices.Add(new Vector3(x + resolutionX, y + vertDiffY * 0.2f + diffY, z + Random.Range(maxRandomPositionZ+0.1f, maxRandomPositionZ + maxRandomPositionZ))); //Rechts Unten
                newVertices.Add(newVertices[startIndex + i * 4 - 2]); //Rechts Unten

                newColors.Add(earth);
                newColors.Add(grass);
                newColors.Add(grass);
                newColors.Add(earth);
            }
            else
            {
                newVertices.Add(newVertices[startIndex - (resolutionY * 4) + 3 + (i * 4)]); //Links Unten
                newVertices.Add(newVertices[startIndex - (resolutionY * 4) + 3 + (i * 4) - 1]); //Links Oben
                newVertices.Add(new Vector3(x + resolutionX, y + vertDiffY + diffY, z + Random.Range(-maxRandomPositionZ, maxRandomPositionZ)));
                newVertices.Add(newVertices[startIndex + i * 4 - 2]); //Rechts Unten

                newColors.Add(earth);
                newColors.Add(earth);
                newColors.Add(earth);
                newColors.Add(earth);
            }

           

            newTriangles.Add(startIndex + i * 4);
            newTriangles.Add(startIndex + i * 4 + 1);
            newTriangles.Add(startIndex + i * 4 + 2);

            newTriangles.Add(startIndex + i * 4);
            newTriangles.Add(startIndex + i * 4 + 2);
            newTriangles.Add(startIndex + i * 4 + 3);
        }
    }

    private void DeleteFirstStripe()
    {
        for (int i = 0; i < 4 * resolutionY; i++)
        {
            newVertices.RemoveAt(0);
            newColors.RemoveAt(0);
            newVertices2.RemoveAt(0);
            newColors2.RemoveAt(0);
        }
        for (int i = 0; i < 6 * resolutionY; i++)
        {
           // newTriangles.RemoveAt(0);
            //newTriangles2.RemoveAt(0);
        }
    }

	// Use this for initialization
	void Start () {
        BuildMesh();
	}

    private float movement = 0.0f;

	// Update is called once per frame
	void Update () {
        movement += PlayerScript.currentMovement;

        if (movement >= resolutionX)
        {
            movement -= resolutionX;

            //Delete one stripe and add one
            DeleteFirstStripe();
            AddOneStrip(StripeCount - 1);
        }
	}
}
