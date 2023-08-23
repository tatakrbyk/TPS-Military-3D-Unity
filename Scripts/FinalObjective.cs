using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalObjective : MonoBehaviour
{
    [Header("Vehicle button")]

    [SerializeField] private KeyCode vehicleButton = KeyCode.F;

    

    [Header("Generator Sound Effects and Animator")]
  
    [SerializeField] Player player;

    private float radius = 3f;


  

    private void Update()
    {
        if (Input.GetKeyDown(vehicleButton) && Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            ObjectivesComplete.Instance.GetObjectivesDone(true, true, true, true);

            Time.timeScale = 1.0f;
            SceneManager.LoadScene("EndGameMenuScenes");
        }

    }
}
