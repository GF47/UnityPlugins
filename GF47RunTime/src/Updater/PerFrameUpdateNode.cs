namespace GF47RunTime.Updater
{
    using System;

    public class PerFrameUpdateNode : NormalUpdateNode
    {
        public PerFrameUpdateNode(Action<float> callback) : base(callback) { } 

        public override void Start()
        {
            MonoUpdater.Instance.Target.Add(this, Updater.UpdateStyle.PerFrame);
            isUpdating = true;
        }

        public override void Stop()
        {
            MonoUpdater.Instance.Target.RemoveFrom(this, Updater.UpdateStyle.PerFrame);
            isUpdating = false;
        }
    }
}