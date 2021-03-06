﻿using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Net.Http;
using DotnetSpider.Enterprise.Core.Utils;
using Newtonsoft.Json;

namespace Scheduler.NET.Core.JobManager.Job
{
	/// <summary>
	/// 
	/// </summary>
	public class CallbackJobExecutor : BaseJobExecutor<CallbackJob>
	{
		private readonly RetryPolicy _retryPolicy;

		public CallbackJobExecutor()
		{
			_retryPolicy = Policy.Handle<HttpRequestException>().Retry(RetryTimes, (ex, count) =>
			{
				Logger.LogError($"Execute callback job failed [{count}]: {ex}.");
			});
		}

		public override void Execute(CallbackJob job)
		{
			try
			{
				_retryPolicy.Execute(async () =>
				{
					var response = await HttpUtil.Get(job.Url);
					response.EnsureSuccessStatusCode();
				});

				Logger.LogInformation($"Execute callback job {JsonConvert.SerializeObject(job)} success.");

			}
			catch (Exception e)
			{
				Logger.LogError($"Execute callback job {JsonConvert.SerializeObject(job)} failed: {e}.");
			}
		}
	}
}
