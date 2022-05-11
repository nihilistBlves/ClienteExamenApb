using ClienteExamenApb.Models;
using ClienteExamenApb.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteExamenApb.Controllers {
    public class EventosController : Controller {
        private ServiceApiEventos service;
        public EventosController(ServiceApiEventos service) {
            this.service = service;
        }
        public async Task<IActionResult> Categorias() {
            return View(await this.service.GetCategoriasAsync());
        }
        public async Task<IActionResult> Eventos() {
            return View(await this.service.GetEventosAsync());
        }
        public async Task<IActionResult> EventosByCategoria(int idcategoria) {
            ViewData["CATEGORIA"] = await this.service.FindCategoriaAsync(idcategoria);
            return View(await this.service.GetEventosByCategoriaAsync(idcategoria));
        }
        public async Task<IActionResult> DetailsEvento(int id) {
            Evento evento = await this.service.FindEventoAsync(id);
            ViewData["CATEGORIA"] = await this.service.FindCategoriaAsync(evento.IdCategoria);
            return View(evento);
        }
        [HttpGet]
        public async Task<IActionResult> CreateEvento() {
            ViewData["CATEGORIAS"] = await this.service.GetCategoriasAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvento(Evento evento) {
            await this.service.CreateEventoAsync(evento);
            return RedirectToAction("EventosByCategoria", new { idcategoria = evento.IdCategoria });
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEventoCategoria(int idevento) {
            Evento evento = await this.service.FindEventoAsync(idevento);
            List<Categoria> categorias = await this.service.GetCategoriasAsync();
            ViewData["CATEGORIAS"] = categorias;
            return View(evento);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEventoCategoria(int idevento, int idcategoria) {
            Evento evento = await this.service.FindEventoAsync(idevento);
            evento.IdCategoria = idcategoria;
            await this.service.UpdateEventoCategoriaAsync(evento);
            return RedirectToAction("DetailsEvento", new { id = idevento });
        }
        public async Task<IActionResult> DeleteEvento(int id) {
            await this.service.DeleteEventoAsync(id);
            return RedirectToAction("Eventos");
        }
    }
}
