﻿using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cocktail_Magician_Services
{
    public class BarManager : IBarManager
    {
        private readonly CMContext _context;

        public BarManager(CMContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Bar> CreateBar(string name, string address, double rating, string picture)
        {
            var barToAdd = new Bar();
            try
            {
                barToAdd = new Bar(name, address, rating, picture);
            }
            catch
            {
                throw new ArgumentException();
            }

            var barToFind = this._context.Bars.SingleOrDefault(bar => bar.Name == barToAdd.Name && bar.Address == barToAdd.Address);

            if (barToFind != null)
            {
                throw new InvalidOperationException("Book already exists in the catalogue");
            }

            await this._context.Bars.AddAsync(barToAdd);
            await this._context.SaveChangesAsync();
            return barToAdd;
        }

        public async Task RemoveBar(string id)
        {
            Bar barToRemove = await this.GetBar(id);
            barToRemove.IsDeleted = true;
            this._context.Bars.Update(barToRemove);
            await this._context.SaveChangesAsync();
        }

        public async Task<Bar> GetBar(string id)
        {
            var bar = await this._context.Bars.Where(b => !b.IsDeleted).FirstOrDefaultAsync(b => b.BarId == id);

            return bar;
        }
    }
}