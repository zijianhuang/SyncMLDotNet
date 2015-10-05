using System;
using System.Windows.Forms;
using System.Diagnostics;
using Fonlow.SyncML.Common;

namespace Fonlow.SyncML.Windows
{
    /// <summary>
    /// Listen to trace and append message to memo box.
    /// The memo box is handled properly in asynchronous manner.
    /// </summary>
    public class MemoTraceListener : TraceListener
    {
        private TextBox box;
        public MemoTraceListener(TextBox memoBox)
            : base()
        {
            box = memoBox;
        }

        public override bool IsThreadSafe { get { return false; } }


        public override void Write(string message, string category)
        {
            base.Write(message, category);
            ProcessTextDelegate(message);
        }

        public override void Write(Object o)
        {
            if (o == null)
            {
                return;
            }
            base.Write(o);

            ProcessTextDelegate(o.ToString());
        }

        public override void Write(string message)
        {
            //base method abstract.

            ProcessTextDelegate(message);
        }

        public override void Write(Object o, string category)
        {
            if (o == null)
            {
                return;
            } 
            
            base.Write(o, category);
            ProcessTextDelegate(o.ToString());
        }

        public override void WriteLine(string message, string category)
        {
            base.WriteLine(message, category);
            ProcessTextDelegate(message + Environment.NewLine);
        }

        public override void WriteLine(Object o)
        {
            if (o == null)
            {
                return;
            }

            base.WriteLine(o);
            ProcessTextDelegate(o.ToString() + Environment.NewLine);
        }

        public override void WriteLine(string message)
        {
            //the base class one is abstract
            ProcessTextDelegate(message + Environment.NewLine);
        }

        public override void WriteLine(Object o, string category)
        {
            if (o == null)
            {
                return;
            }

            base.WriteLine(o, category);
            ProcessTextDelegate(o.ToString() + Environment.NewLine);
        }

        /// <summary>
        /// Provide async/threaded call upon box for Trace and Debug
        /// </summary>
        /// <param name="text"></param>
        private void ProcessTextDelegate(string text)
        {
            if (box.InvokeRequired)
            {
                ProcessTextHandler d = new ProcessTextHandler(ProcessTextDelegate);
        //        Action<string> d = ProcessTextDelegate;
                box.Invoke(d, new object[] { text });
            }
            else
            {
                box.AppendText(text);
            }
        }



    }
}
