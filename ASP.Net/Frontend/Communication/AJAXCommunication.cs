using Frontend.Framework;
using BLL;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontend.Communication
{
    /// <summary>
    /// Objecto proxy que recebe os pedidos de AJAX depois de reconhecidos pelo server-side
    /// Todos os métodos devem sempre retornar um objecto complexo
    /// </summary>
    public class AJAXCommunication
    {
        public MyViewModel MyModule_GetSomething(string someData)
        {
            var model = new MyViewModel(WebAppUtils.GetNecessaryData(someData));

            try
            {
                model.GetSomething();
                model.ResultOperation = ResultEnum.Sucess.ToString();
            }
            catch
            {
                model.ResultOperation = ResultEnum.Error.ToString();
            }

            return model;
        }

    }
}
