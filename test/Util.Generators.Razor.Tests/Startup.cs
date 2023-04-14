using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Util.Helpers;
using Util.Logging.Serilog;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace Util.Generators.Razor.Tests {
    /// <summary>
    /// ��������
    /// </summary>
    public class Startup {
        /// <summary>
        /// ��������
        /// </summary>
        public void ConfigureHost( IHostBuilder hostBuilder ) {
            hostBuilder.ConfigureDefaults( null )
                .ConfigureHostConfiguration( builder => {
                    Environment.SetDevelopment();
                } )
                .AddUtil( t => t.UseSerilog() );
        }

        /// <summary>
        /// ������־�ṩ����
        /// </summary>
        public void Configure( ILoggerFactory loggerFactory, ITestOutputHelperAccessor accessor ) {
            loggerFactory.AddProvider( new XunitTestOutputLoggerProvider( accessor ) );
        }
    }
}
