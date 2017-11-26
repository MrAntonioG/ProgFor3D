using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coursework
{
    class Score:Stat
    {
        public override void Increase(int val)
        {
            this.value += val;
        }
    }
}
