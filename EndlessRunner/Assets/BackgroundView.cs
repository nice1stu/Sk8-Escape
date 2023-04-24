using UnityEngine;
using UnityEngine.UI;
public class BackgroundView : MonoBehaviour 
{
    public Sprite[] backgrounds;
    public float speed = 1f;

    private RectTransform canvasRectTransform;
    private RectTransform backgroundRectTransform;
    private float backgroundWidth;
    private int currentIndex = 0;

    void Start()
    {
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        backgroundRectTransform = GetComponent<RectTransform>();
        backgroundWidth = backgrounds[0].texture.width * backgroundRectTransform.localScale.x;

        // Set the first background image
        backgroundRectTransform.GetComponent<Image>().sprite = backgrounds[0];
    }
    void Update()
    {
        float movement = speed * Time.deltaTime;
        backgroundRectTransform.anchoredPosition -= new Vector2(movement, 0f);

        if (backgroundRectTransform.anchoredPosition.x < -backgroundWidth)
        {
            // Get the position of the old background relative to the canvas
            Vector3 oldPos = canvasRectTransform.InverseTransformPoint(backgroundRectTransform.position);

            // Teleport the old background to the end of the new background
            backgroundRectTransform.anchoredPosition += new Vector2(backgroundWidth, 0f);

            // Get the position of the new background relative to the canvas
            Vector3 newPos = canvasRectTransform.TransformPoint(oldPos + new Vector3(backgroundWidth, 0f, 0f));

            // Set the position of the new background
            backgroundRectTransform.position = newPos;

            // Set the new background image
            currentIndex = (currentIndex + 1) % backgrounds.Length;
            backgroundRectTransform.GetComponent<Image>().sprite = backgrounds[currentIndex];
        }
    }
}