namespace GF47RunTime.UI
{
    using System;

    /// <summary>
    /// 按钮接口
    /// </summary>
    public interface IButtonItem
    {
        event Action<IButtonItem, EventArgs> OnClickButtonHandler;

        int ID { get; set; }
        string Text { get; set; }
        int State { get; }

        void Initialize(int id, string text);
    }
}
