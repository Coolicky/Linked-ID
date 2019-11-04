using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Windows;
using Form = System.Windows.Forms.Form;

namespace Linked_ID.Scripts.Forms
{
    public partial class Selected_ID : Form
    {
        ICollection<Reference> references;

        public Selected_ID()
        {
            InitializeComponent();
            
        }

        public Selected_ID(UIApplication uiapp)
        {
            InitializeComponent();

            listBox1.Items.Clear();

            FindID findID = new FindID();

            references = findID.linkedObjects(uiapp);

            foreach (var id in references)
            {
                listBox1.Items.Add(findID.GetElementID(id, uiapp));
            }
        }


        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void B_OK_Click(object sender, EventArgs e)
        {

            string a = "";
            foreach (var id in references)
            {
                a += findID.GetElementID(id, uiapp) + ";";
            }
            System.Windows.Clipboard.SetText(a);
        }

        private void B_Show_Click(object sender, EventArgs e)
        {

        }
    }
}
