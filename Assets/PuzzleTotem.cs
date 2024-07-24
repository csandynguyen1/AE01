using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PuzzleTotem : MonoBehaviour
{
    public Animator animator;
    public int puzzleUpdate = 0;

    public PuzzlePlate PlateOne;
    public PuzzlePlate PlateTwo;
    public PuzzlePlate PlateThree;
    public PuzzlePlate PlateFour;


    public Tilemap transition;

    // Start is called before the first frame update
    private void Start()
    {
        // Register each PuzzlePlate object to listen for updates
        // Register event listeners for each puzzle plate
        PlateOne.OnPlateActivated.AddListener(UpdatePuzzle);
        PlateTwo.OnPlateActivated.AddListener(UpdatePuzzle);
        PlateThree.OnPlateActivated.AddListener(UpdatePuzzle);
        PlateFour.OnPlateActivated.AddListener(UpdatePuzzle);
    }

    // Method to handle updates from PuzzlePlate objects
    private void UpdatePuzzle()
    {
        // Implement puzzle update logic here
        Debug.Log("Puzzle plate activated!");
        puzzleUpdate++;
        if (puzzleUpdate == 4)
        {
            animator.SetBool("isComplete", true);

            // Load tilemap with scene transition here
            ActivateTilemap();
        }
    }

    public void ActivateTilemap()
    {
        transition.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
