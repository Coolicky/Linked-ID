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

            PushButtonData selectBD = new PushButtonData("Hide Selection in View", "Hide\nSelection", this_assembly_path, "Coolicky_Plugins.HideInView");
            
            selectBD.ToolTip = "Hide Selection in View";
            BitmapImage pb_hide_image = 
            selectBD.LargeImage = new BitmapImage(new Uri("pack://application:,,,/Coolicky_Plugins;component/Resources/Hide.png"));



            SplitButtonData splitButtonData = new SplitButtonData("LinkedID", "Linked ID");
            SplitButton splitButton = ribbon_panel_tools.AddItem(splitButtonData) as SplitButton;

            PushButton selectB = ribbon_panel_tools.AddItem(selectBD) as PushButton;

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
}
