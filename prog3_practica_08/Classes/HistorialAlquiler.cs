namespace prog3_practica_08.Classes
{
    public class HistorialAlquiler
    {
        public int idAlquiler { get; set; }
        public DateTime fechaAlquilada { get; set; }
        public DateTime fechaTope {  get; set; }
        public DateTime? fechaEntrega {  get; set; }
        public string? nombreCliente {  get; set; }
        public string? clienteTelefono { get; set; }
        public string peliculaTitulo {  get; set; }
        public string formatoPelicula {  get; set; }

        public HistorialAlquiler(int idAlquiler, DateTime fechaAlquilada, DateTime fechaTope, DateTime? fechaEntrega, string? nombreCliente, string? clienteTelefono, string peliculaTitulo, string formatoPelicula)
        {
            this.idAlquiler = idAlquiler;
            this.fechaAlquilada = fechaAlquilada;
            this.fechaTope = fechaTope;
            this.fechaEntrega = fechaEntrega;
            this.nombreCliente = nombreCliente;
            this.clienteTelefono = clienteTelefono;
            this.peliculaTitulo = peliculaTitulo;
            this.formatoPelicula = formatoPelicula;
        }

        public HistorialAlquiler()
        {

        }
    }
}
