<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EciTransmGroup
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_Contacts_Editable1 = New Winforms.Xl_Contacts_Editable()
        Me.CheckBoxCentres = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Xl_Contact2Platform = New Winforms.Xl_Contact2()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.Xl_Contacts_Editable1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(271, 13)
        Me.Label3.TabIndex = 64
        Me.Label3.Text = "Inclou els centres amb destinació la següent plataforma:"
        '
        'Xl_Contacts_Editable1
        '
        Me.Xl_Contacts_Editable1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contacts_Editable1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Contacts_Editable1.DisplayObsolets = False
        Me.Xl_Contacts_Editable1.Filter = Nothing
        Me.Xl_Contacts_Editable1.Location = New System.Drawing.Point(19, 145)
        Me.Xl_Contacts_Editable1.MouseIsDown = False
        Me.Xl_Contacts_Editable1.Name = "Xl_Contacts_Editable1"
        Me.Xl_Contacts_Editable1.Size = New System.Drawing.Size(342, 284)
        Me.Xl_Contacts_Editable1.TabIndex = 62
        '
        'CheckBoxCentres
        '
        Me.CheckBoxCentres.AutoSize = True
        Me.CheckBoxCentres.Location = New System.Drawing.Point(19, 122)
        Me.CheckBoxCentres.Name = "CheckBoxCentres"
        Me.CheckBoxCentres.Size = New System.Drawing.Size(175, 17)
        Me.CheckBoxCentres.TabIndex = 63
        Me.CheckBoxCentres.Text = "i que estiguin a la següent llista:"
        Me.CheckBoxCentres.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 58
        Me.Label1.Text = "Nom:"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(82, 12)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(279, 20)
        Me.TextBoxNom.TabIndex = 59
        '
        'Xl_Contact2Platform
        '
        Me.Xl_Contact2Platform.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact2Platform.Contact = Nothing
        Me.Xl_Contact2Platform.Emp = Nothing
        Me.Xl_Contact2Platform.Location = New System.Drawing.Point(82, 79)
        Me.Xl_Contact2Platform.Name = "Xl_Contact2Platform"
        Me.Xl_Contact2Platform.ReadOnly = False
        Me.Xl_Contact2Platform.Size = New System.Drawing.Size(279, 20)
        Me.Xl_Contact2Platform.TabIndex = 61
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Plataforma:"
        '
        'Frm_EciTransmGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(373, 450)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_Contacts_Editable1)
        Me.Controls.Add(Me.CheckBoxCentres)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Xl_Contact2Platform)
        Me.Controls.Add(Me.Label2)
        Me.Name = "Frm_EciTransmGroup"
        Me.Text = "Grup de transmisions Eci"
        CType(Me.Xl_Contacts_Editable1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_Contacts_Editable1 As Xl_Contacts_Editable
    Friend WithEvents CheckBoxCentres As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents Xl_Contact2Platform As Xl_Contact2
    Friend WithEvents Label2 As Label
End Class
