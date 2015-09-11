using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ConnectUO.Framework.Windows.Forms;

namespace ConnectUO.Controls
{
    public sealed class DonationButton : Button
    {
        const string DonationUrl = "https://www.paypal.com/xclick/business=jeff@runuo.com&item_name=ConnectUO%20Donation&no_shipping=1&no_note=1&tax=0&currency_code=USD";

        const string Notice = "Thank you for wanting to make a contribution to support ConnectUO.  "
            + "Before making your contribution please make sure you understand the following:\n\n"
            + "Making a contribution to ConnectUO does not entitle you to any special treatment.  "
            + "This condition also applies to any and all servers you have registered with ConnectUO\n\n"
            + "ConnectUO is dedicated to providing a free, unbiased listing service for "
            + "any and all free Ultima Online Servers.\n\n"
            + "If you would still like to contribute and support ConnectUO, Please click OK";

        public event EventHandler<EventArgs> DonationMessageBoxClosed;

        public DonationButton()
        {
            Size = new Size(74, 14);
            BackgroundImage = Properties.Resources.Donate;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (MessageBoxEx.Show(Parent, Notice,
                "Donation Notice", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Process.Start(DonationUrl);
            }

            OnDonationMessageBoxClosed(this, EventArgs.Empty);
        }
        private void OnDonationMessageBoxClosed(object sender, EventArgs e)
        {
            if (DonationMessageBoxClosed != null)
            {
                DonationMessageBoxClosed(sender, e);
            }
        }
    }
}
