<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_WinMenuItem
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_WinMenuItem))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TextBoxNomPor = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxNomEng = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxNomCat = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox()
        Me.Xl_ImageBig = New Winforms.Xl_Image()
        Me.TextBoxAction = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxNomEsp = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelParent = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_RolsAllowed1 = New Winforms.Xl_RolsAllowed()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_Emps_Checklist1 = New Winforms.Xl_Emps_Checklist()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_RolsAllowed1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_Emps_Checklist1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 321)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(400, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(181, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(292, 4)
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
        Me.ButtonDel.TabIndex = 13
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(6, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(390, 307)
        Me.TabControl1.TabIndex = 15
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxNomPor)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.TextBoxNomEng)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxNomCat)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.ComboBoxCod)
        Me.TabPage1.Controls.Add(Me.Xl_ImageBig)
        Me.TabPage1.Controls.Add(Me.TextBoxAction)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBoxNomEsp)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.LabelParent)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(382, 281)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        '
        'TextBoxNomPor
        '
        Me.TextBoxNomPor.Location = New System.Drawing.Point(64, 110)
        Me.TextBoxNomPor.Name = "TextBoxNomPor"
        Me.TextBoxNomPor.Size = New System.Drawing.Size(301, 20)
        Me.TextBoxNomPor.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(8, 110)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 16)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "portuguès:"
        '
        'TextBoxNomEng
        '
        Me.TextBoxNomEng.Location = New System.Drawing.Point(64, 84)
        Me.TextBoxNomEng.Name = "TextBoxNomEng"
        Me.TextBoxNomEng.Size = New System.Drawing.Size(301, 20)
        Me.TextBoxNomEng.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 84)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 16)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "english:"
        '
        'TextBoxNomCat
        '
        Me.TextBoxNomCat.Location = New System.Drawing.Point(64, 58)
        Me.TextBoxNomCat.Name = "TextBoxNomCat"
        Me.TextBoxNomCat.Size = New System.Drawing.Size(301, 20)
        Me.TextBoxNomCat.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "català:"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 149)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "codi:"
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Items.AddRange(New Object() {"(seleccionar codi)", "carpeta", "item"})
        Me.ComboBoxCod.Location = New System.Drawing.Point(64, 146)
        Me.ComboBoxCod.Name = "ComboBoxCod"
        Me.ComboBoxCod.Size = New System.Drawing.Size(184, 21)
        Me.ComboBoxCod.TabIndex = 9
        '
        'Xl_ImageBig
        '
        Me.Xl_ImageBig.Bitmap = CType(resources.GetObject("Xl_ImageBig.Bitmap"), System.Drawing.Bitmap)
        Me.Xl_ImageBig.EmptyImageLabelText = ""
        Me.Xl_ImageBig.IsDirty = False
        Me.Xl_ImageBig.Location = New System.Drawing.Point(64, 206)
        Me.Xl_ImageBig.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ImageBig.Name = "Xl_ImageBig"
        Me.Xl_ImageBig.Size = New System.Drawing.Size(48, 48)
        Me.Xl_ImageBig.TabIndex = 10
        Me.Xl_ImageBig.ZipStream = Nothing
        '
        'TextBoxAction
        '
        Me.TextBoxAction.Location = New System.Drawing.Point(64, 173)
        Me.TextBoxAction.Name = "TextBoxAction"
        Me.TextBoxAction.Size = New System.Drawing.Size(301, 20)
        Me.TextBoxAction.TabIndex = 10
        Me.TextBoxAction.Visible = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 173)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "acció:"
        '
        'TextBoxNomEsp
        '
        Me.TextBoxNomEsp.Location = New System.Drawing.Point(64, 32)
        Me.TextBoxNomEsp.Name = "TextBoxNomEsp"
        Me.TextBoxNomEsp.Size = New System.Drawing.Size(301, 20)
        Me.TextBoxNomEsp.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "español:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "parent:"
        '
        'LabelParent
        '
        Me.LabelParent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelParent.Location = New System.Drawing.Point(64, 8)
        Me.LabelParent.Name = "LabelParent"
        Me.LabelParent.Size = New System.Drawing.Size(301, 18)
        Me.LabelParent.TabIndex = 2
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_RolsAllowed1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(382, 281)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Permisos"
        '
        'Xl_RolsAllowed1
        '
        Me.Xl_RolsAllowed1.AllowUserToAddRows = False
        Me.Xl_RolsAllowed1.AllowUserToDeleteRows = False
        Me.Xl_RolsAllowed1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_RolsAllowed1.DisplayObsolets = False
        Me.Xl_RolsAllowed1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RolsAllowed1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_RolsAllowed1.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_RolsAllowed1.MouseIsDown = False
        Me.Xl_RolsAllowed1.Name = "Xl_RolsAllowed1"
        Me.Xl_RolsAllowed1.Size = New System.Drawing.Size(382, 281)
        Me.Xl_RolsAllowed1.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_Emps_Checklist1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(382, 281)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Empreses"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_Emps_Checklist1
        '
        Me.Xl_Emps_Checklist1.AllowUserToAddRows = False
        Me.Xl_Emps_Checklist1.AllowUserToDeleteRows = False
        Me.Xl_Emps_Checklist1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Emps_Checklist1.DisplayObsolets = False
        Me.Xl_Emps_Checklist1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Emps_Checklist1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Emps_Checklist1.MouseIsDown = False
        Me.Xl_Emps_Checklist1.Name = "Xl_Emps_Checklist1"
        Me.Xl_Emps_Checklist1.ReadOnly = True
        Me.Xl_Emps_Checklist1.Size = New System.Drawing.Size(376, 275)
        Me.Xl_Emps_Checklist1.TabIndex = 0
        '
        'Frm_WinMenuItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 352)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_WinMenuItem"
        Me.Text = "Windows Menuitem"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_RolsAllowed1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_Emps_Checklist1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCod As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_ImageBig As Winforms.Xl_Image
    Friend WithEvents TextBoxAction As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNomEsp As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LabelParent As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_RolsAllowed1 As Xl_RolsAllowed
    Friend WithEvents TextBoxNomPor As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxNomEng As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxNomCat As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_Emps_Checklist1 As Xl_Emps_Checklist
End Class
