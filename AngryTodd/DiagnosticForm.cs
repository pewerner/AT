using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace AngryTodd
{
    public partial class DiagnosticForm : Form
    {
        // TODO: module level refrence to your driver - change "Object" to the correct device class
        Object _myControlRef;

        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control in a thread safe manner - use if your
        // low level driver throws status update events.
        delegate void SetTextCallback(string text);

        public DiagnosticForm(Object ControlRef)
        {
            InitializeComponent();
            this.Text = "Diagnostics v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            // see if we are talking to a live device, or new profile
            if (ControlRef == null)
            {
                // TODO: Disable controls, profile has not been initialized.
            }
            else
            {
                _myControlRef = ControlRef;

                // TODO:
                // Enable controls, profile has been initialized.
                // create event handler - use this id called if your driver throws status update events:
                //_myControlRef.StatusChanged += new BioShakeControl.BioShake.ChangedEventHandler(UpdateState);

            }
        }

        // event handler for update status events if your low level
        // driver fires status update events.
        private void UpdateState(object sender, EventArgs e)
        {
            string currentState = "Not Connected";
            // TODO: get status update from EventArgs - note that your eventargs can be specialized
            // as in a status string, enumeration, object etc...

            this.SetText("Status: " + currentState); // Call the thread safe lable update method
        }

        // thread safe status label updater
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.labelStatus.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelStatus.Text = text;
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutBox aboutBoxRef = new AboutBox(_myControlRef);
            aboutBoxRef.ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonToDo_Click(object sender, EventArgs e)
        {
            // since driver calls in VWorks MUST be synchronous, commands must be executed on a separate worker thread 
            // to ensure user interface is responsive.
            System.Threading.Thread taskThread = new System.Threading.Thread(ThreadToDo);
            taskThread.Start();
        }

        private void buttonInit_Click(object sender, EventArgs e)
        {
            // since driver calls in VWorks MUST be synchronous, commands must be executed on a separate worker thread 
            // to ensure user interface is responsive.
            System.Threading.Thread taskThread = new System.Threading.Thread(ThreadInit);
            taskThread.Start();
        }

        private void ThreadInit()
        {
            // call init command
        }

        private void ThreadToDo()
        {
            // call command
        }

        private void DiagnosticForm__FormClosing(object sender, FormClosingEventArgs a)
        {
            // TODO:
            // If untilizing event handler, unregister the event handler at form close or the form wont
            // close successfully 
            //_myControlRef.StatusChanged -= UpdateState
        }


    }



}
