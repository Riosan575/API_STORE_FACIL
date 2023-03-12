namespace ApiStore.Model
{
    public class Usuario
    {
        public int id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? nombre { get; set; }
        public string? apellidoPaterno { get; set; }
        public string? apellidoMaterno { get; set; }
        public string? dni { get; set; }
        public string? telefono { get; set; }
        public string? tipo { get; set; }
        public bool estado { get; set; }
    }
}
