using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = PlayerStats.Lives + " LIVES";
    }
}
