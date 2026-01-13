<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PortsCondicions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPageGral = New System.Windows.Forms.TabPage
        Me.TextBoxText = New System.Windows.Forms.TextBox
        Me.GroupBoxPortsPagats = New System.Windows.Forms.GroupBox
        Me.CheckBoxForfait = New System.Windows.Forms.CheckBox
        Me.CheckBoxPerImport = New System.Windows.Forms.CheckBox
        Me.CheckBoxPerQty = New System.Windows.Forms.CheckBox
        Me.GroupBoxPcs = New System.Windows.Forms.GroupBox
        Me.Xl_AmtUnitsMinPreu = New Xl_Amount
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBoxUnitsQty = New System.Windows.Forms.TextBox
        Me.GroupBoxPdcMinVal = New System.Windows.Forms.GroupBox
        Me.Xl_AmtPdcMin = New Xl_Amount
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.GroupBoxForfait = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Xl_AmtForfait = New Xl_Amount
        Me.RadioButtonPortsPagats = New System.Windows.Forms.RadioButton
        Me.RadioButtonPortsDeguts = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBoxNom = New System.Windows.Forms.TextBox
        Me.TabPagePeces = New System.Windows.Forms.TabPage
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonDel = New System.Windows.Forms.Button
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.TabControl1.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        Me.GroupBoxPortsPagats.SuspendLayout()
        Me.GroupBoxPcs.SuspendLayout()
        Me.GroupBoxPdcMinVal.SuspendLayout()
        Me.GroupBoxForfait.SuspendLayout()
        Me.TabPagePeces.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGral)
        Me.TabControl1.Controls.Add(Me.TabPagePeces)
        Me.TabControl1.Location = New System.Drawing.Point(4, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(347, 455)
        Me.TabControl1.TabIndex = 13
        '
        'TabPageGral
        '
        Me.TabPageGral.Controls.Add(Me.TextBoxText)
        Me.TabPageGral.Controls.Add(Me.GroupBoxPortsPagats)
        Me.TabPageGral.Controls.Add(Me.RadioButtonPortsPagats)
        Me.TabPageGral.Controls.Add(Me.RadioButtonPortsDeguts)
        Me.TabPageGral.Controls.Add(Me.Label1)
        Me.TabPageGral.Controls.Add(Me.TextBoxNom)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGral.Size = New System.Drawing.Size(339, 429)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "GENERAL"
        '
        'TextBoxText
        '
        Me.TextBoxText.Location = New System.Drawing.Point(13, 361)
        Me.TextBoxText.Multiline = True
        Me.TextBoxText.Name = "TextBoxText"
        Me.TextBoxText.Size = New System.Drawing.Size(307, 62)
        Me.TextBoxText.TabIndex = 21
        '
        'GroupBoxPortsPagats
        '
        Me.GroupBoxPortsPagats.Controls.Add(Me.CheckBoxForfait)
        Me.GroupBoxPortsPagats.Controls.Add(Me.CheckBoxPerImport)
        Me.GroupBoxPortsPagats.Controls.Add(Me.CheckBoxPerQty)
        Me.GroupBoxPortsPagats.Controls.Add(Me.GroupBoxPcs)
        Me.GroupBoxPortsPagats.Controls.Add(Me.GroupBoxPdcMinVal)
        Me.GroupBoxPortsPagats.Controls.Add(Me.GroupBoxForfait)
        Me.GroupBoxPortsPagats.Location = New System.Drawing.Point(13, 87)
        Me.GroupBoxPortsPagats.Name = "GroupBoxPortsPagats"
        Me.GroupBoxPortsPagats.Size = New System.Drawing.Size(307, 267)
        Me.GroupBoxPortsPagats.TabIndex = 17
        Me.GroupBoxPortsPagats.TabStop = False
        Me.GroupBoxPortsPagats.Visible = False
        '
        'CheckBoxForfait
        '
        Me.CheckBoxForfait.AutoSize = True
        Me.CheckBoxForfait.Location = New System.Drawing.Point(43, 208)
        Me.CheckBoxForfait.Margin = New System.Windows.Forms.Padding(3, 0, 3, 1)
        Me.CheckBoxForfait.Name = "CheckBoxForfait"
        Me.CheckBoxForfait.Size = New System.Drawing.Size(125, 17)
        Me.CheckBoxForfait.TabIndex = 25
        Me.CheckBoxForfait.Text = "a carregar en factura"
        '
        'CheckBoxPerImport
        '
        Me.CheckBoxPerImport.AutoSize = True
        Me.CheckBoxPerImport.Location = New System.Drawing.Point(43, 9)
        Me.CheckBoxPerImport.Name = "CheckBoxPerImport"
        Me.CheckBoxPerImport.Size = New System.Drawing.Size(134, 17)
        Me.CheckBoxPerImport.TabIndex = 16
        Me.CheckBoxPerImport.Text = "per import de comanda"
        '
        'CheckBoxPerQty
        '
        Me.CheckBoxPerQty.AutoSize = True
        Me.CheckBoxPerQty.Location = New System.Drawing.Point(43, 83)
        Me.CheckBoxPerQty.Margin = New System.Windows.Forms.Padding(3, 0, 3, 1)
        Me.CheckBoxPerQty.Name = "CheckBoxPerQty"
        Me.CheckBoxPerQty.Size = New System.Drawing.Size(126, 17)
        Me.CheckBoxPerQty.TabIndex = 20
        Me.CheckBoxPerQty.Text = "per numero de peces"
        '
        'GroupBoxPcs
        '
        Me.GroupBoxPcs.Controls.Add(Me.Xl_AmtUnitsMinPreu)
        Me.GroupBoxPcs.Controls.Add(Me.TextBox4)
        Me.GroupBoxPcs.Controls.Add(Me.TextBox3)
        Me.GroupBoxPcs.Controls.Add(Me.TextBoxUnitsQty)
        Me.GroupBoxPcs.Location = New System.Drawing.Point(36, 83)
        Me.GroupBoxPcs.Margin = New System.Windows.Forms.Padding(1, 3, 3, 1)
        Me.GroupBoxPcs.Name = "GroupBoxPcs"
        Me.GroupBoxPcs.Size = New System.Drawing.Size(265, 112)
        Me.GroupBoxPcs.TabIndex = 18
        Me.GroupBoxPcs.TabStop = False
        '
        'Xl_AmtUnitsMinPreu
        '
        Me.Xl_AmtUnitsMinPreu.Amt = Nothing
        Me.Xl_AmtUnitsMinPreu.Location = New System.Drawing.Point(220, 86)
        Me.Xl_AmtUnitsMinPreu.Name = "Xl_AmtUnitsMinPreu"
        Me.Xl_AmtUnitsMinPreu.Size = New System.Drawing.Size(35, 20)
        Me.Xl_AmtUnitsMinPreu.TabIndex = 18
        Me.Xl_AmtUnitsMinPreu.TabStop = False
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.SystemColors.MenuBar
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox4.Location = New System.Drawing.Point(54, 22)
        Me.TextBox4.Multiline = True
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(154, 37)
        Me.TextBox4.TabIndex = 17
        Me.TextBox4.Text = "ports pagats a partir de la següent quantitat de peces:"
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.SystemColors.MenuBar
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox3.Location = New System.Drawing.Point(54, 66)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(154, 40)
        Me.TextBox3.TabIndex = 16
        Me.TextBox3.Text = "Peça es un article dels que hi surten a la pestanya PECES, que tingui un cost min" & _
            "im de:"
        '
        'TextBoxUnitsQty
        '
        Me.TextBoxUnitsQty.Location = New System.Drawing.Point(219, 27)
        Me.TextBoxUnitsQty.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.TextBoxUnitsQty.Name = "TextBoxUnitsQty"
        Me.TextBoxUnitsQty.Size = New System.Drawing.Size(36, 20)
        Me.TextBoxUnitsQty.TabIndex = 14
        Me.TextBoxUnitsQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBoxPdcMinVal
        '
        Me.GroupBoxPdcMinVal.Controls.Add(Me.Xl_AmtPdcMin)
        Me.GroupBoxPdcMinVal.Controls.Add(Me.TextBox5)
        Me.GroupBoxPdcMinVal.Location = New System.Drawing.Point(36, 9)
        Me.GroupBoxPdcMinVal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.GroupBoxPdcMinVal.Name = "GroupBoxPdcMinVal"
        Me.GroupBoxPdcMinVal.Size = New System.Drawing.Size(265, 62)
        Me.GroupBoxPdcMinVal.TabIndex = 17
        Me.GroupBoxPdcMinVal.TabStop = False
        '
        'Xl_AmtPdcMin
        '
        Me.Xl_AmtPdcMin.Amt = Nothing
        Me.Xl_AmtPdcMin.Location = New System.Drawing.Point(220, 24)
        Me.Xl_AmtPdcMin.Name = "Xl_AmtPdcMin"
        Me.Xl_AmtPdcMin.Size = New System.Drawing.Size(35, 20)
        Me.Xl_AmtPdcMin.TabIndex = 15
        Me.Xl_AmtPdcMin.TabStop = False
        '
        'TextBox5
        '
        Me.TextBox5.BackColor = System.Drawing.SystemColors.MenuBar
        Me.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox5.Location = New System.Drawing.Point(54, 24)
        Me.TextBox5.Multiline = True
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(154, 17)
        Me.TextBox5.TabIndex = 12
        Me.TextBox5.Text = "Ports pagats a partir de:"
        '
        'GroupBoxForfait
        '
        Me.GroupBoxForfait.Controls.Add(Me.Label2)
        Me.GroupBoxForfait.Controls.Add(Me.Xl_AmtForfait)
        Me.GroupBoxForfait.Location = New System.Drawing.Point(36, 208)
        Me.GroupBoxForfait.Name = "GroupBoxForfait"
        Me.GroupBoxForfait.Size = New System.Drawing.Size(265, 53)
        Me.GroupBoxForfait.TabIndex = 19
        Me.GroupBoxForfait.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(57, 20)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "forfait portes"
        '
        'Xl_AmtForfait
        '
        Me.Xl_AmtForfait.Amt = Nothing
        Me.Xl_AmtForfait.Location = New System.Drawing.Point(219, 20)
        Me.Xl_AmtForfait.Name = "Xl_AmtForfait"
        Me.Xl_AmtForfait.Size = New System.Drawing.Size(36, 20)
        Me.Xl_AmtForfait.TabIndex = 16
        Me.Xl_AmtForfait.TabStop = False
        '
        'RadioButtonPortsPagats
        '
        Me.RadioButtonPortsPagats.AutoSize = True
        Me.RadioButtonPortsPagats.Location = New System.Drawing.Point(25, 65)
        Me.RadioButtonPortsPagats.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.RadioButtonPortsPagats.Name = "RadioButtonPortsPagats"
        Me.RadioButtonPortsPagats.Size = New System.Drawing.Size(84, 17)
        Me.RadioButtonPortsPagats.TabIndex = 20
        Me.RadioButtonPortsPagats.Text = "Ports pagats"
        '
        'RadioButtonPortsDeguts
        '
        Me.RadioButtonPortsDeguts.AutoSize = True
        Me.RadioButtonPortsDeguts.Checked = True
        Me.RadioButtonPortsDeguts.Location = New System.Drawing.Point(25, 44)
        Me.RadioButtonPortsDeguts.Name = "RadioButtonPortsDeguts"
        Me.RadioButtonPortsDeguts.Size = New System.Drawing.Size(84, 17)
        Me.RadioButtonPortsDeguts.TabIndex = 18
        Me.RadioButtonPortsDeguts.TabStop = True
        Me.RadioButtonPortsDeguts.Text = "Ports deguts"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "nom:"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(68, 7)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxNom.TabIndex = 0
        '
        'TabPagePeces
        '
        Me.TabPagePeces.Controls.Add(Me.DataGridView1)
        Me.TabPagePeces.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePeces.Name = "TabPagePeces"
        Me.TabPagePeces.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePeces.Size = New System.Drawing.Size(339, 429)
        Me.TabPagePeces.TabIndex = 1
        Me.TabPagePeces.Text = "PECES"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 468)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(358, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(139, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(250, 4)
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
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(333, 423)
        Me.DataGridView1.TabIndex = 0
        '
        'Frm_PortsCondicions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(358, 499)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_PortsCondicions"
        Me.Text = "CONDICIONS PORTS"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabPageGral.PerformLayout()
        Me.GroupBoxPortsPagats.ResumeLayout(False)
        Me.GroupBoxPortsPagats.PerformLayout()
        Me.GroupBoxPcs.ResumeLayout(False)
        Me.GroupBoxPcs.PerformLayout()
        Me.GroupBoxPdcMinVal.ResumeLayout(False)
        Me.GroupBoxPdcMinVal.PerformLayout()
        Me.GroupBoxForfait.ResumeLayout(False)
        Me.GroupBoxForfait.PerformLayout()
        Me.TabPagePeces.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxText As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxPortsPagats As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxForfait As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxPerImport As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxPerQty As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxPcs As System.Windows.Forms.GroupBox
    Friend WithEvents Xl_AmtUnitsMinPreu As Xl_Amount
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxUnitsQty As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxPdcMinVal As System.Windows.Forms.GroupBox
    Friend WithEvents Xl_AmtPdcMin As Xl_Amount
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxForfait As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtForfait As Xl_Amount
    Friend WithEvents RadioButtonPortsPagats As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonPortsDeguts As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents TabPagePeces As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
End Class
