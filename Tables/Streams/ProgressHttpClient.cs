using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Streams;

public class ProgressHttpClient : HttpClient
{

	public event ProgressChangedEventHandler? ProgressChanged;

	public async Task DownloadDataAsync(string requestUrl, Stream destination, uint bufferSize = 81920, CancellationToken cancellationToken = default)
	{
        using var response = await GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        var contentLength = response.Content.Headers.ContentLength;
        using var download = await response.Content.ReadAsStreamAsync(cancellationToken);

        if (!contentLength.HasValue)
        {
            await download.CopyToAsync(destination, cancellationToken);
            return;
        }

        if (!download.CanRead)
            throw new InvalidOperationException($"'{nameof(download)}' is not readable.");
        if (destination == null)
            throw new ArgumentNullException(nameof(destination));
        if (!destination.CanWrite)
            throw new InvalidOperationException($"'{nameof(destination)}' is not writable.");

        var buffer = new byte[bufferSize];
        long totalBytesRead = 0;
        int bytesRead;
        while ((bytesRead = await download.ReadAsync(buffer, cancellationToken).ConfigureAwait(false)) != 0)
        {
            await destination.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken).ConfigureAwait(false);
            totalBytesRead += bytesRead;
            ProgressChanged?.Invoke(this, new ProgressChangedEventArgs(totalBytesRead / contentLength.Value * 100f));
        }
    }
}
