using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RastreioFacil.Service;
using RastreioFacil.Domain.Interfaces.Services;
using RastreioFacil.Domain.Entities;
using RastreioFacil.Library.Web;
using RastreioFacil.Web.Models;
using RastreioFacil.Domain.DTO;
using System.Security.Claims;
using AutoMapper;
using Microsoft.Practices.Unity; 

namespace RastreioFacil.Web.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IClienteServices iClienteServices;

        public DefaultController(IClienteServices iClienteServices)
        {
            this.iClienteServices = iClienteServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        public FileResult GerarBoleto(string hash)
        {
            Boleto boleto = new Boleto(1);

            var dto = Mapper.Map<Cliente, ClienteModels>(iClienteServices.GetCliente(Convert.ToInt32(SecurityDb.Decrypt(hash))));

            return File(boleto.BancodoBrasilPDF(dto), "application/pdf");
        }
    }
}