using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAPI1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Crud : ContentPage
    {
        string apiUrl = "https://agendarcitasapiperezjairo2.azurewebsites.net/api/Clientes";
        public Crud()
        {
            InitializeComponent();
        }

        private async void cmdInsert_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (var webClient = new HttpClient())
                {
                    webClient.BaseAddress = new Uri(apiUrl);
                    webClient
                        .DefaultRequestHeaders
                        .Accept
                        .Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(new Cliente
                    {
                        cedula = txtCedula.Text,
                        nombre = txtNombre.Text,
                        direccion = txtDireccion.Text,
                        telefono = txtTelefono.Text,
                        Citaid = int.Parse(txtCitaFecha.Text)
                    });

                    var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                    var resp = webClient.SendAsync(request);
                    resp.Wait();

                    json = resp.Result.Content.ReadAsStringAsync().Result;
                    var prod = JsonConvert.DeserializeObject<Cliente>(json);

                    txtCedula.Text = prod.cedula;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error: {ex.Message}";
            }
        }

        private async void cmdUpdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{apiUrl}/{txtCedula.Text}";
                    client.BaseAddress = new Uri(url);
                    client
                        .DefaultRequestHeaders
                        .Accept
                        .Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var json = JsonConvert.SerializeObject(new Cliente
                    {
                        cedula = txtCedula.Text,
                        nombre = txtNombre.Text,
                        direccion = txtDireccion.Text,
                        telefono = txtTelefono.Text,
                        Citaid = int.Parse(txtCitaFecha.Text)
                    });

                    var rqst = new HttpRequestMessage(HttpMethod.Put, url);
                    rqst.Content = new StringContent(json, Encoding.UTF8, "application/json");

                    var resp = client.SendAsync(rqst);
                    resp.Wait();

                    //json = resp.Result.Content.ReadAsStringAsync().Result;
                    //var prod = JsonConvert.DeserializeObject<Producto>(json);
                }


            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error: {ex.Message}";
            }
        }

        private async void cmdReadOne_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (var webClient = new HttpClient())
                {
                    var resp = webClient.GetStringAsync(apiUrl + "/" + txtCedula.Text);
                    resp.Wait();

                    var json = resp.Result;
                    var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<Cliente>(json);

                    txtCedula.Text = prod.cedula;
                    txtNombre.Text = prod.nombre;
                    txtDireccion.Text = prod.direccion;
                    txtTelefono.Text = prod.telefono;
                    txtCitaFecha.Text = prod.Citaid.ToString();
                    
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error: {ex.Message}";
            }
        }

        private async void cmdDelete_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{apiUrl}/{txtCedula.Text}";
                    client.BaseAddress = new Uri(url);
                    client
                        .DefaultRequestHeaders
                        .Accept
                        .Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var resp = client.DeleteAsync(url);
                    resp.Wait();

                    txtCedula.Text = "";
                    txtNombre.Text = "";
                    txtDireccion.Text = string.Empty;
                    txtTelefono.Text = string.Empty;
                    txtCitaFecha.Text = string.Empty;
                   
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error: {ex.Message}";
            }
        }

    }
}