<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SkuSwitch
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonSwitch = New System.Windows.Forms.RadioButton()
        Me.RadioButtonObsolet = New System.Windows.Forms.RadioButton()
        Me.Xl_LookupProductFrom = New Winforms.Xl_LookupProduct()
        Me.Xl_LookupProductTo = New Winforms.Xl_LookupProduct()
        Me.PanelButtons.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(135, 29)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePicker1.TabIndex = 0
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Controls.Add(Me.ButtonDel)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 180)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(508, 31)
        Me.PanelButtons.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(289, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 7
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(400, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 6
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
        Me.ButtonDel.TabIndex = 8
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 60
        Me.Label1.Text = "Data"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "Producte original"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Xl_LookupProductTo)
        Me.GroupBox1.Controls.Add(Me.RadioButtonSwitch)
        Me.GroupBox1.Controls.Add(Me.RadioButtonObsolet)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 90)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(488, 76)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'RadioButtonSwitch
        '
        Me.RadioButtonSwitch.AutoSize = True
        Me.RadioButtonSwitch.Location = New System.Drawing.Point(21, 42)
        Me.RadioButtonSwitch.Name = "RadioButtonSwitch"
        Me.RadioButtonSwitch.Size = New System.Drawing.Size(92, 17)
        Me.RadioButtonSwitch.TabIndex = 4
        Me.RadioButtonSwitch.TabStop = True
        Me.RadioButtonSwitch.Text = "Substituit per: "
        Me.RadioButtonSwitch.UseVisualStyleBackColor = True
        '
        'RadioButtonObsolet
        '
        Me.RadioButtonObsolet.AutoSize = True
        Me.RadioButtonObsolet.Location = New System.Drawing.Point(21, 19)
        Me.RadioButtonObsolet.Name = "RadioButtonObsolet"
        Me.RadioButtonObsolet.Size = New System.Drawing.Size(61, 17)
        Me.RadioButtonObsolet.TabIndex = 3
        Me.RadioButtonObsolet.TabStop = True
        Me.RadioButtonObsolet.Text = "Obsolet"
        Me.RadioButtonObsolet.UseVisualStyleBackColor = True
        '
        'Xl_LookupProductFrom
        '
        Me.Xl_LookupProductFrom.IsDirty = False
        Me.Xl_LookupProductFrom.Location = New System.Drawing.Point(135, 55)
        Me.Xl_LookupProductFrom.Name = "Xl_LookupProductFrom"
        Me.Xl_LookupProductFrom.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProductFrom.Product = Nothing
        Me.Xl_LookupProductFrom.ReadOnlyLookup = False
        Me.Xl_LookupProductFrom.Size = New System.Drawing.Size(363, 20)
        Me.Xl_LookupProductFrom.TabIndex = 62
        Me.Xl_LookupProductFrom.Value = Nothing
        '
        'Xl_LookupProductTo
        '
        Me.Xl_LookupProductTo.IsDirty = False
        Me.Xl_LookupProductTo.Location = New System.Drawing.Point(119, 42)
        Me.Xl_LookupProductTo.Name = "Xl_LookupProductTo"
        Me.Xl_LookupProductTo.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProductTo.Product = Nothing
        Me.Xl_LookupProductTo.ReadOnlyLookup = False
        Me.Xl_LookupProductTo.Size = New System.Drawing.Size(363, 20)
        Me.Xl_LookupProductTo.TabIndex = 5
        Me.Xl_LookupProductTo.Value = Nothing
        '
        'Frm_SkuSwitch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(508, 211)
        Me.Controls.Add(Me.Xl_LookupProductFrom)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.PanelButtons)
        Me.Name = "Frm_SkuSwitch"
        Me.Text = "Obsolescencia de producte"
        Me.PanelButtons.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioButtonSwitch As RadioButton
    Friend WithEvents RadioButtonObsolet As RadioButton
    Friend WithEvents Xl_LookupProductFrom As Xl_LookupProduct
    Friend WithEvents Xl_LookupProductTo As Xl_LookupProduct
End Class
