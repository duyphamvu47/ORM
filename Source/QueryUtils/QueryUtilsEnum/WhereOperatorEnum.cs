using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.QueryUtils.QueryUtilsEnum
{
    enum WhereOperatorEnum
    {
        [Description("=")]
        Equals = 0,

        [Description(">")]
        GreaterThan = 1,

        [Description("<")]
        LessThan = 2,

        [Description(">=")]
        GreaterThanOrEqual = 3,

        [Description("<=")]
        LessThanOrEqual = 4,

        [Description("<>")]
        NotEqual = 5
    }
}
