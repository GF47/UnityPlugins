/* ****************************************************************
 * @File Name   :   IUserInterfaceView
 * @Author      :   GF47
 * @Date        :   2015/5/19 15:16:35
 * @Description :   用户视图接口
 * @Edit        :   2015/5/19 15:16:35
 * ***************************************************************/
namespace GF47RunTime.UI
{
    using System;

    /// <summary>
    /// 用户视图接口
    /// </summary>
    public interface IUserInterfaceView : IDisposable
    {
        event Action<IUserInterfaceView, EventArgs> OnInitializeHandler;
        event Action<IUserInterfaceView, EventArgs> OnDisposeHandler;
        event Action<IUserInterfaceView, EventArgs> OnViewShowHandler;
        event Action<IUserInterfaceView, EventArgs> OnViewHideHandler;
        bool State { get; }
        void Initialize();
        void ViewShow();
        void ViewHide();
    }
}
