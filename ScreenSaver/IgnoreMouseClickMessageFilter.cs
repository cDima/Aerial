using System;
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
            const Boolean FilterTheMessageOut = true;
            const Boolean LetTheMessageThrough = false;

            if (IsNull(parent)) return LetTheMessageThrough;
            if (IsNull(target)) return LetTheMessageThrough;
            if (WasNotClickedOnTarget(parent, target)) return LetTheMessageThrough;
            if (MessageContainsAnyMousebutton(messageBeforeFiltering)) return FilterTheMessageOut;
            return LetTheMessageThrough;
        }

        private bool MessageContainsAnyMousebutton(Message message)
        {
            if (message.Msg == 0x202) return true; /* WM_LBUTTONUP*/
            if (message.Msg == 0x203) return true; /* WM_LBUTTONDBLCLK*/
            if (message.Msg == 0x204) return true; /* WM_RBUTTONDOWN */
            if (message.Msg == 0x205) return true; /* WM_RBUTTONUP */
            return false;
        }

        private bool WasNotClickedOnTarget(Control parent, Control target)
        {
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
