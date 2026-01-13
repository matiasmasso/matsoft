<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Address
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TextBoxZip = New System.Windows.Forms.TextBox()
        Me.ComboBoxPlaceNames = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ComboBoxCountry = New System.Windows.Forms.ComboBox()
        Me.Xl_Lookup_Zip1 = New Winforms.Xl_LookupZip()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ButtonCoordenadasReset = New System.Windows.Forms.Button()
        Me.TextBoxPiso = New System.Windows.Forms.TextBox()
        Me.TextBoxCoordenadas = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxNumero = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxVia = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxAdr = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_GoogleMaps1 = New Winforms.Xl_GoogleMaps()
        Me.Xl_StreetView1 = New Winforms.Xl_StreetView()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 402)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(900, 31)
        Me.Panel1.TabIndex = 46
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(681, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 6
        Me.ButtonCancel.Text = "Cancel·lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(792, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 5
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxZip)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ComboBoxPlaceNames)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ComboBoxCountry)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Lookup_Zip1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ButtonCoordenadasReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxPiso)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxCoordenadas)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxNumero)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxVia)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxAdr)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(900, 402)
        Me.SplitContainer1.SplitterDistance = 480
        Me.SplitContainer1.TabIndex = 47
        '
        'TextBoxZip
        '
        Me.TextBoxZip.Location = New System.Drawing.Point(93, 41)
        Me.TextBoxZip.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxZip.MaxLength = 50
        Me.TextBoxZip.Name = "TextBoxZip"
        Me.TextBoxZip.Size = New System.Drawing.Size(75, 20)
        Me.TextBoxZip.TabIndex = 84
        '
        'ComboBoxPlaceNames
        '
        Me.ComboBoxPlaceNames.FormattingEnabled = True
        Me.ComboBoxPlaceNames.Location = New System.Drawing.Point(173, 41)
        Me.ComboBoxPlaceNames.Name = "ComboBoxPlaceNames"
        Me.ComboBoxPlaceNames.Size = New System.Drawing.Size(74, 21)
        Me.ComboBoxPlaceNames.TabIndex = 83
        Me.ComboBoxPlaceNames.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(91, 25)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 13)
        Me.Label9.TabIndex = 82
        Me.Label9.Text = "codi postal"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(26, 13)
        Me.Label8.TabIndex = 81
        Me.Label8.Text = "pais"
        '
        'ComboBoxCountry
        '
        Me.ComboBoxCountry.FormattingEnabled = True
        Me.ComboBoxCountry.Items.AddRange(New Object() {"ES", "PT", "AD", "(altres)"})
        Me.ComboBoxCountry.Location = New System.Drawing.Point(13, 41)
        Me.ComboBoxCountry.Name = "ComboBoxCountry"
        Me.ComboBoxCountry.Size = New System.Drawing.Size(74, 21)
        Me.ComboBoxCountry.TabIndex = 79
        '
        'Xl_Lookup_Zip1
        '
        Me.Xl_Lookup_Zip1.IsDirty = False
        Me.Xl_Lookup_Zip1.Location = New System.Drawing.Point(12, 92)
        Me.Xl_Lookup_Zip1.Name = "Xl_Lookup_Zip1"
        Me.Xl_Lookup_Zip1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Lookup_Zip1.ReadOnlyLookup = False
        Me.Xl_Lookup_Zip1.Size = New System.Drawing.Size(451, 20)
        Me.Xl_Lookup_Zip1.TabIndex = 0
        Me.Xl_Lookup_Zip1.Value = Nothing
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 253)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 13)
        Me.Label7.TabIndex = 78
        Me.Label7.Text = "Coordinades"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(60, 121)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(0, 13)
        Me.Label6.TabIndex = 77
        '
        'ButtonCoordenadasReset
        '
        Me.ButtonCoordenadasReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCoordenadasReset.Location = New System.Drawing.Point(394, 265)
        Me.ButtonCoordenadasReset.Name = "ButtonCoordenadasReset"
        Me.ButtonCoordenadasReset.Size = New System.Drawing.Size(69, 23)
        Me.ButtonCoordenadasReset.TabIndex = 68
        Me.ButtonCoordenadasReset.Text = "reconfirmar"
        Me.ButtonCoordenadasReset.UseVisualStyleBackColor = True
        '
        'TextBoxPiso
        '
        Me.TextBoxPiso.Location = New System.Drawing.Point(11, 218)
        Me.TextBoxPiso.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxPiso.MaxLength = 50
        Me.TextBoxPiso.Name = "TextBoxPiso"
        Me.TextBoxPiso.Size = New System.Drawing.Size(127, 20)
        Me.TextBoxPiso.TabIndex = 3
        '
        'TextBoxCoordenadas
        '
        Me.TextBoxCoordenadas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCoordenadas.Location = New System.Drawing.Point(11, 267)
        Me.TextBoxCoordenadas.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxCoordenadas.MaxLength = 50
        Me.TextBoxCoordenadas.Name = "TextBoxCoordenadas"
        Me.TextBoxCoordenadas.ReadOnly = True
        Me.TextBoxCoordenadas.Size = New System.Drawing.Size(379, 20)
        Me.TextBoxCoordenadas.TabIndex = 65
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 204)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 13)
        Me.Label5.TabIndex = 76
        Me.Label5.Text = "Pis (per exemple 2º 1ª)"
        '
        'TextBoxNumero
        '
        Me.TextBoxNumero.Location = New System.Drawing.Point(11, 177)
        Me.TextBoxNumero.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxNumero.MaxLength = 50
        Me.TextBoxNumero.Name = "TextBoxNumero"
        Me.TextBoxNumero.Size = New System.Drawing.Size(127, 20)
        Me.TextBoxNumero.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 162)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(176, 13)
        Me.Label4.TabIndex = 75
        Me.Label4.Text = "Número (per exemple 403 o Km.2,5)"
        '
        'TextBoxVia
        '
        Me.TextBoxVia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxVia.Location = New System.Drawing.Point(11, 136)
        Me.TextBoxVia.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxVia.MaxLength = 50
        Me.TextBoxVia.Name = "TextBoxVia"
        Me.TextBoxVia.Size = New System.Drawing.Size(452, 20)
        Me.TextBoxVia.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 121)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(201, 13)
        Me.Label3.TabIndex = 74
        Me.Label3.Text = "Via pública (per exemple Avda. Diagonal)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 71
        Me.Label2.Text = "població"
        '
        'TextBoxAdr
        '
        Me.TextBoxAdr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAdr.Location = New System.Drawing.Point(10, 346)
        Me.TextBoxAdr.MaxLength = 250
        Me.TextBoxAdr.Multiline = True
        Me.TextBoxAdr.Name = "TextBoxAdr"
        Me.TextBoxAdr.Size = New System.Drawing.Size(453, 47)
        Me.TextBoxAdr.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 330)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(201, 13)
        Me.Label1.TabIndex = 67
        Me.Label1.Text = "adreça (tal com s'hauria de veure escrita)"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer2.Location = New System.Drawing.Point(4, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_GoogleMaps1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_StreetView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(409, 402)
        Me.SplitContainer2.SplitterDistance = 198
        Me.SplitContainer2.TabIndex = 58
        '
        'Xl_GoogleMaps1
        '
        Me.Xl_GoogleMaps1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_GoogleMaps1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_GoogleMaps1.Name = "Xl_GoogleMaps1"
        Me.Xl_GoogleMaps1.Size = New System.Drawing.Size(409, 198)
        Me.Xl_GoogleMaps1.TabIndex = 0
        '
        'Xl_StreetView1
        '
        Me.Xl_StreetView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_StreetView1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_StreetView1.Name = "Xl_StreetView1"
        Me.Xl_StreetView1.Size = New System.Drawing.Size(409, 200)
        Me.Xl_StreetView1.TabIndex = 0
        '
        'Frm_Address
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(900, 433)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Address"
        Me.Text = "Adreça postal"
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents ButtonCoordenadasReset As Button
    Friend WithEvents TextBoxPiso As TextBox
    Friend WithEvents TextBoxCoordenadas As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxNumero As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxVia As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxAdr As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Xl_GoogleMaps1 As Xl_GoogleMaps
    Friend WithEvents Xl_StreetView1 As Xl_StreetView
    Friend WithEvents Xl_Lookup_Zip1 As Xl_LookupZip
    Friend WithEvents ComboBoxPlaceNames As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents ComboBoxCountry As ComboBox
    Friend WithEvents TextBoxZip As TextBox
End Class
