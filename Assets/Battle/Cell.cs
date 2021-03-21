using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x { get; set; }
    public int z { get; set; }

    public int visited = -1;

    BattleSelectionSystem battleSelectionSystem = null;

    private void Start()
    {
        battleSelectionSystem = FindObjectOfType<BattleSelectionSystem>();
    }

    private void OnMouseDown()
    {
        battleSelectionSystem.SetSelectedCell(this);
    }
}