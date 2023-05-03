using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        // Something to display the size change when doing coffin. (Before we have actual animations)
        public void DummyCoffinScaler(Vector2 scale, Vector2 offset)
        {
            transform.localScale = scale;
            transform.localPosition = new Vector3(transform.localPosition.x, offset.y);
        }
    }
}
