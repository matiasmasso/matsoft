<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PrRevista
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_ImageLogo = New Mat.NET.Xl_Image()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGeneral = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.NumericUpDownSang = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NumericUpDownPageHeight = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownPageWidth = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.TabPageNumeros = New System.Windows.Forms.TabPage()
        Me.TabPageTarifas = New System.Windows.Forms.TabPage()
        Me.NumericUpDownYeaTarifas = New System.Windows.Forms.NumericUpDown()
        Me.DataGridViewTarifas = New System.Windows.Forms.DataGridView()
        Me.TabPageOrdresDeCompra = New System.Windows.Forms.TabPage()
        Me.Xl_PrOrdresDeCompra1 = New Mat.NET.Xl_PrOrdresDeCompra()
        Me.TabPageInsercions = New System.Windows.Forms.TabPage()
        Me.Xl_PrInsercions1 = New Mat.NET.Xl_PrInsercions()
        Me.Xl_PrNumeros1 = New Mat.NET.Xl_PrNumeros()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGeneral.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumericUpDownSang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownPageHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownPageWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageNumeros.SuspendLayout()
        Me.TabPageTarifas.SuspendLayout()
        CType(Me.NumericUpDownYeaTarifas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewTarifas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageOrdresDeCompra.SuspendLayout()
        Me.TabPageInsercions.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 465)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(500, 31)
        Me.Panel1.TabIndex = 45
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(281, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(392, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Xl_ImageLogo
        '
        Me.Xl_ImageLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ImageLogo.Bitmap = Nothing
        Me.Xl_ImageLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ImageLogo.EmptyImageLabelText = ""
        Me.Xl_ImageLogo.Location = New System.Drawing.Point(346, 3)
        Me.Xl_ImageLogo.MaxHeight = 0
        Me.Xl_ImageLogo.MaxWidth = 0
        Me.Xl_ImageLogo.Name = "Xl_ImageLogo"
        Me.Xl_ImageLogo.Size = New System.Drawing.Size(150, 48)
        Me.Xl_ImageLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
        Me.Xl_ImageLogo.TabIndex = 46
        Me.Xl_ImageLogo.ZipStream = Nothing
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGeneral)
        Me.TabControl1.Controls.Add(Me.TabPageNumeros)
        Me.TabControl1.Controls.Add(Me.TabPageTarifas)
        Me.TabControl1.Controls.Add(Me.TabPageOrdresDeCompra)
        Me.TabControl1.Controls.Add(Me.TabPageInsercions)
        Me.TabControl1.Location = New System.Drawing.Point(6, 57)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(490, 406)
        Me.TabControl1.TabIndex = 47
        '
        'TabPageGeneral
        '
        Me.TabPageGeneral.Controls.Add(Me.GroupBox1)
        Me.TabPageGeneral.Controls.Add(Me.TextBoxNom)
        Me.TabPageGeneral.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGeneral.Name = "TabPageGeneral"
        Me.TabPageGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGeneral.Size = New System.Drawing.Size(482, 380)
        Me.TabPageGeneral.TabIndex = 0
        Me.TabPageGeneral.Text = "General"
        Me.TabPageGeneral.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.NumericUpDownSang)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.NumericUpDownPageHeight)
        Me.GroupBox1.Controls.Add(Me.NumericUpDownPageWidth)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(214, 76)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(252, 126)
        Me.GroupBox1.TabIndex = 47
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "mides de página:"
        '
        'NumericUpDownSang
        '
        Me.NumericUpDownSang.Location = New System.Drawing.Point(184, 82)
        Me.NumericUpDownSang.Name = "NumericUpDownSang"
        Me.NumericUpDownSang.Size = New System.Drawing.Size(62, 20)
        Me.NumericUpDownSang.TabIndex = 5
        Me.NumericUpDownSang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(87, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "sang (mm):"
        '
        'NumericUpDownPageHeight
        '
        Me.NumericUpDownPageHeight.Location = New System.Drawing.Point(184, 56)
        Me.NumericUpDownPageHeight.Maximum = New Decimal(New Integer() {900, 0, 0, 0})
        Me.NumericUpDownPageHeight.Name = "NumericUpDownPageHeight"
        Me.NumericUpDownPageHeight.Size = New System.Drawing.Size(62, 20)
        Me.NumericUpDownPageHeight.TabIndex = 3
        Me.NumericUpDownPageHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'NumericUpDownPageWidth
        '
        Me.NumericUpDownPageWidth.Location = New System.Drawing.Point(184, 33)
        Me.NumericUpDownPageWidth.Maximum = New Decimal(New Integer() {900, 0, 0, 0})
        Me.NumericUpDownPageWidth.Name = "NumericUpDownPageWidth"
        Me.NumericUpDownPageWidth.Size = New System.Drawing.Size(62, 20)
        Me.NumericUpDownPageWidth.TabIndex = 2
        Me.NumericUpDownPageWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(87, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "alçada (mm):"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(87, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "amplada (mm):"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(16, 26)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(450, 20)
        Me.TextBoxNom.TabIndex = 0
        '
        'TabPageNumeros
        '
        Me.TabPageNumeros.Controls.Add(Me.Xl_PrNumeros1)
        Me.TabPageNumeros.Location = New System.Drawing.Point(4, 22)
        Me.TabPageNumeros.Name = "TabPageNumeros"
        Me.TabPageNumeros.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageNumeros.Size = New System.Drawing.Size(482, 380)
        Me.TabPageNumeros.TabIndex = 1
        Me.TabPageNumeros.Text = "Numeros"
        Me.TabPageNumeros.UseVisualStyleBackColor = True
        '
        'TabPageTarifas
        '
        Me.TabPageTarifas.Controls.Add(Me.NumericUpDownYeaTarifas)
        Me.TabPageTarifas.Controls.Add(Me.DataGridViewTarifas)
        Me.TabPageTarifas.Location = New System.Drawing.Point(4, 22)
        Me.TabPageTarifas.Name = "TabPageTarifas"
        Me.TabPageTarifas.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTarifas.Size = New System.Drawing.Size(482, 380)
        Me.TabPageTarifas.TabIndex = 3
        Me.TabPageTarifas.Text = "Tarifas"
        Me.TabPageTarifas.UseVisualStyleBackColor = True
        '
        'NumericUpDownYeaTarifas
        '
        Me.NumericUpDownYeaTarifas.Location = New System.Drawing.Point(276, 3)
        Me.NumericUpDownYeaTarifas.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        Me.NumericUpDownYeaTarifas.Minimum = New Decimal(New Integer() {1985, 0, 0, 0})
        Me.NumericUpDownYeaTarifas.Name = "NumericUpDownYeaTarifas"
        Me.NumericUpDownYeaTarifas.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDownYeaTarifas.TabIndex = 2
        Me.NumericUpDownYeaTarifas.Value = New Decimal(New Integer() {1985, 0, 0, 0})
        '
        'DataGridViewTarifas
        '
        Me.DataGridViewTarifas.AllowUserToAddRows = False
        Me.DataGridViewTarifas.AllowUserToDeleteRows = False
        Me.DataGridViewTarifas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewTarifas.Location = New System.Drawing.Point(3, 27)
        Me.DataGridViewTarifas.Name = "DataGridViewTarifas"
        Me.DataGridViewTarifas.ReadOnly = True
        Me.DataGridViewTarifas.Size = New System.Drawing.Size(330, 284)
        Me.DataGridViewTarifas.TabIndex = 1
        '
        'TabPageOrdresDeCompra
        '
        Me.TabPageOrdresDeCompra.Controls.Add(Me.Xl_PrOrdresDeCompra1)
        Me.TabPageOrdresDeCompra.Location = New System.Drawing.Point(4, 22)
        Me.TabPageOrdresDeCompra.Name = "TabPageOrdresDeCompra"
        Me.TabPageOrdresDeCompra.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageOrdresDeCompra.Size = New System.Drawing.Size(482, 380)
        Me.TabPageOrdresDeCompra.TabIndex = 2
        Me.TabPageOrdresDeCompra.Text = "Ordres de compra"
        Me.TabPageOrdresDeCompra.UseVisualStyleBackColor = True
        '
        'Xl_PrOrdresDeCompra1
        '
        Me.Xl_PrOrdresDeCompra1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PrOrdresDeCompra1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_PrOrdresDeCompra1.Name = "Xl_PrOrdresDeCompra1"
        Me.Xl_PrOrdresDeCompra1.Size = New System.Drawing.Size(476, 374)
        Me.Xl_PrOrdresDeCompra1.TabIndex = 0
        '
        'TabPageInsercions
        '
        Me.TabPageInsercions.Controls.Add(Me.Xl_PrInsercions1)
        Me.TabPageInsercions.Location = New System.Drawing.Point(4, 22)
        Me.TabPageInsercions.Name = "TabPageInsercions"
        Me.TabPageInsercions.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageInsercions.Size = New System.Drawing.Size(482, 380)
        Me.TabPageInsercions.TabIndex = 4
        Me.TabPageInsercions.Text = "Insercions"
        Me.TabPageInsercions.UseVisualStyleBackColor = True
        '
        'Xl_PrInsercions1
        '
        Me.Xl_PrInsercions1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PrInsercions1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_PrInsercions1.Name = "Xl_PrInsercions1"
        Me.Xl_PrInsercions1.Size = New System.Drawing.Size(476, 374)
        Me.Xl_PrInsercions1.TabIndex = 0
        '
        'Xl_PrNumeros1
        '
        Me.Xl_PrNumeros1.Location = New System.Drawing.Point(6, 31)
        Me.Xl_PrNumeros1.Name = "Xl_PrNumeros1"
        Me.Xl_PrNumeros1.Size = New System.Drawing.Size(439, 318)
        Me.Xl_PrNumeros1.TabIndex = 0
        '
        'Frm_PrRevista
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(500, 496)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Xl_ImageLogo)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_PrRevista"
        Me.Text = "REVISTA"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGeneral.ResumeLayout(False)
        Me.TabPageGeneral.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumericUpDownSang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownPageHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownPageWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageNumeros.ResumeLayout(False)
        Me.TabPageTarifas.ResumeLayout(False)
        CType(Me.NumericUpDownYeaTarifas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewTarifas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageOrdresDeCompra.ResumeLayout(False)
        Me.TabPageInsercions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Xl_ImageLogo As Xl_Image
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGeneral As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents NumericUpDownSang As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownPageHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDownPageWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents TabPageNumeros As System.Windows.Forms.TabPage
    Friend WithEvents TabPageTarifas As System.Windows.Forms.TabPage
    Friend WithEvents NumericUpDownYeaTarifas As System.Windows.Forms.NumericUpDown
    Friend WithEvents DataGridViewTarifas As System.Windows.Forms.DataGridView
    Friend WithEvents TabPageOrdresDeCompra As System.Windows.Forms.TabPage
    Friend WithEvents Xl_PrOrdresDeCompra1 As Xl_PrOrdresDeCompra
    Friend WithEvents TabPageInsercions As System.Windows.Forms.TabPage
    Friend WithEvents Xl_PrInsercions1 As Xl_PrInsercions
    Friend WithEvents Xl_PrNumeros1 As Mat.NET.Xl_PrNumeros
End Class
