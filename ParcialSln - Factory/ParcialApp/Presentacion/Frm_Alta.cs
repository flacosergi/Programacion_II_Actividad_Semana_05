
using ParcialApp.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParcialApp.Dominio;
using ParcialApp.Acceso_a_datos;

namespace ParcialApp.Presentacion
{
    public partial class Frm_Alta : Form
    {

        GestorOrden gestor_orden;
        GestorMaterial gestor_material;

        public Frm_Alta(GestorFactory factoria)
        {
            InitializeComponent();
            gestor_orden = factoria.CrearGestorOrden();
            gestor_material = factoria.CrearGestorMaterial();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtResp.Text == string.Empty)
            {
                MessageBox.Show("Ingrese un responsable.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtResp.Focus();
                return;
            }

            if (dgvDetalles.Rows.Count == 0)
            {
                MessageBox.Show("Ingrese al menos un detalle.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboMateriales.Focus();
                return;
            }

            Orden_Retiro nuevaorden = new Orden_Retiro();
            nuevaorden.responsable = txtResp.Text;
            foreach (DataGridViewRow Fila in dgvDetalles.Rows)
            {
                Detalle_Orden nuevo_detalle = new Detalle_Orden();
                nuevo_detalle.detalle_nro = Fila.Index + 1;
                nuevo_detalle.cantidad = Int32.Parse(Fila.Cells["cantidad"].Value.ToString());
                nuevo_detalle.material.codigo = Int32.Parse(Fila.Cells["cod_material"].Value.ToString());
                nuevaorden.lista_detalle.Add(nuevo_detalle);
            }
            int resultado = gestor_orden.CargarOrden(nuevaorden);
            if (resultado != 0)
                MessageBox.Show("La Orden fue cargada con el número " + resultado + ".", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Hubo un error al cargar la orden.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtResp.Text = string.Empty;
            cboMateriales.SelectedIndex = -1;
            nudCantidad.Value = 1;
            dgvDetalles.Rows.Clear();
            txtResp.Focus();


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
            else
            {
                return;
            }
        }

        private void Frm_Alta_Presupuesto_Load(object sender, EventArgs e)
        {
            CargarCombo(cboMateriales);
        }

        private void CargarCombo(ComboBox combo)
        {
            combo.DataSource = gestor_material.CargarMateriales();
            combo.DisplayMember = "nombre";
            combo.ValueMember = "codigo";
            combo.SelectedIndex = -1;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboMateriales.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un material.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboMateriales.Focus();
                return;
            }

            foreach (DataGridViewRow Fila in dgvDetalles.Rows)
            {
                if ((int)Fila.Cells["cod_material"].Value == (int)cboMateriales.SelectedValue)
                {
                    MessageBox.Show("Ya ha cargado este material.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboMateriales.Focus();
                    return;
                }
            }

            Material seleccionado = (Material)cboMateriales.SelectedItem;
            if (seleccionado.stock < Convert.ToDecimal(nudCantidad.Value))
            {
                MessageBox.Show("El stock es insuficiente para esta cantidad de material.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                nudCantidad.Focus();
                return;
            }

            dgvDetalles.Rows.Add(seleccionado.codigo, seleccionado.nombre, seleccionado.stock, nudCantidad.Value);
            cboMateriales.SelectedIndex = -1;
            nudCantidad.Value = 1;

        }





        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == dgvDetalles.Columns["acciones"].Index)
            {
                dgvDetalles.Rows.RemoveAt(e.RowIndex);
            }
        }
    }
}
