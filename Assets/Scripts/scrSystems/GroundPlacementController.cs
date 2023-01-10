using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{

    [SerializeField]
    private GameObject placeableObjectPrefab;

    [SerializeField]
    private KeyCode newObjectHotkey = KeyCode.G;

    private float mouseWheelRotation;

    public GameObject currentPlaceableObject;

    public float placeDistance;

    public bool isInPlaceDistance;
    public bool canPlaceObject;
    public float placeDistanceVector;

    private Renderer colorAdjuster;

    public LayerMask placedItem;

    public LayerMask snapPoint;

    public ConnectionPoint connect;



    public float distance;

    public List<float> distanceFrom;

    //public ConnectionPoint connectionPoints;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
        canPlaceObject = true;
    }
    private void Awake()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        HandeNewObjectHotkey();
        if (currentPlaceableObject != null)
        {
            colorAdjuster = currentPlaceableObject.GetComponentInChildren<Renderer>();

            CollisionWithOverlapBox();

            MovePlaceableObject();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }     
    }
    private void RotateFromMouseWheel()
    {
        if (isInPlaceDistance)
        {
            mouseWheelRotation += Input.mouseScrollDelta.y; //amount the scrollwheel has turned since last frame 
            currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
        }
    }

    private void MovePlaceableObject()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * 20f);

        placeDistanceVector = Vector3.Distance(transform.position, currentPlaceableObject.transform.position);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, placeDistance))
        {
            Vector3 newPosition = hit.point;

            if (hit.transform.gameObject.GetComponentInChildren<ConnectionPoint>())
            {

                foreach(Transform child in hit.transform.root)
                {
                    float foo = Vector3.Distance(hit.point, child.position);
                    distanceFrom = new List<float>();

                    distanceFrom.Add(foo);
                   Debug.Log(foo + child.name);
                   if(foo <= 1)
                    {
                        newPosition = child.position;
                        currentPlaceableObject.transform.rotation = hit.transform.localRotation;
                    }
                    //Debug.Log(child.gameObject.transform.position + child.transform.gameObject.name);
                }

     
                //Get the size of the object that you are looking at by accessing its collider component.
                Bounds objectBounds = hit.collider.bounds;

                //Calculate the center point of the object by adding half of its size to its position.
                Vector3 objectCenter = hit.collider.transform.root.GetComponentInChildren<ConnectionPoint>().transform.position + objectBounds.extents;

                //newPosition = objectBounds.center;
                //currentPlaceableObject.transform.root.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                //newPosition = hit.transform.GetComponentInChildren<ConnectionPoint>().connectPointsPosition[0] + objectBounds.center;
                //currentPlaceableObject.transform.root.rotation = hit.transform.GetComponentInChildren<ConnectionPoint>().connectPointsRotation[0];


            }
            currentPlaceableObject.transform.position = newPosition; 
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal); 
        }

        if (placeDistanceVector < 15)
        {
            isInPlaceDistance = true;
            colorAdjuster.material.SetColor("_Color", Color.green);
        }
        else
        {
            colorAdjuster.material.SetColor("_Color", Color.red);
            isInPlaceDistance = false;
        }
    }
    private void CollisionWithOverlapBox()
    {
        //Collider[] intersecting = Physics.OverlapBox(currentPlaceableObject.transform.position, currentPlaceableObject.GetComponentInChildren<BoxCollider>().bounds.size / 2f,Quaternion.identity, placedItem); 
        //foreach (Collider collid in intersecting)
        //{    
            //Debug.Log(collid.name);
        //}
    }

    void MoveToLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            MoveToLayer(child, layer);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0) && isInPlaceDistance)
        {
            colorAdjuster.material.SetColor("_Color", Color.grey);

           // cycle through all colliders in children and enable them
            var collidersObj = currentPlaceableObject.transform.GetComponentsInChildren<Collider>();
            for (var index = 0; index < collidersObj.Length; index++)
            {
                var colliderItem = collidersObj[index];
                 colliderItem.enabled = true;
             }

            var connectionPoint = currentPlaceableObject.transform.GetComponentsInChildren<ConnectionPoint>();
            for (var index = 0; index < connectionPoint.Length; index++)
            {
                var connectionPointItem = connectionPoint[index];
                connectionPointItem.enabled = true;
            }

            MoveToLayer(currentPlaceableObject.transform, 8);
            currentPlaceableObject.transform.DetachChildren();
            Destroy(currentPlaceableObject);
            currentPlaceableObject = null;
            //currentPlaceableObject = Instantiate(placeableObjectPrefab);
        }
        else
        {
            return;
        }
    }
    private void HandeNewObjectHotkey()
    {
        if (Input.GetKeyDown(newObjectHotkey))
        {
            Debug.Log("build mode: ON");
            if (currentPlaceableObject == null)
            {
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
            }
            else
            {
                Destroy(currentPlaceableObject); //pressing the button again will destroy current object
                Debug.Log("build mode: OFF");
            }
        }
    }
}
       









