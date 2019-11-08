using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;
using System.Reflection;

namespace Linked_ID
{
    public class LinkedID : IExternalApplication
    {
        static void AddRibbonPanel(UIControlledApplication application)
        {
            RibbonPanel ribbon_panel_tools = application.CreateRibbonPanel("LinkedID");

            string this_assembly_path = Assembly.GetExecutingAssembly().Location;

            PushButtonData selectBD = new PushButtonData("FindID", "Find IDs", this_assembly_path, "Linked_ID.Scripts.FindID");
            selectBD.ToolTip = "Find IDs of Objects in Linked Model";
            selectBD.LargeImage = new BitmapImage(new Uri("pack://application:,,,/Linked ID;component/Images/Link_Icon_Find.png"));

            PushButtonData findBD = new PushButtonData("FindbyID", "Find by IDs", this_assembly_path, "Linked_ID.Scripts.FindByID");
            findBD.ToolTip = "Find Objects in a Linked Model by Their ID";
            findBD.LargeImage = new BitmapImage(new Uri("pack://application:,,,/Linked ID;component/Images/Link_Icon_Search.png"));

            SplitButtonData splitButtonData = new SplitButtonData("LinkedID", "Linked ID");
            splitButtonData.LargeImage = new BitmapImage(new Uri("pack://application:,,,/Linked ID;component/Images/Link_Icon_Main.png"));
            SplitButton splitButton = ribbon_panel_tools.AddItem(splitButtonData) as SplitButton;

            splitButton.AddPushButton(selectBD);
            splitButton.AddPushButton(findBD);

        }
        public Result OnShutdown(UIControlledApplication application)
        {

            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            AddRibbonPanel(application);
            return Result.Succeeded;
        }
    }
}

