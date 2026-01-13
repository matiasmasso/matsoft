<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_IntlAgent
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
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_ContactAgent = New Xl_Contact2()
        Me.Xl_ContactPrincipal = New Xl_Contact2()
        Me.Xl_Lookup_Area1 = New Xl_LookupArea()
        Me.CheckBoxObsolet = New System.Windows.Forms.CheckBox()
        Me.LabelFchCreated = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 231)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(420, 31)
        Me.Panel1.TabIndex = 46
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(201, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(312, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
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
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "representada"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "distribuidor"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 51
        Me.Label3.Text = "area"
        '
        'Xl_ContactAgent
        '
        Me.Xl_ContactAgent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactAgent.Location = New System.Drawing.Point(114, 61)
        Me.Xl_ContactAgent.Name = "Xl_ContactAgent"
        Me.Xl_ContactAgent.Size = New System.Drawing.Size(302, 20)
        Me.Xl_ContactAgent.TabIndex = 49
        '
        'Xl_ContactPrincipal
        '
        Me.Xl_ContactPrincipal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactPrincipal.Location = New System.Drawing.Point(114, 35)
        Me.Xl_ContactPrincipal.Name = "Xl_ContactPrincipal"
        Me.Xl_ContactPrincipal.Size = New System.Drawing.Size(302, 20)
        Me.Xl_ContactPrincipal.TabIndex = 47
        '
        'Xl_Lookup_Area1
        '
        Me.Xl_Lookup_Area1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Lookup_Area1.Area = Nothing
        Me.Xl_Lookup_Area1.IsDirty = False
        Me.Xl_Lookup_Area1.Location = New System.Drawing.Point(114, 86)
        Me.Xl_Lookup_Area1.Name = "Xl_Lookup_Area1"
        Me.Xl_Lookup_Area1.Size = New System.Drawing.Size(302, 20)
        Me.Xl_Lookup_Area1.TabIndex = 52
        Me.Xl_Lookup_Area1.Value = Nothing
        '
        'CheckBoxObsolet
        '
        Me.CheckBoxObsolet.AutoSize = True
        Me.CheckBoxObsolet.Location = New System.Drawing.Point(117, 144)
        Me.CheckBoxObsolet.Name = "CheckBoxObsolet"
        Me.CheckBoxObsolet.Size = New System.Drawing.Size(68, 17)
        Me.CheckBoxObsolet.TabIndex = 53
        Me.CheckBoxObsolet.Text = "Obsoleto"
        Me.CheckBoxObsolet.UseVisualStyleBackColor = True
        '
        'LabelFchCreated
        '
        Me.LabelFchCreated.AutoSize = True
        Me.LabelFchCreated.Location = New System.Drawing.Point(114, 118)
        Me.LabelFchCreated.Name = "LabelFchCreated"
        Me.LabelFchCreated.Size = New System.Drawing.Size(34, 13)
        Me.LabelFchCreated.TabIndex = 54
        Me.LabelFchCreated.Text = "(data)"
        '
        'Frm_IntlAgent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 262)
        Me.Controls.Add(Me.LabelFchCreated)
        Me.Controls.Add(Me.CheckBoxObsolet)
        Me.Controls.Add(Me.Xl_Lookup_Area1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_ContactAgent)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_ContactPrincipal)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_IntlAgent"
        Me.Text = "Frm_IntlAgent"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Xl_ContactPrincipal As Xl_Contact2
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactAgent As Xl_Contact2
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_Lookup_Area1 As Xl_LookupArea
    Friend WithEvents CheckBoxObsolet As System.Windows.Forms.CheckBox
    Friend WithEvents LabelFchCreated As System.Windows.Forms.Label
End Class
