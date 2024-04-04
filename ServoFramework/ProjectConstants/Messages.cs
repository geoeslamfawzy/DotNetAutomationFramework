using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ServoFramework.ProjectConstants
{
    public class Messages
    {
        private readonly static string _startActivityMessage = "Activity Started Successfully";

        private readonly static string _completeActivityMessage = "Activity Completed successfully";


        public static string StartActivityMessage()
        {
            return _startActivityMessage;
        }
        public static string CompleteActivityMessage()
        {
            return _completeActivityMessage;
        }
    }
}
