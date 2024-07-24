using UnityEngine;
using UnityEngine.Tilemaps;

public class pressurePlateHandler : MonoBehaviour
{
    public TileBase activeTile; // Assign in the Inspector
    public TileBase deactivatedTile; // Assign in the Inspector
    private Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("Player")) // Check for player or other objects
            {
                Vector3 hitPosition = other.gameObject.transform.position;
                Vector3Int cell = tilemap.WorldToCell(hitPosition);
                Debug.Log($"Attempting to delete tile at cell position: {cell}");
                TryDeleteTileAtCell(cell);
                CheckNeighboringCells(cell);
            }
           
                     // Change the tile to the deactivated tile
                    // Add any additional logic for deactivating the pressure plate here
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
            TryDeleteTileAtCell(neighbor);
        }
    }
    private void TryDeleteTileAtCell(Vector3Int cell)
    {
        if (tilemap.GetTile(cell) != null)
        {
            tilemap.SetTile(cell, null); // Delete the tile
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
