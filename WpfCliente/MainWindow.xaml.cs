using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Forncecedor_WebAPI.Models;

namespace WpfCliente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void gridFornecedores_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            FORNECEDOR objFornecedor = (FORNECEDOR)gridFornecedores.SelectedItem;

            if (objFornecedor != null)
            {
                txtID.Text = objFornecedor.ID_FORNECEDOR.ToString();
                txtNome.Text = objFornecedor.NOME;
                txtCNPJ.Text = objFornecedor.CNPJ;
                txtEndereco.Text = objFornecedor.ENDERECO;
                txtBairro.Text = objFornecedor.BAIRRO;
                txtCidade.Text = objFornecedor.CIDADE;
                ckbSituacao.IsChecked = objFornecedor.SITUACAO == "A" ? true : false;
                txtDataCadastro.Text = objFornecedor.DATA_CADASTRO.ToString();
            }
            
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpCliente = new HttpClient();
            httpCliente.BaseAddress = new Uri("http://localhost:46731/");
            httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            FORNECEDOR fornecedor = new FORNECEDOR();

            fornecedor.NOME = txtNome.Text;
            fornecedor.CNPJ = txtCNPJ.Text;
            fornecedor.BAIRRO = txtBairro.Text;
            fornecedor.CIDADE = txtCidade.Text;
            fornecedor.DATA_CADASTRO = DateTime.Now;
            fornecedor.SITUACAO = ckbSituacao.IsChecked == true ? "A" : "I";
            fornecedor.ENDERECO = txtEndereco.Text;

            var response = httpCliente.PostAsJsonAsync("servicowebapi/fornecedor/incluir", fornecedor);

            ListarFornecedores();
            LimpaCampos();
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            if (txtID.Text != String.Empty)
            {
                HttpClient httpCliente = new HttpClient();
                httpCliente.BaseAddress = new Uri("http://localhost:46731/");
                httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                FORNECEDOR fornecedor = new FORNECEDOR();
                fornecedor.ID_FORNECEDOR = Int32.Parse(txtID.Text);
                fornecedor.NOME = txtNome.Text;
                fornecedor.CNPJ = txtCNPJ.Text;
                fornecedor.ENDERECO = txtEndereco.Text;
                fornecedor.BAIRRO = txtBairro.Text;
                fornecedor.CIDADE = txtCidade.Text;
                fornecedor.SITUACAO = ckbSituacao.IsChecked == true ? "A" : "I";
                fornecedor.DATA_CADASTRO = DateTime.Now;

                var response = httpCliente.PostAsJsonAsync("servicowebapi/fornecedor/alterar", fornecedor);
                ListarFornecedores();
                LimpaCampos();
            }else
            {
                MessageBox.Show("Favor selecionar um cliente");
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpCliente = new HttpClient();
            httpCliente.BaseAddress = new Uri("http://localhost:46731/");

            string id = txtID.Text;

            string url = "servicowebapi/fornecedor/excluir?id=" + id;

            HttpResponseMessage response = httpCliente.GetAsync(url).Result;
            LimpaCampos();
            ListarFornecedores();
        }

        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            ListarFornecedores();
        }

        private void ListarFornecedores()
        {
            HttpClient httpCliente = new HttpClient();
            httpCliente.BaseAddress = new Uri("http://localhost:46731");
            httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpCliente.GetAsync("servicoWebApi/fornecedor/listagem").Result;
            var listaFornecedores = response.Content.ReadAsAsync<IEnumerable<FORNECEDOR>>().Result;
            gridFornecedores.ItemsSource = listaFornecedores;

        }

        private void LimpaCampos()
        {
            txtID.Text = "";
            txtNome.Text = "";
            txtCNPJ.Text = "";
            txtEndereco.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            ckbSituacao.IsChecked = false;
            txtDataCadastro.Text = "";
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpCliente = new HttpClient();
            httpCliente.BaseAddress = new Uri("http://localhost:46731/");
            httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string id = txtID.Text;

            HttpResponseMessage response = httpCliente.GetAsync("servicowebapi/fornecedor/busca?id = " + id).Result;

            var fornecedor = response.Content.ReadAsAsync<FORNECEDOR>().Result;
            List<FORNECEDOR> lista = new List<FORNECEDOR>();
            lista.Add(fornecedor);
            gridFornecedores.ItemsSource = lista;

            LimpaCampos();
        }
    }
}
