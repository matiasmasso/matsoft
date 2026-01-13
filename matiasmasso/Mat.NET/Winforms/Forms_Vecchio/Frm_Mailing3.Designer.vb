<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Mailing3
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Mailing3))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Xl_Rols_CheckList1 = New Winforms.Xl_Rols_CheckList()
        Me.CheckBoxExclouWordpress = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Xl_AmtMinVolume = New Winforms.Xl_Amount()
        Me.Xl_Product1 = New Winforms.Xl_Product()
        Me.CheckBoxProduct = New System.Windows.Forms.CheckBox()
        Me.CheckBoxIncludeInfo = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFchFrom = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMinVolume = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 376)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(595, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(376, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(487, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Xl_Rols_CheckList1
        '
        Me.Xl_Rols_CheckList1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Xl_Rols_CheckList1.CheckedValues = CType(resources.GetObject("Xl_Rols_CheckList1.CheckedValues"), System.Collections.Generic.List(Of DTORol))
        Me.Xl_Rols_CheckList1.Location = New System.Drawing.Point(13, 13)
        Me.Xl_Rols_CheckList1.Name = "Xl_Rols_CheckList1"
        Me.Xl_Rols_CheckList1.Size = New System.Drawing.Size(154, 357)
        Me.Xl_Rols_CheckList1.TabIndex = 42
        '
        'CheckBoxExclouWordpress
        '
        Me.CheckBoxExclouWordpress.AutoSize = True
        Me.CheckBoxExclouWordpress.Location = New System.Drawing.Point(184, 40)
        Me.CheckBoxExclouWordpress.Name = "CheckBoxExclouWordpress"
        Me.CheckBoxExclouWordpress.Size = New System.Drawing.Size(171, 17)
        Me.CheckBoxExclouWordpress.TabIndex = 43
        Me.CheckBoxExclouWordpress.Text = "Exclou subscripts a Wordpress"
        Me.CheckBoxExclouWordpress.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(344, 63)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePicker1.TabIndex = 44
        Me.DateTimePicker1.Visible = False
        '
        'Xl_AmtMinVolume
        '
        Me.Xl_AmtMinVolume.Amt = Nothing
        Me.Xl_AmtMinVolume.Location = New System.Drawing.Point(344, 92)
        Me.Xl_AmtMinVolume.Name = "Xl_AmtMinVolume"
        Me.Xl_AmtMinVolume.Size = New System.Drawing.Size(80, 20)
        Me.Xl_AmtMinVolume.TabIndex = 46
        Me.Xl_AmtMinVolume.Visible = False
        '
        'Xl_Product1
        '
        Me.Xl_Product1.IsDirty = False
        Me.Xl_Product1.Location = New System.Drawing.Point(184, 146)
        Me.Xl_Product1.Name = "Xl_Product1"
        Me.Xl_Product1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Product1.Product = Nothing
        Me.Xl_Product1.Size = New System.Drawing.Size(319, 20)
        Me.Xl_Product1.TabIndex = 48
        Me.Xl_Product1.Value = Nothing
        Me.Xl_Product1.Visible = False
        '
        'CheckBoxProduct
        '
        Me.CheckBoxProduct.AutoSize = True
        Me.CheckBoxProduct.Location = New System.Drawing.Point(184, 123)
        Me.CheckBoxProduct.Name = "CheckBoxProduct"
        Me.CheckBoxProduct.Size = New System.Drawing.Size(174, 17)
        Me.CheckBoxProduct.TabIndex = 49
        Me.CheckBoxProduct.Text = "exclusivament per un producte:"
        Me.CheckBoxProduct.UseVisualStyleBackColor = True
        '
        'CheckBoxIncludeInfo
        '
        Me.CheckBoxIncludeInfo.AutoSize = True
        Me.CheckBoxIncludeInfo.Checked = True
        Me.CheckBoxIncludeInfo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxIncludeInfo.Location = New System.Drawing.Point(184, 13)
        Me.CheckBoxIncludeInfo.Name = "CheckBoxIncludeInfo"
        Me.CheckBoxIncludeInfo.Size = New System.Drawing.Size(197, 17)
        Me.CheckBoxIncludeInfo.TabIndex = 50
        Me.CheckBoxIncludeInfo.Text = "inclou copia a info@matiasmasso.es"
        Me.CheckBoxIncludeInfo.UseVisualStyleBackColor = True
        '
        'CheckBoxFchFrom
        '
        Me.CheckBoxFchFrom.AutoSize = True
        Me.CheckBoxFchFrom.Location = New System.Drawing.Point(184, 64)
        Me.CheckBoxFchFrom.Name = "CheckBoxFchFrom"
        Me.CheckBoxFchFrom.Size = New System.Drawing.Size(142, 17)
        Me.CheckBoxFchFrom.TabIndex = 51
        Me.CheckBoxFchFrom.Text = "Vendes des de una data"
        Me.CheckBoxFchFrom.UseVisualStyleBackColor = True
        '
        'CheckBoxMinVolume
        '
        Me.CheckBoxMinVolume.AutoSize = True
        Me.CheckBoxMinVolume.Location = New System.Drawing.Point(184, 92)
        Me.CheckBoxMinVolume.Name = "CheckBoxMinVolume"
        Me.CheckBoxMinVolume.Size = New System.Drawing.Size(156, 17)
        Me.CheckBoxMinVolume.TabIndex = 52
        Me.CheckBoxMinVolume.Text = "Vendes a partir de un minim"
        Me.CheckBoxMinVolume.UseVisualStyleBackColor = True
        '
        'Frm_Mailing3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(595, 407)
        Me.Controls.Add(Me.CheckBoxMinVolume)
        Me.Controls.Add(Me.CheckBoxFchFrom)
        Me.Controls.Add(Me.CheckBoxIncludeInfo)
        Me.Controls.Add(Me.CheckBoxProduct)
        Me.Controls.Add(Me.Xl_Product1)
        Me.Controls.Add(Me.Xl_AmtMinVolume)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.CheckBoxExclouWordpress)
        Me.Controls.Add(Me.Xl_Rols_CheckList1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Mailing3"
        Me.Text = "Destinataris mailing"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Xl_Rols_CheckList1 As Winforms.Xl_Rols_CheckList
    Friend WithEvents CheckBoxExclouWordpress As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Xl_AmtMinVolume As Winforms.Xl_Amount
    Friend WithEvents Xl_Product1 As Winforms.Xl_Product
    Friend WithEvents CheckBoxProduct As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxIncludeInfo As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxFchFrom As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxMinVolume As System.Windows.Forms.CheckBox
End Class
