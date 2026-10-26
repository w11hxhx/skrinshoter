using System.IO;
using System.Media;
using Common;

namespace SkrinshoterApp;

public class AudioPlayer : SafeDisposable
{
	private readonly SoundPlayer soundPlayer = new SoundPlayer();

	public void PlayAsync(byte[] sound)
	{
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				return;
			}
			soundPlayer.Stop();
			using MemoryStream stream = new MemoryStream(sound);
			soundPlayer.Stream = stream;
			soundPlayer.Play();
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			soundPlayer.Dispose();
		}
	}
}
