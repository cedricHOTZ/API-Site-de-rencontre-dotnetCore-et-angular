﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatingApp.API.Controllers
{
   //jeton valide pour voir toutes les valeurs
     [Authorize]
   
    [Route("api/[controller]")]
     [ApiController]
    public class ValuesController : ControllerBase
    {
     private readonly DataContext _context;
     private readonly ILogger<ValuesController> _logger;

       public ValuesController(DataContext context,ILogger<ValuesController> logger ) {
         
         _context = context;
          _logger = logger;
        
     }
        
      [AllowAnonymous]
        // GET api/values (toute les valeurs)
        [HttpGet]
        public async Task <IActionResult> GetValues()
        {
           var values = await _context.Values.ToListAsync();
           return Ok(values);
        }

        //pas besoin d'hautenfication avec jeton
[AllowAnonymous]
          // GET api/values/5 (valeur en fonction de l'id)
        [HttpGet("{id}")]
        public async Task <IActionResult> GetValues(int id)
        {
           var values = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
           return Ok(values);
        }
    }
}
