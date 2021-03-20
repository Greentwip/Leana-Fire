using UnityEngine;

[ExecuteInEditMode]
public class CellGrid : MonoBehaviour
{

	public int width = 6;
	public int height = 6;

	public Cell cellPrefab;

	Cell[] cells;

	void Awake()
	{
		cells = new Cell[height * width];

		for (int z = 0, i = 0; z < height; z++)
		{
			for (int x = 0; x < width; x++)
			{
				CreateCell(x, z, i++);
			}
		}
	}

	void CreateCell(int x, int z, int i)
	{
		Vector3 position;
		position.x = x * 10f;
		position.y = 0f;
		position.z = z * 10f;

		Cell cell = cells[i] = Instantiate<Cell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
	}

}