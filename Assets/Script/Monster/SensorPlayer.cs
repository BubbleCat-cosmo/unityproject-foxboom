using UnityEngine;

public class SensorPlayer : MonoBehaviour
{
    private bool isTherePlayer;

    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        isTherePlayer = false;
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log("SensorCollider:" + isTherePlayer);
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isTherePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isTherePlayer = false;
        }
    }

    public bool GetIsTherePlayer()
    {
        Debug.Log("SensorCollider:" + isTherePlayer);
        return isTherePlayer;
    }
}
