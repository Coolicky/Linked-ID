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

        Document linkDoc;

        public TypeID()
        {
            InitializeComponent();
        }
        public TypeID(UIApplication uiapp, Document lnk)
        {
            InitializeComponent();

            linkDoc = lnk;
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
            AddIDstoList(textBox1.Text);
            textBox1.Text = "";
        }

        private void B_Import_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text File|*.txt";
            openFileDialog.ShowDialog();

            string importedText;

            if (openFileDialog.FileName != "")
            {
                listBox1.Items.Clear();

                importedText = System.IO.File.ReadAllText(openFileDialog.FileName);

                AddIDstoList(importedText);
            }

        }

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ';'))
            {
                e.Handled = true;
            }
        }

        private void AddIDstoList(string listOfID)
        {
            if (listOfID.Length < 1)
                TaskDialog.Show("Error", "Please type in some ID's");
            else
            {
                string[] ids = listOfID.Split(';');
                foreach (var id in ids)
                {
                    int idNumber;
                    int.TryParse(id, out idNumber);

                    ElementId elemId = new ElementId(idNumber);
                    if (idNumber != 0)
                        listBox1.Items.Add(elemId);
                }

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null || listBox1.SelectedItem.ToString() != "You have not selected any Elements")
            {
                ElementId id = listBox1.SelectedItem as ElementId;
                var list = new List<ElementId>();
                list.Add(id);
                                
                if (list.Count > 0)
                {
                    Element element = new FilteredElementCollector(linkDoc, list).WhereElementIsNotElementType().ElementAt(0);

                    BoundingBoxXYZ boundingBoxXYZ = element.get_BoundingBox(null);
                    SetSectionBox(boundingBoxXYZ, true);
                }
                
            }
        }
    }
}
