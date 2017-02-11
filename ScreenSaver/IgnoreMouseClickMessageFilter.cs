using Aerial;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ScreenSaver
{

    class IgnoreMouseClickMessageFilter : IMessageFilter
    {
        private Control parent { get; set; }
        private Control target { get; set; }

        public IgnoreMouseClickMessageFilter(Control parent, Control target)
        {
            this.parent = parent;
            this.target = target;
        }

        public bool PreFilterMessage(ref Message messageBeforeFiltering)
        {
            Trace.WriteLine("PreFilterMessage()");
            const bool FilterTheMessageOut = true;
            const bool LetTheMessageThrough = false;

            if (IsNull(parent)) return LetTheMessageThrough;
            if (IsNull(target)) return LetTheMessageThrough;
            if (WasNotClickedOnTarget(parent, target)) return LetTheMessageThrough;
            if (MessageContainsAnyMousebutton(messageBeforeFiltering)) return FilterTheMessageOut;
            return LetTheMessageThrough;
        }

        private bool MessageContainsAnyMousebutton(Message message)
        {
            Trace.WriteLine("MessageContainsAnyMousebutton()");
            if (message.HWnd == parent.Handle)
            {
                if (message.Msg == NativeMethods.WM_LBUTTONUP) return true;
                if (message.Msg == NativeMethods.WM_LBUTTONDBLCLK) return true;
                if (message.Msg == NativeMethods.WM_RBUTTONDOWN) return true;
                if (message.Msg == NativeMethods.WM_RBUTTONUP) return true;
                return false;
            }
            return false;
        }

        private bool WasNotClickedOnTarget(Control parent, Control target)
        {
            Trace.WriteLine("WasNotClickedOnTarget()");
            Control clickedOn = parent.GetChildAtPoint(Cursor.Position);
            if (IsNull(clickedOn)) return true;
            if (AreEqual(clickedOn, target)) return false;
            return true;
        }

        private bool AreEqual(Control controlA, Control controlB)
        {
            if (controlA == controlB) return true;
            return false;
        }

        private bool IsNull(Control control)
        {
            if (control == null) return true;
            return false;
        }
    }
}
