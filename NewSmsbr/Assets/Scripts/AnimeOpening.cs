using UnityEngine;

public class AnimeOpening : MonoBehaviour
{
    public float doorOpenAngle = 90.0f; //Set either positive or negative number to open the door inwards or outwards
    public float openSpeed = 2.0f; //Increasing this value will make the door open faster
    public AudioSource[] sounds;
    bool open = false;
    bool enter = false;

    float defaultRotationAngle;
    float currentRotationAngle;
    float openTime = 0;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;
    }

    void Update()
    {
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeed;
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? doorOpenAngle : 0), openTime), transform.localEulerAngles.z);

        if (Input.GetKeyDown(KeyCode.F) && enter)
        {
            sounds[0].Play();
            sounds[1].Play();
            open = !open;
            currentRotationAngle = transform.localEulerAngles.y;
            openTime = 0;
        }
    }

    void OnGUI()
    {
        if (enter)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 220, 30), "Press 'F' to contact with the door");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = false;
        }
    }
}