using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverCamvas;
    [SerializeField] Text winText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            winText.color = Color.green;
            winText.text = "You WIN!";
            gameOverCamvas.SetActive(true);
        }
    }
}
