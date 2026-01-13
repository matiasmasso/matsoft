<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_TrpZon
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_TrpCosts1 = New Winforms.Xl_TrpCosts()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_ZonasIsoPais1 = New Winforms.Xl_ZonasIsoPais()
        Me.TextBoxTransportista = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxTrpZonaNom = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NumericUpDownCubicatje = New System.Windows.Forms.NumericUpDown()
        Me.CheckBoxInheritCubicatje = New System.Windows.Forms.CheckBox()
        Me.CheckBoxActivat = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_TrpCosts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_ZonasIsoPais1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownCubicatje, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(1, 112)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(463, 323)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_TrpCosts1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(455, 297)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Costos"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_TrpCosts1
        '
        Me.Xl_TrpCosts1.AllowUserToAddRows = False
        Me.Xl_TrpCosts1.AllowUserToDeleteRows = False
        Me.Xl_TrpCosts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_TrpCosts1.DisplayObsolets = False
        Me.Xl_TrpCosts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_TrpCosts1.IsDirty = False
        Me.Xl_TrpCosts1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_TrpCosts1.Name = "Xl_TrpCosts1"
        Me.Xl_TrpCosts1.ReadOnly = True
        Me.Xl_TrpCosts1.Size = New System.Drawing.Size(449, 291)
        Me.Xl_TrpCosts1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_ZonasIsoPais1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(455, 297)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Zones"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_ZonasIsoPais1
        '
        Me.Xl_ZonasIsoPais1.AllowUserToAddRows = False
        Me.Xl_ZonasIsoPais1.AllowUserToDeleteRows = False
        Me.Xl_ZonasIsoPais1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ZonasIsoPais1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ZonasIsoPais1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ZonasIsoPais1.Name = "Xl_ZonasIsoPais1"
        Me.Xl_ZonasIsoPais1.ReadOnly = True
        Me.Xl_ZonasIsoPais1.Size = New System.Drawing.Size(449, 291)
        Me.Xl_ZonasIsoPais1.TabIndex = 0
        '
        'TextBoxTransportista
        '
        Me.TextBoxTransportista.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTransportista.Location = New System.Drawing.Point(82, 14)
        Me.TextBoxTransportista.Name = "TextBoxTransportista"
        Me.TextBoxTransportista.ReadOnly = True
        Me.TextBoxTransportista.Size = New System.Drawing.Size(378, 20)
        Me.TextBoxTransportista.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Transportista:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Zona:"
        '
        'TextBoxTrpZonaNom
        '
        Me.TextBoxTrpZonaNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTrpZonaNom.Location = New System.Drawing.Point(82, 36)
        Me.TextBoxTrpZonaNom.Name = "TextBoxTrpZonaNom"
        Me.TextBoxTrpZonaNom.ReadOnly = True
        Me.TextBoxTrpZonaNom.Size = New System.Drawing.Size(304, 20)
        Me.TextBoxTrpZonaNom.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Cubicatje"
        '
        'NumericUpDownCubicatje
        '
        Me.NumericUpDownCubicatje.Location = New System.Drawing.Point(82, 60)
        Me.NumericUpDownCubicatje.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.NumericUpDownCubicatje.Name = "NumericUpDownCubicatje"
        Me.NumericUpDownCubicatje.Size = New System.Drawing.Size(69, 20)
        Me.NumericUpDownCubicatje.TabIndex = 6
        Me.NumericUpDownCubicatje.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBoxInheritCubicatje
        '
        Me.CheckBoxInheritCubicatje.AutoSize = True
        Me.CheckBoxInheritCubicatje.Location = New System.Drawing.Point(158, 62)
        Me.CheckBoxInheritCubicatje.Name = "CheckBoxInheritCubicatje"
        Me.CheckBoxInheritCubicatje.Size = New System.Drawing.Size(135, 17)
        Me.CheckBoxInheritCubicatje.TabIndex = 7
        Me.CheckBoxInheritCubicatje.Text = "Hereta del transportista"
        Me.CheckBoxInheritCubicatje.UseVisualStyleBackColor = True
        '
        'CheckBoxActivat
        '
        Me.CheckBoxActivat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxActivat.AutoSize = True
        Me.CheckBoxActivat.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxActivat.Location = New System.Drawing.Point(392, 40)
        Me.CheckBoxActivat.Name = "CheckBoxActivat"
        Me.CheckBoxActivat.Size = New System.Drawing.Size(68, 17)
        Me.CheckBoxActivat.TabIndex = 8
        Me.CheckBoxActivat.Text = "Activada"
        Me.CheckBoxActivat.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 437)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(462, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(243, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel·lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(354, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
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
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Frm_TrpZon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(462, 468)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.CheckBoxActivat)
        Me.Controls.Add(Me.CheckBoxInheritCubicatje)
        Me.Controls.Add(Me.NumericUpDownCubicatje)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxTrpZonaNom)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxTransportista)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_TrpZon"
        Me.Text = "Tarifa de transport"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Xl_TrpCosts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_ZonasIsoPais1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownCubicatje, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TextBoxTransportista As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxTrpZonaNom As TextBox
    Friend WithEvents Xl_ZonasIsoPais1 As Xl_ZonasIsoPais
    Friend WithEvents Xl_TrpCosts1 As Xl_TrpCosts
    Friend WithEvents Label3 As Label
    Friend WithEvents NumericUpDownCubicatje As NumericUpDown
    Friend WithEvents CheckBoxInheritCubicatje As CheckBox
    Friend WithEvents CheckBoxActivat As CheckBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
End Class
