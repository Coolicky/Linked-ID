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

namespace Linked_ID.Scripts
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    class FindByID : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            try
            {
                Reference refe = uiapp.ActiveUIDocument.Selection.PickObject(ObjectType.LinkedElement, "Pick Object");

                Element e = doc.GetElement(refe.ElementId);

                Document link = (e as RevitLinkInstance).GetLinkDocument();

                Element eLinked = link.GetElement(refe.LinkedElementId);

                ElementId linkedElementId = eLinked.Id;

                TaskDialog.Show("Linked Element ID", linkedElementId.ToString() + " - Added to clipboard");

                Clipboard.SetText(linkedElementId.ToString());


            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                TaskDialog.Show("Error", message);
                return Result.Failed;
            }
            return Result.Succeeded;
        }

    }
}
