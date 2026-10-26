using System;
using Microsoft.Extensions.DependencyInjection;

namespace SkrinshoterApp;

public sealed class CloudStorageVMDesignFactory : DesignFactory<CloudStorageVM>
{
	public override CloudStorageVM Create(IServiceProvider sp)
	{
		CloudStorageVM cloudStorageVM = ActivatorUtilities.CreateInstance<CloudStorageVM>(sp, Array.Empty<object>());
		cloudStorageVM.usedBytes = 72897620921548L;
		cloudStorageVM.totalBytes = 109951162777600L;
		cloudStorageVM.UpdateProperties();
		return cloudStorageVM;
	}
}
