using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class WallTrigger : ActionTrigger
    {
        private float height = 1;
        public override void TriggerEnter(ThirdPersonCharacter thirdPersonCharacter)
        {
            base.TriggerEnter(thirdPersonCharacter);
            thirdPersonCharacter.m_WallrunPressed = true;
            var m_Rigidbody = thirdPersonCharacter.m_Rigidbody;
            m_Rigidbody.useGravity = false;
            thirdPersonCharacter.m_Animator.SetBool("WallrunPressed", thirdPersonCharacter.m_Vault);
        }

        public override void TriggerStay(ThirdPersonCharacter thirdPersonCharacter)
        {
            base.TriggerStay(thirdPersonCharacter);
            var m_Rigidbody = thirdPersonCharacter.m_Rigidbody;
            m_Rigidbody.position = new Vector3(m_Rigidbody.position.x, height, m_Rigidbody.position.z);
        }

        public override void TriggerExit(ThirdPersonCharacter thirdPersonCharacter)
        {
            base.TriggerExit(thirdPersonCharacter);
            thirdPersonCharacter.m_WallrunPressed = false;
            thirdPersonCharacter.m_Rigidbody.useGravity = true;
        }
    }
    public class VaultTrigger:ActionTrigger
    {
        public override void TriggerEnter(ThirdPersonCharacter thirdPersonCharacter)
        {
            base.TriggerEnter(thirdPersonCharacter);
            thirdPersonCharacter.m_Vault = true;
            var m_Rigidbody = thirdPersonCharacter.m_Rigidbody;
            thirdPersonCharacter.m_Animator.SetBool("Vault", thirdPersonCharacter.m_Vault);
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, thirdPersonCharacter.m_JumpPower, m_Rigidbody.velocity.z);
            
            //thirdPersonCharacter.m_IsGrounded = false;
            //thirdPersonCharacter.m_Animator.applyRootMotion = false;
            //thirdPersonCharacter.m_GroundCheckDistance = 0.1f;
        }

        public override void TriggerExit(ThirdPersonCharacter thirdPersonCharacter)
        {
            base.TriggerExit(thirdPersonCharacter);
            thirdPersonCharacter.m_Vault = false;
        }
    }
}