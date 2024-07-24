using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PuzzleListenerMap : MonoBehaviour
{
    public int puzzleUpdate = 0;

    public EarthTotem TotemOne;
    public EarthTotem TotemTwo;
    public EarthTotem TotemThree;
    public EarthTotem TotemFour;


    public Tilemap transition;

    // Start is called before the first frame update
    private void Start()
    {
        // Register each PuzzlePlate object to listen for updates
        // Register event listeners for each puzzle plate
        TotemOne.OnTotemActivated.AddListener(UpdatePuzzle);
        TotemTwo.OnTotemActivated.AddListener(UpdatePuzzle);
        TotemThree.OnTotemActivated.AddListener(UpdatePuzzle);
        TotemFour.OnTotemActivated.AddListener(UpdatePuzzle);
    }

    // Method to handle updates from PuzzlePlate objects
    private void UpdatePuzzle()
    {
        // Implement puzzle update logic here
        Debug.Log("Puzzle plate activated!");
        puzzleUpdate++;
        if (puzzleUpdate == 4)
        {
            

            // Load tilemap with scene transition here
            ActivateTilemap();
        }
    }

    public void ActivateTilemap()
    {
        transition.gameObject.SetActive(true);
    }
}
