<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_CreateContact_StepCustomer
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.TextBoxRaoSocial = New System.Windows.Forms.TextBox()
        Me.TextBoxNomComercial = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelNif = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CheckBoxIva = New System.Windows.Forms.CheckBox()
        Me.CheckBoxReq = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupAddress1 = New Winforms.Xl_LookupAddress()
        Me.Xl_LookupNif1 = New Winforms.Xl_LookupNif()
        Me.Xl_LookupNif2 = New Winforms.Xl_LookupNif()
        Me.LabelNif2 = New System.Windows.Forms.Label()
        Me.Xl_LookupIncoterm1 = New Winforms.Xl_LookupIncoterm()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Rao Social"
        '
        'TextBoxRaoSocial
        '
        Me.TextBoxRaoSocial.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxRaoSocial.Location = New System.Drawing.Point(115, 15)
        Me.TextBoxRaoSocial.MaxLength = 50
        Me.TextBoxRaoSocial.Name = "TextBoxRaoSocial"
        Me.TextBoxRaoSocial.Size = New System.Drawing.Size(215, 20)
        Me.TextBoxRaoSocial.TabIndex = 0
        '
        'TextBoxNomComercial
        '
        Me.TextBoxNomComercial.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomComercial.Location = New System.Drawing.Point(115, 41)
        Me.TextBoxNomComercial.MaxLength = 50
        Me.TextBoxNomComercial.Name = "TextBoxNomComercial"
        Me.TextBoxNomComercial.Size = New System.Drawing.Size(215, 20)
        Me.TextBoxNomComercial.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Nom Comercial"
        '
        'LabelNif
        '
        Me.LabelNif.AutoSize = True
        Me.LabelNif.Location = New System.Drawing.Point(14, 97)
        Me.LabelNif.Name = "LabelNif"
        Me.LabelNif.Size = New System.Drawing.Size(20, 13)
        Me.LabelNif.TabIndex = 4
        Me.LabelNif.Text = "Nif"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 70)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Adreça"
        '
        'CheckBoxIva
        '
        Me.CheckBoxIva.AutoSize = True
        Me.CheckBoxIva.Location = New System.Drawing.Point(114, 178)
        Me.CheckBoxIva.Name = "CheckBoxIva"
        Me.CheckBoxIva.Size = New System.Drawing.Size(41, 17)
        Me.CheckBoxIva.TabIndex = 6
        Me.CheckBoxIva.Text = "Iva"
        Me.CheckBoxIva.UseVisualStyleBackColor = True
        '
        'CheckBoxReq
        '
        Me.CheckBoxReq.AutoSize = True
        Me.CheckBoxReq.Location = New System.Drawing.Point(161, 178)
        Me.CheckBoxReq.Name = "CheckBoxReq"
        Me.CheckBoxReq.Size = New System.Drawing.Size(141, 17)
        Me.CheckBoxReq.TabIndex = 7
        Me.CheckBoxReq.Text = "Recàrrec d'equivalència"
        Me.CheckBoxReq.UseVisualStyleBackColor = True
        '
        'Xl_LookupAddress1
        '
        Me.Xl_LookupAddress1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupAddress1.IsDirty = False
        Me.Xl_LookupAddress1.Location = New System.Drawing.Point(115, 67)
        Me.Xl_LookupAddress1.Name = "Xl_LookupAddress1"
        Me.Xl_LookupAddress1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupAddress1.ReadOnlyLookup = False
        Me.Xl_LookupAddress1.Size = New System.Drawing.Size(215, 20)
        Me.Xl_LookupAddress1.TabIndex = 2
        Me.Xl_LookupAddress1.Value = Nothing
        '
        'Xl_LookupNif1
        '
        Me.Xl_LookupNif1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupNif1.IsDirty = False
        Me.Xl_LookupNif1.Location = New System.Drawing.Point(115, 93)
        Me.Xl_LookupNif1.Name = "Xl_LookupNif1"
        Me.Xl_LookupNif1.Nif = Nothing
        Me.Xl_LookupNif1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupNif1.ReadOnlyLookup = False
        Me.Xl_LookupNif1.Size = New System.Drawing.Size(215, 20)
        Me.Xl_LookupNif1.TabIndex = 3
        Me.Xl_LookupNif1.Value = Nothing
        '
        'Xl_LookupNif2
        '
        Me.Xl_LookupNif2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupNif2.IsDirty = False
        Me.Xl_LookupNif2.Location = New System.Drawing.Point(116, 121)
        Me.Xl_LookupNif2.Name = "Xl_LookupNif2"
        Me.Xl_LookupNif2.Nif = Nothing
        Me.Xl_LookupNif2.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupNif2.ReadOnlyLookup = False
        Me.Xl_LookupNif2.Size = New System.Drawing.Size(215, 20)
        Me.Xl_LookupNif2.TabIndex = 4
        Me.Xl_LookupNif2.Value = Nothing
        Me.Xl_LookupNif2.Visible = False
        '
        'LabelNif2
        '
        Me.LabelNif2.AutoSize = True
        Me.LabelNif2.Location = New System.Drawing.Point(15, 125)
        Me.LabelNif2.Name = "LabelNif2"
        Me.LabelNif2.Size = New System.Drawing.Size(20, 13)
        Me.LabelNif2.TabIndex = 18
        Me.LabelNif2.Text = "Nif"
        '
        'Xl_LookupIncoterm1
        '
        Me.Xl_LookupIncoterm1.FormattingEnabled = True
        Me.Xl_LookupIncoterm1.Location = New System.Drawing.Point(115, 147)
        Me.Xl_LookupIncoterm1.Name = "Xl_LookupIncoterm1"
        Me.Xl_LookupIncoterm1.Size = New System.Drawing.Size(73, 21)
        Me.Xl_LookupIncoterm1.TabIndex = 5
        Me.Xl_LookupIncoterm1.Value = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 150)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Incoterm"
        '
        'Xl_CreateContact_StepCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_LookupIncoterm1)
        Me.Controls.Add(Me.Xl_LookupNif2)
        Me.Controls.Add(Me.LabelNif2)
        Me.Controls.Add(Me.Xl_LookupNif1)
        Me.Controls.Add(Me.CheckBoxReq)
        Me.Controls.Add(Me.CheckBoxIva)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_LookupAddress1)
        Me.Controls.Add(Me.LabelNif)
        Me.Controls.Add(Me.TextBoxNomComercial)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxRaoSocial)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Xl_CreateContact_StepCustomer"
        Me.Size = New System.Drawing.Size(347, 263)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxRaoSocial As TextBox
    Friend WithEvents TextBoxNomComercial As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents LabelNif As Label
    Friend WithEvents Xl_LookupAddress1 As Xl_LookupAddress
    Friend WithEvents Label4 As Label
    Friend WithEvents CheckBoxIva As CheckBox
    Friend WithEvents CheckBoxReq As CheckBox
    Friend WithEvents Xl_LookupNif1 As Xl_LookupNif
    Friend WithEvents Xl_LookupNif2 As Xl_LookupNif
    Friend WithEvents LabelNif2 As Label
    Friend WithEvents Xl_LookupIncoterm1 As Xl_LookupIncoterm
    Friend WithEvents Label3 As Label
End Class
