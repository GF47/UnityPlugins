namespace GF47RunTime.Updater
{
    using System;

    public class PerAfterFrameUpdateNode : NormalUpdateNode
    {
        public PerAfterFrameUpdateNode(Action<float> callback) : base(callback) { }

        public override void Start()
        {
            MonoUpdater.Instance.Target.Add(this, Updater.UpdateStyle.PerAfterFrame);
            isUpdating = true;
        }

        public override void Stop()
        {
            MonoUpdater.Instance.Target.RemoveFrom(this, Updater.UpdateStyle.PerAfterFrame);
            isUpdating = false;
        }
    }
}
