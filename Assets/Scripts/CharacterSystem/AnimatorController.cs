using UnityEngine;

namespace Game.CharacterSystem
{
    public class AnimatorController
    {
        
        private readonly Animator animator;

        public AnimatorController(Animator animator)
        {
            this.animator = animator;
        }

        public void Play(string animationName, int layer = 0, float normalizedTime = 0)
        {
            animator.Play(animationName,0,normalizedTime);
        }
    }
}