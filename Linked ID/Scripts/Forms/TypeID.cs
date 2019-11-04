using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Form = System.Windows.Forms.Form;

namespace Linked_ID.Scripts.Forms
{
    public partial class TypeID : Form
    {
        ICollection<Reference> references;

        UIApplication uiApp;
        FindByID findByID;
        string a = "";
        BoundingBoxXYZ OriginalBoundingBox;
        bool OriginalViewCropped;

        public TypeID()
        {
            InitializeComponent();
        }
        public TypeID(UIApplication uiapp)
        {
            InitializeComponent();

            uiApp = uiapp;

            OriginalBoundingBox = GetOriginalBox();
            OriginalViewCropped = IsViewCropped();

            listBox1.Items.Clear();

            findByID = new FindByID();
        }


        public void SetSectionBox(BoundingBoxXYZ box, bool cropOrNot)
        {
            using (Transaction trans = new Transaction(uiApp.ActiveUIDocument.Document))
            {
                trans.Start("Cropping View");

                if (uiApp.ActiveUIDocument.Document.ActiveView.ViewType != ViewType.ThreeD)
                {
                    uiApp.ActiveUIDocument.ActiveView.CropBoxActive = cropOrNot;
                    uiApp.ActiveUIDocument.ActiveView.CropBox = box;
                }
                else
                {
                    View3D view3D = uiApp.ActiveUIDocument.ActiveView as View3D;

                    view3D.IsSectionBoxActive = cropOrNot;
                    view3D.SetSectionBox(box);

                }
                trans.Commit();
            }
        }

        public BoundingBoxXYZ GetOriginalBox()
        {
            if (uiApp.ActiveUIDocument.Document.ActiveView.ViewType != ViewType.ThreeD)
            {
                return uiApp.ActiveUIDocument.ActiveView.CropBox;
            }
            else
            {
                View3D view3D = uiApp.ActiveUIDocument.ActiveView as View3D;
                return view3D.GetSectionBox();
            }
        }

        public bool IsViewCropped()
        {
            if (uiApp.ActiveUIDocument.Document.ActiveView.ViewType != ViewType.ThreeD)
            {
                return uiApp.ActiveUIDocument.ActiveView.CropBoxActive;
            }
            else
            {
                View3D view3D = uiApp.ActiveUIDocument.ActiveView as View3D;

                return view3D.IsSectionBoxActive;
            }
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            SetSectionBox(OriginalBoundingBox, OriginalViewCropped);
            this.Close();
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            if (textBox1.Text.Length < 1)
                TaskDialog.Show("Error", "Please type in some ID's");
            else
            {
                string[] ids = textBox1.Text.Split(';');
            }
            
            foreach (var id )

        }

        private void B_Import_Click(object sender, EventArgs e)
        {

        }

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ';'))
            {
                e.Handled = true;
            }
        }
    }
}
