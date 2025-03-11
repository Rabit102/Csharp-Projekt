using TMPro;
using UnityEngine;

public class CounterCode : MonoBehaviour
{
    public float counter;
    public TextMeshProUGUI counterText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        counterText.text = counter.ToString("0.00");
    }
}
