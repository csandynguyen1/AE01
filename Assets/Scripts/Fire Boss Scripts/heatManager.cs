using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class heatManager : MonoBehaviour
{
    public TileBase activeTile; // Assign in the Inspector
    public TileBase deactivatedTile; // Assign in the Inspector
    private Tilemap tilemap;
    public HeatBar heatBar;
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
            DeactivateTile(cell);
            //TryDeleteTileAtCell(cell);
            CheckNeighboringCells(cell);
        }

        // Change the tile to the deactivated tile
        // Add any additional logic for deactivating the pressure plate here
    }

    private void DeactivateTile(Vector3Int cell)
    {
        if (tilemap.GetTile(cell) == activeTile) // Ensure it's the correct tile before changing
        {
            tilemap.SetTile(cell, deactivatedTile); // Set the tile to inactive
            heatBar.setHeat(0);
        }
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
            DeactivateTile(neighbor);
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

}
