using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileMovement : MonoBehaviour
{
    Vector3 up = Vector3.zero;
    Vector3 right = new Vector3(0, 90, 0);
    Vector3 down = new Vector3(0, 180, 0);
    Vector3 left = new Vector3(0, 270, 0);
    Vector3 currentDirection = Vector3.zero;

    Vector3 none = new Vector3(10, 0, 0);

    Vector3 nextPosition, destination;

    int gridDistance = 10;
    float speed = 30;

    public int maxMovements = 3;

    BattleSelectionSystem battleSelectionSystem;

    // Start is called before the first frame update
    void Start()
    {
        battleSelectionSystem = FindObjectOfType<BattleSelectionSystem>();
        currentDirection = up;
        nextPosition = Vector3.forward;
        destination = transform.position;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
                                                 destination, 
                                                 speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        battleSelectionSystem.SetSelectedUnit(this);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        bool canMove = false;
        currentDirection = none;

        if (Input.GetKey(KeyCode.W))
        {
            nextPosition = Vector3.forward * gridDistance;
            currentDirection = up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            nextPosition = Vector3.left * gridDistance;
            currentDirection = left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            nextPosition = Vector3.back * gridDistance;
            currentDirection = down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            nextPosition = Vector3.right * gridDistance;
            currentDirection = right;
        }

        canMove = currentDirection != none;

        if(Vector3.Distance(destination, transform.position) <= 0.00001f)
        {
            if (canMove)
            {
                transform.localEulerAngles = currentDirection;
                destination = transform.position + nextPosition;
            }
            
        }

    }
}
