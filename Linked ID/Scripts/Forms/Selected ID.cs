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

        UIApplication uiApp;
        FindID findID;
        string a = "";
        BoundingBoxXYZ OriginalBoundingBox;
        bool OriginalViewCropped;

        public Selected_ID()
        {
            InitializeComponent();
            
        }

        public Selected_ID(UIApplication uiapp, ICollection<Reference> refe)
        {
            uiApp = uiapp;
            InitializeComponent();

            OriginalBoundingBox = GetOriginalBox();
            OriginalViewCropped = IsViewCropped();

            listBox1.Items.Clear();

            findID = new FindID();


            references = refe;

            if (references.Count < 1)
            {
                TaskDialog.Show("Error", "Please Select at least one Element");
                a += "You have not selected any Elements";
                listBox1.Items.Add(a);
            }

            foreach (var id in references)
            {
                listBox1.Items.Add(findID.GetElementID(id, uiApp));
                a += findID.GetElementID(id, uiApp) + ";";
            }
        }


        private void B_Cancel_Click(object sender, EventArgs e)
        {
            SetSectionBox(OriginalBoundingBox, OriginalViewCropped);
            this.Close();
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            SetSectionBox(OriginalBoundingBox, OriginalViewCropped);

            System.Windows.Clipboard.SetText(a);
        }

        private void B_Show_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.File.WriteAllText(saveFileDialog.FileName, a);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null || listBox1.SelectedItem.ToString() != "You have not selected any Elements")
            {
                Document linkDoc = findID.linkedDocument;
                ElementId id = listBox1.SelectedItem as ElementId;
                var list = new List<ElementId>();
                list.Add(id);

                Element element = new FilteredElementCollector(linkDoc, list).WhereElementIsNotElementType().ElementAt(0);

                BoundingBoxXYZ boundingBoxXYZ = element.get_BoundingBox(null);
                SetSectionBox(boundingBoxXYZ, true);
            }
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
    }
}
