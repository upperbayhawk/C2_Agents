// Copyright (C) Upperbay Systems, LLC - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Dave Hardin <dave@upperbay.com>, 2001-2020

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OadrVenXLib
{
    public class OadrData
    {
        public string GroupName { get; set; }
        public string SignalName { get; set; }
        public string SignalType { get; set; }
        public string SignalId { get; set; }
        public string DateTime { get; set; }
        public string Duration { get; set; }
    }

}
