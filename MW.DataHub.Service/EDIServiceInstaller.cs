﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace MW.DataHub.Service
{
    [RunInstaller(true)]
    public partial class EDIServiceInstaller : Installer
    {
        public EDIServiceInstaller()
        {
            InitializeComponent();
        }
    }
}
