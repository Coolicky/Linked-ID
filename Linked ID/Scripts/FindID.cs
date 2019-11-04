using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Windows;
using Linked_ID.Scripts.Forms;

namespace Linked_ID.Scripts
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class FindID : IExternalCommand
    {
        public Document linkedDocument;
        public RevitLinkInstance linkInstance;

        public ICollection<Reference> linkedObjects(UIApplication uiapp)
        {

            return uiapp.ActiveUIDocument.Selection.PickObjects(ObjectType.LinkedElement, "Pick Objects");
        }

        public ElementId GetElementID(Reference refe, UIApplication uiapp)
        {
            Document doc = uiapp.ActiveUIDocument.Document;

            Element e = doc.GetElement(refe.ElementId);
            linkInstance = e as RevitLinkInstance;
            Document link = (e as RevitLinkInstance).GetLinkDocument();
            linkedDocument = link;

            Element eLinked = link.GetElement(refe.LinkedElementId);

            ElementId linkedElementId = eLinked.Id;

            return linkedElementId;
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

                        
                using (Selected_ID selected_ID = new Selected_ID(uiapp))
                {
                    selected_ID.ShowDialog();
                }

            return Result.Succeeded;
        }
    }
}
