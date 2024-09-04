namespace parcial.Models
{
    public class Venta
    {
        public int VentaId { get; set; }
        public int? VehiculoId { get; set; }
        public int Cantidad { get; set; }
        public double TotalVentas { get; set; }
        public virtual Vehiculo? Vehiculo { get; set; }
    }
}
