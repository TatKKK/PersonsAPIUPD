//using System.Globalization;

//namespace WebApi.Middleware
//{
//    public class LanguageMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public LanguageMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            var supportedCultures = new[] { "en-US", "en", "ka-GE", "ka" }; // Add more cultures as needed
//            var acceptLanguageHeader = context.Request.Headers["Accept-Language"].ToString();

//            // Default to English if no Accept-Language header is provided
//            var culture = "en-US";

//            if (!string.IsNullOrEmpty(acceptLanguageHeader))
//            {
//                var languages = acceptLanguageHeader.Split(',')
//                    .Select(lang => lang.Split(';')[0].Trim()) // Remove any weight parameters like "en-US;q=0.8"
//                    .ToList();

//                // Check for a matching supported culture
//                var matchedCulture = languages.FirstOrDefault(lang => supportedCultures.Contains(lang));
//                if (matchedCulture != null)
//                {
//                    culture = matchedCulture; // Use the matched culture
//                }
//            }

//            // Set the culture for the current request
//            var cultureInfo = new CultureInfo(culture);
//            CultureInfo.CurrentCulture = cultureInfo;
//            CultureInfo.CurrentUICulture = cultureInfo;

//            await _next(context);
//        }
//    }
//}
