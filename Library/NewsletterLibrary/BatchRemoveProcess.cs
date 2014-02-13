using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections;
using System.Threading;
using i386.Newsletter;

namespace i386.Newsletter
{
    public class BatchRemoveProcess : PopUpProcess
    {
        private ArrayList _Lists;
        private ArrayList _Subscribers;
        private string _ProcessMode;
        public string ProcessMode
        {
            get { return _ProcessMode; }
            set { _ProcessMode = value; }
        }

        public ArrayList Lists
        {
            set { _Lists = value; }
            get { return _Lists; }
        }
        public ArrayList Subscribers
        {
            set { _Subscribers = value; }
            get { return _Subscribers; }
        }

        override public void ProcessToExecute()
        {
            this.ItemCount = this.Subscribers.Count;
            int Counter = 0;
            foreach (string Subscriber in this.Subscribers) // Items
            {
                string ItemName = Subscriber;
                string[] SubscriberDetails = Subscriber.Split(new char[] { ';' });
                #region loop

                if (DemoMode) StatusMessage = "Processing demo item #" + Counter.ToString() + "... ";
                else StatusMessage = ProcessMode + " Process " + SubscriberDetails[0] + "...";

                if (!DemoMode)
                {
                    if (ProcessMode == "Import")
                    {
                        SubscriberMember.InsertIntoList(SubscriberDetails[0], SubscriberDetails[1], this._Lists);
                    }
                    else if (ProcessMode == "Remove")
                    {
                        SubscriberMember.RemoveFromList(SubscriberDetails[0], SubscriberDetails[1], this._Lists);
                    }
                    // Thread.Sleep(10);

                }
                else Thread.Sleep(10);
                Counter++;
                ItemCurrentCount = Counter;
                #endregion
            }
        }


    }
}