namespace GF47RunTime.ActionSystem
{
    using System;

    [Flags]
    public enum TriggerMethod
    {
        #region 原生事件
        None                        = 0x0,

        OnMouseDown 				= 1<<0,
        OnMouseUp                   = 1<<1,
        OnMouseUpAsButton 		    = 1<<2,
        OnMouseDrag 				= 1<<3,

        OnMouseRightUp 			    = 1<<4,
        OnMouseRightDown 			= 1<<5,
        OnMouseRightUpAsButton 	    = 1<<6,
        OnMouseRightDrag 			= 1<<7,

        OnMouseEnter 				= 1<<8,
        OnMouseExit 				= 1<<9,
        OnMouseOver 				= 1<<10,

        OnCollisionEnter 			= 1<<11,
        OnCollisionExit 			= 1<<12,
        OnCollisionStay 			= 1<<13,

        OnTriggerEnter 			    = 1<<14,
        OnTriggerExit 			    = 1<<15,
        OnTriggerStay 			    = 1<<16,

        OnEnable 					= 1<<17,
        OnDisable 				    = 1<<18,
        Start 					    = 1<<19,
        #endregion 原生事件
        #region NGUI事件
        OnClick 					= 1<<20,
        OnHover 					= 1<<21,
        OnPress 					= 1<<22,
        OnSelect 					= 1<<23,
        OnDragStart 				= 1<<24,
        OnDragEnd 			    	= 1<<25,
        OnDragOver 			    	= 1<<26,
        OnDragOut 			    	= 1<<27,
        OnDrag 				    	= 1<<28,
        #endregion NGUI事件
    }
}
