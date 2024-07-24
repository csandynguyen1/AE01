using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzlePlate : MonoBehaviour
{

    public Animator animator;
    public UnityEvent OnPlateActivated = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PuzzlePiece"))
        {
            // Activate the animation when a Collider2D with tag "PuzzlePiece" enters
            animator.SetBool("placed", true);
            OnPlateActivated.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PuzzlePiece"))
        {
            // Deactivate the animation when a Collider2D with tag "PuzzlePiece" exits
            animator.SetBool("placed", false);
        }
    }
}
