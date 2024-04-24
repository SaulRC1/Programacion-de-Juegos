using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {         
            SceneManager.LoadScene("Level 2");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("Puntuation screen");
        }
    }
}
