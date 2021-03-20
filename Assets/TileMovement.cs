using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using FloodSpill;
public class TileMovement : MonoBehaviour
{
    Vector3 up = Vector3.zero;
    Vector3 right = new Vector3(0, 90, 0);
    Vector3 down = new Vector3(0, 180, 0);
    Vector3 left = new Vector3(0, 270, 0);
    Vector3 currentDirection = Vector3.zero;

    Vector3 none = new Vector3(10, 0, 0);

    Vector3 nextPosition, destination, direction;

    int gridDistance = 10;
    float speed = 30;

    int maxMovements = 3;

    public int startX = 0;
    public int startZ = 0;

    public int endX = 5;
    public int endZ = 5;

    public List<Cell> path = new List<Cell>();

    public bool findDistance = false;
    private bool traceCellsRange = false;

    // Start is called before the first frame update
    void Start()
    {
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

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    Cell CellAt(int x, int z)
    {
        var cellGrid = FindObjectOfType<CellGrid>();
        var cells = cellGrid.cells;

        Cell result = null;

        foreach (var cell in cells)
        {
            if (cell.x == x && cell.z == z)
            {
                result = cell;
                break;
            }
        }

        return result;
    }
    bool CellExists(int x, int z)
    {
        var cellGrid = FindObjectOfType<CellGrid>();
        var cells = cellGrid.cells;

        bool exists = false;

        foreach (var cell in cells)
        {
            if(cell.x == x && cell.z == z)
            {
                exists = true;
                break;
            }
        }

        return exists;

    }
    bool TestDirection(int x, int z, int step, Direction direction)
    {
        var cellGrid = FindObjectOfType<CellGrid>();
        int rows = cellGrid.height;
        int columns = cellGrid.width;

        switch (direction)
        {
            case Direction.Left:
                if (x - 1 > -1 && CellExists(x - 1, z) && CellAt(x - 1, z).GetComponent<Cell>().visited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case Direction.Down:
                if (z - 1 > -1 && CellExists(x, z-1) && CellAt(x, z-1).GetComponent<Cell>().visited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case Direction.Right:
                if (x + 1 < columns && CellExists(x + 1, z) && CellAt(x + 1, z).GetComponent<Cell>().visited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case Direction.Up:
                if (z + 1 < rows && CellExists(x, z + 1) && CellAt(x, z + 1).GetComponent<Cell>().visited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
        return false;

    }

    private void SetVisited(int x, int z, int step)
    {
        if(CellExists(x, z))
        {
            Cell cell = CellAt(x, z);
            cell.visited = step;
        }
    }

    private void SetDistance()
    {
        InitialSetup();

        int x = startX;
        int z = startZ;

        var cellGrid = FindObjectOfType<CellGrid>();

        int rows = cellGrid.height;
        int columns = cellGrid.width;


        for (int step = 1; step < rows * columns; ++step)
        {
            foreach(var cell in cellGrid.cells)
            {
                if(cell.visited == step - 1)
                {
                    TestFourDirections(cell.x, cell.z, step);
                }
            }
        }
    }

    private void SetPath()
    {
        int step = -1;
        int x = endX;
        int z = endZ;

        var tempList = new List<Cell>();
        path.Clear();

        if(CellExists(x, z))
        {
            var cell = CellAt(x, z);
            if(cell.visited > 0)
            {
                path.Add(cell);
                step = cell.visited - 1;
            }
        }
        else
        {
            return;
        }

        for (int i = step; step > -1; step--) {
            if (TestDirection(x, z, step, Direction.Up))
            {
                tempList.Add(CellAt(x, z + 1));
            }

            if (TestDirection(x, z, step, Direction.Right))
            {
                tempList.Add(CellAt(x + 1, z));
            }

            if (TestDirection(x, z, step, Direction.Down))
            {
                tempList.Add(CellAt(x, z - 1));
            }

            if (TestDirection(x, z, step, Direction.Left))
            {
                tempList.Add(CellAt(x - 1, z));
            }

            Cell tempObject = FindClosest(CellAt(x, z).gameObject.transform, tempList);
            path.Add(tempObject);

            x = tempObject.x;
            z = tempObject.z;

            tempList.Clear();

        }

    }

    private void OnMouseDown()
    {
        traceCellsRange = !traceCellsRange;
    }


    Cell FindClosest(Transform targetLocation, List<Cell> list)
    {
        var cellGrid = FindObjectOfType<CellGrid>();

        int rows = cellGrid.height;
        int columns = cellGrid.width;

        float currentDistance = rows * columns;

        int indexNumber = 0;
        for(int i = 0; i < list.Count; ++i)
        {
            if(Vector3.Distance(targetLocation.position, list[i].transform.position) < currentDistance){
                currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
                indexNumber = i;
            }
        }

        return list[indexNumber];
    }

    private void TestFourDirections(int x, int z, int step)
    {
        if (TestDirection(x, z, -1, Direction.Up))
        {
            SetVisited(x, z + 1, step);
        }

        if (TestDirection(x, z, -1, Direction.Right))
        {
            SetVisited(x + 1, z, step);
        }

        if (TestDirection(x, z, -1, Direction.Down))
        {
            SetVisited(x, z - 1, step);
        }

        if (TestDirection(x, z, -1, Direction.Left))
        {
            SetVisited(x - 1, z, step);
        }
    }

    private void ClearCellsColor()
    {
        var cellGrid = FindObjectOfType<CellGrid>();

        foreach (var cell in cellGrid.cells)
        {
            cell.GetComponent<Outline>().color = 1;
        }
    }

    private void HighlightPath()
    {
        foreach(var cell in path)
        {
            cell.GetComponent<Outline>().color = 0;
        }
    }
    private void HighlightCells()
    {
        bool reachedDestination = transform.position == destination;
        if (reachedDestination)
        {
            var cellGrid = FindObjectOfType<CellGrid>();

            int thisX = 0;
            int thisZ = 0;

            foreach (var cell in cellGrid.cells)
            {
                int cellX = (int)cell.gameObject.transform.position.x;
                int cellZ = (int)cell.gameObject.transform.position.z;

                if (cellX == (int)transform.position.x && cellZ == (int)transform.position.z)
                {
                    thisX = cell.x;
                    thisZ = cell.z;

                }
            }
            var markMatrix = new int[cellGrid.width, cellGrid.height];
            var floodParameters = new FloodParameters(thisX, thisZ)
            {
                NeighbourhoodType = NeighbourhoodType.Four
            };

            new FloodSpiller().SpillFlood(floodParameters, markMatrix);

            for (int x = 0; x < cellGrid.width; x++)
            {
                for (int z = 0; z < cellGrid.height; z++)
                {
                    int spilledCell = markMatrix[x, z];
                    if (spilledCell <= maxMovements)
                    {
                        foreach (var cell in cellGrid.cells)
                        {
                            if (cell.x == x && cell.z == z)
                            {
                                cell.GetComponent<Outline>().color = 2;
                            }
                        }
                    }
                }
            }
        }
       
 
    }

    private void InitialSetup()
    {
        var cellGrid = FindObjectOfType<CellGrid>();
        var cells = cellGrid.cells;

        foreach (var cell in cells)
        {
            cell.visited = -1;
        }

        CellAt(startX, startZ).visited = 0;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ClearCellsColor();
        if (traceCellsRange)
        {
            HighlightCells();
        }
        
        HighlightPath();

        if (findDistance)
        {
            SetDistance();
            SetPath();
            findDistance = false;
        }

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
