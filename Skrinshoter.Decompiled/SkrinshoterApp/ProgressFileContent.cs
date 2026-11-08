using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
namespace SkrinshoterApp;
internal sealed class ProgressFileContent : HttpContent
{
 private readonly Stream source;
 private readonly IProgress<int> progress;
 private readonly CancellationToken token;
 public ProgressFileContent(Stream source, IProgress<int> progress, CancellationToken token) { this.source = source; this.progress = progress; this.token = token; }
 protected override bool TryComputeLength(out long length) { length = source.Length; return true; }
 protected override async Task SerializeToStreamAsync(Stream target, TransportContext context)
 {
  byte[] buffer = new byte[81920]; long sent = 0; int count;
  while ((count = await source.ReadAsync(buffer, 0, buffer.Length, token)) > 0)
  {
   await target.WriteAsync(buffer, 0, count, token); sent += count;
   progress.Report((int)(sent * 99 / Math.Max(1, source.Length)));
  }
 }
}
