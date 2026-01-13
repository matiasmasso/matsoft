<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OutVivaceExpedicio
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxId = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TextBoxFch = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LabelFchCreated = New System.Windows.Forms.Label()
        Me.TextBoxCliNom = New System.Windows.Forms.TextBox()
        Me.TextBoxTrpNom = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxBultos = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxM3 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxKg = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Xl_OutVivaceCarrecs1 = New Winforms.Xl_OutVivaceCarrecs()
        Me.TextBoxValor = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxCostLogistic = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxRate = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_OutVivaceCarrecs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Id"
        '
        'TextBoxId
        '
        Me.TextBoxId.Location = New System.Drawing.Point(87, 18)
        Me.TextBoxId.Name = "TextBoxId"
        Me.TextBoxId.ReadOnly = True
        Me.TextBoxId.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxId.TabIndex = 1
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(-1, 29)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(540, 311)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxRate)
        Me.TabPage1.Controls.Add(Me.TextBoxCostLogistic)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.TextBoxValor)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.TextBoxKg)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.TextBoxM3)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.TextBoxBultos)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxTrpNom)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.LabelFchCreated)
        Me.TabPage1.Controls.Add(Me.TextBoxCliNom)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBoxFch)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBoxId)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(532, 285)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_OutVivaceCarrecs1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(532, 271)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Carrecs"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TextBoxFch
        '
        Me.TextBoxFch.Location = New System.Drawing.Point(87, 44)
        Me.TextBoxFch.Name = "TextBoxFch"
        Me.TextBoxFch.ReadOnly = True
        Me.TextBoxFch.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFch.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "data"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "destinació"
        '
        'LabelFchCreated
        '
        Me.LabelFchCreated.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFchCreated.AutoSize = True
        Me.LabelFchCreated.Location = New System.Drawing.Point(10, 269)
        Me.LabelFchCreated.Name = "LabelFchCreated"
        Me.LabelFchCreated.Size = New System.Drawing.Size(59, 13)
        Me.LabelFchCreated.TabIndex = 6
        Me.LabelFchCreated.Text = "(registrat...)"
        '
        'TextBoxCliNom
        '
        Me.TextBoxCliNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCliNom.Location = New System.Drawing.Point(87, 70)
        Me.TextBoxCliNom.Name = "TextBoxCliNom"
        Me.TextBoxCliNom.ReadOnly = True
        Me.TextBoxCliNom.Size = New System.Drawing.Size(424, 20)
        Me.TextBoxCliNom.TabIndex = 5
        '
        'TextBoxTrpNom
        '
        Me.TextBoxTrpNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTrpNom.Location = New System.Drawing.Point(87, 96)
        Me.TextBoxTrpNom.Name = "TextBoxTrpNom"
        Me.TextBoxTrpNom.ReadOnly = True
        Me.TextBoxTrpNom.Size = New System.Drawing.Size(424, 20)
        Me.TextBoxTrpNom.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 99)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "transportista"
        '
        'TextBoxBultos
        '
        Me.TextBoxBultos.Location = New System.Drawing.Point(87, 122)
        Me.TextBoxBultos.Name = "TextBoxBultos"
        Me.TextBoxBultos.ReadOnly = True
        Me.TextBoxBultos.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxBultos.TabIndex = 10
        Me.TextBoxBultos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 125)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "bultos"
        '
        'TextBoxM3
        '
        Me.TextBoxM3.Location = New System.Drawing.Point(87, 148)
        Me.TextBoxM3.Name = "TextBoxM3"
        Me.TextBoxM3.ReadOnly = True
        Me.TextBoxM3.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxM3.TabIndex = 12
        Me.TextBoxM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 151)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "volum"
        '
        'TextBoxKg
        '
        Me.TextBoxKg.Location = New System.Drawing.Point(87, 174)
        Me.TextBoxKg.Name = "TextBoxKg"
        Me.TextBoxKg.ReadOnly = True
        Me.TextBoxKg.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxKg.TabIndex = 14
        Me.TextBoxKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 177)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(24, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "pes"
        '
        'Xl_OutVivaceCarrecs1
        '
        Me.Xl_OutVivaceCarrecs1.AllowUserToAddRows = False
        Me.Xl_OutVivaceCarrecs1.AllowUserToDeleteRows = False
        Me.Xl_OutVivaceCarrecs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_OutVivaceCarrecs1.DisplayObsolets = False
        Me.Xl_OutVivaceCarrecs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_OutVivaceCarrecs1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_OutVivaceCarrecs1.MouseIsDown = False
        Me.Xl_OutVivaceCarrecs1.Name = "Xl_OutVivaceCarrecs1"
        Me.Xl_OutVivaceCarrecs1.ReadOnly = True
        Me.Xl_OutVivaceCarrecs1.Size = New System.Drawing.Size(526, 265)
        Me.Xl_OutVivaceCarrecs1.TabIndex = 0
        '
        'TextBoxValor
        '
        Me.TextBoxValor.Location = New System.Drawing.Point(87, 228)
        Me.TextBoxValor.Name = "TextBoxValor"
        Me.TextBoxValor.ReadOnly = True
        Me.TextBoxValor.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxValor.TabIndex = 16
        Me.TextBoxValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(157, 212)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "valor"
        '
        'TextBoxCostLogistic
        '
        Me.TextBoxCostLogistic.Location = New System.Drawing.Point(220, 228)
        Me.TextBoxCostLogistic.Name = "TextBoxCostLogistic"
        Me.TextBoxCostLogistic.ReadOnly = True
        Me.TextBoxCostLogistic.Size = New System.Drawing.Size(59, 20)
        Me.TextBoxCostLogistic.TabIndex = 18
        Me.TextBoxCostLogistic.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(217, 212)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "cost logistic"
        '
        'TextBoxRate
        '
        Me.TextBoxRate.Location = New System.Drawing.Point(285, 228)
        Me.TextBoxRate.Name = "TextBoxRate"
        Me.TextBoxRate.ReadOnly = True
        Me.TextBoxRate.Size = New System.Drawing.Size(50, 20)
        Me.TextBoxRate.TabIndex = 20
        Me.TextBoxRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Frm_OutVivaceExpedicio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(539, 338)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_OutVivaceExpedicio"
        Me.Text = "Expedicio"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_OutVivaceCarrecs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxId As TextBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TextBoxKg As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxM3 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxBultos As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxTrpNom As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents LabelFchCreated As Label
    Friend WithEvents TextBoxCliNom As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxFch As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_OutVivaceCarrecs1 As Xl_OutVivaceCarrecs
    Friend WithEvents TextBoxRate As TextBox
    Friend WithEvents TextBoxCostLogistic As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBoxValor As TextBox
    Friend WithEvents Label4 As Label
End Class
