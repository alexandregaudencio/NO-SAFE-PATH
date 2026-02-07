using UnityEngine;

namespace Game.CharacterSystem
{
    
    public class AnimationController
    {
        
        private readonly Animator animator;

        public AnimationController(Animator animator)
        {
            this.animator = animator;
        }

        public void Play(int animationHash, int layer = 0, float normalizedTime = 0)
        {
            animator.Play(animationHash,0,normalizedTime);
        }
    }
}