using System;
using System.Linq;
using AudioVideoCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AudioCapture;

public sealed class CompositeAudioFrameObserver : IAudioFrameObserver
{
	private readonly IAudioFrameObserver[] observers;

	private readonly ILogger logger;

	public CompositeAudioFrameObserver(IAudioFrameObserver[] observers, ILogger<CompositeAudioFrameObserver> logger = null)
	{
		if (observers == null)
		{
			throw new ArgumentNullException("observers");
		}
		if (observers.Any((IAudioFrameObserver x) => x == null))
		{
			throw new ArgumentNullException("observers", "Observer must not be null.");
		}
		this.observers = observers;
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
	}

	public void OnFrameCaptured(ReadOnlySpan<byte> data, AudioFormat format)
	{
		IAudioFrameObserver[] array = observers;
		foreach (IAudioFrameObserver audioFrameObserver in array)
		{
			try
			{
				audioFrameObserver.OnFrameCaptured(data, format);
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Audio frame observer {observerType} threw an exception", audioFrameObserver.GetType().Name);
			}
		}
	}
}
