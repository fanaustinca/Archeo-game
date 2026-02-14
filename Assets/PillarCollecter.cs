using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PillarCollector : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI factText; 

    [Header("Pillar Facts")]
    [TextArea(3, 10)]
    public string[] archaeologyFacts = {
        "You found a pillar! Göbekli Tepe in Turkey, constructed around 9000 BC (11,000 years ago), is considered the world's oldest temple, featuring T-shaped pillars weighing up to 20 tons.",
        "You found a pillar! The Great Pyramid of Giza (c. 2580–2510 BCE) remained the world's tallest man-made structure for over 3,800 years. It is aligned to cardinal points with a margin of error of only a fraction of a degree.",
        "You found a pillar! Ancient builders, such as the Egyptians, used limestone and granite, transporting massive stones for miles, despite lacking modern machinery. Stonehenge (c. 2500 BC) used levers, sledges, and ropes to arrange its massive stone circles.",
        "You found a pillar! Many structures were built to last forever as tombs or religious centers. The pyramids of Giza were built for pharaohs like Khufu to ensure their safe journey to the afterlife.",
        "You found a pillar! Structures built with stone or fired brick, like the Temple of Hephaestus, have survived for thousands of years, whereas wooden or mud-brick structures usually decayed.",
        "You found a pillar! Chichen Itza in Mexico is a famous Mayan site known for the Temple of Kukulcan (El Castillo), which displays advanced architectural, acoustic, and astronomical knowledge, aligning perfectly with the equinoxes.",
        "You found a pillar! Mohenjo-daro in Pakistan was one of the earliest major cities (c. 2500 BC) with a population of about 40,000, demonstrating early urban planning.",
        "You found a pillar! Dolmens, which are ancient, single-chamber tombs, are found throughout Europe and Asia, featuring massive horizontal capstones supported by vertical stones."
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
                Debug.LogWarning("Pillar couldn't find 'Fact_Display_Text'! Check your naming.");
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
            factText.text = archaeologyFacts[index];
            Debug.Log("Success! Fact Displayed: " + archaeologyFacts[index]);
        }

        if (TryGetComponent<MeshRenderer>(out MeshRenderer renderer)) renderer.enabled = false;
        if (TryGetComponent<Collider>(out Collider col)) col.enabled = false;

        Transform mapIcon = transform.Find("ArtifactIcon");
        if (mapIcon != null) mapIcon.gameObject.SetActive(false);

        Destroy(gameObject, 0.2f);
    }
}