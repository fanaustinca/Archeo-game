using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ArtifactCollector : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI factText; 

    [Header("Archaeology Facts")]
    [TextArea(3, 10)]
    public string[] archaeologyFacts = {
        "Archaeologists use LiDAR (lasers) to see through jungle canopies to find hidden ruins!",
        "The Rosetta Stone, found in 1799, allowed us to finally read Egyptian hieroglyphs.",
        "Archaeology is like a puzzle where most of the pieces have been destroyed by time.",
        "Stratigraphy allows archaeologists to date objects by how deep they are buried in soil layers.",
        "Pottery 'sherds' are the most common find because fired clay lasts for thousands of years.",
        "Archaeologists spend about 3 to 4 hours in the lab for every 1 hour spent digging in the field.",
        "Stratigraphy is the source code of archaeology; the deeper a layer is, the older the artifacts usually are",
        "Most 'lost cities' weren't lost because of a single disaster; they were often gradually abandoned due to climate change or shifts in trade routes.",
        "Organic materials like wood or fabric usually rot, but they can be perfectly preserved for thousands of years in very dry deserts, frozen ice, or anaerobic (oxygen-free) bogs.",
        "Copper artifacts are naturally antimicrobial, which sometimes helps preserve organic material that is touching the metal.",
        "Middens (ancient trash heaps) are some of the most valuable sites for archaeologists because they reveal exactly what people ate, wore, and threw away daily.",
        "The 'Pompeii Effect' refers to sites that were suddenly frozen in time by a disaster, though most archaeology involves piecing together fragments from thousands of years of gradual change.",
        "The Great Pyramid of Giza was originally covered in highly polished white limestone that would have reflected the sun like a giant mirror.",
        "Ancient Roman concrete actually grows stronger over time when exposed to seawater, a chemical secret that modern engineers are still studying.",
        "The Rosetta Stone was the 'decoder key' that allowed us to understand Egyptian hieroglyphs by providing the same text in three different scripts.",
        "Cuneiform, developed by the Sumerians, is one of the oldest forms of writing and was made by pressing reed styluses into wet clay tablets."
    };

    void Start()
    {
        // AUTOMATIC LINKING: 
        // If the slot is empty, this finds your UI text by its name.
        if (factText == null)
        {
            // Make sure your UI Text object is named EXACTLY "Fact_Display_Text" in Unity
            GameObject go = GameObject.Find("Fact_Display_Text");
            if (go != null)
            {
                factText = go.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogWarning("Artifact couldn't find 'Fact_Display_Text'! Check your naming.");
            }
        }
    }

    void Update()
    {
        if (Pointer.current == null) return;

        if (Pointer.current.press.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Safety: Use 'hit.transform' to check against this specific object
                if (hit.transform == transform)
                {
                    CollectArtifact();
                }
            }
        }
    }

    void CollectArtifact()
    {
        // 1. Update text first
        if (factText != null && archaeologyFacts.Length > 0)
        {
            int index = Random.Range(0, archaeologyFacts.Length);
            factText.text = archaeologyFacts[index];
            Debug.Log("Success! Fact Displayed: " + archaeologyFacts[index]);
        }

        // 2. Hide visuals and map icon (if exists)
        if (TryGetComponent<MeshRenderer>(out MeshRenderer renderer)) renderer.enabled = false;
        if (TryGetComponent<Collider>(out Collider col)) col.enabled = false;

        Transform mapIcon = transform.Find("ArtifactIcon");
        if (mapIcon != null) mapIcon.gameObject.SetActive(false);

        // 3. Destroy with a longer delay to ensure the UI has processed everything
        Destroy(gameObject, 0.2f);
    }
}