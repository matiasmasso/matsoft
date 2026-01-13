<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Adr
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TextBoxCoordenadas = New System.Windows.Forms.TextBox()
        Me.ButtonCoordenadasReset = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxPiso = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxNumero = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxVia = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_Zip1 = New Winforms.Xl_Zip()
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
        Me.Panel1.TabIndex = 45
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
        'TextBoxCoordenadas
        '
        Me.TextBoxCoordenadas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCoordenadas.Location = New System.Drawing.Point(12, 207)
        Me.TextBoxCoordenadas.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxCoordenadas.MaxLength = 50
        Me.TextBoxCoordenadas.Name = "TextBoxCoordenadas"
        Me.TextBoxCoordenadas.ReadOnly = True
        Me.TextBoxCoordenadas.Size = New System.Drawing.Size(379, 20)
        Me.TextBoxCoordenadas.TabIndex = 53
        '
        'ButtonCoordenadasReset
        '
        Me.ButtonCoordenadasReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCoordenadasReset.Location = New System.Drawing.Point(395, 205)
        Me.ButtonCoordenadasReset.Name = "ButtonCoordenadasReset"
        Me.ButtonCoordenadasReset.Size = New System.Drawing.Size(69, 23)
        Me.ButtonCoordenadasReset.TabIndex = 55
        Me.ButtonCoordenadasReset.Text = "reconfirmar"
        Me.ButtonCoordenadasReset.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Zip1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxAdr)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(900, 402)
        Me.SplitContainer1.SplitterDistance = 480
        Me.SplitContainer1.TabIndex = 57
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(10, 193)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 13)
        Me.Label7.TabIndex = 64
        Me.Label7.Text = "Coordinades"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(61, 61)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(0, 13)
        Me.Label6.TabIndex = 63
        '
        'TextBoxPiso
        '
        Me.TextBoxPiso.Location = New System.Drawing.Point(12, 158)
        Me.TextBoxPiso.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxPiso.MaxLength = 50
        Me.TextBoxPiso.Name = "TextBoxPiso"
        Me.TextBoxPiso.Size = New System.Drawing.Size(127, 20)
        Me.TextBoxPiso.TabIndex = 56
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 144)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 13)
        Me.Label5.TabIndex = 62
        Me.Label5.Text = "Pis (per exemple 2º 1ª)"
        '
        'TextBoxNumero
        '
        Me.TextBoxNumero.Location = New System.Drawing.Point(12, 117)
        Me.TextBoxNumero.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxNumero.MaxLength = 50
        Me.TextBoxNumero.Name = "TextBoxNumero"
        Me.TextBoxNumero.Size = New System.Drawing.Size(127, 20)
        Me.TextBoxNumero.TabIndex = 55
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(176, 13)
        Me.Label4.TabIndex = 61
        Me.Label4.Text = "Número (per exemple 403 o Km.2,5)"
        '
        'TextBoxVia
        '
        Me.TextBoxVia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxVia.Location = New System.Drawing.Point(12, 76)
        Me.TextBoxVia.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxVia.MaxLength = 50
        Me.TextBoxVia.Name = "TextBoxVia"
        Me.TextBoxVia.Size = New System.Drawing.Size(452, 20)
        Me.TextBoxVia.TabIndex = 53
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(201, 13)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Via pública (per exemple Avda. Diagonal)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "població"
        '
        'Xl_Zip1
        '
        Me.Xl_Zip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Zip1.Location = New System.Drawing.Point(12, 30)
        Me.Xl_Zip1.Name = "Xl_Zip1"
        Me.Xl_Zip1.Size = New System.Drawing.Size(451, 20)
        Me.Xl_Zip1.TabIndex = 59
        Me.Xl_Zip1.Zip = Nothing
        '
        'TextBoxAdr
        '
        Me.TextBoxAdr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAdr.Location = New System.Drawing.Point(11, 286)
        Me.TextBoxAdr.MaxLength = 250
        Me.TextBoxAdr.Multiline = True
        Me.TextBoxAdr.Name = "TextBoxAdr"
        Me.TextBoxAdr.Size = New System.Drawing.Size(453, 47)
        Me.TextBoxAdr.TabIndex = 58
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 270)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(201, 13)
        Me.Label1.TabIndex = 54
        Me.Label1.Text = "adreça (tal com s'hauria de veure escrita)"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer2.Location = New System.Drawing.Point(1, 0)
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
        Me.SplitContainer2.TabIndex = 57
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
        'Frm_Adr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(900, 433)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Adr"
        Me.Text = "ADREÇA"
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TextBoxCoordenadas As TextBox
    Friend WithEvents ButtonCoordenadasReset As Button
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxPiso As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxNumero As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxVia As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_Zip1 As Xl_Zip
    Friend WithEvents TextBoxAdr As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Xl_GoogleMaps1 As Xl_GoogleMaps
    Friend WithEvents Xl_StreetView1 As Xl_StreetView
End Class