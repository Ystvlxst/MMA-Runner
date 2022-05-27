using RunnerMovementSystem;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    private void Update()
    {
        TryStartGame();
    }

    private void TryStartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }
}
