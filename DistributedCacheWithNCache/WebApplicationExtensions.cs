namespace DistributedCacheWithNCache
{
    public static class WebApplicationExtensions
    {
        public static void Deconstruct(this WebApplicationBuilder builder,
                                       out WebApplicationBuilder b,
                                       out IServiceCollection services)
        {
            b = builder;
            services = builder.Services;
        }
    }
}