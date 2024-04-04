using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ServoTestProject.Enums
{
    public class EnumMapping
    {
        public string MapEnumToString(Enum key)
        {
            var dict = new Dictionary<Enum, string>()
            {
                {ActivityStatus.Running, "Running" },
                {ActivityStatus.Aborted, "Aborted" },
                {ActivityStatus.Paused, "Paused" },
                {ActivityStatus.Completed, "Completed" },

                {PageTitles.Dashboard, "Dashboard"},
                {PageTitles.Calendar, "Schedule" },
                {PageTitles.Activity_History, "Activity History" },
                {PageTitles.Maintenance_Workcell, "Maintenance / Work Cell" },
                {PageTitles.Maintenance_Store, "Maintenance / Store" },
                {PageTitles.Settings_Users, "Settings / Users" },
                {PageTitles.WorkCells, "Settings / Work Cells" },
                {PageTitles.Methods, "Settings / Methods" },
                {PageTitles.Worklists, "Settings / Worklists" },
                {PageTitles.Cameras, "Settings / Cameras" },
                {PageTitles.WorkCellDetailsPage, "Work Cell Details" },


                {WorkCellType.Store, "Store" },
                {WorkCellType.LiquidHandler, "Liquid Handler"},
                {WorkCellType.StandAloneDevice, "Stand Alone Device"},
                {WorkCellType.General, "General"},
                {WorkCellType.Screening, "Screening"},
                {WorkCellType.LC_Ms,  "LC-MS"},

                {WorkCellAPIType.NonAPI, "Non-API (No integration )" },
                {WorkCellAPIType.InstinctS, "Instinct S Integration" },
                {WorkCellAPIType.InstinctV,  "Instinct V Integration"},
                {WorkCellAPIType.VenusIntegration,  "Venus Integration"},

                {CalenderType.FIFO,  "FIFO"},
                {CalenderType.TimeBased, "Time based" },

                {WorkCellNames.NON_API_LH_Automation_TB, "NON API LH-Automation TB" },
                {WorkCellNames.NON_API_LH_Automation_FIFO, "NON API LH-Automation FIFO" },
                {WorkCellNames.API_LH_TB, "API TB (liquid handler)" },
                {WorkCellNames.Test, "Test" }
            }; 

            var value = dict[key];
            return value;
        }
    }

}
