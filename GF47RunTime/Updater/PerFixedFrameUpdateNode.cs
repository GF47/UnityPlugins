namespace GF47RunTime.Updater
{
    using System;

    public class PerFixedFrameUpdateNode : NormalUpdateNode
    {
        public PerFixedFrameUpdateNode(Action<float> callback) : base(callback) { }

        public override void Start()
        {
            MonoUpdater.Instance.Target.Add(this, Updater.UpdateStyle.PerFixedFrame);
            isUpdating = true;
        }

        public override void Stop()
        {
            MonoUpdater.Instance.Target.RemoveFrom(this, Updater.UpdateStyle.PerFixedFrame);
            isUpdating = false;
        }
    }
}
