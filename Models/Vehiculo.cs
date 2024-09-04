namespace parcial.Models
{
    public class Vehiculo
    {
        public int VehiculoId { get; set; } 
        public int? MarcaId { get; set; }  
        public string Modelo { get; set; } = null;  
        public int Anio { get; set; }
        public int? CantidadPuertas { get; set; }

        public virtual Marca? Marca { get; set; }

        public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
    }
}
