using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceCtrlLib.PanelExtend
{
    public class GlobalMethod
    {
        //判断两条直接有无交集
        public static Boolean HasIntersection(int firstLitter, int firstBig, int secondLitter, int secondBig)
        {
            return Math.Max(firstLitter, secondLitter) < Math.Min(firstBig, secondBig);
        }
    }
}
