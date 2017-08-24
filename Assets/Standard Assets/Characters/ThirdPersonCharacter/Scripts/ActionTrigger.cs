using System;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public abstract class ActionTrigger
    {
        public virtual void TriggerEnter(ThirdPersonCharacter thirdPersonCharacter)
        {
            Console.WriteLine("entered");
        }

        public virtual void TriggerStay(ThirdPersonCharacter thirdPersonCharacter)
        {
            Console.WriteLine("staying");
        }

        public virtual void TriggerExit(ThirdPersonCharacter thirdPersonCharacter)
        {
            Console.WriteLine("exited");
        }
    }
}