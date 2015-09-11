using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ConnectUO
{
    [Serializable]
    [XmlRoot("User")]
    public sealed class UserState
    {
        public event EventHandler<EventArgs> WorkModeChanged;

        private WorkState workMode = WorkState.Offline;
        private string username;
        private string password;

        [XmlElement()]
        public string UserName
        {
            get { return username; }
            set { username = value; }
        }

        [XmlElement()]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [XmlIgnore()]
        public WorkState WorkMode
        {
            get { return workMode; }
            set 
            {
                if (workMode != value)
                {
                    workMode = value;
                    OnWorkModeChanged(this, EventArgs.Empty);
                }
            }
        }

        protected virtual void OnWorkModeChanged(object sender, EventArgs e)
        {
            if (WorkModeChanged != null)
                WorkModeChanged.Invoke(sender, e);
        }
    }
}
