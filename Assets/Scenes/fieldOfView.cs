using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public GameObject Square;
    private Vector3 offset = new Vector3(0, 0, 10);
    private Mesh mesh;
    private float startingAngle;
    private float degreesOfVision;
    private float screencenterx = Screen.height / 2;
    private float screencentery = Screen.width / 2;
    // Start is called before the first frame update
    private void Start()
    {
        //creates mesh
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        degreesOfVision = 50f;
    }
    private void LateUpdate()
    {
        //variables
        int numOfRays = 50;
        float angle = startingAngle;
        float anglesPerRay = degreesOfVision / numOfRays;
        float viewDistance = 15f;


        //creates attribute arrays for the vision
        Vector3[] vertices = new Vector3[numOfRays + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[numOfRays * 3];

        //creates the first instnace of the ray
        vertices[0] = Square.transform.position;
        int whichVertex = 1;
        int whichTriangle = 0;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float x = Mathf.Atan2(Mathf.Abs(mousePosition.y-Square.transform.position.y), Mathf.Abs(mousePosition.x - Square.transform.position.x)) * Mathf.Rad2Deg;

        Debug.Log(x+"hi");
        if (mousePosition.x >= Square.transform.position.x)
        {
            if (mousePosition.y >= Square.transform.position.y)
            {
                x = x;
            }
            else
            {
                x = -x;
            }
        }
        else if (mousePosition.x < Square.transform.position.x)
        {
            if (mousePosition.y >= Square.transform.position.y)
            {
                x = -x + 180;
            }
            else
            {
                x = x + 180;
            }
        }
        Debug.Log(x);



        startingAngle = x+degreesOfVision/2;



        for (int i = 0; i <= numOfRays; i++)
        {
            Vector3 vertex;
            RaycastHit2D sightCollider = Physics2D.Raycast(Square.transform.position, getVectorFromAngle(angle), viewDistance,layerMask);
            //Does a hit a wall?
            if (sightCollider.collider == null)
            {
                //No
                vertex = Square.transform.position + getVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //Yes
                vertex = sightCollider.point;
            }
            //Creates a point where the ray is defined
            vertices[whichVertex] = vertex;

            //creates all new rays 
            if (i > 0)
            {
                triangles[whichTriangle] = 0;
                triangles[whichTriangle + 1] = whichVertex - 1;
                triangles[whichTriangle + 2] = whichVertex;

                whichTriangle += 3;
            }

            //changes which ray you are creating
            whichVertex++;
            angle -= anglesPerRay;
        }


        //creates the meshes attributes
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    // converts an angle to a coordinate
    public static Vector3 getVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static float getAngleFromVectorFloat()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float x = Mathf.Atan2(mousePosition.x, mousePosition.y) * Mathf.Rad2Deg;
        if (x < 0) 
        {
            x += 360;
        }
        return x;
    }
}