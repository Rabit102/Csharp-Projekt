using System.Collections;
using UnityEngine;

public class DisappearObject : MonoBehaviour
{
    public float hiddentime = 10f;
    public float showtime = 5f;

    private new Renderer renderer;
    private new BoxCollider2D collider;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<BoxCollider2D>();

        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        while (true)
        {
            renderer.enabled = false;
            if(collider) collider.enabled = false;
            yield return new WaitForSeconds(hiddentime);

            renderer.enabled = true;
            if (collider) collider.enabled = true;
            yield return new WaitForSeconds(showtime);
        }
    }
}
