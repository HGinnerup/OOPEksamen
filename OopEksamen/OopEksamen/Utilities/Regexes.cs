using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OopEksamen.Utilities
{
    public static class Regexes
    {
        /* Interpreted requirements from assignment:
            local-part
                Any combination of a-z, A-Z and digits 0-9, as well as '.', '_' and '-'
                Additionally: I assume a length of at least 1
            domain:
                Any combination of a-z, A-Z, the digits 0-9 as well as '.' and '-'
                May not start or end with '-' or '.'
                Must contain at least 1 '.'

        */
        public static readonly Regex Email = new Regex(@"^(?<localPart>[a-zA-Z0-9._-]+)@(?<domain>[a-zA-Z0-9]+[a-zA-Z0-9.-]*?\.[a-zA-Z0-9.-]*?[a-zA-Z0-9]+)$");
    }
}
