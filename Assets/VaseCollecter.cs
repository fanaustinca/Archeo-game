using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class VaseCollector : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI factText; 

    [Header("Vase Facts")]
    // The prefix specific to this object type
    public string artifactPrefix = "You found an ancient vase! ";

    [TextArea(3, 10)]
    public string[] vaseFacts = {
        "Vases, or 'amphorae', were the shipping boxes of the ancient world, used to transport wine, olive oil, and grain across the sea.",
        "Greek pottery often featured two main styles: Black-figure, painted as silhouettes, and Red-figure, which reversed the colors.",
        "Broken vase pieces were actually used as scrap paper; ancient Athenians even used them as voting ballots to banish unpopular politicians.",
        "The world's oldest known pottery vessels were found in a cave in China, dating back over 20,000 years.",
        "The pointed bases of many ancient amphorae allowed them to be tightly packed into the hulls of trading ships without tipping over.",
        "By analyzing the chemical residue left inside unwashed ancient vases, archaeologists can figure out exactly what people were eating and drinking.",
        "The Portland Vase is a famous Roman glass vessel so perfectly crafted that it took modern glassmakers hundreds of years to figure out how to copy it.",
        "Kintsugi is the Japanese art of repairing broken pottery with gold, treating the breakage as a beautiful part of the object's history."
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
                Debug.LogWarning("Vase couldn't find 'Fact_Display_Text'! Check your naming.");
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
                    CollectVase();
                }
            }
        }
    }

    void CollectVase()
    {
        if (factText != null && vaseFacts.Length > 0)
        {
            int index = Random.Range(0, vaseFacts.Length);
            
            // Combine the prefix and the random vase fact
            string fullFactText = artifactPrefix + vaseFacts[index];
            
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