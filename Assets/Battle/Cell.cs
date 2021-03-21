using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x { get; set; }
    public int z { get; set; }

    public int visited = -1;

    BattleSelectionSystem battleSelectionSystem = null;

    //public Outline Outline { get; private set; }

    public GameObject outline;

    public Material Material { get; private set; }

    Material OutlineMaterial { get; set; }

    private void Start()
    {
        battleSelectionSystem = FindObjectOfType<BattleSelectionSystem>();
        var cubeRenderer = GetComponent<Renderer>();

        //Outline = GetComponent<Outline>();
        Material = cubeRenderer.material;
        Material.SetFloat("_Alpha", 0);

        OutlineMaterial = outline.GetComponent<Renderer>().material;
        OutlineMaterial.SetFloat("_Alpha", 0);

    }

    private void OnMouseDown()
    {
        battleSelectionSystem.SetSelectedCell(this);
    }

    void OnMouseOver()
    {
        outline.SetActive(true);
        OutlineMaterial.SetFloat("_Alpha", 0.250f);
    }
    void OnMouseExit()
    {
        outline.SetActive(false);
        OutlineMaterial.SetFloat("_Alpha", 0);
    }

}