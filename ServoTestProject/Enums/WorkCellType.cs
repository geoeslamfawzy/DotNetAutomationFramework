using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoTestProject.Enums
{
    public enum WorkCellType
    {
        Store,
        LiquidHandler,
        StandAloneDevice,
        General,
        Screening,
        LC_Ms
    }

    public enum WorkCellAPIType
    {
      
        NonAPI,
        InstinctS,
        InstinctV,
        VenusIntegration
    }
    public enum CalenderType
    {
        FIFO,
        TimeBased
    }
}
