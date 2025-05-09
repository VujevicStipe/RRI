using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI stateText;
    public CharacterController player;

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = player.transform.position;
    }

    void Update()
    {
        float speed = Vector3.Distance(player.transform.position, lastPosition) / Time.deltaTime;
        lastPosition = player.transform.position;

        speedText.text = "Brzina: " + speed.ToString("F1") + " m/s";

        PlayerController playerController = player.GetComponent<PlayerController>();

        if (playerController.isJumping)
        {
            stateText.text = "Stanje: Skace";
        }
        else if (playerController.isCrouching)
        {
            stateText.text = "Stanje: Crouch";
        }
        else if (speed > 9f)
        {
            stateText.text = "Stanje: Trci";
        }
        else if (speed > 0.1f)
        {
            stateText.text = "Stanje: Hoda";
        }
        else
        {
            stateText.text = "Stanje: Stoji";
        }
    }
}
