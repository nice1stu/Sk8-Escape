using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public Animation characterAnimation;
        public Animation skateboardAnimation;

        public AnimationClip[] characterAnimations;
        public AnimationClip[] skateboardAnimations;
        
        
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

        public void PlayCoastAnim()
        {
            characterAnimation.clip = characterAnimations[0];
            characterAnimation.Play();
            skateboardAnimation.clip = skateboardAnimations[0];
            skateboardAnimation.Play();
        }
        
        public void PlayOllieAnim() 
        {
            characterAnimation.clip = characterAnimations[1];
            characterAnimation.Play();
            skateboardAnimation.clip = skateboardAnimations[1];
            skateboardAnimation.Play();
        }
    }
}
