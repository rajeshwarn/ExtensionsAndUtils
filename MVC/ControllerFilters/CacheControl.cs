using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shared.Filter
{
    public class NoCache : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }

    public class RemoveTempData : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            /*neste projeto são usados TempDatas para armazenar e preencher as comboboxes existentes ao longo da aplicação pois os TempDatas têm um ciclo de vida mais longo
            *contudo os TempDatas também são passados de controller para controller
            *para evitar diversas chamadas ao serviço para preencher os TempDatas com os dados que se pretendem para as views, está-se a verificar se esses TempDatas existem (estão a null) ou não, voltando a preenche-los caso não existam
            *Contudo nem sempre queremos que a vida destes TempDatas se mantenha (é o caso da seleção de uma nova vacina para o PNV, combobox essa que não pode possuir vacinas que já existam no PNV),
            e é essa a finalidade deste filtro
            *no entanto deve-se ter em atenção determinados TempDatas que se pretende que se mantenham. É o caso dos TempDatas usados para enviar mensagens informativas ou de aviso para as views.
            *assim sendo deve-se ter o cuidado de se colocar este filtro apenas nas actions certas*/

            ICollection<string> keys = new Collection<string>(filterContext.Controller.TempData.Keys.ToList());
            foreach (string key in keys)
            {
                filterContext.Controller.TempData.Remove(key);
            }

            base.OnActionExecuting(filterContext);
        }
    } 
}