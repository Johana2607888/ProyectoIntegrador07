﻿using System.Web;
using System.Web.Mvc;

namespace Proyecto_Integrador
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new Filters.VerificaSession());
        }
    }
}
