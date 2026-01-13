<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ProductChannel
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
        Me.RadioButtonInclos = New System.Windows.Forms.RadioButton()
        Me.RadioButtonExclos = New System.Windows.Forms.RadioButton()
        Me.Xl_LookupProduct1 = New Winforms.Xl_LookupProduct()
        Me.Xl_LookupDistributionChannel1 = New Winforms.Xl_LookupDistributionChannel()
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
        Me.Panel1.Location = New System.Drawing.Point(0, 230)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(379, 31)
        Me.Panel1.TabIndex = 55
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(160, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(271, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
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
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Producte"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "Canal"
        '
        'RadioButtonInclos
        '
        Me.RadioButtonInclos.AutoSize = True
        Me.RadioButtonInclos.Checked = True
        Me.RadioButtonInclos.Location = New System.Drawing.Point(115, 118)
        Me.RadioButtonInclos.Name = "RadioButtonInclos"
        Me.RadioButtonInclos.Size = New System.Drawing.Size(53, 17)
        Me.RadioButtonInclos.TabIndex = 58
        Me.RadioButtonInclos.TabStop = True
        Me.RadioButtonInclos.Text = "Inclos"
        Me.RadioButtonInclos.UseVisualStyleBackColor = True
        '
        'RadioButtonExclos
        '
        Me.RadioButtonExclos.AutoSize = True
        Me.RadioButtonExclos.Location = New System.Drawing.Point(115, 141)
        Me.RadioButtonExclos.Name = "RadioButtonExclos"
        Me.RadioButtonExclos.Size = New System.Drawing.Size(56, 17)
        Me.RadioButtonExclos.TabIndex = 59
        Me.RadioButtonExclos.Text = "Exclos"
        Me.RadioButtonExclos.UseVisualStyleBackColor = True
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(80, 48)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(287, 20)
        Me.Xl_LookupProduct1.TabIndex = 60
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Xl_LookupDistributionChannel1
        '
        Me.Xl_LookupDistributionChannel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupDistributionChannel1.DistributionChannel = Nothing
        Me.Xl_LookupDistributionChannel1.IsDirty = False
        Me.Xl_LookupDistributionChannel1.Location = New System.Drawing.Point(80, 76)
        Me.Xl_LookupDistributionChannel1.Name = "Xl_LookupDistributionChannel1"
        Me.Xl_LookupDistributionChannel1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupDistributionChannel1.Size = New System.Drawing.Size(287, 20)
        Me.Xl_LookupDistributionChannel1.TabIndex = 61
        Me.Xl_LookupDistributionChannel1.Value = Nothing
        '
        'Frm_ProductChannel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(379, 261)
        Me.Controls.Add(Me.Xl_LookupDistributionChannel1)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.RadioButtonExclos)
        Me.Controls.Add(Me.RadioButtonInclos)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_ProductChannel"
        Me.Text = "Canal de distribució per producte"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents RadioButtonInclos As RadioButton
    Friend WithEvents RadioButtonExclos As RadioButton
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
    Friend WithEvents Xl_LookupDistributionChannel1 As Xl_LookupDistributionChannel
End Class
