using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingleGame
{
    public class AnimatorController : MonoBehaviour
    {
        private PlayerControllerSingleGame playerController;
        private Animator animator;
        public Animator Anim => animator;
        readonly int hashIdle = Animator.StringToHash("Idle");
        readonly int hashRun = Animator.StringToHash("Run");

        readonly int hashAttack_01 = Animator.StringToHash("Attack_01");
        readonly int hashAttack_02 = Animator.StringToHash("Attack_02");
        readonly int hashAttack_03 = Animator.StringToHash("Attack_03");
        readonly int hashAttack_04 = Animator.StringToHash("Attack_04");

        readonly int hashSlope = Animator.StringToHash("Slope");
        private int rnd;
        void Start()
        {
            playerController = GetComponent<PlayerControllerSingleGame>();
            animator = GetComponentInChildren<Animator>();
            //if(player.ViewPlayer.IsMine)
            //{

            //}
            playerController.OnChangeState += PlayAnimation;
        }



        private void PlayAnimation()
        {
            switch (playerController.CurrentState)
            {
                case (PlayerControllerSingleGame.State )0:
                    animator.SetBool(hashIdle, true);
                    animator.SetBool(hashRun, false);
                    animator.SetBool(hashAttack_01, false);
                    animator.SetBool(hashAttack_02, false);
                    animator.SetBool(hashAttack_03, false);
                    animator.SetBool(hashAttack_04, false);
                    animator.SetBool(hashSlope, false);
                    break;
                case (PlayerControllerSingleGame.State)1:
                    animator.SetBool(hashRun, true);
                    animator.SetBool(hashIdle, false);
                    animator.SetBool(hashAttack_01, false);
                    animator.SetBool(hashAttack_02, false);
                    animator.SetBool(hashAttack_03, false);
                    animator.SetBool(hashAttack_04, false);
                    animator.SetBool(hashSlope, false);
                    break;
                case (PlayerControllerSingleGame.State)2:
                    rnd = Random.Range(0, 4);
                    animator.SetBool(hashAttack_01, false);
                    animator.SetBool(hashAttack_02, false);
                    animator.SetBool(hashAttack_03, false);
                    animator.SetBool(hashAttack_04, false);

                    if (rnd == 0)
                    {
                        animator.SetBool(hashAttack_01, true);
                    }
                    if (rnd == 1)
                    {
                        animator.SetBool(hashAttack_02, true);
                    }
                    if (rnd == 2)
                    {
                        animator.SetBool(hashAttack_03, true);
                    }
                    if (rnd == 3)
                    {
                        animator.SetBool(hashAttack_04, true);
                    }

                    animator.SetBool(hashRun, false);
                    animator.SetBool(hashIdle, false);
                    animator.SetBool(hashSlope, false);
                    break;
                case (PlayerControllerSingleGame.State)3:
                    animator.SetBool(hashSlope, true);
                    animator.SetBool(hashRun, false);
                    animator.SetBool(hashAttack_01, false);
                    animator.SetBool(hashAttack_02, false);
                    animator.SetBool(hashAttack_03, false);
                    animator.SetBool(hashAttack_04, false);
                    animator.SetBool(hashIdle, false);
                    break;
                default:
                    break;
            }
        }
        public void Damage()
        {
            animator.SetBool("Damage", true);
            StartCoroutine(CorReturnIdle());
            IEnumerator CorReturnIdle()
            {
                yield return new WaitForSeconds(0.3f);
                animator.SetBool("Damage", false);
            }
        }
    }
}
