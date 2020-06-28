using Microsoft.EntityFrameworkCore;
using RPrestamosDetalle.DAL;
using RPrestamosDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RPrestamosDetalle.BLL
{
    class MorasBLL
    {
        public static bool Guardar(Moras mora)
        {
            if (!Existe(mora.MoraId))
            {
                return Insertar(mora);
            }
            else
            {
                return Modificar(mora);
            }

        }

        private static bool Insertar(Moras mora)
        {
            bool ok = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Moras.Add(mora);
                ok = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return ok;
        }

        private static bool Modificar(Moras mora)
        {
            bool ok = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM MorasDetalle Where moraId={mora.MoraId}");

                foreach (var item in mora.Detalles)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(mora).State = EntityState.Modified;
                ok = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return ok;
        }

        public static bool Eliminar(int id)
        {
            bool ok = false;
            Contexto contexto = new Contexto();

            try
            {
                var mora = MorasBLL.Buscar(id);

                if (mora != null)
                {
                    contexto.Moras.Remove(mora);
                    ok = contexto.SaveChanges() > 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return ok;
        }

        public static Moras Buscar(int id)
        {
            Moras mora = new Moras();
            Contexto contexto = new Contexto();

            try
            {
                mora = contexto.Moras.Include(x => x.Detalles).Where(x => x.MoraId == id).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return mora;
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Moras.Any(e => e.MoraId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return ok;
        }

        public static List<Moras> GetList(Expression<Func<Moras, bool>> criterio)
        {
            List<Moras> Lista = new List<Moras>();
            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Moras.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
    }
}
