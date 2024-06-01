using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prog3_practica_08.Models;

public partial class Pelicula
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Debe ingresar un titulo.")]
    public string Titulo { get; set; } = null!;

    [Required(ErrorMessage = "Debe ingresar un año.")]
    public int Anio { get; set; }

    [Range(1, 10, ErrorMessage = "La clasificacion debe ser entre 1 y 10.")]
    [Required(ErrorMessage = "Debe ingrear una clasificacion.")]
    public int Calificacion { get; set; }

    public virtual ICollection<Copia> Copia { get; set; } = new List<Copia>();
}
