﻿using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Monitor;

public record class RemovedComponent(ulong Entity, EntityComponent[] Components);