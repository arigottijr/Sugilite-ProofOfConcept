using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastUI : MonoBehaviour
{
    public Camera cam;

    [SerializeField]
    private float distance = 3f;

    public TextMeshProUGUI promptText;

    public string objectcollectPrompt;
    public string leftMousePrompt;
    public string rightMousePrompt;

    [SerializeField]
    private LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText(string.Empty);

        //create a ray at center of camera, shooting forward at specificied distance
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.gameObject.tag == "Object")
            {
                UpdateText(objectcollectPrompt);
            }

            if (hitInfo.collider.gameObject.tag == "Sound Check" && hitInfo.collider.gameObject.GetComponent<AudioSource>().clip == null)
            {
                UpdateText(leftMousePrompt);
            }

            if (hitInfo.collider.gameObject.tag == "Sound Check" && hitInfo.collider.gameObject.GetComponent<AudioSource>().clip != null)
            {
                UpdateText(rightMousePrompt);
            }

        }

      
    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}
