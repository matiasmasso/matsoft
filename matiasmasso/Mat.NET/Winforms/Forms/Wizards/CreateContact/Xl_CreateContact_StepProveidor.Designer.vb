<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_CreateContact_StepProveidor
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
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_LookupAddress1 = New Winforms.Xl_LookupAddress()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxNomComercial = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxRaoSocial = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_LookupNif1 = New Winforms.Xl_LookupNif()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_LookupIncoterm1 = New Winforms.Xl_LookupIncoterm()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Adreça"
        '
        'Xl_LookupAddress1
        '
        Me.Xl_LookupAddress1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupAddress1.IsDirty = False
        Me.Xl_LookupAddress1.Location = New System.Drawing.Point(115, 95)
        Me.Xl_LookupAddress1.Name = "Xl_LookupAddress1"
        Me.Xl_LookupAddress1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupAddress1.ReadOnlyLookup = False
        Me.Xl_LookupAddress1.Size = New System.Drawing.Size(215, 20)
        Me.Xl_LookupAddress1.TabIndex = 22
        Me.Xl_LookupAddress1.Value = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Nif"
        '
        'TextBoxNomComercial
        '
        Me.TextBoxNomComercial.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomComercial.Location = New System.Drawing.Point(115, 42)
        Me.TextBoxNomComercial.Name = "TextBoxNomComercial"
        Me.TextBoxNomComercial.Size = New System.Drawing.Size(215, 20)
        Me.TextBoxNomComercial.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Nom Comercial"
        '
        'TextBoxRaoSocial
        '
        Me.TextBoxRaoSocial.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxRaoSocial.Location = New System.Drawing.Point(115, 16)
        Me.TextBoxRaoSocial.Name = "TextBoxRaoSocial"
        Me.TextBoxRaoSocial.Size = New System.Drawing.Size(215, 20)
        Me.TextBoxRaoSocial.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Rao Social"
        '
        'Xl_LookupNif1
        '
        Me.Xl_LookupNif1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupNif1.IsDirty = False
        Me.Xl_LookupNif1.Location = New System.Drawing.Point(115, 69)
        Me.Xl_LookupNif1.Name = "Xl_LookupNif1"
        Me.Xl_LookupNif1.Nif = Nothing
        Me.Xl_LookupNif1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupNif1.ReadOnlyLookup = False
        Me.Xl_LookupNif1.Size = New System.Drawing.Size(215, 20)
        Me.Xl_LookupNif1.TabIndex = 24
        Me.Xl_LookupNif1.Value = Nothing
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 13)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "Incoterm"
        '
        'Xl_LookupIncoterm1
        '
        Me.Xl_LookupIncoterm1.FormattingEnabled = True
        Me.Xl_LookupIncoterm1.Location = New System.Drawing.Point(115, 121)
        Me.Xl_LookupIncoterm1.Name = "Xl_LookupIncoterm1"
        Me.Xl_LookupIncoterm1.Size = New System.Drawing.Size(73, 21)
        Me.Xl_LookupIncoterm1.TabIndex = 25
        Me.Xl_LookupIncoterm1.Value = Nothing
        '
        'Xl_CreateContact_StepProveidor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_LookupIncoterm1)
        Me.Controls.Add(Me.Xl_LookupNif1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_LookupAddress1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxNomComercial)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxRaoSocial)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Xl_CreateContact_StepProveidor"
        Me.Size = New System.Drawing.Size(347, 263)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_LookupAddress1 As Xl_LookupAddress
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxNomComercial As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxRaoSocial As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_LookupNif1 As Xl_LookupNif
    Friend WithEvents Label5 As Label
    Friend WithEvents Xl_LookupIncoterm1 As Xl_LookupIncoterm
End Class
