using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using prog3_practica_08.CustomValidations;

namespace prog3_practica_08.Models;

public partial class Alquilere
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Debe seleccionar una copia.")]
    public int IdCopia { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un cliente.")]
    public int IdCliente { get; set; }

    public DateTime FechaAlquiler { get; set; }

    [Required(ErrorMessage = "Debe marcar fecha tope del alquiler (maximo 3 dias.)")]
    [ValidateAlquilerDateRange]
    public DateTime FechaTope { get; set; }

    public DateTime? FechaEntregada { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Copia? IdCopiaNavigation { get; set; }
}
