using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clue : MonoBehaviour, IInteractable
{
    [Header("Clue Content")]
    [TextArea(3, 10)]
    [SerializeField] private string clueText = "A mysterious note...";
    [SerializeField] private Sprite clueImage;


    private static GameObject sharedCanvas;
    private static RectTransform paperBackground;
    private static TextMeshProUGUI uiText;
    private static Image uiImage;

    private void Start()
    {
        if (sharedCanvas == null)
        {
            sharedCanvas = GameObject.Find("ClueCanvas");
            
            if (sharedCanvas != null)
            {
                paperBackground = sharedCanvas.transform.Find("ClueBackground").GetComponent<RectTransform>();
                uiText = paperBackground.Find("ClueText").GetComponent<TextMeshProUGUI>();
                uiImage = paperBackground.Find("ClueImage").GetComponent<Image>();
                
                sharedCanvas.SetActive(false); 
            }
            else
            {
                Debug.LogError("Could not find 'ClueCanvas' in the scene! Check the spelling.");
            }
        }
    }

    public bool Interact()
    {
        if (sharedCanvas == null) return false;

        uiText.text = clueText;

        if (clueImage != null)
        {
            uiImage.sprite = clueImage;
            uiImage.gameObject.SetActive(true);

            AspectRatioFitter fitter = uiImage.GetComponent<AspectRatioFitter>();
            if (fitter != null)
            {
                fitter.aspectRatio = clueImage.rect.width / clueImage.rect.height;
            }
        }
        else
        {
            uiImage.gameObject.SetActive(false);
        }

        sharedCanvas.SetActive(true);

        LayoutRebuilder.ForceRebuildLayoutImmediate(paperBackground);

        return true; 
    }

    public void Dismiss()
    {
        if (sharedCanvas != null)
        {
            sharedCanvas.SetActive(false);
        }
    }
}