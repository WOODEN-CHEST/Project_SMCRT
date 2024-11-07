using System;
using System.Collections.Generic;
using System.Text;

namespace Project_SMCRT_Client.Section;

public class SectionLoadArgs : EventArgs
{
    // Fields.
    public GameSection Section { get; private init; }


    // Constructors.
    public SectionLoadArgs(GameSection section)
    {
        Section = section ?? throw new ArgumentNullException(nameof(section)); 
    }
}