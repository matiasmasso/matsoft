<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ECITransmGroups
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
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_Contact2Platform = New Xl_Contact2()
        Me.Xl_Contacts_Editable1 = New Xl_Contacts_Editable()
        Me.CheckBoxCentres = New System.Windows.Forms.CheckBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_ECITransmGroups1 = New Xl_ECITransmGroups()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.Xl_Contacts_Editable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_ECITransmGroups1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(80, 13)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(330, 20)
        Me.TextBoxNom.TabIndex = 51
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Nom:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Plataforma:"
        '
        'Xl_Contact2Platform
        '
        Me.Xl_Contact2Platform.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact2Platform.Contact = Nothing
        Me.Xl_Contact2Platform.Emp = Nothing
        Me.Xl_Contact2Platform.Location = New System.Drawing.Point(80, 80)
        Me.Xl_Contact2Platform.Name = "Xl_Contact2Platform"
        Me.Xl_Contact2Platform.ReadOnly = False
        Me.Xl_Contact2Platform.Size = New System.Drawing.Size(330, 20)
        Me.Xl_Contact2Platform.TabIndex = 54
        '
        'Xl_Contacts_Editable1
        '
        Me.Xl_Contacts_Editable1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contacts_Editable1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Contacts_Editable1.DisplayObsolets = False
        Me.Xl_Contacts_Editable1.Filter = Nothing
        Me.Xl_Contacts_Editable1.Location = New System.Drawing.Point(17, 146)
        Me.Xl_Contacts_Editable1.MouseIsDown = False
        Me.Xl_Contacts_Editable1.Name = "Xl_Contacts_Editable1"
        Me.Xl_Contacts_Editable1.Size = New System.Drawing.Size(393, 284)
        Me.Xl_Contacts_Editable1.TabIndex = 55
        '
        'CheckBoxCentres
        '
        Me.CheckBoxCentres.AutoSize = True
        Me.CheckBoxCentres.Location = New System.Drawing.Point(17, 123)
        Me.CheckBoxCentres.Name = "CheckBoxCentres"
        Me.CheckBoxCentres.Size = New System.Drawing.Size(175, 17)
        Me.CheckBoxCentres.TabIndex = 56
        Me.CheckBoxCentres.Text = "i que estiguin a la següent llista:"
        Me.CheckBoxCentres.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_ECITransmGroups1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_Contacts_Editable1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CheckBoxCentres)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxNom)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_Contact2Platform)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Size = New System.Drawing.Size(652, 444)
        Me.SplitContainer1.SplitterDistance = 232
        Me.SplitContainer1.TabIndex = 57
        '
        'Xl_ECITransmGroups1
        '
        Me.Xl_ECITransmGroups1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ECITransmGroups1.DisplayObsolets = False
        Me.Xl_ECITransmGroups1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ECITransmGroups1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ECITransmGroups1.MouseIsDown = False
        Me.Xl_ECITransmGroups1.Name = "Xl_ECITransmGroups1"
        Me.Xl_ECITransmGroups1.Size = New System.Drawing.Size(232, 444)
        Me.Xl_ECITransmGroups1.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(271, 13)
        Me.Label3.TabIndex = 57
        Me.Label3.Text = "Inclou els centres amb destinació la següent plataforma:"
        '
        'Frm_ECITransmGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(652, 444)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_ECITransmGroup"
        Me.Text = "El Corte Inglés - Agrupació de Centres per transmisio al magatzem"
        CType(Me.Xl_Contacts_Editable1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_ECITransmGroups1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_Contact2Platform As Xl_Contact2
    Friend WithEvents Xl_Contacts_Editable1 As Xl_Contacts_Editable
    Friend WithEvents CheckBoxCentres As CheckBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_ECITransmGroups1 As Xl_ECITransmGroups
    Friend WithEvents Label3 As Label
End Class
