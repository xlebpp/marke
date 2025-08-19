namespace marketplaceE.JwtSettings
{
    public class JwtSetting
    {
        public string Issuer { get; set; } 
        public string Audience { get; set; } 
        public string key { get; set; } 
        public TimeSpan Expires { get; set; }
    }
}
