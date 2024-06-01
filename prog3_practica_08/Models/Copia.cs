using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prog3_practica_08.Models;

public partial class Copia
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Se necesita ID de la pelicula.")]
    public int IdPelicula { get; set; }

    public bool Deteriorada { get; set; }

    public string? Formato { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Valor debe ser mayor a 0.")]
    [Required(ErrorMessage = "Se necesita especificar precio del alquiler.")]
    public double PrecioAlquiler { get; set; }

    public virtual ICollection<Alquilere> Alquileres { get; set; } = new List<Alquilere>();

    public virtual Pelicula? IdPeliculaNavigation { get; set; }
}
