using System.Windows.Forms;
using ConnectUO.Controls;

namespace ConnectUO.Forms
{
    public partial class SkinnableForm : Form
    {
        private ConnectUORenderer renderer = new DefaultRenderer();

        public ConnectUORenderer Renderer
        {
            get { return renderer; }
        }

        public SkinnableForm()
        {
            InitializeComponent();
        }
    }
}
