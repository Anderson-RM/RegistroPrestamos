using RPrestamosDetalle.BLL;
using RPrestamosDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RPrestamosDetalle.UI.Registros
{
    /// <summary>
    /// Interaction logic for rMora.xaml
    /// </summary>
    public partial class rMora : Window
    {
        Moras mora = new Moras();
        public rMora()
        {
            InitializeComponent();
            this.DataContext = mora;
        }

        private void Refresh()
        {
            this.DataContext = null;
            this.DataContext = mora;
        }

        private void Limpiar()
        {
            this.mora = new Moras();
            this.DataContext = mora;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Moras encontrado = MorasBLL.Buscar(mora.MoraId);

            if (encontrado != null)
            {
                mora = encontrado;
                Refresh();
                MessageBox.Show("Mora encontrada", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Limpiar();
                MessageBox.Show("Mora no existe en la base de datos", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            mora.Detalles.Add(new MorasDetalle(mora.MoraId, int.Parse(PrestamoIdTextBox.Text), Double.Parse(ValorTextBox.Text)));

            Refresh();

            PrestamoIdTextBox.Clear();
            ValorTextBox.Clear();
        }

        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                mora.Detalles.RemoveAt(DetalleDataGrid.SelectedIndex);
                Refresh();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool ok = false;
            if (mora.MoraId == 0)
            {
                ok = MorasBLL.Guardar(mora);
            }
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    ok = MorasBLL.Guardar(mora);
                }
                else
                {
                    MessageBox.Show("No existe en la base de datos", "Error");
                }
            }

            if (ok)
            {
                Limpiar();
                MessageBox.Show("Se ha guardado.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Funcion Fallida.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Moras existe = MorasBLL.Buscar(mora.MoraId);

            if (existe == null)
            {
                MessageBox.Show("No existe en la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                MorasBLL.Eliminar(mora.MoraId);
                MessageBox.Show("Se ha eliminado.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Moras esValido = MorasBLL.Buscar(mora.MoraId);

            return esValido != null;
        }

        public bool Validar()
        {
            if (MoraIdTextBox.Text.Length == 0 || PrestamoIdTextBox.Text.Length == 0 ||
                PrestamoIdTextBox.Text.Length == 0 || ValorTextBox.Text.Length == 0 || MoraIdTextBox.Text.Length == 0)
            {
                MessageBox.Show("Favor llenar el campo.", "Obligatorio.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }


            if (!Regex.IsMatch(MoraIdTextBox.Text, "^[0-9]+$"))
            {
                MessageBox.Show("Id Solo permite un digito del 0 - 9.", "Id.", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Regex.IsMatch(PrestamoIdTextBox.Text, "^[0-9]+$"))
            {
                MessageBox.Show("Id Solo permite un digito del 0 - 9.", "MoraId.", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Regex.IsMatch(PrestamoIdTextBox.Text, "^[0-9]+$"))
            {
                MessageBox.Show("Id Solo permite un digito del 0 - 9.", "PrestamoId.", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Regex.IsMatch(ValorTextBox.Text, "^[0-9]+$"))
            {
                MessageBox.Show("Solo permite un digito del 0 - 9.", "Valor.", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
