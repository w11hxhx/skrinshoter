using System;
using Microsoft.Extensions.DependencyInjection;
namespace SkrinshoterApp;
public sealed class AppUpdateCheckWindowVMDesignFactory : DesignFactory<AppUpdateCheckWindowVM>
{
    public override AppUpdateCheckWindowVM Create(IServiceProvider sp) => ActivatorUtilities.CreateInstance<AppUpdateCheckWindowVM>(sp);
}
