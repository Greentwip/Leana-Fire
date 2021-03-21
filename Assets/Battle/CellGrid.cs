using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class CellGrid : MonoBehaviour
{
    public void Reinit()
    {
		List<Transform> list = new List<Transform>();

		foreach (Transform child in this.transform)
		{
			list.Add(child);
			
		}

		for(int i = 0; i<list.Count; ++i)
        {
			var child = list[i];

			GameObject.DestroyImmediate(child.gameObject);
		}
		cells = new Cell[height * width];

		for (int z = 0, i = 0; z < height; z++)
		{
			for (int x = 0; x < width; x++)
			{
				CreateCell(x, z, i++);
			}
		}
	}
    public int width = 6;
	public int height = 6;

	public int gridDistance = 10;

	public Cell cellPrefab;

	public Cell[] cells;

	void Awake()
	{
		Reinit();
	}

	void CreateCell(int x, int z, int i)
	{
		Vector3 position;
		position.x = x * 10f + (0.4f * (width - (width - x)));
		position.y = 0f;
		position.z = z * 10f + (0.4f * (height - (height - z)));

		/*position.x = x * 10f;
		position.y = 0f;
		position.z = z * 10f;*/

		Cell cell = cells[i] = Instantiate<Cell>(cellPrefab);

		cell.x = x;
		cell.z = z;

		cell.name = "Cell_" + i.ToString();
		//cell.GetComponent<Outline>().color = 3;
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
	}


	Cell CellAt(int x, int z)
	{
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
		bool exists = false;

		foreach (var cell in cells)
		{
			if (cell.x == x && cell.z == z)
			{
				exists = true;
				break;
			}
		}

		return exists;

	}
	public Vector3 GetArrayValueFromTransform(Transform transform)
    {
		var position = new Vector3(0, 0, 0);
		int x = (int)Mathf.Round(transform.position.x / gridDistance);
		int z = (int)Mathf.Round(transform.position.z / gridDistance);
		if (CellExists(x, z))
        {
			var cell = CellAt(x, z);
			position.x = cell.x;
			position.y = 0;
			position.z = cell.z;
        }

		return position;
    }

}