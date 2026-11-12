using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField] private AudioSource pickupAudioSource;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private TextMeshProUGUI countText;

    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private NavMeshAgent enemyAgent;
    [SerializeField] private float fallHeight = -3f;

    [SerializeField] private GameMenu gameMenu;

    private Rigidbody body;
    private Vector2 moveInput;
    private int count;
    private int totalPickups;
    private bool gameEnded;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        totalPickups = GameObject.FindGameObjectsWithTag("PickUp").Length;
        UpdateCountText();
        resultText.gameObject.SetActive(false);
    }

    private void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (gameEnded)
        {
            return;
        }

        if (body.position.y < fallHeight)
        {
            EndGame("Проигрыш!", loseSound);
            return;
        }

        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
        body.AddForce(direction * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameEnded)
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            EndGame("Проигрыш!", loseSound);
            return;
        }

        if (!other.CompareTag("PickUp"))
        {
            return;
        }

        other.gameObject.SetActive(false);
        count++;
        UpdateCountText();

        if (count == totalPickups)
        {
            EndGame("Победа!", winSound);
        }
        else
        {
            PlaySound(pickupSound);
        }
    }

    private void UpdateCountText()
    {
        countText.text = $"Собрано: {count}";
    }

    private void EndGame(string message, AudioClip sound)
    {
        gameEnded = true;
        if (enemyAgent != null && enemyAgent.isOnNavMesh)
        {
            enemyAgent.isStopped = true;
        }
        body.isKinematic = true;

        resultText.text = message;
        resultText.gameObject.SetActive(true);
        PlaySound(sound);
        gameMenu.ShowEndMenu();
    }

    private void PlaySound(AudioClip clip)
    {
        if (pickupAudioSource != null && clip != null)
        {
            pickupAudioSource.PlayOneShot(clip);
        }
    }

   
}