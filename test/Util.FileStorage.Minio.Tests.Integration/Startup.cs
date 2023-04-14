using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Util.FileStorage.Minio.Samples;
using Util.Sessions;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace Util.FileStorage.Minio {
    /// <summary>
    /// ��������
    /// </summary>
    public class Startup {
        /// <summary>
        /// ��������
        /// </summary>
        public void ConfigureHost( IHostBuilder hostBuilder ) {
            hostBuilder.ConfigureDefaults( null )
                .ConfigureWebHostDefaults( webHostBuilder => {
                    webHostBuilder.UseTestServer()
                        .ConfigureServices( services => {
                            services.AddControllers();
                            services.AddHttpClient();
                        } )
                        .Configure( t => {
                            t.UseRouting();
                            t.UseEndpoints( endpoints => {
                                endpoints.MapControllers();
                            } );
                        } );
                } )
                .AddUtil( options => {
                    Util.Helpers.Environment.SetDevelopment();
                    options.UseMinio( minioOptions => minioOptions.Endpoint( "minio-endpoint.a.com" )
                        .AccessKey( "rfgzi0KnOhM3CCrc" ).SecretKey( "2x0wzz6f1QzwrvJONXda2Y59rZ1WdvTG" )
                        .DefaultBucketName( "Util.FileStorage.Minio.Test" )
                        .UseSSL()
                     );
                } );
        }

        /// <summary>
        /// ���÷���
        /// </summary>
        public void ConfigureServices( IServiceCollection services ) {
            services.AddSingleton<ISession, TestSession>();
        }

        /// <summary>
        /// ������־�ṩ����
        /// </summary>
        public void Configure( ILoggerFactory loggerFactory, ITestOutputHelperAccessor accessor ) {
            loggerFactory.AddProvider( new XunitTestOutputLoggerProvider( accessor, ( s, logLevel ) => logLevel >= LogLevel.Trace ) );
        }
    }
}
