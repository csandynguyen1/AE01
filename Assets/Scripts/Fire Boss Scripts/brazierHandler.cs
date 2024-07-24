using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class brazierHandler : MonoBehaviour
{
    public TileBase activeTile; // Assign in the Inspector
    public TileBase deactivatedTile; // Assign in the Inspector
    private Tilemap tilemap;
    public int totalBraziers;
    private int activeBraziers = 0;
    public FireBossAI fireAI;
    public float delay = 5.0f;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    private void CheckAllBraziersActive()
    {
        if (activeBraziers == totalBraziers)
        {
            fireAI.deactivateShield(); // Call the method to deactivate the shield
        }
        else
        {
            fireAI.activateShield();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("Brazier")) // Check for player or other objects
            {
                Vector3 hitPosition = other.gameObject.transform.position;
                Vector3Int cell = tilemap.WorldToCell(hitPosition);
                Debug.Log($"Attempting to delete tile at cell position: {cell}");
                ActivateTile(cell);
                //TryDeleteTileAtCell(cell);
                CheckNeighboringCells(cell);
            }
           
                     // Change the tile to the deactivated tile
                    // Add any additional logic for deactivating the pressure plate here
    }

    private void ActivateTile(Vector3Int cell)
    {
        if (tilemap.GetTile(cell) == deactivatedTile) // Ensure it's the correct tile before changing
        {
            tilemap.SetTile(cell, activeTile); // Set the tile to active
            activeBraziers++;
            CheckAllBraziersActive();
            StartCoroutine(DeactivateTileAfterDelay(cell, delay)); // Deactivate after 10 seconds
        }
    }

    IEnumerator DeactivateTileAfterDelay(Vector3Int cell, float delay)
    {
        yield return new WaitForSeconds(delay);
        activeBraziers--;
        CheckAllBraziersActive();
        tilemap.SetTile(cell, deactivatedTile); // Set the tile back to deactivated
    }
    private void CheckNeighboringCells(Vector3Int primaryCell)
    {
        Vector3Int[] neighbors = new Vector3Int[]
        {
        new Vector3Int(primaryCell.x + 1, primaryCell.y, primaryCell.z),
        new Vector3Int(primaryCell.x - 1, primaryCell.y, primaryCell.z),
        new Vector3Int(primaryCell.x, primaryCell.y + 1, primaryCell.z),
        new Vector3Int(primaryCell.x, primaryCell.y - 1, primaryCell.z)
        };

        foreach (Vector3Int neighbor in neighbors)
        {
            // Optionally, you can add further checks here to ensure the neighbor interaction
            // is consistent with game logic, such as checking for specific tile types or conditions
            ActivateTile(neighbor);
        }
    }
    private void TryDeleteTileAtCell(Vector3Int cell)
    {
        if (tilemap.GetTile(cell) != null)
        {
            tilemap.SetTile(cell, activeTile); // Delete the tile
            Debug.Log($"Deleted tile at cell position: {cell}");
        }
    }

    void OnDrawGizmos()
    {
        if (tilemap == null)
            return;

        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;
        Gizmos.color = Color.yellow;

        for (int y = bounds.min.y; y < bounds.max.y; y++)
        {
            for (int x = bounds.min.x; x < bounds.max.x; x++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                Vector3 cellCenter = tilemap.GetCellCenterWorld(cell);
                if (tilemap.HasTile(cell))
                {
                    // Draw a small sphere at each cell center
                    Gizmos.DrawSphere(cellCenter, 0.1f);

                    // Optionally, use Handles to draw text (requires UnityEditor namespace)
#if UNITY_EDITOR
                    UnityEditor.Handles.Label(cellCenter, cell.ToString());
#endif
                }
            }
        }
    }
}
