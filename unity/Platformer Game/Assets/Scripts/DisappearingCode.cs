using UnityEngine;

public class Disar : MonoBehaviour
{
    [SerializeField] private bool visible = true;
    [SerializeField] private float time;

    private GameObject objects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        objects.SetActive(visible);

        if (time == 10.0f)
        {
            visible = false;
        }
    }
}
