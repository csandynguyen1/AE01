using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EarthTotem : MonoBehaviour
{
    public Animator animator;

    public UnityEvent OnTotemActivated = new UnityEvent();
    public EarthPlate TargetPlate;




    // Start is called before the first frame update
    private void Start()
    {
        
        // Register each PuzzlePlate object to listen for updates
        // Register event listeners for each puzzle plate
        TargetPlate.OnPlateActivated.AddListener(UpdatePuzzle);
        
        
    }

    // Method to handle updates from PuzzlePlate objects
    private void UpdatePuzzle()
    {
         Debug.Log("Puzzle totem activated!");
         animator.SetBool("pressed", true);
         OnTotemActivated.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

