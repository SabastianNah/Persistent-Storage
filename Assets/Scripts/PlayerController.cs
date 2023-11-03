using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;

    public int score;
    public int goal;
    // public GameObject door;
    public GameObject pickUp;

    public LayerMask groundLayer;
    public Rigidbody rb;
    public SpriteRenderer rbSprite;
    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;
    // Start is called before the first frame update
    void Start()
    {
        rb.gameObject.GetComponent<Rigidbody>();
        score = 0;
        goal = 1;
        setScoreText();
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 castPosition = transform.position;
        if (Physics.Raycast(castPosition, -transform.up, out hit, Mathf.Infinity, groundLayer))
        {
            // hit ground, set player above point hit
            if (hit.collider != null)
            {
                Vector3 movementPosition = transform.position;
                movementPosition.y = hit.point.y + groundDist;
                transform.position = movementPosition;
            }
        }
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 MovementDirection = new Vector3(x, 0, y);
        rb.velocity = MovementDirection * speed;

        // flip depending on direction of momement
        if (x != 0 && x < 0)
        {
            rbSprite.flipX = true;
        }
        else if (x != 0 && x > 0)
        {
            rbSprite.flipX = false;
        }
    }
    void setScoreText()
    {
        scoreText.text = "Apples: " + score.ToString();

        if (score >= goal)
        {
            Time.timeScale = 0f;
            winTextObject.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            pickUp = other.gameObject;
            pickUp.gameObject.SetActive(false);
            score++;
            setScoreText();
        }
        if (other.gameObject.CompareTag("Save"))
        {
            savePlayer();
        }
    }
    public void savePlayer()
    {
        SaveLoadResetController.savePlayerData(this);
    }
    public void loadPlayer()
    {
        GameDataController data = SaveLoadResetController.loadPlayerData();
        // reset world
        winTextObject.SetActive(false);
        Time.timeScale = 1f;
        pickUp.gameObject.SetActive(true);
        score = data.playerScore;
        setScoreText();

        Vector3 playerPosition;
        playerPosition.x = data.playerPosition[0];
        playerPosition.y = data.playerPosition[1];
        playerPosition.z = data.playerPosition[2];
        transform.position = playerPosition;

    }
    public void resetPlayer()
    {
        SaveLoadResetController.resetPlayerData();
        // reset world
        winTextObject.SetActive(false);
        Time.timeScale = 1f;
        pickUp.gameObject.SetActive(true);
        score = 0;
        setScoreText();
        Vector3 playerPosition;
        playerPosition.x = (float)-5.34;
        playerPosition.y = 0;
        playerPosition.z = (float)-0.1;
        transform.position = playerPosition;

    }

    public void ExitGame()
    {
        Debug.Log("Quiting...");
        Application.Quit();

    }
}
    
