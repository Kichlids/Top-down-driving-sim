using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerController pc;
    public Text currentSpeed;
    public Text maxSpeed;

    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        currentSpeed.text = Mathf.Round(pc.GetCurrentVelocity()) + " mph";
        maxSpeed.text = "Max speed: " + pc.GetMaxVelocity();
    }
}
