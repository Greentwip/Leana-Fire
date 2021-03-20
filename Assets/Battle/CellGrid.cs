using UnityEngine;
using cakeslice;
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

	public Cell cellPrefab;

	public Cell[] cells;

	void Awake()
	{
		Reinit();
	}

	void CreateCell(int x, int z, int i)
	{
		Vector3 position;
		position.x = x * 10f + (0.1f * (width - (width - x)));
		position.y = 0f;
		position.z = z * 10f + (0.1f * (height - (height - z)));

		Cell cell = cells[i] = Instantiate<Cell>(cellPrefab);

		cell.x = x;
		cell.z = z;

		cell.name = "Cell_" + i.ToString();
		cell.GetComponent<Outline>().color = 1;
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
	}

}