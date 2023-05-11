using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace UI.Scripts.PauseMenu
{
    [RequireComponent(typeof(Image))]
    public class SpriteToggler : MonoBehaviour
    {
        private Sprite _prevSprite;
        [SerializeField] public Sprite alternateSprite;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void Start()
        {
            _prevSprite = image.sprite;
            Assert.IsNotNull(image.sprite, "No initial sprite found");
        }

        public void ToggleIcon()
        {
            bool isAlternateShowing = image.sprite == alternateSprite;

            image.sprite = isAlternateShowing ? _prevSprite : alternateSprite;
        }
    }
}
