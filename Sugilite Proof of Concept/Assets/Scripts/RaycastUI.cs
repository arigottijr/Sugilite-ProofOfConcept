using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;
using UnityEngine.EventSystems;

public class RaycastUI : MonoBehaviour
{
    public Camera cam;

    [SerializeField]
    private float distance = 3f;

    public TextMeshProUGUI promptText;
    public Image mouseSpriteHolder;
    
    public string addSoundPrompt;
    public string removeSoundPrompt;
    public string editSoundPrompt;
    public Sprite leftMouseSprite;
    public Sprite rightMouseSprite;

    private Transform highlight;
    private Transform selection;
    void Update()
    {
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        UpdateText(string.Empty,null, Color.clear);

        //create a ray at center of camera, shooting forward at specificied distance
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            if (hitInfo.collider.gameObject.tag == "Sound Check" && hitInfo.collider.gameObject.GetComponent<AudioSource>().clip == null)
            {
                Debug.Log("Hit");
                UpdateText(addSoundPrompt, leftMouseSprite, Color.white);
            } 
            else if (hitInfo.collider.gameObject.tag == "Sound Check" && hitInfo.collider.gameObject.GetComponent<AudioSource>().clip != null)
            {
                UpdateText(removeSoundPrompt, rightMouseSprite, Color.white);
            }
            
            if(hitInfo.collider.gameObject.tag == "SoundEditor")
            {
                UpdateText(editSoundPrompt, leftMouseSprite, Color.white);
            }

        }
        
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hitInfo, 8)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            highlight = hitInfo.transform;
            if (highlight.CompareTag("Object") && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.white;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 20f;
                }
            }
            else
            {
                highlight = null;
            }
        }

      
    }

    public void UpdateText(string promptMessage, Sprite mouseSprite, Color imageTransparency)
    {
        promptText.text = promptMessage;
        mouseSpriteHolder.sprite = mouseSprite;
        mouseSpriteHolder.color = imageTransparency;
        
    }
}
