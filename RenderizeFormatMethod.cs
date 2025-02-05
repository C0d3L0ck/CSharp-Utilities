using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;

namespace RetentionDesign
{
    class RenderizeFormatMethod
    {
        private async void RenderizeInWeb(byte[] bytes, string format)
        {
            string base64 = Convert.ToBase64String(bytes);

            string filePath = Path.Combine(Path.GetTempPath(), $"temp.{format.ToLower()}"); // PDF OR XLS 

            File.WriteAllBytes(filePath, bytes);

            if (File.Exists(filePath))
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(filePath)
                    {
                        UseShellExecute = true
                    };
                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    ContentDialog content = new()
                    {
                       // XamlRoot = this.XamlRoot,
                        Title = "Error",
                        Content = "No se pudo abrir el archivo. Error: " + ex.Message,
                        CloseButtonText = "Cerrar"
                    };
                    content.ShowAsync();
                }
            }
            else
            {
                ContentDialog content = new()
                {
                   // XamlRoot = this.XamlRoot,
                    Title = "Error",
                    Content = "El archivo no existe: " + filePath,
                    CloseButtonText = "Cerrar"
                };
                content.ShowAsync();
            }
        }
    }
}
