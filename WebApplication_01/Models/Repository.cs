﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication_01.Utility;

namespace WebApplication_01.Models
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly UygulamaDbContext _uygulamaDbContext;
		internal DbSet<T> dbSet; // dbset == _uygulamadbcontext.kitapturleri
		public Repository(UygulamaDbContext uygulamaDbContext)
		{
			_uygulamaDbContext = uygulamaDbContext;
			this.dbSet = _uygulamaDbContext.Set<T>();
			//id den foreign key veri adı getirme
			_uygulamaDbContext.Kitaplar.Include(k => k.KitapTuru).Include(k => k.KitapTuruId);
		}
		public void Ekle(T entity)
		{
			dbSet.Add(entity);
		}

		public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null)
		{
			IQueryable<T> sorgu = dbSet;
			sorgu = sorgu.Where(filtre);
			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					sorgu = sorgu.Include(includeProp);
				}

			}
			return sorgu.FirstOrDefault();
		}

		public IEnumerable<T> GetAll(string? includeProps = null)
		{
			IQueryable<T> sorgu = dbSet;
			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach(var includeProp in includeProps.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries)) 
				{
					sorgu= sorgu.Include(includeProp);
				}

			}
			return sorgu.ToList();
		}

		public void Sil(T entity)
		{
			dbSet.Remove(entity);	
		}

		public void SilAralik(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}
	}
}
