using UnityEngine;

public static class HashAnimationNames
{
    public static class PlayerAnimation
    {
        public static int SpeedHash = Animator.StringToHash("Speed");
        public static int IsGroundedHash = Animator.StringToHash("IsGrounded");
        public static int AttackHash = Animator.StringToHash("Attack");
        public static int SnakeAttackHash = Animator.StringToHash("SnakeAttack");
    }
}