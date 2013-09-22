using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Fonlow.SyncML.Common
{
    public delegate void ProcessTextHandler(string text);
    public delegate void ProcessIntegerHandler(int number);
    public delegate void ProcessCallback(IAsyncResult asyncResult);
    public delegate void InitProgressBarHandler(InitProgressBarEventArgs e);


    /// <summary>
    /// Helper for safely firing events
    /// </summary>
    public static class EventsHelper
    {
        /// <summary>
        /// Fire subscribed functions safely even if some of them fail.
        /// </summary>
        /// <typeparam name="T">.NET2.0's event model requires generic.</typeparam>
        /// <param name="eventProperty">The publisher's event</param>
        /// <param name="sender"></param>
        /// <param name="eventArgs">Publish's even arguments to pass on.</param>
        public static void Fire<T>(EventHandler<T> eventProperty, object sender, T eventArgs) where T : EventArgs
        {
            if (eventProperty == null)
            {
                throw new ArgumentNullException("eventProperty");
            }

            if (sender == null)
            {
                throw new ArgumentNullException("sender");
            }

            Delegate[] delegates = eventProperty.GetInvocationList();
            foreach (Delegate delg in delegates)
            {
                EventHandler<T> handler = (EventHandler<T>)delg;
                try
                {
                    handler(sender, eventArgs);
                }
                catch (Exception e)
                {
                    Trace.TraceWarning(String.Format(
                        "When firing event with {0} and {1}:{2}",
                        sender, eventArgs.ToString(), e));
                }//catch everything for purpose.
            }
        }
    }


    /// <summary>
    /// This is for return a text message in an event
    /// </summary>
    public class StatusEventArgs : EventArgs
    {
        public StatusEventArgs(string text)
        {
            Text = text;
            Status = OperationStatus.Unspecified;
        }

        public StatusEventArgs(string text, OperationStatus status)
        {
            Text = text;
            Status = status;
        }

        public string Text
        {
            get;
            set;
        }

        public OperationStatus Status { get; set; }

    }

    public enum OperationStatus { Unspecified, Successful, Failed }

    /// <summary>
    /// Contain the amount of increment that the progress bar should proceed.
    /// </summary>
    public class IncrementProgressBarEventArgs : EventArgs
    {
        public IncrementProgressBarEventArgs(int number)
        {
            incrementAmount = number;
        }

        private int incrementAmount;
        public int IncrementAmount
        {
            get { return incrementAmount; }
        }

    }

    /// <summary>
    /// Contain init info of progress bar.
    /// </summary>
    public class InitProgressBarEventArgs : EventArgs
    {

        public InitProgressBarEventArgs(int minValue, int maxValue, int step)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.step = step;
        }

        private int minValue;

        public int MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }


        private int maxValue;

        public int MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        private int step;

        public int Step
        {
            get { return step; }
            set { step = value; }
        }


    }

    /// <summary>
    /// This is to provide client codes with two texts for comparison in order to do unit tests.
    /// </summary>
    public class CompareTextsEventArgs : EventArgs
    {
        public CompareTextsEventArgs(string text1, string text2)
        {
            this.text1 = text1;
            this.text2 = text2;
        }
        private string text1;

        public string Text1
        {
            get { return text1; }
            set { text1 = value; }
        }
        private string text2;

        public string Text2
        {
            get { return text2; }
            set { text2 = value; }
        }

    }

    /// <summary>
    /// Report new location and size in an event
    /// </summary>
    public class LocationSizeChangedEventArgs : EventArgs
    {
        public LocationSizeChangedEventArgs(System.Drawing.Point location, System.Drawing.Size size)
        {
            this.size = size;
            this.location = location;
        }

        private System.Drawing.Point location;

        public System.Drawing.Point Location
        {
            get { return location; }
            set { location = value; }
        }

        private System.Drawing.Size size;

        public System.Drawing.Size Size
        {
            get { return size; }
            set { size = value; }
        }

    }

    /// <summary>
    /// Return a anchor including the anchor number and the time in an event. 
    /// </summary>
    public class AnchorChangedEventArgs : EventArgs
    {
        public AnchorChangedEventArgs(DateTime time, string anchor)
        {
            Time = time;
            LastAnchor = anchor;
        }

        public DateTime Time
        {
            get;
            private set;
        }

        public string LastAnchor
        {
            get;
            private set;
        }

    }
}
