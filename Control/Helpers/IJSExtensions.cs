using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Control.Helpers
{
    public static class IJSExtensions
    {
        public static async Task<object> SaveAs( this IJSRuntime js, string nameFile, byte[] file)
        {
            return await js.InvokeAsync<object>("saveAsFile", nameFile, Convert.ToBase64String(file));
        }
    }
}
