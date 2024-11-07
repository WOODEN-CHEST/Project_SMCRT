using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Client.Section;

public interface ISectionHolder
{
    GameSection CurrentGameSection { get; set; }

    void CloseAllSections();
}