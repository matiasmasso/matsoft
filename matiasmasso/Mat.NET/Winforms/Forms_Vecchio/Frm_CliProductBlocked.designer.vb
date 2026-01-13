<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CliProductBlocked
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
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBoxStandard = New System.Windows.Forms.PictureBox()
        Me.TextBoxLabelStandard = New System.Windows.Forms.TextBox()
        Me.RadioButtonStandard = New System.Windows.Forms.RadioButton()
        Me.ListBoxClis = New System.Windows.Forms.ListBox()
        Me.PictureBoxAltresExclusiva = New System.Windows.Forms.PictureBox()
        Me.RadioButtonAltresExclusiva = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxZip = New System.Windows.Forms.TextBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.RadioButtonExclusiva = New System.Windows.Forms.RadioButton()
        Me.RadioButtonNA = New System.Windows.Forms.RadioButton()
        Me.RadioButtonExclos = New System.Windows.Forms.RadioButton()
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox()
        Me.TextBoxCliNom = New System.Windows.Forms.TextBox()
        Me.PictureBoxDistribuidorOficial = New System.Windows.Forms.PictureBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.RadioButtonDistribuidorOficial = New System.Windows.Forms.RadioButton()
        Me.PanelDetall = New System.Windows.Forms.Panel()
        Me.Xl_LookupProduct1 = New Winforms.Xl_LookupProduct()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBoxStandard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxAltresExclusiva, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxDistribuidorOficial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelDetall.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 492)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(686, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(467, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(578, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(24, 304)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(640, 97)
        Me.TextBoxObs.TabIndex = 61
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 285)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 60
        Me.Label1.Text = "Observacions:"
        '
        'PictureBoxStandard
        '
        Me.PictureBoxStandard.Image = Global.Winforms.My.Resources.Resources.Ok
        Me.PictureBoxStandard.Location = New System.Drawing.Point(16, 15)
        Me.PictureBoxStandard.Name = "PictureBoxStandard"
        Me.PictureBoxStandard.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxStandard.TabIndex = 59
        Me.PictureBoxStandard.TabStop = False
        '
        'TextBoxLabelStandard
        '
        Me.TextBoxLabelStandard.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.TextBoxLabelStandard.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxLabelStandard.Location = New System.Drawing.Point(63, 39)
        Me.TextBoxLabelStandard.Multiline = True
        Me.TextBoxLabelStandard.Name = "TextBoxLabelStandard"
        Me.TextBoxLabelStandard.ReadOnly = True
        Me.TextBoxLabelStandard.Size = New System.Drawing.Size(239, 45)
        Me.TextBoxLabelStandard.TabIndex = 58
        Me.TextBoxLabelStandard.TabStop = False
        Me.TextBoxLabelStandard.Text = "Opció per defecte. Client habitual o potencial, sense restriccions en aquesta mar" & _
    "ca salvo que algú en tingui la exclusiva al seu codi postal"
        '
        'RadioButtonStandard
        '
        Me.RadioButtonStandard.AutoSize = True
        Me.RadioButtonStandard.Checked = True
        Me.RadioButtonStandard.Location = New System.Drawing.Point(38, 15)
        Me.RadioButtonStandard.Name = "RadioButtonStandard"
        Me.RadioButtonStandard.Size = New System.Drawing.Size(68, 17)
        Me.RadioButtonStandard.TabIndex = 57
        Me.RadioButtonStandard.TabStop = True
        Me.RadioButtonStandard.Text = "Standard"
        '
        'ListBoxClis
        '
        Me.ListBoxClis.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ListBoxClis.FormattingEnabled = True
        Me.ListBoxClis.Location = New System.Drawing.Point(423, 207)
        Me.ListBoxClis.Name = "ListBoxClis"
        Me.ListBoxClis.Size = New System.Drawing.Size(255, 69)
        Me.ListBoxClis.TabIndex = 56
        Me.ListBoxClis.TabStop = False
        '
        'PictureBoxAltresExclusiva
        '
        Me.PictureBoxAltresExclusiva.Image = Global.Winforms.My.Resources.Resources.warn
        Me.PictureBoxAltresExclusiva.Location = New System.Drawing.Point(379, 183)
        Me.PictureBoxAltresExclusiva.Name = "PictureBoxAltresExclusiva"
        Me.PictureBoxAltresExclusiva.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxAltresExclusiva.TabIndex = 55
        Me.PictureBoxAltresExclusiva.TabStop = False
        '
        'RadioButtonAltresExclusiva
        '
        Me.RadioButtonAltresExclusiva.AutoSize = True
        Me.RadioButtonAltresExclusiva.Location = New System.Drawing.Point(402, 183)
        Me.RadioButtonAltresExclusiva.Name = "RadioButtonAltresExclusiva"
        Me.RadioButtonAltresExclusiva.Size = New System.Drawing.Size(235, 17)
        Me.RadioButtonAltresExclusiva.TabIndex = 54
        Me.RadioButtonAltresExclusiva.Text = "Els següents clients ja en tenen la exclusiva:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(236, 228)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "(% comodín)"
        '
        'TextBoxZip
        '
        Me.TextBoxZip.Location = New System.Drawing.Point(138, 224)
        Me.TextBoxZip.Name = "TextBoxZip"
        Me.TextBoxZip.Size = New System.Drawing.Size(75, 20)
        Me.TextBoxZip.TabIndex = 52
        Me.TextBoxZip.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.Winforms.My.Resources.Resources.star
        Me.PictureBox4.Location = New System.Drawing.Point(16, 183)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox4.TabIndex = 51
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.Winforms.My.Resources.Resources.NoPark
        Me.PictureBox3.Location = New System.Drawing.Point(379, 99)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox3.TabIndex = 50
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Winforms.My.Resources.Resources.wrong
        Me.PictureBox2.Location = New System.Drawing.Point(379, 16)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.TabIndex = 49
        Me.PictureBox2.TabStop = False
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox3.Location = New System.Drawing.Point(63, 207)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(239, 34)
        Me.TextBox3.TabIndex = 48
        Me.TextBox3.TabStop = False
        Me.TextBox3.Text = "Gaudeix de la exclusiva de vendes en el següent codi postal:"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Location = New System.Drawing.Point(426, 123)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(252, 39)
        Me.TextBox2.TabIndex = 47
        Me.TextBox2.TabStop = False
        Me.TextBox2.Text = "El client no comercialitza aquesta marca i no cal molestar-lo amb comunicats o st" & _
    "ocks"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Location = New System.Drawing.Point(426, 39)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(252, 39)
        Me.TextBox1.TabIndex = 46
        Me.TextBox1.TabStop = False
        Me.TextBox1.Text = "No está permesa la venda de productes d'aquesta marca als clients exclosos"
        '
        'RadioButtonExclusiva
        '
        Me.RadioButtonExclusiva.AutoSize = True
        Me.RadioButtonExclusiva.Location = New System.Drawing.Point(39, 183)
        Me.RadioButtonExclusiva.Name = "RadioButtonExclusiva"
        Me.RadioButtonExclusiva.Size = New System.Drawing.Size(70, 17)
        Me.RadioButtonExclusiva.TabIndex = 45
        Me.RadioButtonExclusiva.Text = "Exclusiva"
        '
        'RadioButtonNA
        '
        Me.RadioButtonNA.AutoSize = True
        Me.RadioButtonNA.Location = New System.Drawing.Point(402, 99)
        Me.RadioButtonNA.Name = "RadioButtonNA"
        Me.RadioButtonNA.Size = New System.Drawing.Size(85, 17)
        Me.RadioButtonNA.TabIndex = 44
        Me.RadioButtonNA.Text = "No Aplicable"
        '
        'RadioButtonExclos
        '
        Me.RadioButtonExclos.AutoSize = True
        Me.RadioButtonExclos.Location = New System.Drawing.Point(402, 15)
        Me.RadioButtonExclos.Name = "RadioButtonExclos"
        Me.RadioButtonExclos.Size = New System.Drawing.Size(56, 17)
        Me.RadioButtonExclos.TabIndex = 43
        Me.RadioButtonExclos.Text = "Exclos"
        '
        'PictureBoxLogo
        '
        Me.PictureBoxLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxLogo.Location = New System.Drawing.Point(532, 14)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxLogo.TabIndex = 42
        Me.PictureBoxLogo.TabStop = False
        '
        'TextBoxCliNom
        '
        Me.TextBoxCliNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCliNom.Location = New System.Drawing.Point(12, 14)
        Me.TextBoxCliNom.Name = "TextBoxCliNom"
        Me.TextBoxCliNom.ReadOnly = True
        Me.TextBoxCliNom.Size = New System.Drawing.Size(514, 20)
        Me.TextBoxCliNom.TabIndex = 65
        '
        'PictureBoxDistribuidorOficial
        '
        Me.PictureBoxDistribuidorOficial.Image = Global.Winforms.My.Resources.Resources.star_green
        Me.PictureBoxDistribuidorOficial.Location = New System.Drawing.Point(16, 99)
        Me.PictureBoxDistribuidorOficial.Name = "PictureBoxDistribuidorOficial"
        Me.PictureBoxDistribuidorOficial.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxDistribuidorOficial.TabIndex = 68
        Me.PictureBoxDistribuidorOficial.TabStop = False
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox4.Location = New System.Drawing.Point(63, 123)
        Me.TextBox4.Multiline = True
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(252, 39)
        Me.TextBox4.TabIndex = 67
        Me.TextBox4.TabStop = False
        Me.TextBox4.Text = "té acces a tarifes i stocks, i surt sempre a publicitat independentment del temps que faci que ha comprat, fins que algú el tregui d'aqui"
        '
        'RadioButtonDistribuidorOficial
        '
        Me.RadioButtonDistribuidorOficial.AutoSize = True
        Me.RadioButtonDistribuidorOficial.Location = New System.Drawing.Point(39, 99)
        Me.RadioButtonDistribuidorOficial.Name = "RadioButtonDistribuidorOficial"
        Me.RadioButtonDistribuidorOficial.Size = New System.Drawing.Size(109, 17)
        Me.RadioButtonDistribuidorOficial.TabIndex = 66
        Me.RadioButtonDistribuidorOficial.Text = "Distribuidor Oficial"
        '
        'PanelDetall
        '
        Me.PanelDetall.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelDetall.Controls.Add(Me.TextBoxZip)
        Me.PanelDetall.Controls.Add(Me.TextBoxObs)
        Me.PanelDetall.Controls.Add(Me.PictureBoxDistribuidorOficial)
        Me.PanelDetall.Controls.Add(Me.Label1)
        Me.PanelDetall.Controls.Add(Me.TextBox4)
        Me.PanelDetall.Controls.Add(Me.RadioButtonExclos)
        Me.PanelDetall.Controls.Add(Me.RadioButtonDistribuidorOficial)
        Me.PanelDetall.Controls.Add(Me.RadioButtonNA)
        Me.PanelDetall.Controls.Add(Me.RadioButtonExclusiva)
        Me.PanelDetall.Controls.Add(Me.TextBox1)
        Me.PanelDetall.Controls.Add(Me.PictureBoxStandard)
        Me.PanelDetall.Controls.Add(Me.TextBox2)
        Me.PanelDetall.Controls.Add(Me.TextBoxLabelStandard)
        Me.PanelDetall.Controls.Add(Me.TextBox3)
        Me.PanelDetall.Controls.Add(Me.RadioButtonStandard)
        Me.PanelDetall.Controls.Add(Me.PictureBox2)
        Me.PanelDetall.Controls.Add(Me.ListBoxClis)
        Me.PanelDetall.Controls.Add(Me.PictureBox3)
        Me.PanelDetall.Controls.Add(Me.PictureBoxAltresExclusiva)
        Me.PanelDetall.Controls.Add(Me.PictureBox4)
        Me.PanelDetall.Controls.Add(Me.RadioButtonAltresExclusiva)
        Me.PanelDetall.Controls.Add(Me.Label2)
        Me.PanelDetall.Location = New System.Drawing.Point(0, 74)
        Me.PanelDetall.Name = "PanelDetall"
        Me.PanelDetall.Size = New System.Drawing.Size(685, 413)
        Me.PanelDetall.TabIndex = 69
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(12, 41)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(514, 20)
        Me.Xl_LookupProduct1.TabIndex = 70
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Frm_CliProductBlocked
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 523)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.PanelDetall)
        Me.Controls.Add(Me.TextBoxCliNom)
        Me.Controls.Add(Me.PictureBoxLogo)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_CliProductBlocked"
        Me.Text = "EXCLUSIVES"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBoxStandard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxAltresExclusiva, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxDistribuidorOficial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelDetall.ResumeLayout(False)
        Me.PanelDetall.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBoxStandard As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxLabelStandard As System.Windows.Forms.TextBox
    Friend WithEvents RadioButtonStandard As System.Windows.Forms.RadioButton
    Friend WithEvents ListBoxClis As System.Windows.Forms.ListBox
    Friend WithEvents PictureBoxAltresExclusiva As System.Windows.Forms.PictureBox
    Friend WithEvents RadioButtonAltresExclusiva As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxZip As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents RadioButtonExclusiva As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonNA As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonExclos As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBoxLogo As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxCliNom As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxDistribuidorOficial As System.Windows.Forms.PictureBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents RadioButtonDistribuidorOficial As System.Windows.Forms.RadioButton
    Friend WithEvents PanelDetall As System.Windows.Forms.Panel
    Friend WithEvents Xl_LookupProduct1 As Winforms.Xl_LookupProduct
End Class
