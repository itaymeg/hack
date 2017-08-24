using System;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class TriggerFactory
    {
        public static ActionTrigger GeTrigger(string trigger)
        {
            var type = Type.GetType(string.Format("UnityStandardAssets.Characters.ThirdPerson.{0}", trigger));
            return (ActionTrigger)Activator.CreateInstance(type);
        }
    }
}