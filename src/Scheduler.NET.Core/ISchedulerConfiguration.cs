﻿using System.Collections.Generic;

namespace Scheduler.NET.Core
{
	public interface ISchedulerConfiguration
	{
		string HangfireStorageType { get; set; }
		string HangfireConnectionString { get; set; }
		string Host { get; set; }
		bool AuthorizeApi { get; }
		HashSet<string> Tokens { get; }
		string TokenHeader { get; }
	}
}
