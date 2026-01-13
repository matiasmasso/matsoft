<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Country
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
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.TabPageProvincias = New System.Windows.Forms.TabPage()
        Me.TextBoxPrefixeTel = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_ImageFlag = New Mat.NET.Xl_Image()
        Me.CheckBoxCEE = New System.Windows.Forms.CheckBox()
        Me.TextBoxNom_ENG = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxNom_CAT = New System.Windows.Forms.TextBox()
        Me.TabPageBancs = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabZonas = New System.Windows.Forms.TabPage()
        Me.Xl_Zonas1 = New Mat.NET.Xl_Zonas()
        Me.TextBoxNom_ESP = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TabPageGral = New System.Windows.Forms.TabPage()
        Me.TextBoxISO = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabPageRegions = New System.Windows.Forms.TabPage()
        Me.Xl_AreaProvincias1 = New Mat.NET.Xl_AreaProvincias()
        Me.Xl_AreaRegions1 = New Mat.NET.Xl_AreaRegions()
        Me.TabPageProvincias.SuspendLayout()
        Me.TabZonas.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageRegions.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(238, 323)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 5
        Me.ButtonCancel.Text = "Cancel.lar"
        '
        'TabPageProvincias
        '
        Me.TabPageProvincias.Controls.Add(Me.Xl_AreaProvincias1)
        Me.TabPageProvincias.Location = New System.Drawing.Point(4, 22)
        Me.TabPageProvincias.Name = "TabPageProvincias"
        Me.TabPageProvincias.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageProvincias.Size = New System.Drawing.Size(386, 268)
        Me.TabPageProvincias.TabIndex = 3
        Me.TabPageProvincias.Text = "Provincies"
        Me.TabPageProvincias.UseVisualStyleBackColor = True
        '
        'TextBoxPrefixeTel
        '
        Me.TextBoxPrefixeTel.Location = New System.Drawing.Point(110, 172)
        Me.TextBoxPrefixeTel.MaxLength = 3
        Me.TextBoxPrefixeTel.Name = "TextBoxPrefixeTel"
        Me.TextBoxPrefixeTel.Size = New System.Drawing.Size(44, 20)
        Me.TextBoxPrefixeTel.TabIndex = 52
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 175)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 13)
        Me.Label5.TabIndex = 51
        Me.Label5.Text = "Prefixe telefònic: "
        '
        'Xl_ImageFlag
        '
        Me.Xl_ImageFlag.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ImageFlag.Bitmap = Nothing
        Me.Xl_ImageFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ImageFlag.EmptyImageLabelText = ""
        Me.Xl_ImageFlag.IsDirty = False
        Me.Xl_ImageFlag.Location = New System.Drawing.Point(342, 16)
        Me.Xl_ImageFlag.Margin = New System.Windows.Forms.Padding(2, 3, 1, 3)
        Me.Xl_ImageFlag.MaxHeight = 0
        Me.Xl_ImageFlag.MaxWidth = 0
        Me.Xl_ImageFlag.Name = "Xl_ImageFlag"
        Me.Xl_ImageFlag.Size = New System.Drawing.Size(30, 16)
        Me.Xl_ImageFlag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Xl_ImageFlag.TabIndex = 50
        Me.Xl_ImageFlag.ZipStream = Nothing
        '
        'CheckBoxCEE
        '
        Me.CheckBoxCEE.AutoSize = True
        Me.CheckBoxCEE.Location = New System.Drawing.Point(110, 198)
        Me.CheckBoxCEE.Name = "CheckBoxCEE"
        Me.CheckBoxCEE.Size = New System.Drawing.Size(47, 17)
        Me.CheckBoxCEE.TabIndex = 8
        Me.CheckBoxCEE.Text = "CEE"
        '
        'TextBoxNom_ENG
        '
        Me.TextBoxNom_ENG.Location = New System.Drawing.Point(110, 146)
        Me.TextBoxNom_ENG.Name = "TextBoxNom_ENG"
        Me.TextBoxNom_ENG.Size = New System.Drawing.Size(262, 20)
        Me.TextBoxNom_ENG.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 149)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Anglés"
        '
        'TextBoxNom_CAT
        '
        Me.TextBoxNom_CAT.Location = New System.Drawing.Point(110, 119)
        Me.TextBoxNom_CAT.Name = "TextBoxNom_CAT"
        Me.TextBoxNom_CAT.Size = New System.Drawing.Size(262, 20)
        Me.TextBoxNom_CAT.TabIndex = 3
        '
        'TabPageBancs
        '
        Me.TabPageBancs.Location = New System.Drawing.Point(4, 22)
        Me.TabPageBancs.Name = "TabPageBancs"
        Me.TabPageBancs.Size = New System.Drawing.Size(386, 268)
        Me.TabPageBancs.TabIndex = 2
        Me.TabPageBancs.Text = "Bancs"
        Me.TabPageBancs.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Catalá"
        '
        'TabZonas
        '
        Me.TabZonas.Controls.Add(Me.Xl_Zonas1)
        Me.TabZonas.Location = New System.Drawing.Point(4, 22)
        Me.TabZonas.Name = "TabZonas"
        Me.TabZonas.Padding = New System.Windows.Forms.Padding(3)
        Me.TabZonas.Size = New System.Drawing.Size(386, 268)
        Me.TabZonas.TabIndex = 1
        Me.TabZonas.Text = "Zones"
        Me.TabZonas.UseVisualStyleBackColor = True
        '
        'Xl_Zonas1
        '
        Me.Xl_Zonas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Zonas1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Zonas1.Name = "Xl_Zonas1"
        Me.Xl_Zonas1.Size = New System.Drawing.Size(380, 262)
        Me.Xl_Zonas1.TabIndex = 0
        '
        'TextBoxNom_ESP
        '
        Me.TextBoxNom_ESP.Location = New System.Drawing.Point(110, 92)
        Me.TextBoxNom_ESP.Name = "TextBoxNom_ESP"
        Me.TextBoxNom_ESP.Size = New System.Drawing.Size(262, 20)
        Me.TextBoxNom_ESP.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Espanyol"
        '
        'ButtonOk
        '
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(319, 323)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOk.TabIndex = 4
        Me.ButtonOk.Text = "Acceptar"
        '
        'TabPageGral
        '
        Me.TabPageGral.Controls.Add(Me.TextBoxISO)
        Me.TabPageGral.Controls.Add(Me.Label4)
        Me.TabPageGral.Controls.Add(Me.TextBoxPrefixeTel)
        Me.TabPageGral.Controls.Add(Me.Label5)
        Me.TabPageGral.Controls.Add(Me.Xl_ImageFlag)
        Me.TabPageGral.Controls.Add(Me.CheckBoxCEE)
        Me.TabPageGral.Controls.Add(Me.TextBoxNom_ENG)
        Me.TabPageGral.Controls.Add(Me.Label3)
        Me.TabPageGral.Controls.Add(Me.TextBoxNom_CAT)
        Me.TabPageGral.Controls.Add(Me.Label2)
        Me.TabPageGral.Controls.Add(Me.TextBoxNom_ESP)
        Me.TabPageGral.Controls.Add(Me.Label1)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGral.Size = New System.Drawing.Size(386, 268)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "General"
        Me.TabPageGral.UseVisualStyleBackColor = True
        '
        'TextBoxISO
        '
        Me.TextBoxISO.Location = New System.Drawing.Point(110, 66)
        Me.TextBoxISO.MaxLength = 2
        Me.TextBoxISO.Name = "TextBoxISO"
        Me.TextBoxISO.Size = New System.Drawing.Size(31, 20)
        Me.TextBoxISO.TabIndex = 54
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 13)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "ISO"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGral)
        Me.TabControl1.Controls.Add(Me.TabPageBancs)
        Me.TabControl1.Controls.Add(Me.TabZonas)
        Me.TabControl1.Controls.Add(Me.TabPageProvincias)
        Me.TabControl1.Controls.Add(Me.TabPageRegions)
        Me.TabControl1.Location = New System.Drawing.Point(4, 22)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(394, 294)
        Me.TabControl1.TabIndex = 3
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.Location = New System.Drawing.Point(4, 323)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonDel.TabIndex = 6
        Me.ButtonDel.Text = "Eliminar"
        '
        'TabPageRegions
        '
        Me.TabPageRegions.Controls.Add(Me.Xl_AreaRegions1)
        Me.TabPageRegions.Location = New System.Drawing.Point(4, 22)
        Me.TabPageRegions.Name = "TabPageRegions"
        Me.TabPageRegions.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageRegions.Size = New System.Drawing.Size(386, 268)
        Me.TabPageRegions.TabIndex = 4
        Me.TabPageRegions.Text = "Regions"
        Me.TabPageRegions.UseVisualStyleBackColor = True
        '
        'Xl_AreaProvincias1
        '
        Me.Xl_AreaProvincias1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_AreaProvincias1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_AreaProvincias1.Name = "Xl_AreaProvincias1"
        Me.Xl_AreaProvincias1.Size = New System.Drawing.Size(380, 262)
        Me.Xl_AreaProvincias1.TabIndex = 0
        '
        'Xl_AreaRegions1
        '
        Me.Xl_AreaRegions1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_AreaRegions1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_AreaRegions1.Name = "Xl_AreaRegions1"
        Me.Xl_AreaRegions1.Size = New System.Drawing.Size(380, 262)
        Me.Xl_AreaRegions1.TabIndex = 0
        '
        'Frm_Country
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 351)
        Me.Controls.Add(Me.ButtonDel)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Country"
        Me.Text = "Pais"
        Me.TabPageProvincias.ResumeLayout(False)
        Me.TabZonas.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabPageGral.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageRegions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents TabPageProvincias As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxPrefixeTel As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_ImageFlag As Mat.NET.Xl_Image
    Friend WithEvents CheckBoxCEE As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxNom_ENG As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom_CAT As System.Windows.Forms.TextBox
    Friend WithEvents TabPageBancs As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabZonas As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxNom_ESP As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TextBoxISO As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Xl_Zonas1 As Mat.NET.Xl_Zonas
    Friend WithEvents TabPageRegions As System.Windows.Forms.TabPage
    Friend WithEvents Xl_AreaProvincias1 As Mat.NET.Xl_AreaProvincias
    Friend WithEvents Xl_AreaRegions1 As Mat.NET.Xl_AreaRegions
End Class
