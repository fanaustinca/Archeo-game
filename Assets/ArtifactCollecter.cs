using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ArtifactCollector : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI factText; 

    [Header("Archaeology Facts")]
    // 1. Here is the trick! We store the prefix once in its own string.
    public string artifactPrefix = "You found an ancient Egyptian statue! ";

    // 2. The array and its TextArea attribute are now properly inside the class.
    [TextArea(3, 10)]
    public string[] archaeologyFacts = {
        "Archaeologists use LiDAR lasers to find hidden ruins beneath thick jungle canopies.",
        "Found in 1799, the Rosetta Stone finally allowed us to decode Egyptian hieroglyphs.",
        "Archaeology is a giant puzzle where most of the pieces have been destroyed by time.",
        "'Stratigraphy' is the method of dating objects based on how deep they are buried in the earth.",
        "Pottery 'sherds' are the most common artifact because fired clay survives for millennia.",
        "For every hour digging in the field, archaeologists spend up to four hours researching in the lab.",
        "The golden rule of digging: the deeper a soil layer is, the older the artifacts usually are.",
        "Most 'lost cities' weren't destroyed by disasters; they were gradually abandoned due to climate or trade shifts.",
        "Wood usually rots, but extreme cold, dry deserts, or oxygen-free bogs can preserve it perfectly.",
        "Copper is naturally antimicrobial, which often preserves any organic materials touching the metal.",
        "Ancient trash heaps, called 'middens,' are incredibly valuable because they reveal how everyday people lived.",
        "The 'Pompeii Effect' happens when a sudden disaster freezes an archaeological site perfectly in time.",
        "The Great Pyramid of Giza was once covered in polished white limestone, reflecting the sun like a giant mirror.",
        "Ancient Roman concrete actually grows stronger over time when exposed to seawater.",
        "The Rosetta Stone acted as a decoder key because it featured the exact same text written in three different scripts.",
        "Cuneiform is one of the oldest forms of writing, created by pressing reeds into wet clay."
    };

    void Start()
    {
        if (factText == null)
        {
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
                if (hit.transform == transform)
                {
                    CollectArtifact();
                }
            }
        }
    }

    void CollectArtifact()
    {
        if (factText != null && archaeologyFacts.Length > 0)
        {
            int index = Random.Range(0, archaeologyFacts.Length);
            
            // 3. We stitch the prefix and the randomly chosen fact together here!
            string fullFactText = artifactPrefix + archaeologyFacts[index];
            
            factText.text = fullFactText;
            Debug.Log("Success! Fact Displayed: " + fullFactText);
        }

        if (TryGetComponent<MeshRenderer>(out MeshRenderer renderer)) renderer.enabled = false;
        if (TryGetComponent<Collider>(out Collider col)) col.enabled = false;

        Transform mapIcon = transform.Find("ArtifactIcon");
        if (mapIcon != null) mapIcon.gameObject.SetActive(false);

        Destroy(gameObject, 0.2f);
    }
}