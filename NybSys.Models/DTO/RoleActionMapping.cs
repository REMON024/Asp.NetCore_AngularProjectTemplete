using System;
using System.Collections.Generic;
using System.Text;

namespace NybSys.Models.DTO
{
  public  class RoleActionMapping
    {
        public long RoleActionMappingID { get; set; }
        public int AccessRightID { get; set; }
        public int ActionID { get; set; }
        public int ControllerID { get; set; }
    }
}
