using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using prog3_practica_08.Classes;
using prog3_practica_08.Models;

namespace prog3_practica_08.Controllers
{
    public class ConsultasController : Controller
    {
        private string? filtroHistorial = "todo";
        private string? filtroCliente;
        private readonly Prg3EfPr1Context _context;
        public ConsultasController(Prg3EfPr1Context context)
        {
            _context = context;
        }
        public IActionResult CopiasStock()
        {
            ViewData["Peliculas"] = new List<Pelicula>(_context.Peliculas);
            ViewData["Copias"] = new List<Copia>(_context.Copias);
            return View();
        }
        public IActionResult CopiasDisponibleParaAlquilar()
        {
            ViewData["Peliculas"] = new List<Pelicula>(_context.Peliculas);
            ViewData["Copias"] = new List<Copia>(_context.Copias);
            ViewData["Alquileres"] = new List<Alquilere>(_context.Alquileres);
            return View();
        }

        private List<HistorialAlquiler> setHistorial()
        {
            List<Alquilere> alquileres = new List<Alquilere>(_context.Alquileres);
            List<Pelicula> peliculas = new List<Pelicula>(_context.Peliculas);
            List<Cliente> clientes = new List<Cliente>(_context.Clientes);
            List<Copia> copias = new List<Copia>(_context.Copias);

            List<HistorialAlquiler> historialAlquileres = new List<HistorialAlquiler>(alquileres.Count);

            switch (filtroHistorial)
            {
                case "todo":
                    for (int i = 0; i < alquileres.Count; i++)
                    {
                        HistorialAlquiler temp = new HistorialAlquiler();

                        temp.idAlquiler = alquileres[i].Id;
                        temp.fechaAlquilada = alquileres[i].FechaAlquiler;
                        temp.fechaTope = alquileres[i].FechaTope;
                        temp.fechaEntrega = alquileres[i].FechaEntregada;

                        temp.nombreCliente = clientes.Where(x => x.Id == alquileres[i].IdCliente).ToList()[0].Nombre;
                        temp.clienteTelefono = clientes.Where(x => x.Id == alquileres[i].IdCliente).ToList()[0].Telefono;

                        List<Copia> copiaFiltrada = copias.Where(x => x.Id == alquileres[i].IdCopia).ToList();
                        List<Pelicula> peliculaFiltrada = peliculas.Where(x => x.Id == copiaFiltrada[0].IdPelicula).ToList();

                        temp.peliculaTitulo = peliculaFiltrada[0].Titulo;
                        temp.formatoPelicula = copiaFiltrada[0].Formato;

                        historialAlquileres.Add(temp);
                    }
                    break;
                case "activo":
                    foreach(var alquiler in alquileres)
                    {
                        HistorialAlquiler temp = new HistorialAlquiler();

                        if (!alquiler.FechaEntregada.HasValue)
                        {
                            Copia copiaFiltrada = copias.Where(x => x.Id == alquiler.IdCopia).ToList()[0];
                            Pelicula peliculaFiltrada = peliculas.Where(x => x.Id == copiaFiltrada.IdPelicula).ToList()[0];

                            temp.idAlquiler = alquiler.Id;
                            temp.fechaAlquilada = alquiler.FechaAlquiler;
                            temp.fechaTope = alquiler.FechaTope;
                            temp.fechaEntrega = alquiler.FechaEntregada;
                            temp.nombreCliente = clientes.Where(x => x.Id == alquiler.IdCliente).ToList()[0].Nombre;
                            temp.clienteTelefono = clientes.Where(x => x.Id == alquiler.IdCliente).ToList()[0].Telefono;
                            temp.peliculaTitulo = peliculaFiltrada.Titulo;
                            temp.formatoPelicula = copiaFiltrada.Formato;

                            historialAlquileres.Add(temp);
                        }
                    }
                    break;
                case "excedido":
                    foreach (var alquiler in alquileres)
                    {
                        HistorialAlquiler temp = new HistorialAlquiler();

                        if (DateTime.Now >= alquiler.FechaTope && !alquiler.FechaEntregada.HasValue)
                        {
                            Copia copiaFiltrada = copias.Where(x => x.Id == alquiler.IdCopia).ToList()[0];
                            Pelicula peliculaFiltrada = peliculas.Where(x => x.Id == copiaFiltrada.IdPelicula).ToList()[0];

                            temp.idAlquiler = alquiler.Id;
                            temp.fechaAlquilada = alquiler.FechaAlquiler;
                            temp.fechaTope = alquiler.FechaTope;
                            temp.fechaEntrega = alquiler.FechaEntregada;
                            temp.nombreCliente = clientes.Where(x => x.Id == alquiler.IdCliente).ToList()[0].Nombre;
                            temp.clienteTelefono = clientes.Where(x => x.Id == alquiler.IdCliente).ToList()[0].Telefono;
                            temp.peliculaTitulo = peliculaFiltrada.Titulo;
                            temp.formatoPelicula = copiaFiltrada.Formato;

                            historialAlquileres.Add(temp);
                        }
                    }
                    break;
                case "cliente":
                    if (Request.Form["filtroCliente"].ToString() != null)
                    {
                        this.filtroCliente = Request.Form["filtroCliente"];

                        foreach (var alquiler in alquileres)
                        {
                            HistorialAlquiler temp = new HistorialAlquiler();

                            if (filtroCliente == "ninguno")
                            {
                                Copia copiaFiltrada = copias.Where(x => x.Id == alquiler.IdCopia).ToList()[0];
                                Pelicula peliculaFiltrada = peliculas.Where(x => x.Id == copiaFiltrada.IdPelicula).ToList()[0];

                                temp.idAlquiler = alquiler.Id;
                                temp.fechaAlquilada = alquiler.FechaAlquiler;
                                temp.fechaTope = alquiler.FechaTope;
                                temp.fechaEntrega = alquiler.FechaEntregada;
                                temp.nombreCliente = clientes.Where(x => x.Id == alquiler.IdCliente).ToList()[0].Nombre;
                                temp.clienteTelefono = clientes.Where(x => x.Id == alquiler.IdCliente).ToList()[0].Telefono;
                                temp.peliculaTitulo = peliculaFiltrada.Titulo;
                                temp.formatoPelicula = copiaFiltrada.Formato;

                                historialAlquileres.Add(temp);
                            }
                            else
                            {
                                if (alquiler.IdCliente.ToString() == filtroCliente)
                                {
                                    Copia copiaFiltrada = copias.Where(x => x.Id == alquiler.IdCopia).ToList()[0];
                                    Pelicula peliculaFiltrada = peliculas.Where(x => x.Id == copiaFiltrada.IdPelicula).ToList()[0];

                                    temp.idAlquiler = alquiler.Id;
                                    temp.fechaAlquilada = alquiler.FechaAlquiler;
                                    temp.fechaTope = alquiler.FechaTope;
                                    temp.fechaEntrega = alquiler.FechaEntregada;
                                    temp.nombreCliente = clientes.Where(x => x.Id == alquiler.IdCliente).ToList()[0].Nombre;
                                    temp.clienteTelefono = clientes.Where(x => x.Id == alquiler.IdCliente).ToList()[0].Telefono;
                                    temp.peliculaTitulo = peliculaFiltrada.Titulo;
                                    temp.formatoPelicula = copiaFiltrada.Formato;

                                    historialAlquileres.Add(temp);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < alquileres.Count; i++)
                        {
                            HistorialAlquiler temp = new HistorialAlquiler();

                            temp.idAlquiler = alquileres[i].Id;
                            temp.fechaAlquilada = alquileres[i].FechaAlquiler;
                            temp.fechaTope = alquileres[i].FechaTope;
                            temp.fechaEntrega = alquileres[i].FechaEntregada;

                            temp.nombreCliente = clientes.Where(x => x.Id == alquileres[i].IdCliente).ToList()[0].Nombre;
                            temp.clienteTelefono = clientes.Where(x => x.Id == alquileres[i].IdCliente).ToList()[0].Telefono;

                            List<Copia> copiaFiltrada = copias.Where(x => x.Id == alquileres[i].IdCopia).ToList();
                            List<Pelicula> peliculaFiltrada = peliculas.Where(x => x.Id == copiaFiltrada[0].IdPelicula).ToList();

                            temp.peliculaTitulo = peliculaFiltrada[0].Titulo;
                            temp.formatoPelicula = copiaFiltrada[0].Formato;

                            historialAlquileres.Add(temp);
                        }
                    }
                    break;
            }
            return historialAlquileres;
        }

        [HttpPost]
        public ActionResult SeleccionarFiltro(string filtro)
        {
            this.filtroHistorial = filtro;
            ViewBag.Historial = setHistorial();
            ViewBag.Clientes = new List<Cliente>(_context.Clientes);
            return View("HistorialAlquileres");
        }

        public IActionResult HistorialAlquileres()
        {
            ViewBag.Historial = setHistorial();
            ViewBag.Clientes = new List<Cliente>(_context.Clientes);
            return View();
        }

        public IActionResult HistorialAlquileresDeUnCliente()
        {
            return View();
        }
    }
}
